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
    public partial class AdminGiris : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");

        public AdminGiris()
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
            SqlCommand komut = new SqlCommand("Select * from yonetici where yonetici_sifre=@yonetici_sifre and yonetici_kullaniciad=@yonetici_kullaniciad", baglanti);
            komut.Parameters.AddWithValue("@yonetici_kullaniciad", textBox1.Text);
            komut.Parameters.AddWithValue("@yonetici_sifre", textBox2.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı!");
                baglanti.Close();
                this.Hide();
                AdminIslem frmadminislem = new AdminIslem();
                frmadminislem.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Hatalı Giriş!");
                baglanti.Close();
            }
        }

        private void admingiris_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
