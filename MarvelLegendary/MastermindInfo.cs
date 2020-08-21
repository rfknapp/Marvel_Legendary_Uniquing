using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary
{
    public class MastermindInfo
    {
        public string MastermindName { get; set; }
        public GameInfo.Set SetName { get; set; }
        public string LeadsVillain { get; set; }
        public string LeadsHenchmen { get; set; }
        public bool DoesLeadVillain { get; set; }
        public bool DoesLeadHenchmen { get; set; }
        public bool IncludeBindings { get; set; }
        public bool IncludeMadameHydra { get; set; }
        public bool IncludeHorrors { get; set; }
    }
}
