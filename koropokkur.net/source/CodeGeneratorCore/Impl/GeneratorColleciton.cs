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
using System.Text;

namespace CodeGeneratorCore.Impl
{
    /// <summary>
    /// コード生成オブジェクトの集合
    /// </summary>
    public class GeneratorColleciton : ICodeGenerator, IEnumerator<ICodeGenerator>, IEnumerable<ICodeGenerator>
    {
        private readonly IList<ICodeGenerator> _codeGenerators = new List<ICodeGenerator>();

        /// <summary>
        /// コード生成オブジェクトの追加
        /// </summary>
        /// <param name="generator"></param>
        public void Add(ICodeGenerator generator)
        {
            _codeGenerators.Add(generator);
        }

        /// <summary>
        /// コード生成オブジェクトリストのクリア
        /// </summary>
        public void Clear()
        {
            _codeGenerators.Clear();
        }

        #region ICodeGenerator メンバ

        /// <summary>
        /// コード生成
        /// </summary>
        /// <returns></returns>
        public string GenerateCode(string startIndent)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ICodeGenerator generator in _codeGenerators)
            {
                builder.AppendLine(generator.GenerateCode(startIndent));
            }
            return builder.ToString();
        }

        #endregion

        #region IEnumerator<ICodeGenerator> メンバ

        protected int _currentIndex = -1;

        public ICodeGenerator Current
        {
            get { return _codeGenerators[_currentIndex]; }
        }

        #endregion

        #region IDisposable メンバ

        public void Dispose()
        {
            Clear();
        }

        #endregion

        #region IEnumerator メンバ

        object System.Collections.IEnumerator.Current
        {
            get { return _codeGenerators[_currentIndex]; }
        }

        public bool MoveNext()
        {
            _currentIndex++;
            if(_currentIndex < _codeGenerators.Count)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        #endregion

        #region IEnumerable<ICodeGenerator> メンバ

        public IEnumerator<ICodeGenerator> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable メンバ

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion
    }
}
