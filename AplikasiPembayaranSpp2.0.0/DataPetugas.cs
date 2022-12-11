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
    public partial class DataPetugas : Form
    {
        Utils util = new Utils();

        private void tampilData()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM petugas", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgvPetugas.DataSource = dataSet.Tables[0];
        }

        private void clear()
        {
            txtIdPetugas.Text = null;
            txtUsername.Text = null;
            txtPassword.Text = null;
            txtNamaPetugas.Text = null;
            cbLevel.SelectedItem = "ADMIN";
        }

        private void rand()
        {
            util.koneksi.Close();
            string col = "id_petugas", table = "petugas";
            txtIdPetugas.Text = util.dataRand(col, table);
        }

        public DataPetugas()
        {
            InitializeComponent();
            tampilData();
            rand();
            txtPassword.PasswordChar = '*';
        }

        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to login using another account? \n\n*This session will automatically log you out of the previous account", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                ALogin login = new ALogin();
                this.Hide();
                login.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to close this application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            TransaksiPembayaran transaksi = new TransaksiPembayaran();
            transaksi.Show();
            this.Hide();
        }

        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            DataSpp spp = new DataSpp();
            spp.Show();
            this.Hide();
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            DataKelas kelas = new DataKelas();
            kelas.Show();
            this.Hide();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            DataSiswa siswa = new DataSiswa();
            siswa.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            rand();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdPetugas.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtNamaPetugas.Text))
                {
                    MessageBox.Show("Kesalahan, isi semua data dengan benar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
                else
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("INSERT INTO petugas VALUES ('"+txtIdPetugas.Text+"', '"+txtUsername.Text+"', '"+txtPassword.Text+"', '"+txtNamaPetugas.Text+"', '"+cbLevel.Text+"')",util.koneksi);
                    util.cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diinput!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilData();
                    clear();
                    rand();
                    util.koneksi.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan "+err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tampilData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdPetugas.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtNamaPetugas.Text))
                {
                    MessageBox.Show("Kesalahan, isi semua data dengan benar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult check = MessageBox.Show("Apakah anda yakin untuk mengubah data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (check == DialogResult.Yes)
                    {
                        util.koneksi.Open();
                        util.cmd = new SqlCommand("UPDATE petugas SET username = '" + txtUsername.Text + "', password = '" + txtPassword.Text + "', nama_petugas = '" + txtNamaPetugas.Text + "', level = '" + cbLevel.Text + "' WHERE id_petugas = '" + txtIdPetugas.Text + "'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil mengubah data!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilData();
                        clear();
                        rand();
                        util.koneksi.Close();
                    }
                    else
                    {
                        clear();
                        rand();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan "+err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah anda yakin ingin menghapus data tersebut?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("DELETE FROM petugas WHERE id_petugas = '" + txtIdPetugas.Text + "'", util.koneksi);
                    util.cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menghapus data!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilData();
                    clear();
                    rand();
                    util.koneksi.Close();
                }
                else
                {
                    clear();
                    rand();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }

        private void dgvPetugas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvPetugas.CurrentCell.RowIndex;

            txtIdPetugas.Text = dgvPetugas.Rows[baris].Cells[0].Value.ToString();
            txtUsername.Text = dgvPetugas.Rows[baris].Cells[1].Value.ToString();
            txtPassword.Text = dgvPetugas.Rows[baris].Cells[2].Value.ToString();
            txtNamaPetugas.Text = dgvPetugas.Rows[baris].Cells[3].Value.ToString();
            cbLevel.Text = dgvPetugas.Rows[baris].Cells[4].Value.ToString();
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
