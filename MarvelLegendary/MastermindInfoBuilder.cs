using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;

namespace MarvelLegendary
{
    class MastermindInfoBuilder
    {
        private MastermindInfo _mastermindInfo;

        public MastermindInfoBuilder()
        {
            _mastermindInfo = new MastermindInfo
            {
                Id = 0,
                Name = "",
                SetName = Set.Core,
                RequiredVillain = "",
                LeadsVillain = null,
                LeadsHenchmen = null,
                AlwaysLeadsOnSolo = false,
                DoesLeadHenchmen = false,
                DoesLeadVillain = false,
                IncludeBindings = false,
                IncludeMadameHydra = false,
                IncludeHorrors = false,
                IsZombieSoloVillain = false,
                RequireVillain = false,
                IncludeExtraHero = false,
                MastermindNumberOfHeroes = 0,
                DuplicateMastermindIds = null,
                IsDuplicate = false,
                IsEnabled = true
            };
        }

        public MastermindInfoBuilder MastermindId(int id)
        {
            _mastermindInfo.Id = id;
            return this;
        }

        public MastermindInfoBuilder SetMastermindName(string name)
        {
            _mastermindInfo.Name = name;
            return this;
        }

        public MastermindInfoBuilder SetMastermindSet(Set set)
        {
            _mastermindInfo.SetName = set;
            return this;
        }

        public MastermindInfoBuilder LeadsVillain(string villainName, Set setName)
        {
            if (!villainName.Equals(""))
            {
                _mastermindInfo.LeadsVillain = Villain.GetNewVillain(villainName, setName);
                _mastermindInfo.DoesLeadVillain = true;
            }
            return this;
        }

        public MastermindInfoBuilder LeadsHenchmen(string henchmenName, Set set)
        {
            var henchmen = Henchmen.GetNewHenchmen(henchmenName, set);
            _mastermindInfo.LeadsHenchmen = henchmen;
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder LeadsHenchmen(List<Henchmen> henchmenNames)
        {
            _mastermindInfo.LeadsHenchmen = henchmenNames[RandomHelper.Instance.Next(henchmenNames.Count)];
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder LeadsHenchmenByKind(List<string> henchmenKind)
        {
            var henchmenList = HenchmenRepository.All.ToList();
            var henchmenInfoList = new List<HenchmenInfo>();

            foreach (var henchmanKind in henchmenKind)
            {
                henchmenInfoList = henchmenInfoList.Concat(henchmenList.Where(item => item.Name.Contains(henchmanKind)).ToList()).ToList();
            }

            _mastermindInfo.LeadsHenchmen = Henchmen.GetNewHenchmen(henchmenInfoList[RandomHelper.Instance.Next(henchmenInfoList.Count)]);
            _mastermindInfo.DoesLeadHenchmen = true;
            return this;
        }

        public MastermindInfoBuilder LeadsVillainsByKind(List<string> villainKinds)
        {
            var villainsList = VillainRepository.AllVillains.ToList();
            var listOfVillains = new List<Villain>();
            foreach (var villainKind in villainKinds)
            {
                listOfVillains.AddRange((villainsList.Where(item => item.Name.Contains(villainKind))).ToList());
            }

            _mastermindInfo.LeadsVillain = listOfVillains[RandomHelper.Instance.Next(listOfVillains.Count)];
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

        public MastermindInfoBuilder Duplicates(List<int> duplicateCardIds)
        {
            _mastermindInfo.DuplicateMastermindIds = duplicateCardIds;
            _mastermindInfo.IsDuplicate = true;
            return this;
        }

        public MastermindInfoBuilder Disable()
        {
            _mastermindInfo.IsEnabled = false;
            return this;
        }

        public MastermindInfo Build()
        {
            return _mastermindInfo;
        }
    }
}
