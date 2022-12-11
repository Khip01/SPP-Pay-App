using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AplikasiPembayaranSpp2._0._0
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo
            {
                FileName = "https://github.com/Khip01",
                UseShellExecute = true
            };
            Process.Start(sInfo);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
