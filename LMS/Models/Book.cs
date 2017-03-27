using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Book
    {
        public Guid ID { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public Publisher Publisher { get; set; }
        public Press Press { get; set; }
        public DateTime PublishedDate { get; set; }
        public double Charge { get; set; }
        public double PenaltyCharge { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<BookCopy> Copies { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}