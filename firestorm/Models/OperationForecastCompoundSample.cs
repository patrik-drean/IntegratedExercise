using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace firestorm.Models
{
    public class OperationForecastCompoundSample
    {
        [DisplayName("Order ID")]
        public int OrderID { get; set; }

        [DisplayName("Date Due")]
        public DateTime DateDue { get; set; }
    }
}