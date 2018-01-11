using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentID { get; set; }
        public decimal PaymentAmount { get; set; }
        public String PaymentForm { get; set; }

        [ForeignKey("WorkOrder")]
        public virtual int OrderID { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        [ForeignKey("Company")]
        public virtual int CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}