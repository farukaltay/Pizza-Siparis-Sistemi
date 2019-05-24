using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 *** Faruk_Altay 11.11.2017
 **/

namespace veritabanı
{
    public partial class FormGiris : Form
    {
        public string musteri_id;
        public FormGiris()
        {
            InitializeComponent();
        }

        private void btnUyeOl(object sender, EventArgs e)
        {
            this.Hide();
            UyeOl frmüyeol = new UyeOl();
            frmüyeol.ShowDialog();
            this.Show();
        }

        private void btnUyeGiris(object sender, EventArgs e)
        {
            this.Hide();
            UyeGiris frmüyegiris = new UyeGiris();
            frmüyegiris.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnPizzaciGiris(object sender, EventArgs e)
        {
            this.Hide();
            PizzaciGiris frmpizzacigiris = new PizzaciGiris();
            frmpizzacigiris.ShowDialog();
            this.Show();
        }

        private void btnAdminGiris(object sender, EventArgs e)
        {
            this.Hide();
            AdminGiris frmadmingiris = new AdminGiris();
            frmadmingiris.ShowDialog();
            this.Show();
        }

     

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
