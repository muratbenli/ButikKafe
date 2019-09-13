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
    public partial class UrunlerForm : Form
    {
        KafeVeri db;
        BindingList<Urun> blUrunler;

        public UrunlerForm(KafeVeri kafeVeri)
        {
            db = kafeVeri;
            blUrunler = new BindingList<Urun>(db.Urunler);
            InitializeComponent();
            dgvUrunler.DataSource = blUrunler;
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAd.Text.Trim();

            if (urunAd == "")
            {
                MessageBox.Show("Lütfen bir ürün adı giriniz.");
                return;
            }

            Urun urun = new Urun
            {
                UrunAd = urunAd,
                BirimFiyat = nudBirimFiyat.Value
            };

            blUrunler.Add(urun);
        }
    }
}
