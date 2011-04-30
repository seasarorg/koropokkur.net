#region Copyright
/*
 * Copyright 2005-2011 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using System.IO;
using AddInCommon.Wrapper;
using EnvDTE;
using EnvDTE80;
using VSArrange.Config;
using VSArrange.Const;
using VSArrange.Message;
using VSArrange.Report;
using VSArrange.Util;
using VSArrangeConsole.Message;
using VSArrangeConsole.Report.Impl;

namespace VSArrangeConsole
{
    class Program
    {
        /// <summary>
        /// デフォルト設定ファイルパス
        /// </summary>
        private const string DEFAULT_CONFIG_PATH = @"./VSArrange.config";

        /// <summary>
        /// プログラムの起点
        /// </summary>
        /// <param name="args">[0](必須)=処理対象ファイルパス, [1](任意)=設定ファイルパス, [2](任意)処理対象プロジェクト名</param>
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;
            Log4NetUtils.InfoIfEnable(VSArrangeConsoleMessage.GetStartMessage(args));
            if (args.Length == 0)
            {
                Log4NetUtils.WarnIfEnable(VSArrangeConsoleMessage.GetNoArgumentMessage());
                return;
            }

            var targetPath = args[0];
            var configPath = DEFAULT_CONFIG_PATH;
            if (args.Length > 1)
            {
                configPath = args[1];
            }

            string[] targetProjectNames = null;
            if (args.Length > 2)
            {
                targetProjectNames = args[2].Split(',');
            }

            var solution = new SolutionEx();
            try
            {
                InvalidPath(targetPath);
                var version = GetVersion(targetPath);

                var solutionOrg = LoadSolution(targetPath, version);
                solution.SetSolution(solutionOrg);

                Log4NetUtils.DebugIfEnable(string.Format("configPath:[{0}]", configPath));
                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException("設定ファイルがありません。", configPath);
                }

                var configInfo = ConfigFileManager.ReadConfig(configPath);
                var reporter = CreateReporter();
                var arranger = ArrangeUtils.CreateArranger(configInfo, reporter);
                var projectEx = new ProjectEx();

                Log4NetUtils.InfoIfEnable(string.Format("対象プロジェクト数：{0}", solution.Projects.Count));
                foreach (Project project in solution.Projects)
                {
                    projectEx.SetProject(project);
                    if (IsTargetProject(projectEx, targetProjectNames))
                    {
                        var projectName = projectEx.Name;
                        Log4NetUtils.InfoIfEnable(string.Format("処理開始[{0}]", projectName));
                        arranger.ArrangeProject(projectEx);
                        projectEx.Save();
                        Log4NetUtils.InfoIfEnable(string.Format("処理完了[{0}]", projectName));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log4NetUtils.ErrorIfEnable(ex.ToString());
            }
            finally
            {
                CloseSolution(solution);
            }

            var endTime = DateTime.Now;
            Log4NetUtils.InfoIfEnable(VSArrangeConsoleMessage.GetEndMessage(startTime, endTime));
        }

        /// <summary>
        /// プロジェクト整理処理オブジェクトの生成
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        private static ProjectArranger CreateArranger(ConfigInfo configInfo, IOutputReport reporter)
        {
            return ArrangeUtils.CreateArranger(configInfo, reporter, false);
        }

        /// <summary>
        /// 処理状況出力オブジェクトの生成
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        private static IOutputReport CreateReporter()
        {
            return new BatchReport();
        }

        /// <summary>
        /// VisualStudioバージョン番号を取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetVersion(string path)
        {
            const string DEFAULT_VERSION = "10.0";
            const string VERSION_LINE = "Microsoft Visual Studio Solution File, Format Version ";

            if (path.EndsWith(VSArrangeConst.SUPPORTED_EXT_SLN))
            {
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line.StartsWith(VERSION_LINE))
                        {
                            var formatVersion = float.Parse(line.Replace(VERSION_LINE, "").Trim());
                            // ファイルフォーマットのバージョン番号はVisualStudio+1なので
                            // ファイルフォーマットから１引いた値をバージョン番号として扱う
                            var version = (((int)formatVersion) - 1).ToString() + ".0";
                            Log4NetUtils.DebugIfEnable(string.Format("VisualStudio version:[{0}]", version));
                            return version;
                        }
                    }
                }
            }
            return DEFAULT_VERSION;
        }

        /// <summary>
        /// ソリューションを閉じる
        /// </summary>
        /// <param name="solution"></param>
        private static void CloseSolution(Solution solution)
        {
            try
            {
                if (solution != null && solution.IsOpen)
                {
                    solution.Close();
                }
            }
            catch (Exception ex)
            {
                Log4NetUtils.ErrorIfEnable(ex.ToString());
            }
        }

        /// <summary>
        /// 処理対象のプロジェクトか判定する
        /// </summary>
        /// <param name="project"></param>
        /// <param name="targetNames"></param>
        /// <returns></returns>
        private static bool IsTargetProject(Project project, string[] targetNames)
        {
            if (targetNames == null)
            {
                // プロジェクト名指定なしの場合は必ず処理対象とする
                return true;
            }

            foreach (var targetName in targetNames)
            {
                if (targetName == project.Name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 処理対象のパスを検証する
        /// </summary>
        /// <param name="path"></param>
        private static void InvalidPath(string path)
        {
            if (path == null) { throw new ArgumentNullException("path(args[0])"); }
            if(!File.Exists(path))
            {
                throw new FileNotFoundException(VSArrangeConsoleMessage.GetNotExistsProject(path));
            }

            foreach(var supported in VSArrangeConst.SUPPORTED_EXTENSIONS)
            {
                if (path.EndsWith(supported))
                {
                    // 処理可能なファイル
                    return;
                }
            }
            throw new ArgumentException(VSArrangeMessage.GetNotSupported(path));
        }

        /// <summary>
        /// ソリューションオブジェクトを取得する
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static Solution LoadSolution(string targetPath, string version)
        {
            const string ENV_DTE_OBJ_NAME = "VisualStudio.DTE.";

            var vs = (DTE2)Activator.CreateInstance(Type.GetTypeFromProgID(ENV_DTE_OBJ_NAME + version));
            if (targetPath.EndsWith(VSArrangeConst.SUPPORTED_EXT_SLN))
            {
                vs.Solution.Open(targetPath);
            }
            else
            {
                vs.Solution.Create(Path.GetDirectoryName(targetPath), Path.GetFileNameWithoutExtension(targetPath));
                vs.Solution.AddFromFile(targetPath);
            }
            return vs.Solution;
        }
    }
}
