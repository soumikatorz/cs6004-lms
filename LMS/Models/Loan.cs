using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Loan : BaseEntity
    {
        public Guid ID { get; set; }
        public BookCopy BookCopy { get; set; }
        public Member Member { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime? ReturnedOn { get; set; }
        public double LoanCharge { get; set; }
        public double? PenaltyCharge { get; set; }
        public DateTime DueDate { get; set; }
        public LoanType LoanType { get; set; }
        public ApplicationUser LoanedBy { get; set; }
    }
}