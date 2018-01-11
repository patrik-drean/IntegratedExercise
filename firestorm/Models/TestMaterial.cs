using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("TestMaterial")]
    public class TestMaterial
    {
        [Key, Column(Order = 0), ForeignKey("Test")]
        public virtual int TestID { get; set; }
        public virtual Test Test { get; set; }

        [Key, Column(Order = 1), ForeignKey("Material")]
        public virtual int MaterialID { get; set; }
        public virtual Material Material { get; set; }
    }
}