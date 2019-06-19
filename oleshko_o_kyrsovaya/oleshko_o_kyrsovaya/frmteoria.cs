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
    public partial class frmteoria : Form
    {
        public frmteoria()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmvhod z = new frmvhod();
            z.Show();
            this.Hide();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string s = treeView1.SelectedNode.Name;
            richTextBox1.LoadFile("instr\\" + s + ".rtf");
        }
    }
}
