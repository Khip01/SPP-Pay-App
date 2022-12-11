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
    public partial class DataKelas : Form
    {
        Utils util = new Utils();

        private void rand()
        {
            util.koneksi.Close();
            String col = "id_kelas", table = "kelas";
            txtIdKelas.Text = util.dataRand(col, table);
        }

        private void clear()
        {
            txtIdKelas.Text = null;
            txtNamaKelas.Text = null;
            txtKompetensiKeahlian.Text = null; 
        }

        private void tampilData()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM kelas", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgvKelas.DataSource = dataSet.Tables[0];
        }

        public DataKelas()
        {
            InitializeComponent();
            tampilData();
            clear();
            rand();
        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            DataPetugas petugas = new DataPetugas();
            petugas.Show();
            this.Hide();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            DataSiswa siswa = new DataSiswa();
            siswa.Show();
            this.Show();
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
                if (string.IsNullOrEmpty(txtIdKelas.Text) || string.IsNullOrEmpty(txtKompetensiKeahlian.Text) || string.IsNullOrEmpty(txtNamaKelas.Text))
                {
                    MessageBox.Show("Isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("INSERT INTO kelas VALUES ('"+txtIdKelas.Text+"', '"+txtNamaKelas.Text+"', '"+txtKompetensiKeahlian.Text+"')",util.koneksi);
                    util.cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diinput!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    rand();
                    tampilData();
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
                if (string.IsNullOrEmpty(txtIdKelas.Text) || string.IsNullOrEmpty(txtKompetensiKeahlian.Text) || string.IsNullOrEmpty(txtNamaKelas.Text))
                {
                    MessageBox.Show("Isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin untuk mengubah data tersebut?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        util.koneksi.Open();
                        util.cmd = new SqlCommand("UPDATE kelas SET nama_kelas = '"+txtNamaKelas.Text+"', kompetensi_keahlian = '"+txtKompetensiKeahlian.Text+"' WHERE id_kelas = '"+txtIdKelas.Text+"'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil mengubah data!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                        rand();
                        tampilData();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah anda yakin untu menghapus data tersebut?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("DELETE FROM kelas WHERE id_kelas = '"+txtIdKelas.Text+"'", util.koneksi);
                    MessageBox.Show("Berhasil menghapus data!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    rand();
                    tampilData();
                    util.koneksi.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tampilData();
        }

        private void dgvKelas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvKelas.CurrentCell.RowIndex;

            txtIdKelas.Text = dgvKelas.Rows[baris].Cells[0].Value.ToString();
            txtNamaKelas.Text = dgvKelas.Rows[baris].Cells[1].Value.ToString();
            txtKompetensiKeahlian.Text = dgvKelas.Rows[baris].Cells[2].Value.ToString();
        }
    }
}
