using ProspectManagementTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProspectManagementTool.ViewModels
{
    public class TeamProspectVM
    {
        [Display(Name = "Team")]
        public string HockeyTeam
        {
            get
            {
                return CityName + " " + TeamName;
            }
        }
        public int ID { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Enter a City")]
        [StringLength(50, ErrorMessage = "City Should Not Exceed 50 Characters")]
        public string CityName { get; set; }

        [Display(Name = "Team")]
        [Required(ErrorMessage = "Enter a Team Name")]
        [StringLength(50, ErrorMessage = "Team Name Should Not Exceed 50 Characters")]
        public string TeamName { get; set; }

        [Display(Name = "Prospects")]
        public ICollection<Prospect> Prospects { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter A Name")]
        [StringLength(70, ErrorMessage = "Prospect Name Should Not Exceed 70 Characters")]
        public string ProspectName { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter A Player Position")]
        public Position ProspectPosition { get; set; }

        [Display(Name = "OV")]
        [Required(ErrorMessage = "Enter a Prospect OV")]
        [Range(60, 99, ErrorMessage = "Prospect OV Should Be Between 60 and 99")]
        public byte ProspectOV { get; set; }

        [Display(Name = "Potential")]
        [Required(ErrorMessage = "Enter A Player Potential")]
        public Potential ProspectPotential { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Enter An Age")]
        [Range(18, 40, ErrorMessage = "Prospect Age Should Be Between 18 and 40")]
        public byte ProspectAge { get; set; }

        [Display(Name = "Initial Rating")]
        [Required(ErrorMessage = "Enter an Initial Rating")]
        [StringLength(6, ErrorMessage = "Initial Rating Should Not Exceed 6 Characters")]
        public string ProspectInitialRating { get; set; }

        public int TeamID { get; set; }

        public virtual Team Team { get; set; }

        public int AttributeID { get; set; }

        public virtual Models.Attribute Attribute { get; set; }
    }
}
