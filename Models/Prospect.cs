 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProspectManagementTool.Models
{
    public class Prospect
    {
        public int ID { get; set; }

        [Display(Name = "Rating")]
        public string ProspectRating
        {
            get
            {
                return ProspectOV + "/" + ProspectPotential;
            }
        }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter A Name")]
        [StringLength(70, ErrorMessage = "Prospect Name Should Not Exceed 70 Characters")]
        public string ProspectName { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter A Player Position")]
        public Position ProspectPosition { get; set; }

        [Display(Name = "OV")]
        [Required(ErrorMessage = "Enter a Prospect OV")]
        [Range(25, 99, ErrorMessage = "Prospect OV Should Be Between 25 and 99")]
        public byte ProspectOV { get; set; }

        [Display(Name = "Potential")]
        [Required(ErrorMessage = "Enter A Player Potential")]
        public Potential ProspectPotential { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Enter An Age")]
        [Range(18, 40, ErrorMessage = "Prospect Age Should Be Between 18 and 40")]
        public byte ProspectAge { get; set; }

        [Display(Name = "Reroll Rating")]
        [StringLength(4, ErrorMessage = "Reroll Rating Should Be 4 Characters or Less")]
        public string ProspectRerollRating { get; set; }

        [Display(Name = "Initial Rating")]
        [Required(ErrorMessage = "Enter an Initial Rating")]
        [StringLength(6, ErrorMessage = "Initial Rating Should Not Exceed 6 Characters")]
        public string ProspectInitialRating { get; set; }

        [Display(Name = "Drafted By")]
        public string ProspectDraftedBy { get; set; }

        public int TeamID { get; set; }

        public virtual Team Team { get; set; }

        public int AttributeID { get; set; }

        public virtual Attribute Attribute { get; set; }
    }
}