using System;
using System.Collections.Generic;
using System.Text;

namespace VSArrange.Condition
{
    /// <summary>
    /// 正規表現集合クラス
    /// </summary>
    public class RegexCollection
    {
        /// <summary>
        /// どれか一つでも一致する正規表現がある
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsMatchMoreOneRegex(string target)
        {
            return true;
        }
    }
}
