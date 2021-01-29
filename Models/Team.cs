using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ProspectManagementTool.Models
{
    public class Team
    {
        public Team()
        {
            this.Prospects = new HashSet<Prospect>();
        }
        public int ID { get; set; }

        [Display(Name ="Team")]
        public string HockeyTeam
        {
            get
            {
                return CityName + " " + TeamName;
            }
        }

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

        public ICollection<Draft> Drafts { get; set; }
    }
}