using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyGameForm
{
    class Oyun
    {
        private List<Etap> etaplar;
        private string oyuncuAdi;
        private int puan;
        private int kalanSure;
        private DateTime tarih;
        private int mevcutSoru = 0;
        public Oyun()
        {
            puan = 0;
        }
        public bool oyunBittiMi()
        {
            return mevcutSoru == etaplar.Count+1;
        }
        public Etap sonrakiSoru()
        {
            mevcutSoru++;
            if (oyunBittiMi())
            {
                return null;
            }
            return etaplar[mevcutSoru-1];
        }
        public List<Etap> Etaplar { get => etaplar; set => etaplar = value; }
        public string OyuncuAdi { get => oyuncuAdi; set => oyuncuAdi = value; }
        public int Puan { get => puan; set => puan = value; }
        public int KalanSure { get => kalanSure; set => kalanSure = value; }
        public DateTime Tarih { get => tarih; set => tarih = value; }
    }
}
