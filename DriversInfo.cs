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
    public partial class DriversInfo : Form
    {
        public NpgsqlConnection connection;
        public DriversInfo(NpgsqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            FillGrid();
        }

        public void FillGrid()
        {
            List<Drivers> listDrivers = new List<Drivers>();
            connection.Open();
            var sql = "select * from drivers";
            var command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var view_drivers = new Drivers()
                {
                    driverId = Convert.ToInt32(reader["driver_id"]),
                    FIO = Convert.ToString(reader["driver_name"]),
                    phoneNumber = Convert.ToString(reader["driver_phone"]),
                    numberLic = Convert.ToString(reader["driver_license"])
                };
                listDrivers.Add(view_drivers);
            }
            connection.Close();
            dataGridView1.DataSource = listDrivers;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var updatedriver = new DriversAddUpdate(connection, id);
            updatedriver.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var driversAdd = new DriversAddUpdate(connection, 0);
            driversAdd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var result = MessageBox.Show("Удалить этого водителя?", "Удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("delete_drivers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Водитель удален!");
                FillGrid();
            }
        }
    }
}
