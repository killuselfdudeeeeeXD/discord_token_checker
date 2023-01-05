using System;
using System.Windows.Forms;

namespace discordtokenchecker
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Class1.save_good_path = openFileDialog.FileName;
                    textBox1.Text = openFileDialog.FileName;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Class1.save_bad_path = openFileDialog.FileName;
                    textBox2.Text = openFileDialog.FileName;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Class1.save_payments_path = openFileDialog.FileName;
                    textBox3.Text = openFileDialog.FileName;
                }

            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = Class1.save_good_path;
            textBox2.Text = Class1.save_bad_path;
            textBox3.Text = Class1.save_payments_path;
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
