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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.OleDb;

/**
 *** Faruk_Altay 11.11.2017
 **/

namespace veritabanı
{
    public partial class AdminIslem : Form
    {
        public SqlConnection myConn = new SqlConnection("Data Source=.;Integrated Security=TRUE;Initial Catalog=Veritabani_odev");
        public string id;
        SqlCommandBuilder cmb;      
        SqlDataAdapter sda;
        SqlDataAdapter sda2;
        SqlDataAdapter sda3;
        DataTable dtm2123;
        DataTable dtm212;
        DataTable dtm21;
        public string satis_id;
        public string yol;
        public string yol2;
        public AdminIslem()
        {
            InitializeComponent();
        }

        private void adminislem_Load(object sender, EventArgs e)
        {
            //1.store procedure
            DataTable dt = new DataTable();

            myConn.Open();
            sda = new SqlDataAdapter(@"select pizzaci_id,pizzaci_ad,pizzaci_soyad,pizzaci_kullaniciad,pizzaci_sifre,pizzaci_tel from pizzaci", myConn);
            dtm21 = new DataTable();
            sda.Fill(dtm21);
            dataGridView1.DataSource = dtm21;

            sda2 = new SqlDataAdapter(@"select pizzaci_ad,pizzaci_soyad,pizzaci_kullaniciad,pizzaci_sifre,pizzaci_tel from silinen_pizzacilar", myConn);
            dtm212 = new DataTable();
            sda2.Fill(dtm212);
            dataGridView4.DataSource = dtm212;

            sda3 = new SqlDataAdapter(@"select urun_id,musteri_id,satis_tarih,adet from silinen_siparisler", myConn);
            dtm2123 = new DataTable();
            sda3.Fill(dtm2123);
            dataGridView5.DataSource = dtm2123;
            //SqlCommand myCmd = new SqlCommand("sp_pizzaci2", myConn);
            //myCmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(myCmd);
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //2.sp
            DataTable dt2 = new DataTable();
            SqlCommand myCmd2 = new SqlCommand("sp_musteri2", myConn);
            myCmd2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da2 = new SqlDataAdapter(myCmd2);
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
           
            //3.sp
            DataTable dt3 = new DataTable();
            SqlCommand myCmd3 = new SqlCommand("sp_siparisler", myConn);
            myCmd3.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da3 = new SqlDataAdapter(myCmd3);
            da3.Fill(dt3);
            dataGridView3.DataSource = dt3;


            myConn.Close();


            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //4.rapor
            SqlDataAdapter da;
            DataTable dt;
            myConn.Open();
            da = new SqlDataAdapter("select * from satislar where musteri_id like '" + textBox1.Text + "%'", myConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "satislar");
            dataGridView3.DataSource = ds.Tables["satislar"];
            dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            myConn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //5.rapor
            SqlDataAdapter da;
            DataTable dt;
            myConn.Open();
            da = new SqlDataAdapter("select * from pizzaci where pizzaci_ad like '" + textBox2.Text + "%'", myConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "pizzaci");
            dataGridView1.DataSource = ds.Tables["pizzaci"];
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            myConn.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //6.rapor
            SqlDataAdapter da;
            DataTable dt;
            myConn.Open();
            da = new SqlDataAdapter("select * from musteri where musteri_ad like '" + textBox3.Text + "%'", myConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "musteri");
            dataGridView2.DataSource = ds.Tables["musteri"];
            dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            myConn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPizzaciSil(object sender, EventArgs e)
        {
            // pizzaci silme işlemi
            myConn.Open();
            if (dataGridView1.SelectedRows.Count != 0)
            {
                id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                SqlCommand komut = new SqlCommand("delete from pizzaci where pizzaci_id=@pizzaci_id ", myConn);
                komut.Parameters.AddWithValue("@pizzaci_id", dataGridView1.CurrentRow.Cells[0].Value);
                komut.ExecuteNonQuery();
                MessageBox.Show("Seçilen veri silindi.");
                DataTable dt2 = new DataTable();
                SqlCommand myCmd2 = new SqlCommand("sp_pizzaci2", myConn);
                myCmd2.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da2 = new SqlDataAdapter(myCmd2);
                da2.Fill(dt2);
                dataGridView1.DataSource = dt2;
                //SqlDataAdapter da = new SqlDataAdapter("select * from pizzaci  ", myConn);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "pizzaci");
                //dataGridView1.DataSource = ds.Tables["pizzaci"];
                myConn.Close();
            }
            else
                MessageBox.Show("Silme işlemi için satır seçiniz!");

            myConn.Close();
        }

        private void btnEkle(object sender, EventArgs e)
        {
            myConn.Open();
            if(tbad.Text!=string.Empty && tbadres.Text!=string.Empty && tbmail.Text!=string.Empty && tbsifre.Text!=string.Empty && tbsoyad.Text!=string.Empty && tbtel.Text!=string.Empty)               
            {
                if (tbmail.Text.IndexOf("@gmail.com") != -1 || tbmail.Text.IndexOf("@hotmail.com") != -1 || tbmail.Text.IndexOf("@outlook.com") != -1 || tbmail.Text.IndexOf("@msn.com") != -1 && tbmail.Text.IndexOf("@yahoo.com") != -1)
                {
                    SqlCommand komut = new SqlCommand("Select * from musteri where musteri_tel=@musteri_tel  or musteri_mail=@musteri_mail", myConn);
                    komut.Parameters.AddWithValue("@musteri_mail", tbmail.Text);
                    komut.Parameters.AddWithValue("@musteri_tel", tbtel.Text);
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Böyle bir Müsteri bulunmaktadır");
                        myConn.Close();
                        tbad.Text = "";
                        tbsoyad.Text = "";
                        tbmail.Text = "";
                        tbadres.Text = "";
                        tbsifre.Text = "";
                        tbtel.Text = "";
                    }


                    else
                    {

                        dr.Close();
                        SqlCommand komut2 = new SqlCommand("insert into musteri(musteri_ad,musteri_soyad,musteri_tel,musteri_mail,musteri_adres,musteri_sifre) values('" + tbad.Text + "','" + tbsoyad.Text + "','" + tbtel.Text + "','" + tbmail.Text + "','" + tbadres.Text + "','" + tbsifre.Text + "')", myConn);
                        komut2.ExecuteNonQuery();
                        myConn.Close();
                        MessageBox.Show("Kaydınız Başarı ile gerçekleşti.");
                        DataTable dt2 = new DataTable();
                        SqlCommand myCmd2 = new SqlCommand("sp_musteri2", myConn);
                        myCmd2.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da2 = new SqlDataAdapter(myCmd2);
                        da2.Fill(dt2);
                        dataGridView2.DataSource = dt2;

                    }
                    myConn.Close();
                }
                else MessageBox.Show("Düzgün mail adresi giriniz!");
                myConn.Close();
             }           
            else MessageBox.Show("Boş Girilemez");
            myConn.Close();
        }

        private void btnPizzaciEkle(object sender, EventArgs e)
        {
             myConn.Open();
             if (tbad_p.Text != string.Empty && tbkulad_p.Text != string.Empty && tbsifre_p.Text != string.Empty && tbsoyad_p.Text != string.Empty && tbtel_p.Text != string.Empty)
             {

                 SqlCommand komut = new SqlCommand("Select * from pizzaci where pizzaci_kullaniciad=@pizzaci_kullaniciad  or pizzaci_tel=@pizzaci_tel", myConn);
                 komut.Parameters.AddWithValue("@pizzaci_tel", tbtel_p.Text);
                 komut.Parameters.AddWithValue("@pizzaci_kullaniciad", tbkulad_p.Text);
                 SqlDataReader dr = komut.ExecuteReader();
                 if (dr.Read())
                 {
                     MessageBox.Show("Böyle bir Pizzacı bulunmaktadır");
                     myConn.Close();
                     tbad_p.Text = "";
                     tbsoyad_p.Text = "";
                     tbkulad_p.Text = "";
                     tbsifre_p.Text = "";
                     tbtel_p.Text = "";
                 }


                 else
                 {

                     dr.Close();
                     SqlCommand komut2 = new SqlCommand("insert into pizzaci(pizzaci_ad,pizzaci_soyad,pizzaci_kullaniciad,pizzaci_sifre,pizzaci_tel) values('" + tbad_p.Text + "','" + tbsoyad_p.Text + "','" + tbkulad_p.Text + "','" + tbsifre_p.Text + "','" + tbtel_p.Text + "')", myConn);
                     komut2.ExecuteNonQuery();
                     myConn.Close();
                     MessageBox.Show("Kaydınız Başarı ile gerçekleşti.");
                     DataTable dt2 = new DataTable();
                     SqlCommand myCmd2 = new SqlCommand("sp_pizzaci2", myConn);
                     myCmd2.CommandType = CommandType.StoredProcedure;
                     SqlDataAdapter da2 = new SqlDataAdapter(myCmd2);
                     da2.Fill(dt2);
                     dataGridView1.DataSource = dt2;

                 }
                 myConn.Close();

             }
             else MessageBox.Show("Boş Girilemez");
        }


        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbtel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbtel_p_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSiparisSil(object sender, EventArgs e)
        {

            // sipariş silme işlemi
            myConn.Open();
            if (dataGridView3.SelectedRows.Count != 0)
            {
                id = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();

                SqlCommand komut = new SqlCommand("delete from satislar where satis_id=@satis_id ", myConn);
                komut.Parameters.AddWithValue("@satis_id", dataGridView3.CurrentRow.Cells[0].Value);
                komut.ExecuteNonQuery();
                MessageBox.Show("Seçilen veri silindi.");

                SqlDataAdapter da = new SqlDataAdapter("select * from satislar  ", myConn);
                DataSet ds = new DataSet();
                da.Fill(ds, "satislar");
                dataGridView3.DataSource = ds.Tables["satislar"];
                myConn.Close();

            }
            else
                MessageBox.Show("Silme işlemi için satır seçiniz!");

            myConn.Close();
        }

        private void btnPizzaciGüncelle(object sender, EventArgs e)
        {
            cmb = new SqlCommandBuilder(sda);
            sda.Update(dtm21);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void btn_raporla_Click(object sender, EventArgs e)
        {
  

            SqlDataAdapter sales_report = new SqlDataAdapter();
            if (nd_satis_id.Value == 0 && nd_urun_id.Value == 0 && nd_musteri_id.Value == 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' ", myConn);
            }
            else if (nd_satis_id.Value == 0 && nd_urun_id.Value == 0 && nd_musteri_id.Value != 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE  musteri_id='" + nd_musteri_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' ) ", myConn);
            }
            else if (nd_satis_id.Value == 0 && nd_urun_id.Value != 0 && nd_musteri_id.Value == 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE  urun_id='" + nd_urun_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' ) ", myConn);

            }
            else if (nd_satis_id.Value != 0 && nd_urun_id.Value == 0 && nd_musteri_id.Value == 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE  satis_id='" + nd_satis_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' ) ", myConn);
            }
            else if (nd_satis_id.Value != 0 && nd_urun_id.Value != 0 && nd_musteri_id.Value == 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE urun_id='" + nd_urun_id.Value + "' AND urun_id='" + nd_urun_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' )", myConn);

            }
            else if (nd_satis_id.Value == 0 && nd_urun_id.Value != 0 && nd_musteri_id.Value != 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE musteri_id='" + nd_musteri_id.Value + "' AND urun_id='" + nd_urun_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' )", myConn);

            }
            else if (nd_satis_id.Value != 0 && nd_urun_id.Value == 0 && nd_musteri_id.Value != 0)
            {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE musteri_id='" + nd_musteri_id.Value + "' AND satis_id='" + nd_satis_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' )", myConn);

            }
            else {
                sales_report = new SqlDataAdapter("SELECT * from satislar WHERE urun_id='" + nd_urun_id.Value + "' AND musteri_id='" + nd_musteri_id.Value + "' AND satis_id='" + nd_satis_id.Value + "' AND ( satis_tarih BETWEEN '" + first_date.Value.ToString("yyyy-MM-dd") + "' AND '" + last_date.Value.ToString("yyyy-MM-dd") + "' )", myConn);


            }

            DataTable tbl_sales_rp = new DataTable();
            sales_report.Fill(tbl_sales_rp);

            
            
        }

        private void btn_excel_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            dataGridView3.SelectAll();
            DataObject dataObj = dataGridView3.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_excel_ice_aktar_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + lb_dosya_yolu.Text + "'; Extended Properties='Excel 12.0 xml;HDR=YES;'");
                baglanti.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                db_excel_data.DataSource = dt.DefaultView;
                db_excel_data.Refresh();
                baglanti.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Veri çekilirken bir hata oluştu !");
            }
        }

        private void btn_excel_browse_Click(object sender, EventArgs e)
        {
            try
            {
                of_excel_browse.ShowDialog();
                lb_dosya_yolu.Text = of_excel_browse.FileName;
                lb_dosya_yolu.Visible = true;
                btn_excel_ice_aktar.Visible = true;
                btn_excel_db_check.Visible = true;

            }
            catch (Exception)
            {

                MessageBox.Show("Bir hata oluştu !");
            }
        }

        private void btn_excel_db_check_Click(object sender, EventArgs e)
        {
            //    //try
            //    //{
            //    for (int i = 1; i < db_excel_data.Rows.Count; i++)
            //    {

            //        myConn.Open();

            //        SqlDataAdapter adp_sales = new SqlDataAdapter("insert into satislar(urun_id, musteri_id, adet) values('" + Convert.ToInt32(db_excel_data.Rows[i].Cells[1].Value.ToString()) + "','" + Convert.ToInt32(db_excel_data.Rows[i].Cells[2].Value.ToString()) + "','" + Convert.ToInt32(db_excel_data.Rows[i].Cells[4].Value.ToString()) + "'", myConn);

            //        //SqlDataAdapter adp_sales = new SqlDataAdapter("exec proc_add_sale " + Convert.ToInt32(db_excel_data.Rows[i].Cells[1].Value.ToString()) + " , " + Convert.ToInt32(db_excel_data.Rows[i].Cells[2].Value.ToString()) + " , " + Convert.ToInt32(db_excel_yedek.Rows[i].Cells[3].Value.ToString()) + " , " + Convert.ToInt32(db_excel_yedek.Rows[i].Cells[4].Value.ToString()) + "", db);
            //        DataTable tbl_adp_sales = new DataTable();
            //        adp_sales.Fill(tbl_adp_sales);
            //        myConn.Close();

            //    }
            //    MessageBox.Show("Veriler başarılı bir şekilde eklendi !");

            //    //}
            //    //catch (Exception)
            //    //{

            //    //    MessageBox.Show("Veriler eklenirken hata oluştu !");
            //    //}

            if (textBox4.Text != string.Empty && textBox5.Text != string.Empty && textBox6.Text != string.Empty && db_excel_data.SelectedRows.Count!=0)
            {
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                string sql = null;
                string sql2 = null;
                string sql3 = null;
                OleDbConnection Baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + lb_dosya_yolu.Text + "'; Extended Properties='Excel 12.0 xml;HDR=YES;'");
                Baglanti.Open();
                myCommand.Connection = Baglanti;
                sql = "Update [Sheet1$] set urun_id=" + textBox4.Text + "  where adet=" + satis_id;
                sql2 = "Update [Sheet1$] set musteri_id=" + textBox5.Text + "  where adet=" + satis_id;
                sql3 = "Update [Sheet1$] set adet=" + textBox6.Text + "  where adet=" + satis_id;


                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = sql2;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = sql3;
                myCommand.ExecuteNonQuery();
                Baglanti.Close();
                MessageBox.Show("Güncellendi");
            }
            else MessageBox.Show("Boş değer girilemez!");
        }

        private void btnVeriyiAl(object sender, EventArgs e)
        {
            if (db_excel_data.SelectedRows.Count != 0)
            {
                satis_id = db_excel_data.SelectedRows[0].Cells[0].Value.ToString();
                textBox4.Text = db_excel_data.SelectedRows[0].Cells[1].Value.ToString();
                textBox5.Text = db_excel_data.SelectedRows[0].Cells[2].Value.ToString();
                textBox6.Text = db_excel_data.SelectedRows[0].Cells[4].Value.ToString();
            }
            else MessageBox.Show("Hata bütün satırı seçiniz!");
        }

    
        private void btnGozatYedekAl(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowDialog();
            yol = fb.SelectedPath;
            textBox42.Text = yol.ToString();
        }

        private void btnYedekAl(object sender, EventArgs e)
        {
            string database = myConn.Database.ToString();
            string cmd = "backup database[" + database + "] to disk='" + yol + "\\" + textBox62.Text.ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
            using (SqlCommand command = new SqlCommand(cmd, myConn))
            {
                if (myConn.State != ConnectionState.Open)
                {
                    myConn.Open();
                }
                command.ExecuteNonQuery();
                myConn.Close();
                MessageBox.Show("yedek alındı");
            }
        }

        private void btnGozatYedekYukle(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            yol2 = file.FileName;
            textBox52.Text = yol2.ToString();
        }

        private void btnYedekYukle(object sender, EventArgs e)
        {
            if (textBox52.Text != string.Empty)
            {
                myConn.Open();
                string database = myConn.Database.ToString();
                string cmd2 = string.Format("alter database [" + database + "]set single_user with rollback immediate");
                SqlCommand sc = new SqlCommand(cmd2, myConn);
                sc.ExecuteNonQuery();
                string cmd3 = "use master restore database[" + database + "] from disk='" + yol2 + "'with replace;";
                SqlCommand sc3 = new SqlCommand(cmd3, myConn);
                sc3.ExecuteNonQuery();
                string cmd4 = string.Format("alter database[" + database + "]set multi_user");
                SqlCommand sc4 = new SqlCommand(cmd4, myConn);
                sc4.ExecuteNonQuery();
                MessageBox.Show("Yedek yükleme işlemi tamamlandı!");
                myConn.Close();
            }
            else MessageBox.Show("Yedek dosyasını seçiniz!");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
      
       
      

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
       
      

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void btnCopKutusuYenile(object sender, EventArgs e)
        {
            sda2 = new SqlDataAdapter(@"select pizzaci_ad,pizzaci_soyad,pizzaci_kullaniciad,pizzaci_sifre,pizzaci_tel from silinen_pizzacilar", myConn);
            dtm212 = new DataTable();
            sda2.Fill(dtm212);
            dataGridView4.DataSource = dtm212;

            sda3 = new SqlDataAdapter(@"select urun_id,musteri_id,satis_tarih,adet from silinen_siparisler", myConn);
            dtm2123 = new DataTable();
            sda3.Fill(dtm2123);
            dataGridView5.DataSource = dtm2123;
        }
    }

}
    

