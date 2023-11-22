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
    public partial class MotorPoolInfo : Form
    {
        public NpgsqlConnection connection;
        public MotorPoolInfo(NpgsqlConnection connection)
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
            List<Cars> listCars = new List<Cars>();
            connection.Open();
            var sql = "select * from cars";
            var command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var cars = new Cars()
                {
                    carId = Convert.ToInt32(reader["car_id"]),
                    carBrand = Convert.ToString(reader["car_brand"]),
                    carModel = Convert.ToString(reader["car_model"]),
                    carNumber = Convert.ToString(reader["car_number"]),
                    carFuel = Convert.ToString(reader["car_fuel"]),
                    carVin = Convert.ToString(reader["car_vin"])
                };
                listCars.Add(cars);
            }
            connection.Close();
            dataGridView1.DataSource = listCars;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var carInsert = new MotorPoolAddUpdate(connection, 0);
            carInsert.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var result = MessageBox.Show("Удалить эту машину?", "Удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("delete_cars", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Машина удалена!");
                FillGrid();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var updateCar = new MotorPoolAddUpdate(connection, id);
            updateCar.Show();
        }
    }
}
