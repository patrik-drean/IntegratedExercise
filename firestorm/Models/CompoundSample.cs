using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using firestorm.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace firestorm.Models
{
    [Table("CompoundSample")]
    public class CompoundSample
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0), ForeignKey("Compound")]

        public int LT { get; set; }
        public virtual Compound Compound { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SequenceCode { get; set; }

        public int QuantityMG { get; set; }
        public DateTime DateArrived { get; set; }
        public String ReceivedBy { get; set; }

        [DisplayName("Desired Completion Date")]
        public DateTime DateDue { get; set; }
        public String Appearance { get; set; }

        [Required(ErrorMessage = "Please enter a weight of compound")]
        [DisplayName("Weight of Compound (mg)")]
        public int Weight { get; set; }

        public decimal? MolMass { get; set; }


        [DisplayName("Authorize additional tests based upon test outcome.")]
        public string AuthAddTest { get; set; }

        [ForeignKey("Assay")]
        public String AssayID { get; set; }
        public virtual Assay Assay { get; set; }

        [ForeignKey("WorkOrder")]
        public int OrderID { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }



    }
}