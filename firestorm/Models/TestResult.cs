using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("TestResult")]
    public class TestResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        [ForeignKey("SampleTest")]
        public int TestTubeID { get; set; }
        public virtual SampleTest SampleTest { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("TestReport")]
        public int ReportTypeCode { get; set; }
        public virtual TestReport TestReport { get; set; }

        public string FilePath { get; set; }

    }
}