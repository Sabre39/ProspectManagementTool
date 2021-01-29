using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProspectManagementTool.Models
{
    public class Attribute
    {
        public int ID { get; set; }

        [Display(Name = "Player Type")]
        [Required(ErrorMessage = "Player Type Is Required")]
        [StringLength(50, ErrorMessage = "Player Type Should Not Exceed 50 Characters")]
        public string AttributeName { get; set; }

        public ICollection<Prospect> Prospects { get; set; }

    }
}