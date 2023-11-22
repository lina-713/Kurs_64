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
    public partial class Homepage : Form
    {
        NpgsqlConnection connection;
        public Homepage(NpgsqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var routes = new RoutesInfo(connection);
            routes.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var drivers = new DriversInfo(connection);
            drivers.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cars = new MotorPoolInfo(connection);
            cars.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var orders = new OrdersInfo(connection);
            orders.Show();
        }
    }
}
