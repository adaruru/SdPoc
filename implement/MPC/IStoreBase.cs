using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static implement.MPC.Media;

namespace implement.MPC
{
    public interface IStoreBase<T, I>
    {
        T RawMedia { get; set; }
        I ReportInfo { get; set; }

    }

    public class StoreBase<TMedia,TReport> : IStoreBase<TMedia, TReport>
    {
        public TMedia TMedia { get; set; }
        public TReport TReport { get; set; }

        public TReport ConverReport(TMedia rawMedia)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(rawMedia.GetType());
            typeof(T).GetProperty("Name")

            foreach (var attr in attrs)
            {
                switch attr.Name
                    case 
                //tofo 資料處理
            }

            return (TReport)Convert.ChangeType(new Report711(), typeof(T));
        }
    }


}
