using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class BookCopy : BaseEntity
    {
        public Guid ID { get; set; }
        [Required]
        public int CopyNumber { get; set; }
        [Required, Display(Name ="Book")]
        public Guid BookID { get; set; }
        public virtual Book Book { get; set; }
        [Required]
        public string Location { get; set; }
        public Boolean Available { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}