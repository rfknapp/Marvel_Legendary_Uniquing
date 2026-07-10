using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelLegendary.Enums;

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
                SetName = Set.Core,
                RequiredVillain = "",
                LeadsVillain = "",
                LeadsHenchmen = "",
                AlwaysLeadsOnSolo = false,
                DoesLeadHenchmen = false,
                DoesLeadVillain = false,
                IncludeBindings = false,
                IncludeMadameHydra = false,
                IncludeHorrors = false,
                IsZombieSoloVillain = false,
                RequireVillain = false,
                IncludeExtraHero = false,
                MastermindNumberOfHeroes = 0
            };
        }

        public MastermindInfoBuilder SetMastermindName(string name)
        {
            _mastermindInfo.MastermindName = name;
            return this;
        }

        public MastermindInfoBuilder SetMastermindSet(Set set)
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
            var henchmenList = Henchmen.GetListOfHenchmen();
            var henchmenNames = (henchmenList.Where(item => item.Contains(henchmenKind))).ToList();

            _mastermindInfo.LeadsHenchmen = henchmenNames[new Random().Next(henchmenNames.Count)];
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder LeadsVillainsByKind(List<string> villainKinds)
        {
            var villainsList = Villain.GetListOfVillains();
            var listOfVillains = new List<string>();
            foreach (var villainKind in villainKinds)
            {
                listOfVillains.AddRange((villainsList.Where(item => item.Contains(villainKind))).ToList());
            }

            _mastermindInfo.LeadsVillain = listOfVillains[new Random().Next(listOfVillains.Count)];
            _mastermindInfo.DoesLeadVillain = true;
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

        public MastermindInfoBuilder AlwaysLeadsOnSolo()
        {
            _mastermindInfo.AlwaysLeadsOnSolo = true;
            return this;
        }

        public MastermindInfoBuilder SetZombieSoloVillains()
        {
            _mastermindInfo.IsZombieSoloVillain = true;
            return this;
        }

        public MastermindInfoBuilder IncludeExtraHero()
        {
            _mastermindInfo.IncludeExtraHero = true;
            _mastermindInfo.MastermindNumberOfHeroes = 1;
            return this;
        }

        public MastermindInfoBuilder IncludeExtraVillain()
        {
            _mastermindInfo.IncludeExtraVillain = true;
            _mastermindInfo.MastermindNumberOfVillains = 1;
            return this;
        }

        public MastermindInfo Build()
        {
            return _mastermindInfo;
        }
    }
}
