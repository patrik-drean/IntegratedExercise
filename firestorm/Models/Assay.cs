using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace firestorm.Models
{
    [Table("Assay")]
    public class Assay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String AssayID { get; set; }


        [DisplayName("Please type the Assay Name")]
        public String Name { get; set; }

        [DisplayName("Please type the Assay Procedure")]
        public String Procedure { get; set; }
    }
}