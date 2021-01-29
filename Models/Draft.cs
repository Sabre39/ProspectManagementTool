using ProspectManagementTool.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProspectManagementTool.Models
{
    public class Draft
    {
        public int ID { get; set; }

        [Display(Name = "Rating")]
        public string DraftRating
        {
            get
            {
                return DraftOV + "/" + DraftPotential;
            }
        }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter A Name")]
        [StringLength(70, ErrorMessage = "Prospect Name Should Not Exceed 70 Characters")]
        public string DraftName { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter A Player Position")]
        public Position DraftPosition { get; set; }

        [Display(Name = "OV")]
        [Required(ErrorMessage = "Enter a Prospect OV")]
        [Range(60, 99, ErrorMessage = "Prospect OV Should Be Between 60 and 99")]
        public byte DraftOV { get; set; }

        [Display(Name = "Potential")]
        [Required(ErrorMessage = "Enter A Player Potential")]
        public Potential DraftPotential { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Enter An Age")]
        [Range(18, 40, ErrorMessage = "Prospect Age Should Be Between 18 and 40")]
        public byte DraftAge { get; set; }

        [Display(Name = "Initial Rating")]
        [Required(ErrorMessage = "Enter an Initial Rating")]
        [StringLength(6, ErrorMessage = "Initial Rating Should Not Exceed 6 Characters")]
        public string DraftInitialRating { get; set; }

        [Display(Name = "Draft Pos.")]
        public int? DraftSelected { get; set; }



        [Display(Name = "Team")]
        public int? TeamID { get; set; }

        public virtual Team Team { get; set; }

        [Display(Name = "Player Type")]
        public int AttributeID { get; set; }

        public virtual Attribute Attribute { get; set; }

     }
}
