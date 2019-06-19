using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oleshko_o_kyrsovaya
{
    public partial class frmvhod : Form
    {
        public frmvhod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmpereb z = new frmpereb();
            z.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmokremvipad z = new frmokremvipad();
            z.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmteoria z = new frmteoria();
            z.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmpriklad z = new frmpriklad();
            z.Show();
            this.Hide();
        }
    }
}
