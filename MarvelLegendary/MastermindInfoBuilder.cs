using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary
{
    class MastermindInfoBuilder
    {
        private MastermindInfo _mastermindInfo;

        public MastermindInfoBuilder()
        {
            _mastermindInfo = new MastermindInfo
            {
                MastermindName = "",
                SetName = GameInfo.Set.Core,
                LeadsVillain = "",
                LeadsHenchmen = "",
                DoesLeadHenchmen = false,
                DoesLeadVillain = false,
                IncludeBindings = false,
                IncludeMadameHydra = false,
                IncludeHorrors = false
            };
        }

        public MastermindInfoBuilder SetMastermindName(string name)
        {
            _mastermindInfo.MastermindName = name;
            return this;
        }

        public MastermindInfoBuilder SetMastermindSet(GameInfo.Set set)
        {
            _mastermindInfo.SetName = set;
            return this;
        }

        public MastermindInfoBuilder LeadsVillain(string villainName)
        {
            if (!villainName.Equals(""))
            {
                _mastermindInfo.LeadsVillain = villainName;
                _mastermindInfo.DoesLeadVillain = true;
            }
            return this;
        }

        public MastermindInfoBuilder LeadsHenchmen(string henchmenName)
        {
            _mastermindInfo.LeadsHenchmen = henchmenName;
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder LeadsHenchmen(List<string> henchmenNames)
        {
            _mastermindInfo.LeadsHenchmen = henchmenNames[new Random().Next(henchmenNames.Count)];
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder LeadsHenchmenByKind(string henchmenKind)
        {
            var henchmenList = new Henchmen().GetListOfHenchmen();
            var henchmenNames = (henchmenList.Where(item => item.Contains(henchmenKind))).ToList();

            _mastermindInfo.LeadsHenchmen = henchmenNames[new Random().Next(henchmenNames.Count)];
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder IncludeBindings()
        {
            _mastermindInfo.IncludeBindings = true;
            return this;
        }

        public MastermindInfoBuilder IncludeMadameHydra()
        {
            _mastermindInfo.IncludeMadameHydra = true;
            return this;
        }

        public MastermindInfoBuilder IncludeHorrors()
        {
            _mastermindInfo.IncludeHorrors = true;
            return this;
        }

        public MastermindInfo Build()
        {
            return _mastermindInfo;
        }
    }
}
