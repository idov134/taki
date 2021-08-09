using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace taki
{
    public partial class instructions : Form
    {
        public Form1 fr1 = new Form1();
        public instructions()
        {
            InitializeComponent();
        }

        private void instructions_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fr1.Show();
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            fr1.Show();
        }
    }
}
