using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButikKafe.Data
{
    public class SiparisDetay
    {
        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public int Adet { get; set; }

        public string TutarTL => string.Format("{0:0.00}TL", Tutar());
        //{
        //    get
        //    {
        //        return string.Format("{0:0.00}TL",Tutar());
        //    }
        //}

        public decimal Tutar() => Adet * BirimFiyat;
        //{
        //    return Adet * BirimFiyat;
        //}

    }
}
