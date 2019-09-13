using ButikKafe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButikKafe
{
    public partial class SiparisForm : Form
    {
        KafeVeri db;
        Siparis siparis;
        BindingList<SiparisDetay> blSiparisDetaylar;

        public SiparisForm(KafeVeri kafeVeri, Siparis siparis)
        {
            db = kafeVeri;
            this.siparis = siparis;
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);

            blSiparisDetaylar.ListChanged += BlSiparisDetaylar_ListChanged;// += den sonra tab a bas

            InitializeComponent();
            Text = "Masa " + siparis.MasaNo;
            lblMasaNo.Text = string.Format("{0:00}", siparis.MasaNo);

            cboUrunler.DataSource = db.Urunler;
            dgvSiparisDetaylar.DataSource = blSiparisDetaylar;
            lblOdemeTutari.Text = siparis.ToplamTutarTL;

            MasaNolariYukle();
        }

        private void MasaNolariYukle()
        {
            cboMasaNolar.Items.Clear();
            for (int i = 1; i <= 20; i++)
            {
                if (!db.MasaDoluMu(i) || i == siparis.MasaNo)
                {
                    cboMasaNolar.Items.Add(i);
                }
                cboMasaNolar.SelectedItem = siparis.MasaNo;
            }
        }

        private void BlSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            lblOdemeTutari.Text = siparis.ToplamTutarTL;
        }

        private void btnSiparisDetayEkle_Click(object sender, EventArgs e)
        {
            Urun secili = (Urun)cboUrunler.SelectedItem;

            SiparisDetay sd = new SiparisDetay
            {
                UrunAd = secili.UrunAd,
                BirimFiyat = secili.BirimFiyat,
                Adet = (int)nudUrunAdet.Value
            };

            blSiparisDetaylar.Add(sd);

        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            // Bu pencereyi kapat
            Close();
        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bu siparişi iptal etmek istediğinize emin misiniz ?",
                "Sipariş İptal Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                db.MasayiKapat(siparis.MasaNo, SiparisDurum.Iptal);
                Close();
            }
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(siparis.ToplamTutarTL + " tahsil edildiyse sipariş kapatılacaktır.Onaylıyor musunuz?",
                "Ödeme Alındı Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                db.MasayiKapat(siparis.MasaNo, SiparisDurum.Odendi);
                Close();
            }
        }

        private void btnMasaTasi_Click(object sender, EventArgs e)
        {
            int hedefMasaNo = (int)cboMasaNolar.SelectedItem;
            int kaynakMasaNo = siparis.MasaNo;
            if (hedefMasaNo == siparis.MasaNo)
            {
                return;
            }

            // hedef masa boş, taşımaya başla
            siparis.MasaNo = hedefMasaNo;
            Text = "Masa " + siparis.MasaNo;
            lblMasaNo.Text = string.Format("{0:00}",siparis.MasaNo);

            ((Form1)Owner).MasaTasi(kaynakMasaNo, hedefMasaNo);
                
        }
    }
}
