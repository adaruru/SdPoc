using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement.MPC.DataAttributes
{
    /// <summary>
    /// 媒體檔文字欄位定義
    /// </summary>
    public class MediaString:Attribute
    {
        public MediaString(int order,int length)
        {
            Length = length;
            Order = order;
        }

        /// <summary>
        /// 欄位排序(由小至大)
        /// </summary>
        protected int Order { get; set; }

        /// <summary>
        /// 文字長度(By Bytes)
        /// </summary>
        protected int Length { get; set; }

    }
}
