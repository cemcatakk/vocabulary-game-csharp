using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using  System.Data.SqlClient;

namespace VocabularyGameForm
{
    public partial class formAnasayfa : Form
    {
        Db db = new Db();
        int sure = 240;
        int tahminSure = 20;
        Oyun oyun;
        Etap mevcutEtap;
        public formAnasayfa()
        {
            InitializeComponent();
        }

        private void formAnasayfa_Load(object sender, EventArgs e)
        {
            //Form ilk yüklendiğinde oyuncu adı, oyun tarihi gibi verileri, yeni bir oyun nesnesi oluşturup
            //Bu nesneye aktarıyoruz
            oyun = new Oyun();
            lbl_aktif.Text = Girisform.sendData;
            lbl_soru.Text = "OYUNU BAŞLATINIZ";
            oyun.OyuncuAdi = lbl_aktif.Text;
            oyun.Tarih = DateTime.Now;
            lbl_date.Text = DateTime.Now.ToLongDateString();
            oyun.Etaplar = db.etapListesiAl(); //Bu fonksiyon ile rastgele etap listesi alıyoruz
            /* Buton üstüne açıklama yazısı*/
            ToolTip aciklamaTip = new ToolTip();
            aciklamaTip.SetToolTip(btn_basla, "BAŞLA");
            aciklamaTip.SetToolTip(btn_cevapla, "CEVAPLA");
            aciklamaTip.SetToolTip(btn_cikis, "ÇIKIŞ");
            aciklamaTip.SetToolTip(btn_harfal, "HARF AL");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Timer 1, 4 dakikalık süreyi sayıyor, her saniye güncellenerek(1000ms)
            //label text'lerini değiştiriyor
            sure--;
            int dakika = sure / 60;
            lbl_dakika.Text = dakika.ToString();
            lbl_saniye.Text = (sure - (dakika * 60)).ToString();
            lbl_date.Text = DateTime.Now.ToString();
            if (sure==0||oyun.oyunBittiMi())//Süre veya oyun biterse, OyunBitti metotu çağırılıyor
            {
                sure = 0;
                OyunBitti();
            }
        }
        private void OyunBitti()
        {
            //Bu metot, timer'ları durdurup tüm düğmeleri deaktif ediyor. 
            //Oyuncu bilgilerini yazıp programı kapatıyor
            oyun.KalanSure = sure;
            timer1.Stop();
            timer2.Stop();
            btn_cevapla.Enabled = false;
            btn_harfal.Enabled = false;
            btn_basla.Enabled = false;
            MessageBox.Show("Oyun Bitti!");
            MessageBox.Show("Oyuncu: " + oyun.OyuncuAdi + " | Puan: " + oyun.Puan.ToString());
            MessageBox.Show("Kalan Süre: " + oyun.KalanSure + "saniye | Oynama Tarihi: " + oyun.Tarih.ToString());
            Application.Exit();
        }

        private void btn_basla_Click(object sender, EventArgs e)
        {
            //Başla düğmesine basıldığında, tekrar basılmaması için düğmeyi deaktif ediyoruz
            //Ve diğer düğmeleri aktif hale getirip, timer'ı başlatıyoruz.
            btn_basla.Enabled = false;
            btn_harfal.Enabled = true;
            btn_cevapla.Enabled = true;
            timer1.Start();
            sonrakiEtap(); //İlk çalışmada sonrakiEtap metotu çağırılıyor

        }
        private void sonrakiEtap()
        {
            //Bu metot, oyun nesnesindeki etap listesinden sonraki etabı getiriyor
            if (!oyun.oyunBittiMi())
            {
                //Oyun bitmediyse metot çağırılıyor
                mevcutEtap = oyun.sonrakiSoru();
                if (mevcutEtap==null) //Etap NULL gelirse, oyun bitmiş demektir.
                {
                    OyunBitti();
                }
                else
                {
                    //Etap başarıyla gelirse, ekran güncelleniyor ve sonraki etaba geçiliyor
                    ekranGuncelle();
                    btn_harfal.Enabled = true;
                    btn_cevapla.Enabled = true;
                }
            }
        }
        private void ekranGuncelle()
        {
            //Bu metot, label bilgilerini güncelliyor
            lbl_soru.Text = mevcutEtap.Soru;
            lbl_puan.Text = oyun.Puan.ToString();
            string tmp = "";
            //tmp değişkeninde, mevcut etapta bulunan kelimenin harfleri göz önüne alınarak
            //açılan her bir harfi (bool dizisi ile kontrol ediyoruz) tmp değişkenine ekliyoruz
            for (int i = 0; i < mevcutEtap.HarfSayisi; i++)
            {
                if (mevcutEtap.Acilan[i])
                {
                    tmp += mevcutEtap.Cevap[i];
                }
                else
                {
                    tmp += "_"; //Açılmayanlar '_' olarak geliyor
                }
            }
            tbKelime.Text = tmp;
        }
        private void btn_cevapla_Click(object sender, EventArgs e)
        {
            //Cevapla düğmesine basıldığında, timer1 durdurulup timer2 başlatılıyor
            button1.Enabled = true;
            btn_cevapla.Enabled = false;
            btn_harfal.Enabled = false;
            timer1.Stop();
            timer2.Start();
        }

        private void btn_cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_harfal_Click(object sender, EventArgs e)
        {
            //Harf al düğmesine basıldığında, etap nesnesinin harfAl metotu çağırılır
            //Bu metot, içindeki bool dizisinde(adı Acilan) false olan rastgele bir index'i true yapar
            //Bu da o harfin görüntülenmesi anlamına gelir
            mevcutEtap.harfAl();
            if (mevcutEtap.acildiMi()) //Bu metot, tüm harflerin açıldığını kontrol eder
            {
                //Eğer kullanıcı harf alarak kelimeyi açarsa, ceza olarak puanı eksilir
                oyun.Puan -= mevcutEtap.alinanHarf() * 100;
                sonrakiEtap();
            }
            if (!oyun.oyunBittiMi())
            {
                ekranGuncelle();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Eğer tahmin, etap cevabıyla eşitse
            if (textBox_cevap.Text.ToUpper()==mevcutEtap.Cevap)
            {
                //Puan eklenir, timer'lar durdurulur/başlatılır ve sonraki etap çağırılır
                oyun.Puan += mevcutEtap.skorDegeri() - ((mevcutEtap.HarfSayisi - mevcutEtap.acilmayanHarfSayisi()) * 100);
                sonrakiEtap();
                timer1.Start();
                timer2.Stop();
                tahminSure = 20; //Ayrıca 20 saniyelik tahmin süresi resetlenir
                button1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //20 Saniye boyunca kullanıcıya tahmin yapması için zaman başlatılır
            tahminSure--;
            if (tahminSure==0)
            {
                //Eğer süre biterse, puan azaltılır ve sonraki etaba geçilir
                oyun.Puan -= mevcutEtap.acilmayanHarfSayisi() * 100;
                tahminSure = 20;
                sonrakiEtap();
                button1.Enabled = false;
                timer1.Start();
                timer2.Stop();
            }
        }

        private void formAnasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
