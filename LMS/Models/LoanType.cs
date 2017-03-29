using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LoanType : BaseEntity
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}