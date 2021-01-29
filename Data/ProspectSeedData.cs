using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProspectManagementTool.Models;

namespace ProspectManagementTool.Data
{
    public static class ProspectSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProspectManagementContext(
                    serviceProvider.GetRequiredService<DbContextOptions<ProspectManagementContext>>()))
            {
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                        new Team
                        {
                            CityName = "Anaheim",
                            TeamName = "Ducks"
                        },
                        new Team
                        {
                            CityName = "Boston",
                            TeamName = "Bruins"
                        },
                        new Team
                        {
                            CityName = "Buffalo",
                            TeamName = "Sabres"
                        },
                        new Team
                        {
                            CityName = "Calgary",
                            TeamName = "Flames"
                        },
                        new Team
                        {
                            CityName = "Carolina",
                            TeamName = "Hurricanes"
                        },
                        new Team
                        {
                            CityName = "Chicago",
                            TeamName = "Blackhawks"
                        });
                    context.SaveChanges();
                }
                if (!context.Attributes.Any())
                {
                    context.Attributes.AddRange(
                        new Models.Attribute
                        {
                            AttributeName = "Sniper"
                        },
                        new Models.Attribute
                        {
                            AttributeName = "Power Forward"
                        },
                        new Models.Attribute
                        {
                            AttributeName = "Defensive Forward"
                        },
                        new Models.Attribute
                        {
                            AttributeName = "2-Way Forward"
                        },
                        new Models.Attribute
                        {
                            AttributeName = "Sack of extra chromosomes"
                        });
                    context.SaveChanges();
                }
                if (!context.Prospects.Any())
                {
                    context.Prospects.AddRange(
                        new Prospect
                        {
                            ProspectName = "Erik Gustafsson",
                            ProspectPosition = Position.C,
                            ProspectOV = 72,
                            ProspectPotential = Potential.A,
                            ProspectAge = 20,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Anaheim" && p.TeamName == "Ducks").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Defensive Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Mackenzie Skapski",
                            ProspectPosition = Position.LW,
                            ProspectPotential = Potential.B,
                            ProspectOV = 67,
                            ProspectAge = 19,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Anaheim" && p.TeamName == "Ducks").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Power Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Tom Wilson",
                            ProspectOV = 72,
                            ProspectPosition = Position.RW,
                            ProspectPotential = Potential.B,
                            ProspectAge = 20,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Boston" && p.TeamName == "Bruins").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Defensive Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Brock Nelson",
                            ProspectOV = 75,
                            ProspectPosition = Position.C,
                            ProspectPotential = Potential.A,
                            ProspectAge = 22,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Boston" && p.TeamName == "Bruins").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Power Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Ryan  Spooner",
                            ProspectOV = 78,
                            ProspectPosition = Position.W,
                            ProspectPotential = Potential.A,
                            ProspectAge = 22,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Calgary" && p.TeamName == "Flames").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Darnell Nurse",
                            ProspectOV = 74,
                            ProspectPosition = Position.D,
                            ProspectPotential = Potential.B,
                            ProspectAge = 19,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Calgary" && p.TeamName == "Flames").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Defensive Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Torey Krug",
                            ProspectOV = 77,
                            ProspectPosition = Position.C,
                            ProspectPotential = Potential.B,
                            ProspectAge = 23,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Carolina" && p.TeamName == "Hurricanes").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "2-Way Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Danny Gouden",
                            ProspectOV = 27,
                            ProspectPosition = Position.G,
                            ProspectPotential = Potential.C,
                            ProspectAge = 23,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Chicago" && p.TeamName == "Blackhawks").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sack of extra chromosomes").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Nikita Zadorov",
                            ProspectOV = 74,
                            ProspectPosition = Position.D,
                            ProspectPotential = Potential.A,
                            ProspectAge = 20,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Carolina" && p.TeamName == "Hurricanes").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Defensive Forward").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Bill Randolph",
                            ProspectOV = 55,
                            ProspectPosition = Position.G,
                            ProspectPotential = Potential.C,
                            ProspectAge = 21,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Chicago" && p.TeamName == "Blackhawks").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sack of extra chromosomes").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Jack Mehoff",
                            ProspectOV = 42,
                            ProspectAge = 20,
                            ProspectPosition = Position.G,
                            ProspectPotential = Potential.C,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Chicago" && p.TeamName == "Blackhawks").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sack of extra chromosomes").ID
                        },
                        new Prospect
                        {
                            ProspectName = "Tomas Jurco",
                            ProspectOV = 77,
                            ProspectPosition = Position.W,
                            ProspectPotential = Potential.A,
                            ProspectAge = 20,
                            ProspectInitialRating = "68/B",
                            TeamID = context.Teams.FirstOrDefault(p => p.CityName == "Boston" && p.TeamName == "Bruins").ID,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID
                        });
                    context.SaveChanges();
                }
                if (!context.Drafts.Any())
                {
                    context.Drafts.AddRange(
                        new Draft
                        {
                            DraftName = "Nico Hischier",
                            DraftOV = 75,
                            DraftPosition = Position.C,
                            DraftPotential = Potential.A,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID,
                            DraftAge = 18,
                            DraftInitialRating = "75/A"
                        },
                        new Draft
                        {
                            DraftName = "Nolan Patrick",
                            DraftOV = 74,
                            DraftPosition = Position.C,
                            DraftPotential = Potential.A,
                            DraftAge = 18,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID,
                            DraftInitialRating = "74/A"
                        },
                        new Draft
                        {
                            DraftName = "Miro Heiskanen",
                            DraftOV = 71,
                            DraftPosition = Position.D,
                            DraftPotential = Potential.B,
                            DraftAge = 18,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID,
                            DraftInitialRating = "71/B"
                        },
                        new Draft
                        {
                            DraftName = "Cale Makar",
                            DraftOV = 73,
                            DraftPosition = Position.D,
                            DraftPotential = Potential.A,
                            DraftAge = 18,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID,
                            DraftInitialRating = "73/A"
                        }, new Draft
                        {
                            DraftName = "Elias Pettersson",
                            DraftOV = 74,
                            DraftPosition = Position.C,
                            DraftPotential = Potential.A,
                            DraftAge = 18,
                            AttributeID = context.Attributes.FirstOrDefault(a => a.AttributeName == "Sniper").ID,
                            DraftInitialRating = "74/A"
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
