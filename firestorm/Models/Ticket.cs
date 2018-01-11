using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace firestorm.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketID { get; set; }
        [DisplayName("Submission Date")]
        public DateTime DateSubmitted { get; set; }

        [DisplayName("Resolve Date")]
        public DateTime? DateResolved { get; set; }

        [ForeignKey("Priority")]
        [DisplayName("Priority Level")]
        public virtual string PriorityName { get; set; }
        public virtual Priority Priority { get; set; }

        [DisplayName("Work Order ID")]
        [ForeignKey("WorkOrder")]
        public virtual int OrderID { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        [DisplayName("Submitted by")]
        [ForeignKey("User")]
        public virtual int UserID { get; set; }
        public virtual User User { get; set; }

        public string Comment { get; set; }
        public string Response { get; set; }

        public Ticket()
        {

        }

        public Ticket(DateTime dateSubmitted, int userID, string Comment)
        {

        }
    }
}