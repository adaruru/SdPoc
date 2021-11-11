using Implement.MPC.DataAttributes;
using Implement.MPC.DTOMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Reflection;
//using CustomCodeAttributes;

namespace Implement.MPC
{
    public interface IStoreBase<T>
    {
        T StoreInfo { get; set; }

    }

    public class StoreBase<T> : IStoreBase<T>
    {
        public T StoreInfo { get; set; }

        /// <summary>
        /// 原始資料轉資料結構
        /// </summary>
        /// <param name="rawMedia"></param>
        public T ConverMedia(byte[] rawMedia)
        {
            var storeInfo = new Store711();
            var properties = storeInfo.GetType().GetProperties().ToList();

            foreach (var property in properties)
            {
                //Todo data process
                var attr = property.GetCustomAttributes(typeof(Store711), true).FirstOrDefault();
                switch (attr.GetType().Name)
                {
                    case "MediaDecimal":
                        //TODO:

                        break;
                    case "MediaInteger":
                        //TODO:
                        break;
                    case "MediaString":
                        //TODO:
                        break;
                    default:
                        //TODO:
                        break;

                }

            }
            return (T)Convert.ChangeType(new Store711(), typeof(T));
        }
    }


}
