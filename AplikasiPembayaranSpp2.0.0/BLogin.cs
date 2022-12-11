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
    public partial class BLogin : Form
    {
        private string nisn, nama;

        Utils util = new Utils();
 
        public BLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to close this application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ALogin login = new ALogin();
            this.Hide();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNisn.Text) || string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtIdSpp.Text))
                {
                    MessageBox.Show("Isi data dengan lengkap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("SELECT COUNT(*) FROM siswa WHERE nisn = '"+txtNisn.Text+"'", util.koneksi);
                    int result = (int)util.cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        util.cmd = new SqlCommand("SELECT nama FROM siswa WHERE nisn = '"+txtNisn.Text+"'", util.koneksi);
                        nama = util.cmd.ExecuteScalar().ToString();
                        MessageBox.Show("Selamat datang "+nama, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nisn = txtNisn.Text;
                        util.koneksi.Close();
                        SiswaMain siswa = new SiswaMain(nisn, nama);
                        siswa.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kesalahan tidak ditemuka data.\nMohon isi identitas dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.koneksi.Close();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan "+err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }
    }
}
