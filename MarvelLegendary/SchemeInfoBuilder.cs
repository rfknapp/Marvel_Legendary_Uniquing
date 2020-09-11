using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary
{
    class SchemeInfoBuilder
    {
        private SchemeInfo _schemeInfo;
        static Random rnd = new Random();

        public SchemeInfoBuilder()
        {
            _schemeInfo = new SchemeInfo
            {
                SchemeName = "",
                SchemeTwists = new List<int> { 8, 8, 8, 8, 8 },
                SetName = GameInfo.Set.Core.GetDescription(),
                CannotBeSolo = false,
                ShardCount = 0,
                IsShardCount = false,
                HasBetryalDeck = false,
                IsSchemeTwistsNextToScheme = false,
                NumberTwistsNextToScheme = 0,
                HasAmbitions = false,
                VillainOfficerCount = 0,
                IsVillainOfficer = false,
                IsMonumentDeck = false,
                IsInfectedDeck = false,
                IncludeNewRecruits = false,
                IncludeMadameHydra = false,

                //Wounds/Bindings
                WoundCount = -1,
                BindingCount = -1,
                CustomWoundCount = false,
                CustomBindingCount = false,
                WoundPerPlayer = new List<int>(),
                WoundsPerPlayer = false,
                BindingPerPlayer = new List<int> { 0, 0, 0, 0, 0 },
                BindingsPerPlayer = false,

                //Bystanders
                BystandersInHeroDeck = new List<int> { 0, 0, 0, 0, 0 },
                Bystanders = new List<int> { 1, 2, 8, 8, 12 },
                BystandersNextToScheme = 0,
                IsBystandersNextToScheme = false,
                IsBystandersInHeroDeck = false,

                //Henchmen
                Henchmen = new List<int> { 1, 1, 1, 2, 2 },
                RequiredHenchmen = new List<string>(),
                NumberHenchmenInHeroDeck = 0,
                HenchmenNextToSchemePerPlayer = new List<int>() { 0, 0, 0, 0, 0 },
                HenchmenNextToScheme = "",
                HasAnnihilationHenchmen = false,
                IsHenchmenInHeroDeck = false,
                IsHenchmenNextToScheme = false,
                IsSmugglerHenchmen = false,

                //Villains
                Villains = new List<int> { 1, 2, 3, 3, 4 },
                RequiredVillains = new List<string>(),
                VillainCardNextToScheme = "",
                IsVillainCardNextToScheme = false,
                IsMonsterPitDeck = false,
                VillainsNotAllowed = new List<string>(),

                //Masterminds
                NumberOfMasterminds = 1,
                NumberExtraMasterminds = 0,
                IsDarkAllianceMastermind = false,
                IsTyrantVillain = false,
                IsSecretWarsMasterminds = false,
                IsTacticsInVillainDeck = false,
                IsExtraMasterminds = false,
                IsWorldWarHulkMasterminds = false,
                IsDrainedMastermind = false,
                IncludeExtraAlwaysLeadsVillains = false,

                //Heroes
                Heroes = new List<int> { 3, 5, 5, 5, 6 },
                RequiredHeroes = new List<string>(),
                HeroesInVillainDeck = new List<string>(),
                IsHeroesInVillainDeck = false,
                IsRandomHeroesInVillainDeck = false,
                NumberOfHeroesInVillainDeck = 0,
                Is3v3 = false,
                Is4v2 = false,
                IncludeHeroTeam = HeroTeam.Unaffiliated,
                NumberOfHeroesFromTeam = 0,
                IsIncludeHeroTeam = false,
                IsHeroNameLimit = false,
                NumberOfHeroesWithNameString = 0,
                CustomNameString = "",
                IsMutationDeck = false,
                IsHulkDeck = false,
                IsSoulsHero = false,
                SoulsHero = null,

                //Sidekicks
                SidekicksInVillainDeck = 0,
                IsSidekickInVillainDeck = false
            };
        }

        public SchemeInfoBuilder SetSchemesNextToTwist(int twistsNextToScheme)
        {
            _schemeInfo.IsSchemeTwistsNextToScheme = true;
            _schemeInfo.NumberTwistsNextToScheme = twistsNextToScheme;
            return this;
        }

        public SchemeInfoBuilder SetSchemeName(string name)
        {
            _schemeInfo.SchemeName = name;
            return this;
        }

        public SchemeInfoBuilder SetSchemeSet(GameInfo.Set set)
        {
            _schemeInfo.SetName = EnumDescription.GetDescription(set);
            return this;
        }

        public SchemeInfoBuilder SetSchemeTwists(int schemeTwists)
        {
            _schemeInfo.SchemeTwists = new List<int> { schemeTwists, schemeTwists, schemeTwists, schemeTwists, schemeTwists };
            return this;
        }

        public SchemeInfoBuilder SetSchemeTwists(List<int> schemeTwists)
        {
            _schemeInfo.SchemeTwists =  schemeTwists;
            return this;
        }

        public SchemeInfoBuilder SetWoundCount(bool woundsPerPlayer, int woundNumber)
        {
            _schemeInfo.WoundsPerPlayer = woundsPerPlayer;

            if(woundsPerPlayer)
            {
                _schemeInfo.WoundPerPlayer = new List<int> { woundNumber, 2 * woundNumber, 3 * woundNumber, 4 * woundNumber, 5 * woundNumber };
            }
            else
            {
                _schemeInfo.WoundPerPlayer = new List<int> { woundNumber, woundNumber, woundNumber, woundNumber, woundNumber };
            }
            _schemeInfo.CustomWoundCount = true;

            return this;
        }

        public SchemeInfoBuilder IncludeNewRecruits()
        {
            _schemeInfo.IncludeNewRecruits = true;
            return this;
        }

        public SchemeInfoBuilder IncludeMadameHydra()
        {
            _schemeInfo.IncludeMadameHydra = true;
            return this;
        }

        public SchemeInfoBuilder SetBindingCount(bool bindingsPerPlayer, int bindingNumber)
        {
            _schemeInfo.BindingsPerPlayer = bindingsPerPlayer;
            if (bindingsPerPlayer)
            {
                _schemeInfo.BindingPerPlayer = new List<int> { bindingNumber, 2 * bindingNumber, 3 * bindingNumber, 4 * bindingNumber, 5 * bindingNumber };
            }
            else
            {
                _schemeInfo.BindingPerPlayer = new List<int> { bindingNumber, bindingNumber, bindingNumber, bindingNumber, bindingNumber };
            }

            _schemeInfo.CustomBindingCount = true;
            return this;
        }

        public SchemeInfoBuilder SetBystanderCount(int bystanderNumber)
        {
            _schemeInfo.Bystanders = new List<int> { bystanderNumber, bystanderNumber, bystanderNumber, bystanderNumber, bystanderNumber };
            return this;
        }

        public SchemeInfoBuilder SetHeroBystanderCount(List<int> bystanderNumbers)
        {
            _schemeInfo.BystandersInHeroDeck = bystanderNumbers;
            _schemeInfo.IsBystandersInHeroDeck = true;
            return this;
        }

        public SchemeInfoBuilder AddAdditionalHenchmen(int additionalHenchmen)
        {
            _schemeInfo.Henchmen = new List<int> { 1+additionalHenchmen, 1 + additionalHenchmen, 1 + additionalHenchmen, 2 + additionalHenchmen, 2 + additionalHenchmen };
            return this;
        }

        public SchemeInfoBuilder SetVillainCardNextToScheme(string villainName)
        {
            _schemeInfo.VillainCardNextToScheme = villainName;
            _schemeInfo.IsVillainCardNextToScheme = true;
            return this;
        }

        public SchemeInfoBuilder NumberHenchmenNextToScheme(int henchmenNumber, string henchmenName)
        {
            _schemeInfo.HenchmenNextToSchemePerPlayer = new List<int> { henchmenNumber, 2 * henchmenNumber, 3 * henchmenNumber, 4 * henchmenNumber, 5 * henchmenNumber };
            _schemeInfo.HenchmenNextToScheme = henchmenName;
            _schemeInfo.IsHenchmenNextToScheme = true;
            return this;
        }

        public SchemeInfoBuilder SetBystandersNextToScheme(int numberOfBystanders)
        {
            _schemeInfo.BystandersNextToScheme = numberOfBystanders;
            _schemeInfo.IsBystandersNextToScheme = true;
            return this;
        }

        public SchemeInfoBuilder HeroesInVillainDeck(string heroName)
        {
            _schemeInfo.HeroesInVillainDeck = new List<string> { heroName };
            _schemeInfo.NumberOfHeroesInVillainDeck = _schemeInfo.HeroesInVillainDeck.Count;
            _schemeInfo.IsHeroesInVillainDeck = true;
            return this;
        }

        public SchemeInfoBuilder HeroesInVillainDeck(int numberOfHeroes)
        {
            _schemeInfo.NumberOfHeroesInVillainDeck = numberOfHeroes;
            _schemeInfo.IsRandomHeroesInVillainDeck = true;
            return this;
        }

        public SchemeInfoBuilder AddAdditionalVillain(int additionalVillains)
        {
            var newVillainList = new List<int>();

            foreach (var villainCount in _schemeInfo.Villains)
            {
                newVillainList.Add(villainCount + additionalVillains);
            }

            _schemeInfo.Villains = newVillainList;
            return this;
        }

        public SchemeInfoBuilder AddAdditionalHero(int additionalHeroes)
        {
            var newHeroList = new List<int>();

            foreach (var heroCount in _schemeInfo.Heroes)
            {
                newHeroList.Add(heroCount + additionalHeroes);
            }

            _schemeInfo.Heroes = newHeroList;
            return this;
        }

        public SchemeInfoBuilder SetVillainCount(List<int> villainList)
        {
            _schemeInfo.Villains = villainList;
            return this;
        }

        public SchemeInfoBuilder AddDarkAllianceMastermind(int additionalMasterminds)
        {
            _schemeInfo.NumberExtraMasterminds = additionalMasterminds;
            _schemeInfo.IsDarkAllianceMastermind = true;
            return this;
        }

        public SchemeInfoBuilder AddExtraMastermind(int additionalMasterminds)
        {
            _schemeInfo.NumberExtraMasterminds = additionalMasterminds;
            _schemeInfo.IsExtraMasterminds = true;
            return this;
        }

        public SchemeInfoBuilder SetTyrantVillains()
        {
            _schemeInfo.NumberExtraMasterminds = 3;
            _schemeInfo.IsExtraMasterminds = true;
            _schemeInfo.IsTyrantVillain = true;
            return this;
        }

        public SchemeInfoBuilder SetSecretWarsMasterminds()
        {
            _schemeInfo.NumberExtraMasterminds = 3;
            _schemeInfo.IsExtraMasterminds = true;
            _schemeInfo.IsSecretWarsMasterminds = true;
            return this;
        }

        public SchemeInfoBuilder SetWorldWarHulkMasterminds()
        {
            _schemeInfo.NumberExtraMasterminds = 3;
            _schemeInfo.IsWorldWarHulkMasterminds = true;
            return this;
        }

        public SchemeInfoBuilder SetDrainedMastermind()
        {
            _schemeInfo.NumberExtraMasterminds = 1;
            _schemeInfo.IsDrainedMastermind = true;
            _schemeInfo.IncludeExtraAlwaysLeadsVillains = true;
            return this;
        }

        public SchemeInfoBuilder SetShardNumber(int shardCount)
        {
            _schemeInfo.ShardCount = shardCount;
            _schemeInfo.IsShardCount = true;
            return this;
        }

        public SchemeInfoBuilder SetRequiredHenchmen(string henchmenGroup)
        {
            _schemeInfo.RequiredHenchmen = new List<string> { henchmenGroup };
            return this;
        }

        public SchemeInfoBuilder SetAnnihilationHenchmen()
        {

            _schemeInfo.HasAnnihilationHenchmen = true;
            return this;
        }

        public SchemeInfoBuilder SidekicksInVillainDeck(int villainSidekicks)
        {
            _schemeInfo.SidekicksInVillainDeck = villainSidekicks;
            _schemeInfo.IsSidekickInVillainDeck = true;
            return this;
        }

        public SchemeInfoBuilder HasBetrayalDeck()
        {
            _schemeInfo.HasBetryalDeck = true;
            return this;
        }

        public SchemeInfoBuilder SetAmbitions()
        {
            _schemeInfo.HasAmbitions = true;
            return this;
        }

        public SchemeInfoBuilder SetVillainOfficers(int villainOfficerCount)
        {
            _schemeInfo.VillainOfficerCount = villainOfficerCount;
            _schemeInfo.IsVillainOfficer = true;
            return this;
        }

        public SchemeInfoBuilder CannotBeSolo()
        {
            _schemeInfo.CannotBeSolo = true;
            return this;
        }

        public SchemeInfoBuilder SetHeroCount(int heroCount)
        {
            _schemeInfo.Heroes = new List<int> { heroCount, heroCount, heroCount, heroCount, heroCount };
            return this;
        }

        public SchemeInfoBuilder AvengersVsXmen()
        {
            _schemeInfo.Is3v3 = true;
            return this;
        }

        public SchemeInfoBuilder Is4v2()
        {
            _schemeInfo.Is4v2 = true;
            return this;
        }

        public SchemeInfoBuilder SetHeroCount(List<int> heroCount)
        {
            _schemeInfo.Heroes = heroCount;
            return this;
        }

        public SchemeInfoBuilder SetRequiredVillains(string villainGroup)
        {
            _schemeInfo.RequiredVillains = new List<string> { villainGroup };
            return this;
        }

        public SchemeInfoBuilder SetRequiredVillains(List<string> villainGroup)
        {
            _schemeInfo.RequiredVillains = villainGroup;
            return this;
        }

        public SchemeInfoBuilder SetRequiredVillains(List<string> villainList, int numberOfVillainsFromGroup)
        {
            int r = rnd.Next(villainList.Count);
            var villain = villainList[r];
            _schemeInfo.RequiredVillains = new List<string> { villain };
            villainList.Remove(villain);
            _schemeInfo.VillainsNotAllowed = villainList;

            return this;
        }

        public SchemeInfoBuilder IncludeHenchmenInHeroDeck(int numberOfHenchmen)
        {
            _schemeInfo.NumberHenchmenInHeroDeck = numberOfHenchmen;
            _schemeInfo.IsHenchmenInHeroDeck = true;
            return this;
        }

        public SchemeInfoBuilder IncludeHeroTeams(int numberOfHeroesFromTeam, HeroTeam heroTeam)
        {
            _schemeInfo.IncludeHeroTeam = heroTeam;
            _schemeInfo.NumberOfHeroesFromTeam = numberOfHeroesFromTeam;
            _schemeInfo.IsIncludeHeroTeam = true;
            return this;
        }

        public SchemeInfoBuilder MastermindTacticsInVillainDeck()
        {
            _schemeInfo.IsTacticsInVillainDeck = true;
            return this;
        }

        public SchemeInfoBuilder MonumentDeck()
        {
            _schemeInfo.IsMonumentDeck = true;
            return this;
        }

        public SchemeInfoBuilder IncludeSmugglerHenchmen()
        {
            _schemeInfo.IsSmugglerHenchmen = true;
            return this;
        }

        public SchemeInfoBuilder IncludeMonsterPitDeck()
        {
            _schemeInfo.IsMonsterPitDeck = true;
            return this;
        }

        public SchemeInfoBuilder IncludeInfectedDeck()
        {
            _schemeInfo.IsInfectedDeck = true;
            return this;
        }

        public SchemeInfoBuilder SetNumberOfHeroWithNameLike(int numberOfHeroesWithNameString, string nameString)
        {
            _schemeInfo.IsHeroNameLimit = true;
            _schemeInfo.NumberOfHeroesWithNameString = numberOfHeroesWithNameString;
            _schemeInfo.CustomNameString = nameString;
            return this;
        }

        public SchemeInfoBuilder IncludeMutationDeck()
        {
            _schemeInfo.IsMutationDeck = true;
            return this;
        }

        public SchemeInfoBuilder IncludeHulkDeck()
        {
            _schemeInfo.IsHulkDeck = true;
            return this;
        }

        public SchemeInfoBuilder SetDarkLoyalty()
        {
            _schemeInfo.IsDarkLoyalty = true;
            return this;
        }

        public SchemeInfoBuilder SetSoulsDeck(string heroName)
        {
            _schemeInfo.SoulsHero = new Hero(heroName);
            _schemeInfo.IsSoulsHero = true;
            return this;
        }

        public SchemeInfo Build()
        {
            return _schemeInfo;
        }
    }
}
