﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement.MPC.DataAttributes
{
    /// <summary>
    /// 媒體檔文字欄位定義
    /// </summary>
    public class MediaDecimal:Attribute
    {
        public MediaDecimal(int order,int totalLength,int floatLength)
        {
            Order = order;
            TotalLength = totalLength;
            FloatLength = floatLength;
        }

        /// <summary>
        /// 欄位排序(由小至大)
        /// </summary>
        protected int Order { get; set; }

        /// <summary>
        /// 總長度
        /// </summary>
        protected int TotalLength { get; set; }

        /// <summary>
        /// 小數位數長度
        /// </summary>
        protected int FloatLength { get; set; }

    }
}
