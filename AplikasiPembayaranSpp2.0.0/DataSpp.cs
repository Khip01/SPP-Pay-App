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
    public partial class DataSpp : Form
    {
        Utils util = new Utils();

        private void clear()
        {
            txtIdSpp.Text = null;
            txtNominal.Text = null;
            txtTahun.Text = null;
        }

        private void rand()
        {
            util.koneksi.Close();
            string col = "id_spp", table = "spp";
            txtIdSpp.Text = util.dataRand(col, table);
        }

        private void tampilData()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM spp", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgvSpp.DataSource = dataSet.Tables[0];
        }

        public DataSpp()
        {
            InitializeComponent();
            rand();
            tampilData();
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

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            DataKelas kelas = new DataKelas();
            kelas.Show();
            this.Hide();
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            DataPetugas petugas = new DataPetugas();
            petugas.Show();
            this.Hide();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            tampilData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtNominal.Text) || string.IsNullOrEmpty(txtTahun.Text))
                {
                    MessageBox.Show("Isi data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("INSERT INTO spp VALUES ('"+txtIdSpp.Text+"', '"+txtTahun.Text+"', '"+txtNominal.Text+"')", util.koneksi);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtNominal.Text) || string.IsNullOrEmpty(txtTahun.Text))
                {
                    MessageBox.Show("Isi data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin untuk mengubah data tersebut?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        util.koneksi.Open();
                        util.cmd = new SqlCommand("UPDATE spp SET tahun = '"+txtTahun.Text+"', nominal = '"+txtNominal.Text+"' WHERE id_spp = '"+txtIdSpp.Text+"'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diubah!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilData();
                        clear();
                        rand();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtNominal.Text) || string.IsNullOrEmpty(txtTahun.Text))
                {
                    MessageBox.Show("Isi data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin untuk menghapus data tersebut?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        util.koneksi.Open();
                        util.cmd = new SqlCommand("DELETE FROM spp WHERE id_spp = '"+txtIdSpp.Text+"'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil menghapus data!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilData();
                        clear();
                        rand();
                        util.koneksi.Close();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }

        private void dgvSpp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvSpp.CurrentCell.RowIndex;

            txtIdSpp.Text = dgvSpp.Rows[baris].Cells[0].Value.ToString();
            txtTahun.Text = dgvSpp.Rows[baris].Cells[1].Value.ToString();
            txtNominal.Text = dgvSpp.Rows[baris].Cells[2].Value.ToString();
        }
    }
}
