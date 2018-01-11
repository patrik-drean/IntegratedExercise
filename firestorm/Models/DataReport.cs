using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("DataReport")]
    public class DataReport
    {
        [Key]
        public int DataRptID { get; set; }
        public string FilePath { get; set; }

        [ForeignKey("SampleTest")]
        public virtual int TestTubeID { get; set; }
        public virtual SampleTest SampleTest { get; set; }
    }
}