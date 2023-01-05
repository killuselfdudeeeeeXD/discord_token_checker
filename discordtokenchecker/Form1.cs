using System;
using System.Windows.Forms;
namespace discordtokenchecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form6 f6 = new Form6();
        private void Form1_Load(object sender, EventArgs e)
        {
            f6.TopLevel = false;
            this.Controls.Add(f6);
            f6.Show();
        }



    }
}
