using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DataAttributes;

namespace Models.DTOMedias
{
    /// <summary>
    /// 超商日終媒體檔(通用)
    /// </summary>
    public abstract class BaseStore
    {
        /// <summary>
        /// 首筆
        /// </summary>
        public Head HeadInfo { get; set; }
        
        /// <summary>
        /// 明細
        /// </summary>
        public ICollection<Detail> DetailInfos { get; set; }

        /// <summary>
        /// 尾筆
        /// </summary>
        public Tail TailInfo { get; set; }

        /// <summary>
        /// 媒體檔原始資料
        /// </summary>
        public string Source { get; set; }

        public abstract class Head
        {
            /// <summary>
            /// 錄別
            /// </summary>
            [MediaString(0,1)]
            public string SegmentCode { get; set; }

            /// <summary>
            /// 代收代號
            /// </summary>
            [MediaString(10, 8)]
            public string AgencyTypeId { get; set; }

            /// <summary>
            /// 代收機構代號
            /// </summary>
            [MediaString(20, 8)]
            public string CompanyId { get; set; }

            /// <summary>
            /// 轉帳類別
            /// </summary>
            [MediaString(30, 3)]
            public string TransferType1 { get; set; }

            /// <summary>
            /// 轉帳性質別
            /// </summary>
            [MediaString(40, 1)]
            public string TransferType2 { get; set; }

            /// <summary>
            /// 入扣帳日期
            /// </summary>
            [MediaString(50, 8)]
            public string TransDate { get; set; }

            /// <summary>
            /// 保留欄位
            /// </summary>
            [MediaString(60, 91)]
            public string Memo { get; set; }

        }
        public class Detail
        {
            /// <summary>
            /// 錄別
            /// </summary>
            [MediaString(0, 1)]
            public string SegmentCode { get; set; }

            /// <summary>
            /// 代收代號
            /// </summary>
            [MediaString(10, 8)]
            public int AgencyTypeId { get; set; }

            /// <summary>
            /// 代收機構代號
            /// </summary>
            [MediaString(20, 8)]
            public string CompanyId { get; set; }

            /// <summary>
            /// 門市店號
            /// </summary>
            [MediaString(30, 8)]
            public string StoreId { get; set; }

            /// <summary>
            /// 轉帳代繳帳號
            /// </summary>
            [MediaString(40, 14)]
            public string AccountNo { get; set; }

            /// <summary>
            /// 轉帳類別
            /// </summary>
            [MediaString(50, 3)]
            public string TransferType1 { get; set; }

            /// <summary>
            /// 扣繳狀態
            /// </summary>
            [MediaString(60, 2)]
            public string Status { get; set; }


            /// <summary>
            /// 入扣帳日期
            /// </summary>
            [MediaString(70, 8)]
            public string TransDate { get; set; }


            /// <summary>
            /// 繳費日期
            /// </summary>
            [MediaString(80, 8)]
            public string PayDate { get; set; }
            /// <summary>
            /// BarCode1
            /// </summary>
            [MediaString(90, 9)]
            public string BarCode1 { get; set; }
            /// <summary>
            /// BarCode2
            /// </summary>
            [MediaString(100, 20)]
            public string BarCode2 { get; set; }
            /// <summary>
            /// BarCode3
            /// </summary>
            [MediaString(110, 15)]
            public string BarCode3 { get; set; }

            /// <summary>
            /// 保留欄位
            /// </summary>
            [MediaString(120, 16)]
            public string Memo { get; set; }
        }
        public class Tail
        {
            /// <summary>
            /// 錄別
            /// </summary>
            [MediaString(0, 1)]
            public string SegmentCode { get; set; }

            /// <summary>
            /// 代收代號
            /// </summary>
            [MediaString(10, 8)]
            public int AgencyTypeId { get; set; }

            /// <summary>
            /// 代收機構代號
            /// </summary>
            [MediaString(20, 8)]
            public string CompanyId { get; set; }

            /// <summary>
            /// 轉帳類別
            /// </summary>
            [MediaString(30, 3)]
            public string TransferType1 { get; set; }

            /// <summary>
            /// 入扣帳日期
            /// </summary>
            [MediaString(40, 8)]
            public string TransDate { get; set; }

            /// <summary>
            /// 代收成功總金額
            /// </summary>
            [MediaDecimal(50,14,2)]
            public decimal TotalAmount { get; set; }

            /// <summary>
            /// 代收成功總筆數
            /// </summary>
            [MediaInteger(60,10)]
            public int TotalCount { get; set; }

            /// <summary>
            /// 保留欄位
            /// </summary>
            [MediaString(70, 66)]
            public string Memo { get; set; }
        }
    }

}
