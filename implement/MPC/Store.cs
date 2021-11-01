using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static implement.MPC.MediaAndReport;

namespace implement.MPC
{
    public class Store711<T, I> : StoreBase<T, I>
    {
        public T RawMedia { get; set; }
        public I ReportInfo { get; set; }

        public Report711 ConverReport(Media711 Media711)
        {
            //Todo
            return new Report711();
        }

    }

    public class StoreFemaily<T, I> : StoreBase<T, I>
    {
        public T RawMedia { get; set; }
        public I ReportInfo { get; set; }
    }
}
