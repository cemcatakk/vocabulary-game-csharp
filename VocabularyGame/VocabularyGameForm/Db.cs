using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace VocabularyGameForm
{
    class Db
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Db()
        {
            //Test
            //App.config dosyasından ConString ismindeki bağlantı metnini çekiyoruz
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            cmd.Connection = con;
            con.Open();
            con.Close();
        }
        private Etap etapGetir(int harfSayisi)
        {
            //Bu metot, verilen harf sayısına göre bir adet etap nesnesi döndürür
            Etap etap = null;
            con.Open();
            //Sorgu ile, harfSayisi uzunluğundaki tüm cevaplardan rastgele biri seçilir
            cmd.CommandText = "SELECT * FROM Games WHERE LEN(Answers)=" + harfSayisi+" ORDER BY NEWID()";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //Ve etap nesnesi oluşturulur
                etap = new Etap(int.Parse(dr[0].ToString()), dr[1].ToString().Trim(), dr[2].ToString().Trim().ToUpper());
            }
            con.Close();
            return etap;
        }
        private bool etapVarmi(Etap etap,List<Etap> etaplar)
        {
            //Bu liste, her yarışmada aynı etaptan bir kere olması için kullanılır
            foreach (Etap item in etaplar)
            {
                if (item.Id==etap.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Etap> etapListesiAl()
        {
            List<Etap> etaplar = new List<Etap>();
            Etap temp;
            //4 harften 10 harf'e kadar
            for (int i = 4; i <= 10; i++)
            {
                //Her harften 2 soru olacak şekilde
                for (int j = 0; j < 2; j++)
                {
                    //Ve her etaptan sadece 1 tane olacak şekilde
                    do
                    {
                        temp = etapGetir(i);
                    }
                    while (etapVarmi(temp, etaplar));
                    //Etap oluşturulur ve listeye eklenir
                    etaplar.Add(temp);
                }
            }
            return etaplar;
        }
        public void EtapEkle(Etap etap)
        {
            //Etap nesnesini veri tabanına kaydeder
            con.Open();
            cmd.CommandText=("INSERT INTO Games(Questions,Answers) VALUES('" + etap.Soru + "','" + etap.Cevap + "')");
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
