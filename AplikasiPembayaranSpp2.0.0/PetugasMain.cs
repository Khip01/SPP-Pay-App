using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AplikasiPembayaranSpp2._0._0
{
    public partial class PetugasMain : Form
    {
        Utils util = new Utils();

/* USER DEFINED FUNCTION */

        private void setButton(bool condition)
        {
            if (condition == true)
            {
                button15.BackColor = Color.YellowGreen;
                button15.Text = "Cari";
                button15.Image = Properties.Resources.search1;
                tampilDataHistory();
                cbBulanDibayarHistory.SelectedItem = "Semua Bulan";
                cbNama.SelectedItem = "Semua Nama";
            }
            else
            {
                button15.BackColor = Color.IndianRed;
                button15.Text = "Batal";
                button15.Image = Properties.Resources.Close32;
            }
        }

        public void clear()
        {
            txtNama.Text = null;
            txtIdSpp.Text = null;
            txtIdPetugas.Text = null;
            txtIdPembayaran.Text = null;
            txtNominal.Text = null;
            cbBulanDibayarEntri.SelectedItem = "Januari";
            cbTahunDibayar.SelectedItem = "2000";
        }

        private void tampilCbNama()
        {
            util.koneksi.Open();
            util.cmd = new SqlCommand("SELECT DISTINCT nama FROM pembayaran INNER JOIN siswa ON pembayaran.nisn = siswa.nisn", util.koneksi);
            SqlDataReader dataReader;

            dataReader = util.cmd.ExecuteReader();

            while (dataReader.Read())
            {
                cbNama.Items.Add(dataReader.GetString("nama"));
            }

            util.koneksi.Close();
        }

        string query = "SELECT id_pembayaran, pembayaran.id_petugas, nama_petugas, pembayaran.nisn, nama, tgl_bayar, bulan_dibayar, tahun_dibayar, pembayaran.id_spp, jumlah_bayar FROM pembayaran INNER JOIN siswa ON pembayaran.nisn = siswa.nisn INNER JOIN petugas ON pembayaran.id_petugas = petugas.id_petugas";
        private void tampilDataHistory()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            dgvHistoryPembayaran.DataSource = dataSet.Tables[0];
        }

        private void idPembayaranRand()
        {
            util.koneksi.Close();
            string col = "id_pembayaran";
            string tabel = "pembayaran";
            txtIdPembayaran.Text = util.dataRand(col, tabel);
        }


        private void tampilDataEntri()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM pembayaran", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            dgvEntri.DataSource = dataSet.Tables[0];
        }

/* END */

/* MAIN FUNC */

        public PetugasMain()
        {
            InitializeComponent();
            tampilDataEntri();
            tampilDataHistory();
            tampilCbNama();
            idPembayaranRand();

            /* Tampil Data dgvIdSpp */
            string tabelIdSpp = "siswa";
            util.tampilDataCustom(tabelIdSpp, dgvIdSpp);

            /* Tampil Data dgvIdSpp */
            string tabelIdPetugas = "petugas";
            util.tampilDataCustom(tabelIdPetugas, dgvIdPetugas);
        }

/* END */

/* DESIGNER */

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to login using another account? \n\n*This session will automatically log you out of the previous account", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                ALogin login = new ALogin();
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
            idPembayaranRand();

            panel2.Top = button1.Top;
            panel2.Height = button1.Height;
            label1.Text = "Entri Transaksi";
            EntriTransaksi.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Top = button2.Top;
            panel2.Height = button2.Height;
            label1.Text = "History Pembayaran";
            HistoryPembayaran.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            About tentang = new About();
            tentang.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Entri1.BringToFront();
            label2.Text = "1 / 2";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Entri2.BringToFront();
            label2.Text = "2 / 2";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clear();
            idPembayaranRand();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            IdSppShow.Visible = true;
            IdSppShow.Location = new Point(197, 56);
            IdSppShow.BringToFront();

            IdPetugasShow.Hide();
            IdPetugasShow.SendToBack();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            IdSppShow.Hide();
            IdSppShow.SendToBack();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            IdPetugasShow.Visible = true;
            IdPetugasShow.Location = new Point(197, 56);
            IdPetugasShow.BringToFront();

            IdSppShow.Hide();
            IdSppShow.SendToBack();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            IdPetugasShow.Hide();
            IdPetugasShow.SendToBack();
        }

        private void dgvIdSpp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvIdSpp.CurrentCell.RowIndex;

            txtIdSppShow.Text = dgvIdSpp.Rows[baris].Cells[6].Value.ToString();
            txtNamaShow.Text = dgvIdSpp.Rows[baris].Cells[2].Value.ToString();

            string id_spp = dgvIdSpp.Rows[baris].Cells[6].Value.ToString();
            util.koneksi.Open();
            util.cmd = new SqlCommand("SELECT nominal FROM spp WHERE id_spp = '"+id_spp+"'", util.koneksi);
            string nominal = util.cmd.ExecuteScalar().ToString();
            txtNominalShow.Text = nominal;
            util.koneksi.Close();
        }

        private void dgvIdPetugas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvIdPetugas.CurrentCell.RowIndex;

            txtIdPetugasShow.Text = dgvIdPetugas.Rows[baris].Cells[0].Value.ToString();
            txtNamaPetugasShow.Text = dgvIdPetugas.Rows[baris].Cells[3].Value.ToString();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdPetugasShow.Text) || string.IsNullOrEmpty(txtNamaPetugasShow.Text))
            {
                MessageBox.Show("Kesalahan, isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtIdPetugas.Text = txtIdPetugasShow.Text;

                IdPetugasShow.Hide();
                IdPetugasShow.SendToBack();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdSppShow.Text) || string.IsNullOrEmpty(txtNamaShow.Text) || string.IsNullOrEmpty(txtNominalShow.Text))
            {
                MessageBox.Show("Kesalahan, isi semua data dengan benar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtIdSpp.Text = txtIdSppShow.Text;
                txtNama.Text = txtNamaShow.Text;
                txtNominal.Text = txtNominalShow.Text;

                IdSppShow.Hide();
                IdSppShow.SendToBack();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtIdPetugas.Text) || string.IsNullOrEmpty(txtIdPembayaran.Text) || string.IsNullOrEmpty(txtNominal.Text))
                {
                    MessageBox.Show("Kesalahan, isi semua data dengan benar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    util.koneksi.Open();
                    /* MENGAMBIL DATA NISN DARI SISWA BERDASARKAN NAMA */
                    util.cmd = new SqlCommand("SELECT nisn FROM siswa WHERE nama = '" + txtNama.Text + "'", util.koneksi);
                    string nisn = util.cmd.ExecuteScalar().ToString();
                    /* MENGECEK APAKAH BULAN DIBAYAR SUDAH PERNAH DIBAYAR ATAU BELUM */
                    util.cmd = new SqlCommand("SELECT COUNT(*) FROM pembayaran WHERE bulan_dibayar = '"+cbBulanDibayarEntri.Text+"' AND tahun_dibayar = '"+cbTahunDibayar.Text+"'", util.koneksi);
                    int result = (int)util.cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        MessageBox.Show("Siswa atas nama "+txtNama.Text+" sudah pernah membayar di bulan "+cbBulanDibayarEntri.Text+" tahun "+cbTahunDibayar.Text+"!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        util.koneksi.Close();
                    }
                    else
                    {
                        util.cmd = new SqlCommand("INSERT INTO pembayaran VALUES ('" + txtIdPembayaran.Text + "', '" + txtIdPetugas.Text + "', '" + nisn + "', '" + dtpTglBayar.Value.Date.ToString("yyyyMMdd") + "', '" + cbBulanDibayarEntri.Text + "', '" + cbTahunDibayar.Text + "', '" + txtIdSpp.Text + "', '" + txtNominal.Text + "')", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil ditambahkan!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilDataEntri();
                        clear();
                        idPembayaranRand();
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

        private void button11_Click(object sender, EventArgs e)
        {
            tampilDataEntri();
        }

        private void dgvEntri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvEntri.CurrentCell.RowIndex;

            txtIdPembayaran.Text = dgvEntri.Rows[baris].Cells[0].Value.ToString();
            txtIdPetugas.Text = dgvEntri.Rows[baris].Cells[1].Value.ToString();

            /* MENCARI NAMA DENGAN NISN */
            util.koneksi.Open();
            util.cmd = new SqlCommand("SELECT nama FROM siswa WHERE nisn = '"+dgvEntri.Rows[baris].Cells[2].Value.ToString()+"'", util.koneksi);
            string nama = util.cmd.ExecuteScalar().ToString();
            txtNama.Text = nama;
            util.koneksi.Close();
            /* END */

            /* MENGUBAH STRING MENJADI DATE */
            DateTime date = DateTime.Parse(dgvEntri.Rows[baris].Cells[3].Value.ToString());
            dtpTglBayar.Value = date;
            /* END */

            cbBulanDibayarEntri.SelectedItem = dgvEntri.Rows[baris].Cells[4].Value.ToString();
            cbTahunDibayar.SelectedItem = dgvEntri.Rows[baris].Cells[5].Value.ToString();
            txtIdSpp.Text = dgvEntri.Rows[baris].Cells[6].Value.ToString();
            txtNominal.Text = dgvEntri.Rows[baris].Cells[7].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtIdPetugas.Text) || string.IsNullOrEmpty(txtIdPembayaran.Text) || string.IsNullOrEmpty(txtNominal.Text))
                {
                    MessageBox.Show("Kesalahan, isi semua data dengan benar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    util.koneksi.Open();
                    /* MENGAMBIL DATA NISN DARI SISWA BERDASARKAN NAMA */
                    util.cmd = new SqlCommand("SELECT nisn FROM siswa WHERE nama = '" + txtNama.Text + "'", util.koneksi);
                    string nisn = util.cmd.ExecuteScalar().ToString();
                    /* MENGECEK APAKAH BULAN DIBAYAR SUDAH PERNAH DIBAYAR ATAU BELUM */
                    util.cmd = new SqlCommand("SELECT COUNT(*) FROM pembayaran WHERE bulan_dibayar = '" + cbBulanDibayarEntri.Text + "' AND tahun_dibayar = '" + cbTahunDibayar.Text + "'", util.koneksi);
                    int result = (int)util.cmd.ExecuteScalar();
                    int baris = dgvEntri.CurrentCell.RowIndex;
                    if (result > 0 && dgvEntri.Rows[baris].Cells[4].Value.ToString() != cbBulanDibayarEntri.Text)
                    {
                        MessageBox.Show("Siswa atas nama " + txtNama.Text + " sudah pernah membayar di bulan " + cbBulanDibayarEntri.Text + " tahun " + cbTahunDibayar.Text + "!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        util.koneksi.Close();
                    }
                    else
                    {
                        util.cmd = new SqlCommand("UPDATE pembayaran SET id_petugas = '" + txtIdPetugas.Text + "', nisn = '" + nisn + "', tgl_bayar = '" + dtpTglBayar.Value.Date.ToString("yyyyMMdd") + "', bulan_dibayar = '" + cbBulanDibayarEntri.Text + "', tahun_dibayar = '" + cbTahunDibayar.Text + "', id_spp = '" + txtIdSpp.Text + "', jumlah_bayar = '" + txtNominal.Text + "' WHERE id_pembayaran = '" + txtIdPembayaran.Text + "'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diedit!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilDataEntri();
                        clear();
                        idPembayaranRand();
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

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                util.koneksi.Open();
                DialogResult resul = MessageBox.Show("Apakah anda yakin untuk menghapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resul == DialogResult.Yes)
                {
                    util.cmd = new SqlCommand("DELETE FROM pembayaran WHERE id_pembayaran = '"+txtIdPembayaran.Text+"'", util.koneksi);
                    util.cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil dihapus!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilDataEntri();
                    clear();
                    idPembayaranRand();
                    util.koneksi.Close();
                }
                util.koneksi.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan "+err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                util.koneksi.Close();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter dataAdapter;
                DataSet dataSet = new DataSet();

                if (button15.Text == "Batal" || (cbBulanDibayarHistory.Text == "Semua Bulan" && cbNama.Text == "Semua Nama"))
                {
                    setButton(true);
                }
                else if (cbNama.Text == "Semua Nama")
                {
                    dataAdapter = new SqlDataAdapter(query+" WHERE bulan_dibayar = '" + cbBulanDibayarHistory.Text + "'", util.koneksi);
                    dataAdapter.Fill(dataSet);
                    dgvHistoryPembayaran.DataSource = dataSet.Tables[0];
                    setButton(false);
                }
                else if (cbBulanDibayarHistory.Text == "Semua Bulan")
                {
                    dataAdapter = new SqlDataAdapter(query+" WHERE nama = '" + cbNama.Text + "'", util.koneksi);
                    dataAdapter.Fill(dataSet);
                    dgvHistoryPembayaran.DataSource = dataSet.Tables[0];
                    setButton(false);
                }
                else
                {
                    dataAdapter = new SqlDataAdapter(query+" WHERE nama = '" + cbNama.Text + "' AND bulan_dibayar = '" + cbBulanDibayarHistory.Text + "'", util.koneksi);
                    dataAdapter.Fill(dataSet);
                    dgvHistoryPembayaran.DataSource = dataSet.Tables[0];
                    setButton(false);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                util.koneksi.Close();
            }
        }

        /* END */

    }
}
