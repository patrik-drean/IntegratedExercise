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
    [Table("SampleTest")]
    public class SampleTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Test ID")]
        public int TestTubeID { get; set; }
        public double Concentration { get; set; }

        [DisplayName("Scheduled Date")]
        public DateTime? ScheduledDate { get; set; }

        [DisplayName("Test Complete?")]
        public string CompletedTest { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("CompoundSample"), Column(Order = 0)]
        public virtual int? LT { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("CompoundSample"), Column(Order = 1)]
        [DisplayName("Sequence Code?")]
        public virtual int? SequenceCode { get; set; }
        public virtual CompoundSample CompoundSample { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("AssayTest"), Column(Order = 2)]
        [DisplayName("Type of Assay")]
        public virtual string AssayID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("AssayTest"), Column(Order = 3)]
        public virtual int TestID { get; set; }
        public virtual AssayTest AssayTest { get; set; }
    }
}