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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using NpgsqlTypes;

namespace Kurs_64
{
    public partial class DriversAddUpdate : Form
    {
        public NpgsqlConnection connection;
        public int Id;
        public DriversAddUpdate(NpgsqlConnection connection, int Id)
        {
            InitializeComponent();
            this.connection = connection;
            this.Id = Id;
            if (Id != 0)
            {
                EnterInfo(Id);
                button1.Text = "Обновить";
            }
            else
                button1.Text = "Добавить";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                AddDriver();
                var drivers = new DriversInfo(connection);
                drivers.FillGrid();
                this.Close();
            }
            else
            {
                UpdateDrivers(Id);
                var drivers = new DriversInfo(connection);
                drivers.FillGrid();
                this.Close();
            }
        }
        private void EnterInfo(int Id)
        {
            var str = $"Select driver_name, driver_phone, driver_license from drivers where driver_id = {Id}";
            var command = new NpgsqlCommand(str, connection);
            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string[] fio = (reader.GetString(0)).Split(' ');
                textBox1.Text = fio[0];
                textBox2.Text = fio[1];
                textBox3.Text = fio[2];
                textBox4.Text = reader.GetString(1);
                textBox5.Text = reader.GetString(2);

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void UpdateDrivers(int Id)
        {
            connection.Open();
            var command = new NpgsqlCommand("update_drivers", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new NpgsqlParameter("@name", NpgsqlDbType.Varchar));
                command.Parameters["@name"].Value = $"{textBox1.Text} {textBox2.Text} {textBox3.Text}";

                command.Parameters.Add(new NpgsqlParameter("@phone", NpgsqlDbType.Varchar));
                command.Parameters["@phone"].Value = textBox4.Text;

                command.Parameters.Add(new NpgsqlParameter("@license", NpgsqlDbType.Char));
                command.Parameters["@license"].Value = textBox5.Text;

                command.Parameters.AddWithValue("@id", Id);

                command.ExecuteNonQuery();
                MessageBox.Show("Водитель добавлен!");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void AddDriver()
        {
            connection.Open();
            var command = new NpgsqlCommand("insert_drivers", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new NpgsqlParameter("@name", NpgsqlDbType.Varchar)); 
                command.Parameters["@name"].Value = $"{textBox1.Text} {textBox2.Text} {textBox3.Text}";

                command.Parameters.Add(new NpgsqlParameter("@phone", NpgsqlDbType.Varchar));
                command.Parameters["@phone"].Value = textBox4.Text;

                command.Parameters.Add(new NpgsqlParameter("@license", NpgsqlDbType.Char));
                command.Parameters["@license"].Value = textBox5.Text;

                command.ExecuteNonQuery();
                MessageBox.Show("Водитель добавлен!");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
