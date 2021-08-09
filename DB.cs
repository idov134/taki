using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace taki
{
    public partial class DB : Form
    {
        public Form1 fr1 = new Form1();
        int turns;
        public DB(int turns)
        {
            InitializeComponent();
            this.turns = turns;
        }

        private void DB_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB1DataSet.scores' table. You can move, or remove it, as needed.
            this.scoresTableAdapter.Fill(this.dB1DataSet.scores);
            try
            {
                this.scoresTableAdapter.FillBy2(this.dB1DataSet.scores);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.fr1.Show();
            this.Close();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.scoresTableAdapter.FillBy(this.dB1DataSet.scores);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            if (username != "" && this.turns > 0)
            {
                this.scoresTableAdapter.Insert(username, this.turns);
                this.scoresTableAdapter.Fill(this.dB1DataSet.scores);
                this.scoresTableAdapter.Update(this.dB1DataSet);
                try
                {
                    this.scoresTableAdapter.FillBy2(this.dB1DataSet.scores);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.scoresTableAdapter.FillBy1(this.dB1DataSet.scores);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.scoresTableAdapter.FillBy2(this.dB1DataSet.scores);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
