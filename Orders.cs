using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs_64
{
    internal class Orders
    {
        public int orderId { get; set; }
        [DisplayName("Бренд")]
        public string carBrand { get; set; }
        [DisplayName("Модель")]
        public string carModel { get; set; }
        [DisplayName("Гос. Номер")]
        public string carNumber { get; set; }
        [DisplayName("Вес товара(кг)")]
        public string weight { get; set; }
        [DisplayName("Объем товара(м3)")]
        public string volume { get; set; }
        [DisplayName("Описания заказа")]
        public string description { get; set; }

        static public List<int> ComboboxValue(NpgsqlConnection connection, string str)
        {
            connection.Open();
            var carsList = new List<int>();
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                carsList.Add(reader.GetInt32(0));
            }
            connection.Close();
            return carsList;
        }
    }
}
