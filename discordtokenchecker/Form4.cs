using System;
using System.Windows.Forms;

namespace discordtokenchecker
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1.banned_countries = textBox1.Text.Split(',');
            Class1.country_text = textBox1.Text;
            Class1.ban = checkBox1.Checked;

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = Class1.country_text;
            checkBox1.Checked = Class1.ban;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Visible = true;
                return;
            }
            groupBox1.Visible = false;
        }
    }
}
