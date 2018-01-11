using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("CatalogReference")]
    public class CatalogReference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReferenceID { get; set; }
        public String Reference { get; set; }
        
        [ForeignKey("Assay")]
        public virtual string AssayID { get; set; }
        public virtual Assay Assay { get; set; }

    }
}