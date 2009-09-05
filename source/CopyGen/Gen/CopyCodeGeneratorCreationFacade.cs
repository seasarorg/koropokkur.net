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

using System;
using CodeGeneratorCore;

namespace CopyGen.Gen
{
    /// <summary>
    /// コピー処理出力クラス生成処理入り口
    /// </summary>
    public class CopyCodeGeneratorCreationFacade
    {
        protected readonly ICopyCodeGeneratorCreator _codePartsGenerator;

        protected readonly CopyInfo _copyInfo;

        protected readonly PropertyCodeInfo _propertyCodeInfo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="codePartsGenerator"></param>
        /// <param name="copyInfo"></param>
        /// <param name="propertyCodeInfo"></param>
        public CopyCodeGeneratorCreationFacade(ICopyCodeGeneratorCreator codePartsGenerator, CopyInfo copyInfo, PropertyCodeInfo propertyCodeInfo)
        {
            if (codePartsGenerator == null) throw new ArgumentNullException("codePartsGenerator");
            if (copyInfo == null) throw new ArgumentNullException("copyInfo");
            if (propertyCodeInfo == null) throw new ArgumentNullException("propertyCodeInfo");

            _codePartsGenerator = codePartsGenerator;
            _copyInfo = copyInfo;
            _propertyCodeInfo = propertyCodeInfo;
        }

        /// <summary>
        /// コード生成オブジェクトの生成
        /// </summary>
        /// <returns></returns>
        public virtual ICodeGenerator CreateCodeGenerator()
        {
            if(_propertyCodeInfo.CanCodeGenerate == false)
            {
                return null;
            }

            if (_copyInfo.IsOutputMethod)
            {
                return _codePartsGenerator.CreateCopyMethodGenerator(_copyInfo, _propertyCodeInfo);
            }
            return _codePartsGenerator.CreateCopyLinesGenerator(_copyInfo, _propertyCodeInfo);
        }
    }
}
