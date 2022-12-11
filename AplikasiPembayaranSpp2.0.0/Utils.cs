using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AplikasiPembayaranSpp2._0._0
{
    class Utils
    {
/*FOR KONEKSI*/
        public SqlConnection koneksi = new SqlConnection(@"Data Source=Khip01;Initial Catalog=db_spp;Integrated Security=True");
        public SqlCommand cmd;
        public string data;

/*FOR TEXT RANDOM*/
        public string dataRand(string col, string table)
        {
            Random rand = new Random();
            bool create = true;
            
            while (create)
            {
                koneksi.Open();
                int random = rand.Next(100, 1000);
                string randString = random.ToString();
                cmd = new SqlCommand("SELECT COUNT("+col+") FROM "+table+" WHERE "+col+" = "+randString,koneksi);
                int result = (int)cmd.ExecuteScalar();

                if (result == 0)
                {
                    koneksi.Close();
                    return randString;
                }
                koneksi.Close();
            }
            koneksi.Close();
            return null;
        }

/*FOR TAMPIL DATA SMALL BOX*/
        public string table;

        public void tampilDataCustom(String table, DataGridView dgv)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM "+table, koneksi);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgv.DataSource = dataSet.Tables[0];
        }
    }
}
