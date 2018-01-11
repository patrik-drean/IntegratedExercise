using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("ConditionalTest")]
    public class ConditionalTest
    {
        [Key, Column(Order = 0), ForeignKey("AssayTest")]
        public String AssayID { get; set; }
        
        [Key, Column(Order=1), ForeignKey("AssayTest")]
        public int TestID { get; set; }
        public virtual AssayTest AssayTest { get; set; }

        [Key, Column(Order =2), ForeignKey("Test")]
        public int CondTestID { get; set; }
        public virtual Test Test { get; set; }
    }
}