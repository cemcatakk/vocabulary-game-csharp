using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyGameForm
{
    public class Etap
    {
         int _id;
         string _Soru;
         string _Cevap;
        int _harfSayisi;
        private bool[] acilan;
        private int _alinanHarf;
        public Etap(int id, string soru, string cevap)
        {
            //Yapıcı metot
            _id = id;
            _Soru = soru;
            _Cevap = cevap;
            _harfSayisi = cevap.Length;
            _alinanHarf = 0;
            //Bu dizi, Acilan harfleri kontrol eder
            Acilan = new bool[_harfSayisi];
            for (int i = 0; i < _harfSayisi; i++)
            {
                Acilan[i] = false;
            }
        }
        public int alinanHarf()
        {
            return _alinanHarf;
        }
        public void harfAl()
        {
            //Bu metot, açılmayan harflerden rastgele birini seçer
            int index;
            Random rnd = new Random();
            do
            {
                //Seçim işlemi bu döngüde gerçekleşir
                index = rnd.Next(0, _harfSayisi);
            } while (Acilan[index]);
            //Ardından alınan harf arttırılır ve Acilan dizisindeki seçilen index 'true' olarak değiştirilir
            Acilan[index] = true;
            _alinanHarf++;
        }
        public int skorDegeri()
        {
            return _harfSayisi * 100;
        }
        public int acilmayanHarfSayisi()
        {
            //Acilan dizisindeki her FALSE değeri için acilmayan harf sayısı 1 arttırılır ve geri döndürülür
            int adet = 0;
            foreach (bool item in Acilan)
            {
                if (!item)
                {
                    adet++;
                }
            }
            return adet;
        }
        public bool acildiMi()
        {
            foreach (bool item in Acilan)
            {
                //Eğer Acilan dizisindeki elemanlardan en az 1'i false ise, kelimede hala açılmayan harf vardır
                if (!item)
                {
                    return false;
                }
            }
            return true;
        }
        public int HarfSayisi
        {
            get
            {
                return _harfSayisi;
            }
            set
            {
                _harfSayisi = value;
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Soru
        {
            get
            {
                return _Soru;
            }

            set
            {
                _Soru = value;
            }
        }
        public string Cevap
        {
            get
            {
                return _Cevap;
            }

            set
            {
                _Cevap = value;
            }
        }

        public bool[] Acilan { get => acilan; set => acilan = value; }
    }
}
