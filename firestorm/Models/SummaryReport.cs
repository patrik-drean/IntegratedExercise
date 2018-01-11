using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("SummaryReport")]
    public class SummaryReport
    {
        [Key]
        public int SumRptID { get; set; }

        public string ServerFilePath { get; set; }

        [ForeignKey("WorkOrder")]
        public virtual int OrderID { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}