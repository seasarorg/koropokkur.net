#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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

using System.Collections.Generic;
using System.IO;
using AddInCommon.Util;
using CodeGeneratorCore;
using CodeGeneratorCore.Impl;
using System;

namespace CopyGen.Gen
{
    /// <summary>
    /// コピー処理組み立てクラス
    /// </summary>
    public class CopyBuilder
    {
        /// <summary>
        /// プロパティ情報収集プログラム名
        /// </summary>
        private const string FILE_NAME_PROPERTY_INFO_COLLECTOR = "TypeInfoCollector.exe";
        /// <summary>
        /// プロパティ情報ファイル名
        /// </summary>
        private const string FILE_NAME_PROPERTY_INFO = "props.txt";

        protected readonly CopyInfo _copyInfo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="copyInfo"></param>
        public CopyBuilder(CopyInfo copyInfo)
        {
            if (copyInfo == null)
            {
                throw new ArgumentNullException("copyInfo");
            }
            _copyInfo = copyInfo;
        }

        /// <summary>
        /// コード生成オブジェクトの生成
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public ICodeGenerator CreateCodeGenerator(string assemblyPath, string typeName)
        {
            //  型を自動設定する場合
            if(_copyInfo.IsSourceTypeAuto || _copyInfo.IsTargetTypeAuto)
            {
                //  現在開いている型のプロパティ情報を出力する
                IList<string> propList = ExtractPropertyInfo(assemblyPath, typeName);
                if(propList == null)
                {
                    return null;
                }

                if(_copyInfo.IsSourceTypeAuto)
                {
                    _copyInfo.SourcePropertyNames = propList;
                    _copyInfo.SourceTypeName = typeName;
                }

                if(_copyInfo.IsTargetTypeAuto)
                {
                    _copyInfo.TargetPropertyNames = propList;
                    _copyInfo.TargetTypeName = typeName;
                }
            }

            if (_copyInfo.IsOutputMethod)
            {
                return CreateCopyMethodGenerator();
            }
            return CreateCopyLinesGenerator();
        }

        /// <summary>
        /// コピーメソッド生成オブジェクトの生成
        /// </summary>
        /// <returns></returns>
        protected ICodeGenerator CreateCopyMethodGenerator()
        {
            MethodGenerator methodGenerator = new MethodGenerator();
            //  TODO:メッセージ管理方法を考える
            methodGenerator.MethodComment = "コピー処理の実行(Created by auto generator.)";
            methodGenerator.Visibility = _copyInfo.Visibility;
            methodGenerator.MethodOption = _copyInfo.MethodOption;
            methodGenerator.MethodName = _copyInfo.MethodName;

            //  コピー元の設定
            if (_copyInfo.HasSourceArgument)
            {
                ArgumentGenerator sourceArgument = new ArgumentGenerator();
                sourceArgument.ArgumentTypeName = _copyInfo.SourceTypeName;
                sourceArgument.ArgumentName = _copyInfo.SourceArgumentName;
                sourceArgument.Comment = "コピー元";
                sourceArgument.IsNotNull = true;
                methodGenerator.Arguments.Add(sourceArgument);
            }

            LineReturnGenerator returnGenerator = null;
            //  コピー先の設定
            if (_copyInfo.IsReturn)
            {
                methodGenerator.ReturnTypeName = _copyInfo.TargetTypeName;
                //  TODO:メッセージ管理方法を考える
                methodGenerator.ReturnComment = "コピー先";

                //  戻り値のインスタンスを生成する
                LineGenerator returnInstanceGenerator = new LineGenerator();
                returnInstanceGenerator.Items.Add(_copyInfo.TargetTypeName);
                returnInstanceGenerator.Items.Add("target");
                returnInstanceGenerator.Items.Add("=");
                returnInstanceGenerator.Items.Add("new");
                returnInstanceGenerator.Items.Add(_copyInfo.TargetTypeName + "()");
                methodGenerator.Lines.Add(returnInstanceGenerator);

                //  return文の生成設定
                returnGenerator = new LineReturnGenerator();
                returnGenerator.Items.Add("target");
            }
            else
            {
                methodGenerator.ReturnTypeName = "void";

                ArgumentGenerator targetArgument = new ArgumentGenerator();
                targetArgument.ArgumentTypeName = _copyInfo.TargetTypeName;
                targetArgument.ArgumentName = _copyInfo.TargetArgumentName;
                methodGenerator.Arguments.Add(targetArgument);
            }

            //  コピー処理の設定
            methodGenerator.Lines.Add(CreateCopyLinesGenerator());

            //  戻り値
            if (returnGenerator != null)
            {
                methodGenerator.Lines.Add(returnGenerator);
            }
            else
            {
                methodGenerator.Lines.Add(new LineReturnGenerator());
            }
            return methodGenerator;
        }

        /// <summary>
        /// コピー処理生成オブジェクトの生成
        /// </summary>
        /// <returns></returns>
        protected ICodeGenerator CreateCopyLinesGenerator()
        {
            if(_copyInfo.SourcePropertyNames == null ||
                _copyInfo.TargetPropertyNames == null)
            {
                return null;
            }

            GeneratorColleciton generatorColleciton = new GeneratorColleciton();

            foreach (string propertyName in _copyInfo.SourcePropertyNames)
            {
                if (_copyInfo.TargetPropertyNames.Contains(propertyName))
                {
                    LineGenerator lineGenerator = new LineGenerator();
                    lineGenerator.Items.Add(string.Format("target.{0}", propertyName));
                    lineGenerator.Items.Add("=");

                    if (_copyInfo.HasSourceArgument)
                    {
                        lineGenerator.Items.Add(string.Format("source.{0}", propertyName));
                    }
                    else
                    {
                        lineGenerator.Items.Add(string.Format("this.{0}", propertyName));
                    }
                    generatorColleciton.Add(lineGenerator);
                }
            }
            return generatorColleciton;
        }

        /// <summary>
        /// プロパティ情報を収集＆ファイル出力
        /// </summary>
        /// <param name="targetAssemblyPath"></param>
        /// <param name="typeName"></param>
        protected IList<string> ExtractPropertyInfo(string targetAssemblyPath, string typeName)
        {
            string path = PathUtils.GetFolderPath(AssemblyUtils.GetExecutingAssemblyPath());
            string propInfoPath = string.Format("{0}{1}", path, FILE_NAME_PROPERTY_INFO);
            ProcessUtils.StartProcessWithoutWindow(
                string.Format("{0}{1}", path, FILE_NAME_PROPERTY_INFO_COLLECTOR),
                string.Format("{0} {1} {2}", targetAssemblyPath, typeName, propInfoPath));

            List<string> propList = new List<string>();
            //  プロパティ情報の生成に失敗している場合は処理を終了
            if(!File.Exists(propInfoPath))
            {
                return null;
            }
            
            using (StreamReader reader = new StreamReader(propInfoPath))
            {
                while (!reader.EndOfStream)
                {
                    propList.Add(reader.ReadLine());
                }
            }
            return propList;
        }
    }
}
