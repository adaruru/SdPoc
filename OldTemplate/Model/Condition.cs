using static OldTemplate.Lib.Common.Enum;

namespace OldTemplate.Model
{
    /// <summary>
    /// 路由情境判斷(當 IsDefault)
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// 自定義代碼、備註
        /// </summary>
        public string CustomCode { get; set; }

        /// <summary>
        /// 業務種類
        /// </summary>
        public string BusinessType { get; set; }
    }
}
