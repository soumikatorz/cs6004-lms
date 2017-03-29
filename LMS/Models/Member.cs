using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Member : Person
    {
        public Guid ID { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required, Display(Name ="Membership")]
        public Guid MembershipID { get; set; }
        public virtual Membership Membership { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}