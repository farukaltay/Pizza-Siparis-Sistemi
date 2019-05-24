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
    public partial class PizzaciGiris : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");


        public PizzaciGiris()
        {
            InitializeComponent();
        }

        private void btnGeri(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnGiris(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from pizzaci where pizzaci_sifre=@pizzaci_sifre and pizzaci_kullaniciad=@pizzaci_kullaniciad", baglanti);
            komut.Parameters.AddWithValue("@pizzaci_kullaniciad", textBox1.Text);
            komut.Parameters.AddWithValue("@pizzaci_sifre", textBox2.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı!");
                baglanti.Close();
                this.Hide();
                PizzaciIslem frmpizzaciislem = new PizzaciIslem();
                frmpizzaciislem.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Hatalı Giriş!");
                baglanti.Close();
            }
        }

        private void pizzacigiris_Load(object sender, EventArgs e)
        {

        }
    }
}
