using Microsoft.AspNetCore.Http.Headers;


namespace OldTemplate.Model
{
    public class CustomHeader
    {
        public CustomHeader() { }

        /// <summary>
        /// 自定義代碼、備註
        /// </summary>
        public string Service { get; set; }

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
