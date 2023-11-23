using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_64
{
    internal class Case_view_routes
    {
        public int routeId { get; set; }
        [DisplayName("Стартовый город")]
        public string startLocation { get; set; }
        [DisplayName("Пункт назначения")]
        public string endLocation { get; set;}
        [DisplayName("Начальная дата")]
        public DateTime startDate { get; set; }
        [DisplayName("Конечная дата")]
        public DateTime endDate { get; set; }
        [DisplayName("Имя Водителя")]
        public string driverName { get; set; }
        [DisplayName("Номер телефона")]
        public string driverPhone { get; set; }
        [DisplayName("Статус")]
        public string status { get; set; }
        [DisplayName("Номер заказа")]
        public int number { get; set; }
    }
}
