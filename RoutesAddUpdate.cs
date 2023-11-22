﻿using Npgsql;
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
    public partial class RoutesAddUpdate : Form
    {
        public NpgsqlConnection connection;
        public int Id;
        public RoutesAddUpdate(NpgsqlConnection connection, int Id)
        {
            InitializeComponent();
            this.connection = connection;
            this.Id = Id;
            DriverDictionary();
            OrderDictionary();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Id == 0)
                RoutesInsert();
            else
                RoutesUpdate(Id);
        }

        private void RoutesUpdate(int id)
        {
            connection.Open();
            var command = new NpgsqlCommand("update_routes", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new NpgsqlParameter("@s_loc", NpgsqlDbType.Varchar));
                command.Parameters["@s_loc"].Value = textBox1.Text;

                command.Parameters.Add(new NpgsqlParameter("@e_loc", NpgsqlDbType.Varchar));
                command.Parameters["@e_loc"].Value = textBox2.Text;

                command.Parameters.Add(new NpgsqlParameter("@s_date", NpgsqlDbType.Date));
                command.Parameters["@s_date"].Value = dateTimePicker1.Value.Date;

                command.Parameters.Add(new NpgsqlParameter("@e_date", NpgsqlDbType.Date));
                command.Parameters["@e_date"].Value = dateTimePicker2.Value.Date;

                command.Parameters.AddWithValue("@driver_id", comboBox1.SelectedIndex + 1);
                command.Parameters.AddWithValue("@order_id", Int32.Parse(comboBox2.SelectedValue.ToString()));
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                MessageBox.Show("Заказ обновлен!");
                this.Close();
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
        private void RoutesInsert()
        {
            connection.Open();
            var command = new NpgsqlCommand("insert_routes", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new NpgsqlParameter("@s_loc", NpgsqlDbType.Varchar));
                command.Parameters["@s_loc"].Value = textBox1.Text;

                command.Parameters.Add(new NpgsqlParameter("@e_loc", NpgsqlDbType.Varchar));
                command.Parameters["@e_loc"].Value = textBox2.Text;

                command.Parameters.Add(new NpgsqlParameter("@s_date", NpgsqlDbType.Date));
                command.Parameters["@s_date"].Value = dateTimePicker1.Value.Date;

                command.Parameters.Add(new NpgsqlParameter("@e_date", NpgsqlDbType.Date));
                command.Parameters["@e_date"].Value = dateTimePicker2.Value.Date;

                command.Parameters.AddWithValue("@driver_id", comboBox1.SelectedIndex + 1);
                command.Parameters.AddWithValue("@order_id", Int32.Parse(comboBox2.SelectedValue.ToString()));

                command.ExecuteNonQuery();
                MessageBox.Show("Заказ добавлен!");
                this.Close();
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

        private void DriverDictionary()
        {
            string str = "SELECT driver_name FROM drivers ORDER BY driver_id ASC ";
            var drivaresList = Cars.ComboboxValue(connection, str);
            ObservableCollection<Dictionaries> dictionaries = new ObservableCollection<Dictionaries>();
            drivaresList.ForEach(DriverName => dictionaries.Add(new Dictionaries() { IKey = String.Empty, IValue = DriverName }));
            comboBox1.DataSource = dictionaries.ToList();
        }

        private void OrderDictionary()
        {
            string str = "SELECT order_id FROM orders ORDER BY order_id ASC ";
            var orderList = Orders.ComboboxValue(connection, str);
            ObservableCollection<Dictionaries> dictionaries = new ObservableCollection<Dictionaries>();
            orderList.ForEach(OrderDesc => dictionaries.Add(new Dictionaries() { IKey = String.Empty, IValue = OrderDesc.ToString() }));
            comboBox2.DataSource = dictionaries.ToList();
        }
    }
}
