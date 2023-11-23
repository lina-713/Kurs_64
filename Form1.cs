using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Kurs_64
{
    public partial class Form1 : Form
    {
        public NpgsqlConnection connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str;
            switch(textBox1.Text + textBox2.Text)
            {
                case ("user1" + "87654321"):
                    str = "Host=localhost;Port=5432;Database=MotorPool;Username=user1;Password=87654321";
                    break;

                case ("admin1" + "12345678"):
                    str = "Host=localhost;Port=5432;Database=MotorPool;Username=admin1;Password=12345678";
                    break;
                default:
                    MessageBox.Show("Неправильный логин или пароль!");
                    return;
            }
            NpgsqlConnection conn = new NpgsqlConnection(str);
            connection = conn;
            var homepage = new Homepage(connection);
            homepage.Show();
            this.Dispose();
        }
    }
}
