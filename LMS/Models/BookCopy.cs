using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class BookCopy
    {
        public Guid ID { get; set; }
        public int CopyNumber { get; set; }
        public Book Book { get; set; }
        public string Location { get; set; }
        public Boolean Available { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}