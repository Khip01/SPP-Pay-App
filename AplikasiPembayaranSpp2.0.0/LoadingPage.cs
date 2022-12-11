using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AplikasiPembayaranSpp2._0._0
{
    public partial class LoadingPage : Form
    {

        public LoadingPage()
        {
            InitializeComponent();
        }

        private void LoadingPage_Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }

        int i = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 2)
            {
                timer1.Stop();
                BLogin login = new BLogin();
                login.Show();
                this.Hide();
            }
            this.i++;
            label5.Text = "Ready.";
            label6.Text = "Done!";
            panel4.Width = 405;
            timer1.Interval = 2000;
        }
    }
}
