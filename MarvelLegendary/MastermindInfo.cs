using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelLegendary.Enums;

namespace MarvelLegendary
{
    public class MastermindInfo
    {
        public string MastermindName { get; set; }
        public Set SetName { get; set; }
        public string RequiredVillain { get; set; }
        public string LeadsVillain { get; set; }
        public string LeadsHenchmen { get; set; }
        public bool AlwaysLeadsOnSolo { get; set; }
        public bool DoesLeadVillain { get; set; }
        public bool DoesLeadHenchmen { get; set; }
        public bool IncludeBindings { get; set; }
        public bool IncludeMadameHydra { get; set; }
        public bool IncludeHorrors { get; set; }
        public bool IsZombieSoloVillain { get; set; }
        public bool RequireVillain { get; set; }
        public bool IncludeExtraHero { get; set; }
        public int MastermindNumberOfHeroes { get; set; }
        public bool IncludeExtraVillain { get; internal set; }
        public int MastermindNumberOfVillains { get; internal set; }
    }
}
