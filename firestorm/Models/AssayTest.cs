using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("AssayTest")]
    public class AssayTest
    {
        [Key, Column(Order = 0), ForeignKey("Assay")]
        public virtual string AssayID { get; set; }
        public virtual Assay Assay { get; set; }

        [Key, Column(Order = 1), ForeignKey("Test")]
        public virtual int TestID { get; set; }
        public virtual Test Test { get; set; }

    }
}