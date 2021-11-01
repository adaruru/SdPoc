using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataAttributes
{
    /// <summary>
    /// 媒體檔文字欄位定義
    /// </summary>
    public class MediaInteger:Attribute
    {
        public MediaInteger(int order,int totalLength)
        {
            Order = order;
            TotalLength = totalLength;
        }

        /// <summary>
        /// 欄位排序(由小至大)
        /// </summary>
        protected int Order { get; set; }

        /// <summary>
        /// 總長度
        /// </summary>
        protected int TotalLength { get; set; }

    }
}
