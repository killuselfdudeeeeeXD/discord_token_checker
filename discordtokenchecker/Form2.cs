using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;
namespace discordtokenchecker
{
    public partial class Form2 : Form
    {

        ChromiumWebBrowser browser;

        public string name = "";


        public Form2()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public string token_ds = "";

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = name;
            try
            {

                browser = new ChromiumWebBrowser();

                browser.Load("discord.com/login");

                bool complete = false;

                browser.LoadingStateChanged += async (obj, args) =>
                {
                    if (args.IsLoading == false && complete == false)
                    {
                        {
                            await browser.EvaluateScriptAsync("function login(token){setInterval(()=>{document.body.appendChild(document.createElement`iframe`).contentWindow.localStorage.token=`\"${ token}\"`},50);setTimeout(()=>{location.reload()},2500)}login(" + $"`{token_ds}`" + ")");
                            complete = true;
                        }
                    }
                };
                browser.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(browser);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", "Страница не может быть загружена!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
