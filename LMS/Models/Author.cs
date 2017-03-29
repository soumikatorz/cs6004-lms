using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Author : Person
    {
        public Guid ID { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}