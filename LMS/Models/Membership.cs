using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Membership
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int MaxLoans { get; set; }
        public ICollection<Member> Members { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}