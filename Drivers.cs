using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_64
{
    internal class Drivers
    {
        public int driverId { get; set; }
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DisplayName("Номер телефона")]
        public string phoneNumber { get; set; }
        [DisplayName("Номер лицензии")]
        public string numberLic { get; set; }
    }
}
