using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement.MPC.DTOMedias
{
    /// <summary>
    /// OK媒體檔
    /// </summary>
    public class StoreOK:BaseStore
    {
        /// <summary>
        /// 首筆
        /// </summary>
        public class Head : BaseStore.Head { }
        
        /// <summary>
        /// 明細
        /// </summary>
        public class Detail : BaseStore.Detail { }
        
        /// <summary>
        /// 尾筆
        /// </summary>
        public class Tail : BaseStore.Tail { }
    }


}
