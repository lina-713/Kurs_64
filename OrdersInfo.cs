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
    public partial class OrdersInfo : Form
    {
        public NpgsqlConnection connection;
        public OrdersInfo(NpgsqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            FillGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             FillGrid();
        }

        private void FillGrid()
        {
            List<Orders> listOrders = new List<Orders>();
            connection.Open();
            var sql = "select * from view_orders";
            var command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var orders = new Orders()
                {
                    orderId = Convert.ToInt32(reader["order_id"]),
                    carBrand = Convert.ToString(reader["car_brand"]),
                    carModel = Convert.ToString(reader["car_model"]),
                    carNumber = Convert.ToString(reader["car_number"]),
                    weight = Convert.ToString(reader["ord_weight"]),
                    volume = Convert.ToString(reader["ord_volume"]),
                    description = Convert.ToString(reader["ord_description"])
                };
                listOrders.Add(orders);
            }
            connection.Close();
            dataGridView1.DataSource = listOrders;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ordersInsert = new OrderAddUpdate(connection, 0);
            ordersInsert.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var ordersInsert = new OrderAddUpdate(connection, id);
            ordersInsert.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var result = MessageBox.Show("Вы действительно хотите удалить данного спортсмена?", "Удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("delete_orders", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Заказ удален!");
                FillGrid();
            }
        }
    }
}
