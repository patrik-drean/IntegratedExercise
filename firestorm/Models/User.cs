using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Web.Mvc;

namespace firestorm.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Required(ErrorMessage ="Please enter your first name")]
        [DisplayName("First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [DisplayName("Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Please enter " +
                            " phone number as (xxx) xxx-xxxx")]
        public String Phone { get; set; }
        [EmailAddress]

        [Required(ErrorMessage = "Please enter your email address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ForeignKey("Role")]
        public virtual int? RoleID { get; set; }
        public virtual Role Role { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Please enter your company")]
        [ForeignKey("Company")]
        public virtual int? CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}