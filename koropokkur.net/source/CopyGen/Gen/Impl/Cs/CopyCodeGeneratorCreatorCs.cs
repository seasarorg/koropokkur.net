﻿#region Copyright
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

using CodeGeneratorCore;
using CodeGeneratorCore.Impl;
using CodeGeneratorCore.Impl.Cs;

namespace CopyGen.Gen.Impl.Cs
{
    /// <summary>
    /// C#用のコピー処理コードを出力するクラス
    /// </summary>
    public class CopyCodeGeneratorCreatorCs : ICopyCodeGeneratorCreator
    {
        /// <summary>
        /// コピーメソッド生成オブジェクトの生成
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyCodeInfo"></param>
        /// <returns></returns>
        public virtual ICodeGenerator CreateCopyMethodGenerator(CopyInfo copyInfo, PropertyCodeInfo propertyCodeInfo)
        {
            MethodGeneratorCs methodGenerator = new MethodGeneratorCs();
            //  TODO:メッセージ管理方法を考える
            methodGenerator.MethodComment = "コピー処理の実行(Created by auto generator.)";
            methodGenerator.Visibility = copyInfo.Visibility;
            methodGenerator.MethodOption = copyInfo.MethodOption;
            methodGenerator.MethodName = copyInfo.MethodName;

            //  コピー元の設定
            if (copyInfo.CopySource == EnumCopySource.AsArgument)
            {
                ArgumentGeneratorCs sourceArgument = new ArgumentGeneratorCs();
                sourceArgument.ArgumentTypeName = propertyCodeInfo.SourceTypeName;
                sourceArgument.ArgumentName = copyInfo.SourceArgumentName;
                sourceArgument.Comment = "コピー元";
                sourceArgument.IsNotNull = copyInfo.IsNotNullSource;
                methodGenerator.Arguments.Add(sourceArgument);
            }

            LineReturnGeneratorCs returnGenerator = null;
            //  コピー先の設定
            if (copyInfo.CopyTarget == EnumCopyTarget.Return)
            {
                methodGenerator.ReturnTypeName = propertyCodeInfo.TargetTypeName;
                //  TODO:メッセージ管理方法を考える
                methodGenerator.ReturnComment = "コピー先";

                //  戻り値のインスタンスを生成する
                LineGeneratorCs returnInstanceGenerator = new LineGeneratorCs();
                returnInstanceGenerator.Items.Add(propertyCodeInfo.TargetTypeName);
                returnInstanceGenerator.Items.Add(copyInfo.TargetArgumentName);
                returnInstanceGenerator.Items.Add("=");
                returnInstanceGenerator.Items.Add("new");
                returnInstanceGenerator.Items.Add(propertyCodeInfo.TargetTypeName + "()");
                methodGenerator.Lines.Add(returnInstanceGenerator);

                //  return文の生成設定
                returnGenerator = new LineReturnGeneratorCs();
                returnGenerator.Items.Add(copyInfo.TargetArgumentName);
            }
            else
            {
                methodGenerator.ReturnTypeName = "void";

                if (copyInfo.CopyTarget == EnumCopyTarget.AsArgument)
                {
                    ArgumentGeneratorCs targetArgument = new ArgumentGeneratorCs();
                    targetArgument.ArgumentTypeName = propertyCodeInfo.TargetTypeName;
                    targetArgument.ArgumentName = copyInfo.TargetArgumentName;
                    targetArgument.Comment = "コピー先";
                    targetArgument.IsNotNull = copyInfo.IsNotNullTarget;
                    methodGenerator.Arguments.Add(targetArgument);
                }
            }

            //  コピー処理の設定
            ICodeGenerator copyLinesGenerator = CreateCopyLinesGenerator(copyInfo, propertyCodeInfo);
            if (copyLinesGenerator != null)
            {
                methodGenerator.Lines.Add(copyLinesGenerator);
            }

            //  戻り値
            if (returnGenerator != null)
            {
                methodGenerator.Lines.Add(returnGenerator);
            }
            else
            {
                methodGenerator.Lines.Add(new LineReturnGeneratorCs());
            }
            return methodGenerator;
        }

        /// <summary>
        /// コピー処理生成オブジェクトの生成
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyCodeInfo"></param>
        /// <returns></returns>
        public virtual ICodeGenerator CreateCopyLinesGenerator(CopyInfo copyInfo, PropertyCodeInfo propertyCodeInfo)
        {
            if (propertyCodeInfo.SourcePropertyNames == null ||
                propertyCodeInfo.TargetPropertyNames == null)
            {
                return null;
            }

            GeneratorColleciton generatorColleciton = new GeneratorColleciton();

            foreach (string propertyName in propertyCodeInfo.SourcePropertyNames)
            {
                if (propertyCodeInfo.TargetPropertyNames.Contains(propertyName))
                {
                    LineGeneratorCs lineGenerator = new LineGeneratorCs();
                    lineGenerator.Items.Add(GetCopyTargetString(copyInfo, propertyName));
                    lineGenerator.Items.Add("=");
                    lineGenerator.Items.Add(GetCopySourceString(copyInfo, propertyName));

                    generatorColleciton.Add(lineGenerator);
                }
            }
            return generatorColleciton;
        }

        /// <summary>
        /// コピー元文字列の取得
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual string GetCopySourceString(CopyInfo copyInfo, string propertyName)
        {
            if (copyInfo.CopySource == EnumCopySource.AsArgument &&
                !string.IsNullOrEmpty(copyInfo.SourceArgumentName))
            {
                return string.Format("{0}.{1}", copyInfo.SourceArgumentName, propertyName);
            }

            if (copyInfo.CopySource == EnumCopySource.This)
            {
                return string.Format("this.{0}", propertyName);
            }

            return propertyName;
        }

        /// <summary>
        /// コピー先文字列の取得
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual string GetCopyTargetString(CopyInfo copyInfo, string propertyName)
        {
            if ((copyInfo.CopyTarget == EnumCopyTarget.AsArgument || copyInfo.CopyTarget == EnumCopyTarget.Return)
                && !string.IsNullOrEmpty(copyInfo.TargetArgumentName))
            {
                return string.Format("{0}.{1}", copyInfo.TargetArgumentName, propertyName);
            }

            if (copyInfo.CopyTarget == EnumCopyTarget.This)
            {
                return string.Format("this.{0}", propertyName);
            }

            return propertyName;
        }
    }
}