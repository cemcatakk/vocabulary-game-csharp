using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VocabularyGameForm
{
    public partial class FormSoruEkle : Form
    {
        public FormSoruEkle()
        {
            InitializeComponent();
        }

        Db db = new Db();
        private void button1_Click(object sender, EventArgs e)
        {
            string soru = tbSoru.Text.Trim();
            string cevap = tbCevap.Text.Trim();
            if(soru.Length>=15&&(cevap.Length>=4&&cevap.Length<=10)&&!cevap.Contains(" "))
            {
                Etap etap = new Etap(0, soru, cevap);
                db.EtapEkle(etap);
                MessageBox.Show("Etap eklendi.");
            }
            else
            {
                MessageBox.Show("Soru eklenemedi.");
            }
        }
    }
}
