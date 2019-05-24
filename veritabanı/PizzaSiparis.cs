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
    public partial class PizzaSiparis : Form
    {
        SqlConnection myConn = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");
        SqlDataAdapter adp = new SqlDataAdapter();
        public string pizza_fiyat;
        public double pizza_fiyat2;
        public int adet;
        public double tutar;
        public UyeGiris frm = new UyeGiris();
        public string musteri_id2;
        public int idtut;
        DateTime tarih;

        public PizzaSiparis()
        {
            InitializeComponent();
        }

        private void PizzaSiparis_Load(object sender, EventArgs e)
        {
           

            // müsteri id tutma!
            myConn.Open();
            SqlDataAdapter komut5 = new SqlDataAdapter("Select musteri_id from musteri where musteri_mail='" + musteri_id2 + "'", myConn);
            DataTable dt5 = new DataTable();
            komut5.Fill(dt5);
            idtut = Convert.ToInt32(dt5.Rows[0][0].ToString());
            // TODO: This line of code loads data into the 'veritabani_odevDataSet.urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.veritabani_odevDataSet.urunler);         
            SqlDataAdapter da;
            DataTable dt;
            
            da = new SqlDataAdapter("Select urun_fiyat_kucuk,urun_fiyat_orta,urun_fiyat_buyuk from urunler where urun_id=1 ", myConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables["urunler"];
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //stok durum gösterme
            SqlDataAdapter da3;
            DataTable dt3;
          
            da3 = new SqlDataAdapter("select stok_adet from stok ", myConn);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3, "stok");
            dataGridView2.DataSource = ds3.Tables["stok"];
            dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView2.DataSource = dt3;
            //STOK DURUMU LABELLAR
            label9.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
            label11.Text = dataGridView2.Rows[1].Cells[0].Value.ToString();
            label10.Text = dataGridView2.Rows[2].Cells[0].Value.ToString();
            label12.Text = dataGridView2.Rows[3].Cells[0].Value.ToString();

            myConn.Close();
           
        }

        private void btnSiparisVer(object sender, EventArgs e)
        {          
            myConn.Open();
            if (textBox1.Text != string.Empty && comboBox1.Text != string.Empty && comboBox2.Text != string.Empty)
            {
                if ((comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 0 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label11.Text)) || (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 1 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label11.Text)) || (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 2 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label11.Text)))
                {
                    if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 0)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=2 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", (Convert.ToInt32(label11.Text)-(Convert.ToInt32(textBox1.Text))));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 1)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=2 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", (Convert.ToInt32(label11.Text) - (Convert.ToInt32(textBox1.Text))));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 2)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=2 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", (Convert.ToInt32(label11.Text) - (Convert.ToInt32(textBox1.Text))));
                        cmd.ExecuteNonQuery();
                    }
                }
                else if ((comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 0 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label9.Text)) || (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 1 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label9.Text)) || (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 2 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label9.Text)))
                {
                    if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 0)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=1 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label9.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 1)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=1 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label9.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 2)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=1 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label9.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                }
                else if ((comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 0 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label10.Text)) || (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 1 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label10.Text)) || (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 2 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label10.Text)))
                {
                    if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 0)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=3 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label10.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 1)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=3 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label10.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 2)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=3 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label10.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();

                    }
                }
                else if ((comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 0 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label12.Text)) || (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 1 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label12.Text)) || (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 2 && Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(label12.Text)))
                {
                    if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 0)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=4 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label12.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 1)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=4 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label12.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 2)
                    {
                        pizza_fiyat = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        adet = Convert.ToInt32(textBox1.Text);
                        pizza_fiyat2 = Convert.ToDouble(pizza_fiyat);
                        tutar = pizza_fiyat2 * adet;
                        MessageBox.Show("Siparişiniz alınmıştır.Ödeceğiniz tutar=" + tutar + "TL");
                        //Siparişleri veritabanına ekleme!
                        tarih = DateTime.Now.Date;
                        SqlCommand komut21 = new SqlCommand("insert into satislar(urun_id,musteri_id,satis_tarih,adet) values('" + (comboBox1.SelectedIndex + 1) + "','" + idtut + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "')", myConn);
                        komut21.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("UPDATE stok SET stok_adet=@stok_adet WHERE urun_id=4 ", myConn);
                        cmd.Parameters.AddWithValue("@stok_adet", Convert.ToInt32(label12.Text) - (Convert.ToInt32(textBox1.Text)));
                        cmd.ExecuteNonQuery();
                    }
                }
            
                else MessageBox.Show("Seçtiğiniz ürün stokta bulunmamaktadır!");
                          
               
             }
            
            else MessageBox.Show("Bütün seçimleri tam yapınız!");
            //stok durum güncelleme pizza satış tablosu
            SqlDataAdapter da3;
            DataTable dt3;
            da3 = new SqlDataAdapter("select stok_adet from stok ", myConn);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3, "stok");
            dataGridView2.DataSource = ds3.Tables["stok"];
            dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView2.DataSource = dt3;
            //STOK DURUMU LABELLAR
            label9.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
            label11.Text = dataGridView2.Rows[1].Cells[0].Value.ToString();
            label10.Text = dataGridView2.Rows[2].Cells[0].Value.ToString();
            label12.Text = dataGridView2.Rows[3].Cells[0].Value.ToString();
            myConn.Close();

        }

        private void btnGeri(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
