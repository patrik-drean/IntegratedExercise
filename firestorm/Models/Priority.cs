using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace firestorm.Models
{
    [Table("Priority")]
    public class Priority
    {
        [Key]
        [DisplayName("Priority")]
        public string PriorityName { get; set; }

    }
}