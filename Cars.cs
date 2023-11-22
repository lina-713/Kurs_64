using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_64
{
    internal class Cars
    {
        public int carId { get; set; }
        [DisplayName("Марка")]
        public string carBrand { get; set; }
        [DisplayName("Модель")]
        public string carModel { get; set; }
        [DisplayName("Гос.Номер")]
        public string carNumber { get; set; }
        [DisplayName("Вид топлива")]
        public string carFuel { get; set; }
        [DisplayName("VIN номер")]
        public string carVin { get; set; }

        static public List<string> ComboboxValue(NpgsqlConnection connection, string str)
        {
            connection.Open();
            var carsList = new List<string>();
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                carsList.Add(reader.GetString(0));
            }
            connection.Close();
            return carsList;
        }
    }
}
