using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Member : Person
    {
        public Guid ID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Membership Membership { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}