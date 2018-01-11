using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime EarlyPayDate { get; set; }
        public decimal EarlyPayDiscount { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("WorkOrder")]
        public virtual int OrderID { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        [ForeignKey("Company")]
        public virtual int CompanyID { get; set; }
        public virtual Company Company { get; set; }

    }
}