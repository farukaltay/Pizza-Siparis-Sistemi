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
    public partial class PizzaciIslem : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");

        public PizzaciIslem()
        {
            InitializeComponent();
        }

        private void Pizzaciislem_Load(object sender, EventArgs e)
        {
            //4.sp
            DataTable dt3 = new DataTable();
            SqlCommand myCmd3 = new SqlCommand("sp_siparisler", baglanti);
            myCmd3.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da3 = new SqlDataAdapter(myCmd3);
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            //6.sp
            DataTable dt2 = new DataTable();
            SqlCommand myCmd2 = new SqlCommand("sp_musteri3", baglanti);
            myCmd2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da2 = new SqlDataAdapter(myCmd2);
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            baglanti.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //7.rapor
            SqlDataAdapter da;
            DataTable dt;
            baglanti.Open();
            da = new SqlDataAdapter("select * from satislar where musteri_id like '" + textBox1.Text + "%'", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "satislar");
            dataGridView1.DataSource = ds.Tables["satislar"];
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btnAra(object sender, EventArgs e)
        {
            //8.rapor
            if (textBox2.Text != string.Empty && textBox3.Text != string.Empty)
            {
                SqlDataAdapter da;
                DataTable dt;
                baglanti.Open();
                da = new SqlDataAdapter("select * from musteri where musteri_ad like '" + textBox2.Text + "' and musteri_soyad like '" + textBox3.Text + "'", baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds, "musteri");
                dataGridView2.DataSource = ds.Tables["musteri"];
                dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                baglanti.Close();
            }
            else MessageBox.Show("Boş alanları doldurunuz!");
        }
    }
}
