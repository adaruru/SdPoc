using Implement.MPC.DTOMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement.MPC
{
    public class EntryClass
    {
        public void Entry()
        {
            var store = new Store711() { };
        }
    }

    public class Store711<T> : StoreBase<T>
    {
        public T StoreInfo { get; set; }

        public Store711 ConverReport(byte[] rawMedia)
        {
            var result = ConverMedia(rawMedia);
            return (Store711)Convert.ChangeType(result, typeof(Store711));
        }

    }

    public class StoreFemaily<T> : StoreBase<T>
    {
        public T StoreInfo { get; set; }
    }
}
