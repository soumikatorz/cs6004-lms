using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class BaseEntity
    {
        [Editable(false)]
        [Display(Name = "Last Updated By")]
        public string LastUpdatedBy { get; set; }

        [Editable(false)]
        [Display(Name ="Last Updated")]
        public DateTime? LastUpdated { get; set; }
    }
}