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
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using log4net;
using VSArrange.Report;
using VSArrange.Config;
using VSArrange.Const;
using VSArrange.Message;
using VSArrange.Util;
using VSArrangeConsole.Message;
using VSArrangeConsole.Report.Impl;

namespace VSArrangeConsole
{
    class Program
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(VSArrangeConst.ADDIN_NAME);

        static void Main(string[] args)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.InfoFormat("開始({0})：ARGS=[{1}]", DateTime.Now, string.Join(",", args));
            }

            string targetPath = args[0];
            string projectName = null;
            if (args.Length > 1)
            {
                projectName = args[1];
            }

            Solution solution = null;
            try
            {
                InvalidPath(targetPath);
                var version = GetVersion(targetPath);

                solution = LoadSolution(targetPath, version);
                var configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
                var reporter = CreateReporter();
                var arranger = ArrangeUtils.CreateArranger(configInfo, reporter);
                foreach (Project project in solution.Projects)
                {
                    if (IsTargetProject(project, projectName))
                    {
                        if (_logger.IsInfoEnabled)
                        {
                            _logger.InfoFormat("処理実行中。。。[{0}]", project.Name);
                        }
                        arranger.ArrangeProject(project);
                        project.Save();

                        if (_logger.IsInfoEnabled)
                        {
                            _logger.InfoFormat("処理完了[{0}]", project.Name);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (_logger.IsErrorEnabled)
                {
                    _logger.Error(ex);
                }
            }
            finally
            {
                CloseSolution(solution);
            }

            if (_logger.IsInfoEnabled)
            {
                _logger.InfoFormat("終了({0})", DateTime.Now);
            }
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
                            var val = int.Parse(line.Replace(VERSION_LINE, ""));
                            return (val - 1).ToString() + ".0";
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
            if (solution != null && solution.IsOpen)
            {
                solution.Close();
            }
        }

        /// <summary>
        /// 処理対象のプロジェクトか判定する
        /// </summary>
        /// <param name="project"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        private static bool IsTargetProject(Project project, string targetName)
        {
            if (string.IsNullOrEmpty(targetName))
            {
                // プロジェクト名指定なしの場合は必ず処理対象とする
                return true;
            }

            return (project.Name == targetName ? true : false);
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
