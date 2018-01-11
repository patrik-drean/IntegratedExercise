using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace firestorm.Models
{
    public class CompanyWorkOrders
    {
        public Company Company { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
}