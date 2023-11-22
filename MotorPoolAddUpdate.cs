using Npgsql;
using NpgsqlTypes;
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
    public partial class MotorPoolAddUpdate : Form
    {
        public NpgsqlConnection connection;
        public int Id;
        public MotorPoolAddUpdate(NpgsqlConnection connection, int Id)
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
        
        private void EnterInfo(int Id) 
        {
            var str = $"Select car_brand, car_model, car_number, car_fuel, car_vin from cars where car_id = {Id}";
            var command = new NpgsqlCommand(str, connection);
            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                textBox1.Text = reader.GetString(0);
                textBox2.Text = reader.GetString(1);
                textBox3.Text = reader.GetString(2);
                textBox4.Text = reader.GetString(3);
                textBox5.Text = reader.GetString(4);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Id == 0)
                EnterInsert();
            else
                EnterUpdate(Id);
        }

        private void EnterInsert()
        {
            connection.Open();
            var command = new NpgsqlCommand("insert_cars", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new NpgsqlParameter("@brand", NpgsqlDbType.Varchar));
                command.Parameters["@brand"].Value = textBox1.Text;

                command.Parameters.Add(new NpgsqlParameter("@model", NpgsqlDbType.Varchar));
                command.Parameters["@model"].Value = textBox2.Text;

                command.Parameters.Add(new NpgsqlParameter("@number", NpgsqlDbType.Char));
                command.Parameters["@number"].Value = textBox3.Text;

                command.Parameters.Add(new NpgsqlParameter("@fuel", NpgsqlDbType.Varchar));
                command.Parameters["@fuel"].Value = textBox4.Text;

                command.Parameters.Add(new NpgsqlParameter("@vin", NpgsqlDbType.Char));
                command.Parameters["@vin"].Value = textBox5.Text;

                command.ExecuteNonQuery();
                MessageBox.Show("Машина добавлена!");
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
        private void EnterUpdate(int id)
        {
            connection.Open();
            var command = new NpgsqlCommand("update_cars", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new NpgsqlParameter("@brand", NpgsqlDbType.Varchar));
                command.Parameters["@brand"].Value = textBox1.Text;

                command.Parameters.Add(new NpgsqlParameter("@model", NpgsqlDbType.Varchar));
                command.Parameters["@model"].Value = textBox2.Text;

                command.Parameters.Add(new NpgsqlParameter("@number", NpgsqlDbType.Char));
                command.Parameters["@number"].Value = textBox3.Text;

                command.Parameters.Add(new NpgsqlParameter("@fuel", NpgsqlDbType.Varchar));
                command.Parameters["@fuel"].Value = textBox4.Text;

                command.Parameters.Add(new NpgsqlParameter("@vin", NpgsqlDbType.Char));
                command.Parameters["@vin"].Value = textBox5.Text;

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                MessageBox.Show("Машина обновлена!");
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
