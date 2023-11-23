using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kurs_64
{
    public partial class OrderAddUpdate : Form
    {
        public NpgsqlConnection connection;
        public int Id;
        public OrderAddUpdate(NpgsqlConnection connection, int Id)
        {
            InitializeComponent();
            this.connection = connection;
            this.Id = Id;
            CarsDictionary();
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
            var str = $"Select ord_car_id, ord_weight, ord_volume, ord_description, ord_number from orders where order_id = {Id}";
            var command = new NpgsqlCommand(str, connection);
            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                comboBox1.SelectedIndex = reader.GetInt32(0) - 1;
                textBox1.Text = reader.GetFloat(1).ToString();
                textBox2.Text = reader.GetFloat(2).ToString();
                textBox3.Text = reader.GetString(3);
                textBox4.Text = reader.GetString(4);

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

        private void CarsDictionary()
        {
            string str = "SELECT car_model FROM cars ORDER BY car_id ASC ";
            var teamList = Cars.ComboboxValue(connection, str);
            ObservableCollection<Dictionaries> dictionaries = new ObservableCollection<Dictionaries>();
            teamList.ForEach(NameTeam => dictionaries.Add(new Dictionaries() { IKey = String.Empty, IValue = NameTeam }));
            comboBox1.DataSource = dictionaries.ToList();
        }

        private void OrderInsert()
        {
            connection.Open();
            var command = new NpgsqlCommand("insert_orders", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@car_id", comboBox1.SelectedIndex + 1);

                command.Parameters.Add(new NpgsqlParameter("@weight", NpgsqlDbType.Numeric));
                command.Parameters["@weight"].Value = Convert.ToDouble(textBox1.Text);

                command.Parameters.Add(new NpgsqlParameter("@volume", NpgsqlDbType.Numeric));
                command.Parameters["@volume"].Value = Convert.ToDouble(textBox2.Text);

                command.Parameters.Add(new NpgsqlParameter("@description", NpgsqlDbType.Varchar));
                command.Parameters["@description"].Value = textBox3.Text;

                command.Parameters.Add(new NpgsqlParameter("@number", NpgsqlDbType.Char));
                command.Parameters["@number"].Value = textBox4.Text;

                command.ExecuteNonQuery();
                MessageBox.Show("Заказ добавлен!");
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
        private void OrderUpdate(int id)
        {
            connection.Open();
            var command = new NpgsqlCommand("update_orders", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@car_id", comboBox1.SelectedIndex + 1);

                command.Parameters.Add(new NpgsqlParameter("@weight", NpgsqlDbType.Numeric));
                command.Parameters["@weight"].Value = Convert.ToDouble(textBox1.Text);

                command.Parameters.Add(new NpgsqlParameter("@volume", NpgsqlDbType.Numeric));
                command.Parameters["@volume"].Value = Convert.ToDouble(textBox2.Text);

                command.Parameters.Add(new NpgsqlParameter("@description", NpgsqlDbType.Varchar));
                command.Parameters["@description"].Value = textBox3.Text;

                command.Parameters.Add(new NpgsqlParameter("@number", NpgsqlDbType.Char));
                command.Parameters["@number"].Value = textBox4.Text;

                command.Parameters.AddWithValue("@id", Id);

                command.ExecuteNonQuery();
                MessageBox.Show("Заказ обновлен!");
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Id == 0)
                OrderInsert();
            else
                OrderUpdate(Id);

        }
    }
}
