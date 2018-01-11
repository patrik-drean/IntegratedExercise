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
    public class NewTestTube
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestTubeID { get; set; }
        public String AssayID { get; set; }
        public int TestID { get; set; }
        [DisplayName("Assay Name")]
        public String Name { get; set; }
        public int LT { get; set; }
        [DisplayName("Sequence Code")]
        public int SequenceCode { get; set; }
        [DisplayName("Desired Completion Date")]
        public DateTime? DateDue { get; set; }

    }
}