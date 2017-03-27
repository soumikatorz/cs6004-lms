using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Publisher
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Book> Books { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}