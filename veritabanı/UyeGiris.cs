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
    public partial class UyeGiris : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");
       
        
        public UyeGiris()
        {
            InitializeComponent();
            
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

   

        private void btnGiris(object sender, EventArgs e)
        { 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from musteri where musteri_sifre=@musteri_sifre and musteri_mail=@musteri_mail", baglanti);
            komut.Parameters.AddWithValue("@musteri_mail", textBox1.Text);
            komut.Parameters.AddWithValue("@musteri_sifre", textBox2.Text);
            
          
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı!");
                PizzaSiparis frm12 = new PizzaSiparis();
                frm12.musteri_id2 = textBox1.Text.ToString();
                baglanti.Close();
                //form yönlendirme
                this.Hide();
               // PizzaSiparis frmsiparis = new PizzaSiparis();
                frm12.ShowDialog();
               // frmsiparis.ShowDialog();
                this.Show();


            }
            else
            {
                MessageBox.Show("Hatalı Giriş!");
                baglanti.Close();
            }
            

        }

        private void üyegiris_Load(object sender, EventArgs e)
        {

        }
    }
}
