using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButikKafe.Data
{
    public class Siparis
    {

        public Siparis()
        {
            SiparisDetaylar = new List<SiparisDetay>();
            AcilisZamani = DateTime.Now;
        }

        public Siparis(int masaNo) :this()
        {
            MasaNo = masaNo;
        }

        public int MasaNo { get; set; }

        public DateTime? AcilisZamani { get; set; }  // soru işareti nullable yapıyor
        public DateTime? KapanisZamani { get; set; }  // soru işareti nullable yapıyor

        public SiparisDurum Durum { get; set; }

        public decimal OdenenTutar { get; set; }

        public string ToplamTutarTL
        {
            get
            {
                return string.Format("{0:0.00}TL",ToplamTutar());
            }
        }

        public decimal ToplamTutar()
        {
            return SiparisDetaylar.Sum(x => x.Tutar());
        }

        public List<SiparisDetay> SiparisDetaylar { get; set; }


    }
}
