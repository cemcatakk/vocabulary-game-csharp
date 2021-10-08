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
    public partial class Girisform : Form
    {
        public Girisform()
        {
            InitializeComponent();
        }

        public static string sendData;

        private void btn_giris_Click(object sender, EventArgs e)
        {
            formAnasayfa anasayfa = new formAnasayfa();
            if (tb_kadi.Text == "")
            {
                MessageBox.Show("Oyuncu Adı Boş Bırakılamaz!");
            }
            else
            {
                sendData = tb_kadi.Text;
                anasayfa.Show();
                this.Hide();
            }
        }

        private void link_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/SerkanOzkann"); //GitHub linki için gerekli kod.
        }

        private void btn_SoruEkle_Click(object sender, EventArgs e)
        {
            new FormSoruEkle().ShowDialog();
        }
    }
}
