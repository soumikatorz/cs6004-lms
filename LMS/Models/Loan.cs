using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Loan : BaseEntity
    {
        public Guid ID { get; set; }
        [Required, Display(Name="Book Copy")]
        public Guid BookCopyID { get; set; }
        public virtual BookCopy BookCopy { get; set; }
        [Required, Display(Name = "Member")]
        public Guid MemberID { get; set; }
        public virtual Member Member { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime IssuedOn { get; set; }
        [DataType(DataType.Date), Editable(false)]
        public DateTime? ReturnedOn { get; set; }
        [Editable(false)]
        public double LoanCharge { get; set; }
        [Editable(false)]
        public double? PenaltyCharge { get; set; }
        [DataType(DataType.Date), Editable(false)]
        public DateTime DueDate { get; set; }
        [Required, Display(Name ="Loan Type")]
        public Guid LoanTypeID { get; set; }
        public virtual LoanType LoanType { get; set; }
        [Editable(false)]
        public Guid ApplicationUserID { get; set; }
        public virtual ApplicationUser LoanedBy { get; set; }
    }
}