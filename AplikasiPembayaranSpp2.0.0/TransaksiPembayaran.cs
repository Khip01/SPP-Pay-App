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
    public partial class TransaksiPembayaran : Form
    {

/* USER  DEFINITION FUNCTION */
        Utils util = new Utils();

        private void clear()
        {
            txtIdPetugas.Text = null;
            dtpTglBayar.Text = null;
            txtIdSpp.Text = null;
            cbBulanDibayar.SelectedItem = "Januari";
            cbTahunDibayar.SelectedItem = "2000";
            txtIdSpp.Text = null;
            txtJumlahBayar.Text = null;
            txtNama.Text = null;
        }

        private void rand()
        {
            util.koneksi.Close();
            string col = "id_pembayaran", tabel = "pembayaran";
            txtIdPembayaran.Text = util.dataRand(col, tabel);
        }

        private void tampilDataHistory()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select id_pembayaran, id_petugas, pembayaran.nisn, nama, tgl_bayar, bulan_dibayar, tahun_dibayar, pembayaran.id_spp, jumlah_bayar from pembayaran inner join siswa on pembayaran.nisn = siswa.nisn", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgvHistory.DataSource = dataSet.Tables[0];
        }

        private void tampilDataEntri()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select id_pembayaran, id_petugas, nama, tgl_bayar, bulan_dibayar, tahun_dibayar, pembayaran.id_spp, jumlah_bayar from pembayaran inner join siswa on pembayaran.nisn = siswa.nisn", util.koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgvEntri.DataSource = dataSet.Tables[0];
        }
/* END */

/* MAIN FUNC */
        public TransaksiPembayaran()
        {
            InitializeComponent();
            tampilDataHistory();
            tampilDataEntri();
            rand();

            /* Tampil Data dgvIdSpp */
            string tabelIdSpp = "siswa";
            util.tampilDataCustom(tabelIdSpp, dgvIdSpp);

            /* Tampil Data dgvIdSpp */
            string tabelIdPetugas = "petugas";
            util.tampilDataCustom(tabelIdPetugas, dgvIdPetugas);
        }
/* END */

/* DESIGNER */
        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            switch (panel8.Visible)
            {
                case true:
                    panel8.Visible = false;
                    panel9.Visible = false;
                    break;
                case false:
                    panel8.Visible = true;
                    panel9.Visible = true;
                    break;
            }
        }

        private void panel8_MouseClick_1(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
            panel8.BackColor = Color.SteelBlue;
            panel9.BackColor = Color.LightBlue;
        }

        private void panel9_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedTab =  tabControl1.TabPages["tabPage2"];
            panel8.BackColor = Color.LightBlue;
            panel9.BackColor = Color.SteelBlue;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            DataSiswa siswa = new DataSiswa();
            siswa.Show();
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

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            DataPetugas petugas = new DataPetugas();
            petugas.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clear();
            rand();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tampilDataEntri();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtIdPetugas.Text) || string.IsNullOrEmpty(txtIdPembayaran.Text) || string.IsNullOrEmpty(txtJumlahBayar.Text))
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
                    util.cmd = new SqlCommand("SELECT COUNT(*) FROM pembayaran WHERE bulan_dibayar = '" + cbBulanDibayar.Text + "' AND tahun_dibayar = '" + cbTahunDibayar.Text + "'", util.koneksi);
                    int result = (int)util.cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        MessageBox.Show("Siswa atas nama " + txtNama.Text + " sudah pernah membayar di bulan " + cbBulanDibayar.Text + " tahun " + cbTahunDibayar.Text + "!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        util.koneksi.Close();
                    }
                    else
                    {
                        util.cmd = new SqlCommand("INSERT INTO pembayaran VALUES ('" + txtIdPembayaran.Text + "', '" + txtIdPetugas.Text + "', '" + nisn + "', '" + dtpTglBayar.Value.Date.ToString("yyyyMMdd") + "', '" + cbBulanDibayar.Text + "', '" + cbTahunDibayar.Text + "', '" + txtIdSpp.Text + "', '" + txtJumlahBayar.Text + "')", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil ditambahkan!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilDataEntri();
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

        private void button10_Click(object sender, EventArgs e)
        {
            IdPetugasShow.Visible = true;
            IdPetugasShow.Location = new Point(197, 56);
            IdPetugasShow.BringToFront();

            IdSppShow.Hide();
            IdSppShow.SendToBack();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IdSppShow.Visible = true;
            IdSppShow.Location = new Point(197, 56);
            IdSppShow.BringToFront();

            IdPetugasShow.Hide();
            IdPetugasShow.SendToBack();
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

        private void button18_Click(object sender, EventArgs e)
        {
            IdPetugasShow.Hide();
            IdPetugasShow.SendToBack();
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
                txtJumlahBayar.Text = txtNominalShow.Text;

                IdSppShow.Hide();
                IdSppShow.SendToBack();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            IdSppShow.Hide();
            IdSppShow.SendToBack();
        }

        private void dgvIdPetugas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvIdPetugas.CurrentCell.RowIndex;

            txtIdPetugasShow.Text = dgvIdPetugas.Rows[baris].Cells[0].Value.ToString();
            txtNamaPetugasShow.Text = dgvIdPetugas.Rows[baris].Cells[3].Value.ToString();
        }

        private void dgvIdSpp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvIdSpp.CurrentCell.RowIndex;

            txtIdSppShow.Text = dgvIdSpp.Rows[baris].Cells[6].Value.ToString();
            txtNamaShow.Text = dgvIdSpp.Rows[baris].Cells[2].Value.ToString();

            string id_spp = dgvIdSpp.Rows[baris].Cells[6].Value.ToString();
            util.koneksi.Open();
            util.cmd = new SqlCommand("SELECT nominal FROM spp WHERE id_spp = '" + id_spp + "'", util.koneksi);
            string nominal = util.cmd.ExecuteScalar().ToString();
            txtNominalShow.Text = nominal;
            util.koneksi.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtIdSpp.Text) || string.IsNullOrEmpty(txtIdPetugas.Text) || string.IsNullOrEmpty(txtIdPembayaran.Text) || string.IsNullOrEmpty(txtJumlahBayar.Text))
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
                    util.cmd = new SqlCommand("SELECT COUNT(*) FROM pembayaran WHERE bulan_dibayar = '" + cbBulanDibayar.Text + "' AND tahun_dibayar = '" + cbTahunDibayar.Text + "'", util.koneksi);
                    int result = (int)util.cmd.ExecuteScalar();
                    int baris = dgvEntri.CurrentCell.RowIndex;
                    if (result > 0 && dgvEntri.Rows[baris].Cells[4].Value.ToString() != cbBulanDibayar.Text)
                    {
                        MessageBox.Show("Siswa atas nama " + txtNama.Text + " sudah pernah membayar di bulan " + cbBulanDibayar.Text + " tahun " + cbTahunDibayar.Text + "!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        util.koneksi.Close();
                    }
                    else
                    {
                        util.cmd = new SqlCommand("UPDATE pembayaran SET id_petugas = '" + txtIdPetugas.Text + "', nisn = '" + nisn + "', tgl_bayar = '" + dtpTglBayar.Value.Date.ToString("yyyyMMdd") + "', bulan_dibayar = '" + cbBulanDibayar.Text + "', tahun_dibayar = '" + cbTahunDibayar.Text + "', id_spp = '" + txtIdSpp.Text + "', jumlah_bayar = '" + txtJumlahBayar.Text + "' WHERE id_pembayaran = '" + txtIdPembayaran.Text + "'", util.koneksi);
                        util.cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diedit!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilDataEntri();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                util.koneksi.Open();
                DialogResult resul = MessageBox.Show("Apakah anda yakin untuk menghapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resul == DialogResult.Yes)
                {
                    util.cmd = new SqlCommand("DELETE FROM pembayaran WHERE id_pembayaran = '" + txtIdPembayaran.Text + "'", util.koneksi);
                    util.cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil dihapus!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilDataEntri();
                    clear();
                    rand();
                    util.koneksi.Close();
                }
                util.koneksi.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Kesalahan " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                util.koneksi.Close();
            }
        }

        private void dgvEntri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvEntri.CurrentCell.RowIndex;

            txtIdPembayaran.Text = dgvEntri.Rows[baris].Cells[0].Value.ToString();
            txtIdPetugas.Text = dgvEntri.Rows[baris].Cells[1].Value.ToString();
            txtNama.Text = dgvEntri.Rows[baris].Cells[2].Value.ToString();

            /* MENGUBAH STRING MENJADI DATE */
            DateTime date = DateTime.Parse(dgvEntri.Rows[baris].Cells[3].Value.ToString());
            dtpTglBayar.Value = date;
            /* END */

            cbBulanDibayar.SelectedItem = dgvEntri.Rows[baris].Cells[4].Value.ToString();
            cbTahunDibayar.SelectedItem = dgvEntri.Rows[baris].Cells[5].Value.ToString();
            txtIdSpp.Text = dgvEntri.Rows[baris].Cells[6].Value.ToString();
            txtJumlahBayar.Text = dgvEntri.Rows[baris].Cells[7].Value.ToString();
        }
        /* END */

    }
}
