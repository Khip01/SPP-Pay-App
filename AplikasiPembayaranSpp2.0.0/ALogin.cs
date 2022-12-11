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
    public partial class ALogin : Form
    {
        Utils util = new Utils();
        
        public ALogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to close this application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label5_MouseClick(object sender, MouseEventArgs e)
        {
            BLogin login = new BLogin();
            this.Hide();
            login.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                util.koneksi.Open();
                util.cmd = new SqlCommand("SELECT COUNT(*) FROM petugas WHERE id_petugas = '"+txtIdPetugas.Text+"' AND username = '"+txtUsername.Text+"' AND password = '"+txtPassword.Text+"'",util.koneksi);
                int result = (int)util.cmd.ExecuteScalar();
                if (result > 0)
                {
                    util.cmd = new SqlCommand("SELECT level FROM petugas WHERE id_petugas = '"+txtIdPetugas.Text+"'", util.koneksi);
                    string level = util.cmd.ExecuteScalar().ToString();
                    if (level == "ADMIN")
                    {
                        MessageBox.Show("Selamat datang "+txtUsername.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataSiswa siswa = new DataSiswa();
                        siswa.Show();
                        this.Hide();
                        util.koneksi.Close();
                    }
                    else if (level == "PETUGAS")
                    {
                        MessageBox.Show("Selamat datang " + txtUsername.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PetugasMain petugas = new PetugasMain();
                        petugas.Show();
                        this.Hide();
                        util.koneksi.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Kesalahan, tidak ditemukan data!\nPeriksa kembali identitas anda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.koneksi.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan "+err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
            
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            button7.Image = Properties.Resources.openLock;
            txtPassword.PasswordChar = '\0';
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            button7.Image = Properties.Resources._lock;
            txtPassword.PasswordChar = '*';
        }
    }
}
