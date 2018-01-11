using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firestorm.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }


        [Required(ErrorMessage = "Please enter the company name")]
        public String Name { get; set; }


        [Required(ErrorMessage = "Please enter your address")]
        public String Address { get; set; }


        [Required(ErrorMessage = "Please enter your city")]
        public String City { get; set; }


        [Required(ErrorMessage = "Please enter your state")]
        public String State { get; set; }


        [Required(ErrorMessage = "Please enter your zip code")]
        [RegularExpression(@"^\d{5}([\-]\d{4})?$", ErrorMessage = "Zipcode must be numeric and the correct length")]
        public String Zip { get; set; }

        public decimal Balance { get; set; }
    }
}