using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Implement.MPC.MediaAndReport;
//using System.Reflection;
//using CustomCodeAttributes;

namespace Implement.MPC
{
    public interface IStoreBase<T, I>
    {
        T RawMedia { get; set; }
        I ReportInfo { get; set; }

    }

    public class StoreBase<T, I> : IStoreBase<T, I>
    {
        public T RawMedia { get; set; }
        public I ReportInfo { get; set; }

        public virtual void ConverMedia(T rawMedia)
        {
            var properties = rawMedia.GetType().GetProperties().ToList();
            foreach (var property in properties)
            {
                System.Attribute attr = property.GetCustomAttributes(rawMedia.GetType(), true).FirstOrDefault();
                switch (attr.)
                {
                    case attr is StringLength:

                }
            }
        }
    }


}
