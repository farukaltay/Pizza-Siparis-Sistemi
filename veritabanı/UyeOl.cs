using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

/**
 *** Faruk_Altay 11.11.2017
 **/

namespace veritabanı
{
    public partial class UyeOl : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");
        public UyeOl()
        {
            InitializeComponent();
        }

        private void üyeol_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnGeri(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnKayitOl(object sender, EventArgs e)
        {
            if (tbad.Text != string.Empty && tbadres.Text != string.Empty && tbmail.Text != string.Empty && tbsifre.Text != string.Empty && tbsifretekrar.Text != string.Empty && tbsoyad.Text != string.Empty && textBox1.Text != string.Empty)
            {
                if (tbmail.Text.IndexOf("@gmail.com") != -1 || tbmail.Text.IndexOf("@hotmail.com") != -1 || tbmail.Text.IndexOf("@outlook.com") != -1 || tbmail.Text.IndexOf("@msn.com") != -1 && tbmail.Text.IndexOf("@yahoo.com") != -1)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Select * from musteri where musteri_tel=@musteri_tel  or musteri_mail=@musteri_mail", baglanti);
                    komut.Parameters.AddWithValue("@musteri_mail", tbmail.Text);
                    komut.Parameters.AddWithValue("@musteri_tel", textBox1.Text);
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Böyle bir Müsteri bulunmaktadır");
                        baglanti.Close();
                        tbad.Text = "";
                        tbsoyad.Text = "";
                        tbmail.Text = "";
                        tbadres.Text = "";
                        tbsifre.Text = "";
                        tbsifretekrar.Text = "";
                        textBox1.Text = "";

                    }
                    else
                    {
                        if (tbsifre.Text == tbsifretekrar.Text)
                        {

                            dr.Close();
                            SqlCommand komut2 = new SqlCommand("insert into musteri(musteri_ad,musteri_soyad,musteri_tel,musteri_mail,musteri_adres,musteri_sifre) values('" + tbad.Text + "','" + tbsoyad.Text + "','" + textBox1.Text + "','" + tbmail.Text + "','" + tbadres.Text + "','" + tbsifre.Text + "')", baglanti);
                            komut2.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("Kaydınız Başarı ile gerçekleşti.Sipariş Sayfasına yönlendiriliyorsunuz.");
                            this.Hide();
                            PizzaSiparis frmsiparis = new PizzaSiparis();
                            frmsiparis.ShowDialog();
                            this.Show();
                            baglanti.Close();

                        }
                        else
                        {
                            MessageBox.Show("Şifreler aynı değil.");
                            tbsifre.Text = "";
                            tbsifretekrar.Text = "";
                            baglanti.Close();
                        }
                    }
                }
                else MessageBox.Show("Geçersiz mail türü girdiniz!");
            }
            
            else  MessageBox.Show("Bütün alanları doldurunuz.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

                


           
        
    }
}
