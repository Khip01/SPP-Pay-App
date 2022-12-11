using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AplikasiPembayaranSpp2._0._0
{
    public partial class SiswaMain : Form
    {

/* USER DEFINED FUNCTION */
        private void setButton(bool condition)
        {
            if (condition == true)
            {
                button1.BackColor = Color.YellowGreen;
                button1.Text = "Cari";
                button1.Image = Properties.Resources.search1;
                tampilData();
                cbBulan.SelectedItem = "Semua Bulan";
            }
            else
            {
                button1.BackColor = Color.IndianRed;
                button1.Text = "Batal";
                button1.Image = Properties.Resources.Close32;
            }
        }


        private void tampilData()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM pembayaran WHERE nisn = '"+nisnPub+"'", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            dgvSiswa.DataSource = dataSet.Tables[0];
        }
/* END */

        Utils util = new Utils();

/* Pass nisn for accessing in form */
        private string nisnPub;
        public SiswaMain(string nisn, string nama)
        {
            InitializeComponent();

/* Pass nisn and nama from BLogin */
            label1.Text = "Selamat datang " + nama;
            this.nisnPub = nisn;

            tampilData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            About tentang = new About();
            tentang.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to login using another account? \n\n*This session will automatically log you out of the previous account", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                BLogin login = new BLogin();
                this.Hide();
                login.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to close this application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Batal" || cbBulan.Text == "Semua Bulan")
            {
                setButton(true);
            }
            else
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM pembayaran WHERE nisn = '" + nisnPub + "' AND bulan_dibayar = '"+cbBulan.Text+"'", util.koneksi);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dgvSiswa.DataSource = dataSet.Tables[0];
                setButton(false);
            }
        }
    }
}
