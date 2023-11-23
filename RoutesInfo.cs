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
    public partial class RoutesInfo : Form
    {
        public NpgsqlConnection connection;
        public RoutesInfo(NpgsqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            if (connection.UserName != "admin1")
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
            FillGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        public void FillGrid()
        {
            List<Case_view_routes> listRoute = new List<Case_view_routes>();
            connection.Open();
            var sql = "select * from case_view_routes";
            var command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var view_routes = new Case_view_routes()
                {
                    routeId = Convert.ToInt32(reader["route_id"]),
                    startLocation = Convert.ToString(reader["start_location"]),
                    endLocation = Convert.ToString(reader["end_location"]),
                    startDate = Convert.ToDateTime(reader["start_date"]),
                    endDate = Convert.ToDateTime(reader["end_date"]),
                    driverName = Convert.ToString(reader["driver_name"]),
                    driverPhone = Convert.ToString(reader["driver_phone"]),
                    status = Convert.ToString(reader["status"]),
                    number = Convert.ToInt32(reader["order_id"])
                };
                listRoute.Add(view_routes);
            }
            connection.Close();
            dataGridView1.DataSource = listRoute;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var routesInsert = new RoutesAddUpdate(connection, 0);
            routesInsert.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var result = MessageBox.Show("Удалить этот маршрут?", "Удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("delete_routes", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Маршрут удален!");
                FillGrid();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var routesInsert = new RoutesAddUpdate(connection, id);
            routesInsert.Show();
        }
    }
}
