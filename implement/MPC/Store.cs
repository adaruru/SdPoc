using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static implement.MPC.Media;

namespace implement.MPC
{
    public class Store711<TMedia, TReport> : StoreBase<TMedia, TReport>
    {
        public Report711 ConverReport(Media711 Media711)
        {
            base.ConverReport(Media711);
            return new Report711();
        }

    }

    public class StoreFemaily<TMedia, TReport> : StoreBase<TMedia, TReport>
    {

    }
}
