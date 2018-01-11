using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace firestorm.Models
{
    [Table("WorkOrder")]
    public class WorkOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }
        public double? Discount { get; set; }

        [ForeignKey("Company")]
        public virtual int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [DisplayName("Additional comments about this order")]
        public string Comments { get; set; }
    }
}