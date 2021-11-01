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
            var properties = rawMedia.GetType().GetProperties().ToList();
            
            foreach (var property in properties)
            {
                //Todo data process
                System.Attribute attr = property.GetCustomAttributes(rawMedia.GetType(), true).FirstOrDefault();
                switch (attr.)
                {
                    case attr is StringLength:

                }

            }
            return (T)Convert.ChangeType(new Store711(), typeof(T));
        }
    }


}
