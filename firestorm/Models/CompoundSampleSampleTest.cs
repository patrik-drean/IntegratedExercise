using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace firestorm.Models
{
    public class CompoundSampleSampleTest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public CompoundSample compoundSample { get; set; }
        public List<SampleTest> sampleTests { get; set; }
            //= new List<SampleTest>();


    }
}