using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public Boolean AgeRestricted { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}