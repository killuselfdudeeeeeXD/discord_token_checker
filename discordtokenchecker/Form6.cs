using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace discordtokenchecker
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        string[] tokens = null;


        async Task check_token(string token)
        {
            try
            {



                dynamic d;

                string result = "";

                bool payment_result = false;

                bool nitro = false;

                string payments = "";

                bool gifts = false;

                string gifts_str = "";


                using (var client = new HttpClient())
                {



                    var url = "https://discord.com/api/v9/users/@me";
                    client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", token);
                    result = await client.GetStringAsync(url);

                }
                d = JsonConvert.DeserializeObject(result);




                using (var client = new HttpClient())
                {
                    var url = "https://discord.com/api/v9/users/@me/billing/payment-sources";
                    client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", token);

                    if (await client.GetStringAsync(url) != "[]")
                    {
                        payment_result = true;
                        payments = await client.GetStringAsync(url);
                    }


                }

                string locale = d["locale"];


                if (Class1.ban == true && Class1.banned_countries.Contains(locale))
                {
                    return;
                }


                using (var client = new HttpClient())
                {
                    var url = "https://discord.com/api/v9/users/@me/entitlements/gifts";
                    client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", token);

                    if (await client.GetStringAsync(url) != "[]")
                    {
                        gifts = true;
                        gifts_str = await client.GetStringAsync(url);
                    }
                }





                if (d["premium_type"] > 0)
                {
                    nitro = true;
                }


                dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Add("GOOD", $"{d["username"]}#{d["discriminator"]}", $"{token}", d["locale"], d["verified"], d["email"], d["phone"], payment_result, nitro, gifts)));



                if (payment_result != false && Class1.save_payments_path != "")
                {
                    StreamWriter sw = new StreamWriter($"{Class1.save_payments_path}", true);
                    sw.WriteLine($"\r\n\r\n[GOOD] TOKEN: {token}\r\nUSERNAME: {d["username"]}\r\nTAG: {d["discriminator"]}\r\nBIO: {d["bio"]}\r\nCOUNTRY:{d["locale"]}\r\nEMAL: {d["email"]}\r\nVERIFED_STATUS: {d["verified"]}\r\nPHONE: {d["phone"]}\r\nPAYMENTS:\r\n{payments}\r\n========================");
                    sw.Close();

                    return;
                }




                if (Class1.save_good_path != "")
                {
                    StreamWriter sw = new StreamWriter($"{Class1.save_good_path}", true);
                    sw.WriteLine($"\r\n\r\n[GOOD] TOKEN: {token}\r\nUSERNAME: {d["username"]}\r\nTAG: {d["discriminator"]}\r\nBIO: {d["bio"]}\r\nCOUNTRY:{d["locale"]}\r\nEMAL: {d["email"]}\r\nVERIFED_STATUS: {d["verified"]}\r\nPHONE: {d["phone"]}\r\nPAYMENTS:\r\n{payments}\r\n========================");
                    sw.Close();
                }




            }
            catch
            {

                if (Class1.save_bad_path != "")
                {
                    StreamWriter sw = new StreamWriter($"{Class1.save_good_path}");
                    sw.WriteLine($"\r\n\r\n[BAD] TOKEN: {token}\r\n========================");
                    sw.Close();

                }

                dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Add("BAD", "", $"{token}")));
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string fileContent = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();

                        tokens = fileContent.Replace("\n", "").Split('\r');


                        label1.Text = $"Загружено {tokens.Length - 1} токенов!";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 f2 = new Form2();

                f2.token_ds = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                f2.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                f2.Show();
            }
            catch
            {
                MessageBox.Show("Токен не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        async private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                var tasks = new Task[] { };


                for (int i = 0; i < tokens.Length - 1; i++)
                {

                    tasks.Append(check_token(tokens[i]));


                    if (checkBox1.Checked)
                    {
                        if (i % Convert.ToInt32(textBox1.Text) == 0)
                        {
                            await Task.WhenAll(tasks);
                            tasks = new Task[] { };
                            await Task.Delay(Convert.ToInt32((float.Parse(textBox2.Text)) * 1000));
                        }
                    }

                }
                await Task.WhenAll(tasks);
            }
            catch
            {
                MessageBox.Show("Выберите файл с токенами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void Form6_Load(object sender, EventArgs e)
        {

        }



        private void button6_Click_1(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

        }
    }
}
