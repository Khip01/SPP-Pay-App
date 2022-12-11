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
    public partial class DataSiswa : Form
    {
        Utils util = new Utils();

        private void methSpp(string spp)
        {
            this.txtIdSpp.Text = spp;
        }

        private void methKelas(string kelas)
        {
            this.txtIdKelas.Text = kelas;
        }

        private void create()
        {
            txtNisn.Text = null;
            txtNama.Text = null;
            txtAlamat.Text = null;
            txtNoTelp.Text = null;
            txtNis.Text = null;
            txtIdKelas.Text = null;
            txtIdSpp.Text = null;

            txtNisn.Enabled = true;
        }

        private void tampilData()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM siswa", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgvSiswa.DataSource = dataSet.Tables[0];
        }

        public DataSiswa()
        {
            InitializeComponent();
            tampilData();/*
            tampilDataKelas();
            tampilDataSpp();*/
            util.tampilDataCustom(util.table = "kelas", dgvKelas);
            util.tampilDataCustom(util.table = "spp", dgvSpp);
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


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ALogin login = new ALogin();
            /*this.Hide();*/
            login.Show();
        }

        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            TransaksiPembayaran transaksi = new TransaksiPembayaran();
            this.Hide();
            transaksi.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to close this application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
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

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            DataPetugas petugas = new DataPetugas();
            petugas.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tampilData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNisn.Text) || string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtAlamat.Text) || string.IsNullOrEmpty(txtNoTelp.Text) || string.IsNullOrEmpty(txtNis.Text) || string.IsNullOrEmpty(txtIdKelas.Text) || string.IsNullOrEmpty(txtIdSpp.Text))
                {
                    MessageBox.Show("Isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    util.koneksi.Open();
                    util.cmd = new SqlCommand("INSERT INTO siswa VALUES ('"+txtNisn.Text+"', '"+txtNis.Text+"', '"+txtNama.Text+"', '"+txtIdKelas.Text+"', '"+txtAlamat.Text+"', '"+txtNoTelp.Text+"', '"+txtIdSpp.Text+"')", util.koneksi);
                    util.cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil ditambahkan!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilData();
                    create();
                    util.koneksi.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan "+err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.koneksi.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            create();
            txtNisn.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNisn.Text) || string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtAlamat.Text) || string.IsNullOrEmpty(txtNoTelp.Text) || string.IsNullOrEmpty(txtNis.Text) || string.IsNullOrEmpty(txtIdKelas.Text) || string.IsNullOrEmpty(txtIdSpp.Text))
                {
                    MessageBox.Show("Isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin ingin mengubah data tersebut?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        util.koneksi.Open();
                        util.cmd = new SqlCommand("UPDATE siswa SET nis = '" + txtNis.Text + "', nama = '" + txtNama.Text + "', id_kelas = '" + txtIdKelas.Text + "', alamat = '" + txtAlamat.Text + "', no_telp = '" + txtNoTelp.Text + "', id_spp = '" + txtIdSpp.Text + "' WHERE nisn = '"+txtNisn.Text+"'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diubah!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilData();
                        create();
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
                if (string.IsNullOrEmpty(txtNisn.Text) || string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtAlamat.Text) || string.IsNullOrEmpty(txtNoTelp.Text) || string.IsNullOrEmpty(txtNis.Text) || string.IsNullOrEmpty(txtIdKelas.Text) || string.IsNullOrEmpty(txtIdSpp.Text))
                {
                    MessageBox.Show("Isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin ingin menghapus data tersebut?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        util.koneksi.Open();
                        util.cmd = new SqlCommand("DELETE FROM siswa WHERE nisn = '" + txtNisn.Text + "'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilData();
                        create();
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

        private void dgvSiswa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvSiswa.CurrentCell.RowIndex;

            txtNisn.Text = dgvSiswa.Rows[baris].Cells[0].Value.ToString();
            txtNis.Text = dgvSiswa.Rows[baris].Cells[1].Value.ToString();
            txtNama.Text = dgvSiswa.Rows[baris].Cells[2].Value.ToString();
            txtIdKelas.Text = dgvSiswa.Rows[baris].Cells[3].Value.ToString();
            txtAlamat.Text = dgvSiswa.Rows[baris].Cells[4].Value.ToString();
            txtNoTelp.Text = dgvSiswa.Rows[baris].Cells[5].Value.ToString();
            txtIdSpp.Text = dgvSiswa.Rows[baris].Cells[6].Value.ToString();

            txtNisn.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowDataKelas.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowDataKelas.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            methKelas(txtIdKelasShow.Text);
            ShowDataKelas.Visible = false;
        }

        private void dgvKelas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvKelas.CurrentCell.RowIndex;

            txtIdKelasShow.Text = dgvKelas.Rows[baris].Cells[0].Value.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ShowDataSpp.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            methSpp(txtIdSppShow.Text);
            ShowDataSpp.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowDataSpp.Visible = true;
        }

        private void dgvSpp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvSpp.CurrentCell.RowIndex;

            txtIdSppShow.Text = dgvSpp.Rows[baris].Cells[0].Value.ToString();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
