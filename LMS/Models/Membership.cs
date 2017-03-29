using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Membership : BaseEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int MaxLoans { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}