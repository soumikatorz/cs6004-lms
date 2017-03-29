using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LoanType : BaseEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}