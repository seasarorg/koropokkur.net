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

using System.Text;

namespace CodeGeneratorCore.Impl.Cs
{
    /// <summary>
    /// 戻り値行生成クラス
    /// </summary>
    public class LineReturnGeneratorCs : LineGeneratorCs
    {
        /// <summary>
        /// 戻り値行を生成
        /// </summary>
        /// <returns></returns>
        public override string GenerateCode(string startIndent)
        {
            StringBuilder builder = new StringBuilder();
            if(startIndent != null)
            {
                builder.Append(startIndent);
            }
            builder.Append("return");
            if(Items.Count > 0)
            {
                builder.Append(" ").Append(base.GenerateCode(string.Empty));
                return builder.ToString();
            }
            builder.Append(";");
            return builder.ToString();
        }
    }
}
