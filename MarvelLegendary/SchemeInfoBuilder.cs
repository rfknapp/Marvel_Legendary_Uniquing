using MarvelLegendary.Enums;
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
                SetName = Set.Core,
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
                IncludeHorrors = false,
                IsRoyalWedding = false,
                isVeiled = false,

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
                AdditionalBystanders = 0,

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
                IsXerogenHenchmen = false,
                IsVampireNeonaniteHenchmen = false,
                NumberExtraHenchmenGroups = 0,

                //Villains
                Villains = new List<int> { 1, 2, 3, 3, 4 },
                RequiredVillains = new List<string>(),
                VillainCardNextToScheme = "",
                IsVillainCardNextToScheme = false,
                IsMonsterPitDeck = false,
                IsQuantumRealmDeck = false,
                VillainsNotAllowed = new List<string>(),
                IsMarvelZombies = false,
                MarvelZombiesGroup = new List<string>(),
                SchemeVillainName = "",
                ZombieKeyword = Keywords.None,
                NumberOfSchemeVillains = 0,

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
                IsShrinkTechHero = false,
                ShrinkTechHero = null,
                RoyalWeddingHeroCount = 0,
                IsLovedOne = false,

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

        public SchemeInfoBuilder SetVeiledScheme()
        {
            _schemeInfo.isVeiled = true;
            return this;
        }

        public SchemeInfoBuilder SetSchemeSet(Set set)
        {
            _schemeInfo.SetName = set;
            return this;
        }

        public SchemeInfoBuilder SetSchemeTwists(int schemeTwists)
        {
            _schemeInfo.SchemeTwists = Enumerable.Repeat(schemeTwists, 5).ToList();
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
                _schemeInfo.WoundPerPlayer = Enumerable.Range(1, 5).Select(i => i * woundNumber).ToList();
            }
            else
            {
                _schemeInfo.WoundPerPlayer = Enumerable.Repeat(woundNumber, 5).ToList();
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
                _schemeInfo.BindingPerPlayer = Enumerable.Range(1, 5).Select(i => i * bindingNumber).ToList();
            }
            else
            {
                _schemeInfo.BindingPerPlayer = Enumerable.Repeat(bindingNumber, 5).ToList();
            }

            _schemeInfo.CustomBindingCount = true;
            return this;
        }

        public SchemeInfoBuilder SetBystanderCount(int bystanderNumber)
        {
            _schemeInfo.Bystanders = Enumerable.Repeat(bystanderNumber, 5).ToList();
            return this;
        }

        public SchemeInfoBuilder SetBystanderCount(List<int> bystanderCount)
        {
            _schemeInfo.Bystanders = bystanderCount;
            return this;
        }

        public SchemeInfoBuilder SetHeroBystanderCount(List<int> bystanderNumbers)
        {
            _schemeInfo.BystandersInHeroDeck = bystanderNumbers;
            _schemeInfo.IsBystandersInHeroDeck = true;
            return this;
        }

        public SchemeInfoBuilder SetBystandersNextToScheme(int numberOfBystanders)
        {
            _schemeInfo.BystandersNextToScheme = numberOfBystanders;
            _schemeInfo.IsBystandersNextToScheme = true;
            return this;
        }

        public SchemeInfoBuilder IncreaseBystanders(int additionalBystanders)
        {
            _schemeInfo.AdditionalBystanders = additionalBystanders;
            return this;
        }

        public SchemeInfoBuilder SetShardNumber(int shardCount)
        {
            _schemeInfo.ShardCount = shardCount;
            _schemeInfo.IsShardCount = true;
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

        public SchemeInfoBuilder CannotBeSolo()
        {
            _schemeInfo.CannotBeSolo = true;
            return this;
        }

        public SchemeInfoBuilder MonumentDeck()
        {
            _schemeInfo.IsMonumentDeck = true;
            return this;
        }

        public SchemeInfoBuilder IncludeMonsterPitDeck()
        {
            _schemeInfo.IsMonsterPitDeck = true;
            _schemeInfo.SchemeVillainName = "Monsters Unleashed";
            return this;
        }

        public SchemeInfoBuilder IncludeQuantumRealmDeck()
        {
            _schemeInfo.IsQuantumRealmDeck = true;
            _schemeInfo.SchemeVillainName = "Quantum Realm";
            return this;
        }

        public SchemeInfoBuilder IncludeInfectedDeck()
        {
            _schemeInfo.IsInfectedDeck = true;
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
            _schemeInfo.SoulsHero = new Hero().GetNewHero(heroName);
            _schemeInfo.IsSoulsHero = true;
            return this;
        }

        public SchemeInfoBuilder SetShrinkTechDeck()
        {
            var keywordHeroes = new Hero().GetListOfHeroesWithKeyword(Keywords.Size);

            var randomIndex = rnd.Next(keywordHeroes.Count);
            _schemeInfo.ShrinkTechHero = new Hero().GetNewHero(keywordHeroes[randomIndex]);
            _schemeInfo.IsShrinkTechHero = true;
            return this;
        }

        public SchemeInfoBuilder IncludeHorrors()
        {
            _schemeInfo.IncludeHorrors = true;
            return this;
        }

        public SchemeInfoBuilder AddXerogenHenchmen()
        {
            _schemeInfo.IsXerogenHenchmen = true;
            _schemeInfo.NumberExtraHenchmenGroups = 1;
            return this;
        }

        public SchemeInfoBuilder SetRoyalWedding()
        {
            _schemeInfo.IsRoyalWedding = true;
            _schemeInfo.RoyalWeddingHeroCount = 2;
            return this;
        }

        public SchemeInfoBuilder AddAdditionalHenchmen(int additionalHenchmen)
        {
            _schemeInfo.Henchmen = _schemeInfo.Henchmen.Select(henchmenCount => henchmenCount + additionalHenchmen).ToList();
            return this;
        }

        public SchemeInfoBuilder NumberHenchmenNextToScheme(int henchmenNumber, string henchmenName)
        {
            _schemeInfo.HenchmenNextToSchemePerPlayer = Enumerable.Range(1, 5).Select(i => i * henchmenNumber).ToList();
            _schemeInfo.HenchmenNextToScheme = henchmenName;
            _schemeInfo.IsHenchmenNextToScheme = true;
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

        public SchemeInfoBuilder IncludeSmugglerHenchmen()
        {
            _schemeInfo.IsSmugglerHenchmen = true;
            _schemeInfo.NumberExtraHenchmenGroups = 1;
            return this;
        }

        public SchemeInfoBuilder DoubleHenchmen()
        {
            _schemeInfo.Henchmen = _schemeInfo.Henchmen.Select(x => x * 2).ToList();
            return this;
        }

        public SchemeInfoBuilder SetVampireNaniteHenchmen()
        {
            _schemeInfo.IsVampireNeonaniteHenchmen = true;
            _schemeInfo.NumberExtraHenchmenGroups = 1;
            return this;
        }

        public SchemeInfoBuilder SetVillainCardNextToScheme(string villainName)
        {
            _schemeInfo.VillainCardNextToScheme = villainName;
            _schemeInfo.IsVillainCardNextToScheme = true;
            return this;
        }

        public SchemeInfoBuilder AddAdditionalVillain(int additionalVillains)
        {
            _schemeInfo.Villains = _schemeInfo.Villains.Select(villainCount => villainCount + additionalVillains).ToList();
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

        public SchemeInfoBuilder HeroesInVillainDeckWithNameLike(int numberOfHeroesWithNameString, string nameString)
        {
            var heroes = new Hero().GetListOfHeroes();
            var namedHeroes = heroes.Where(x => x.Contains(nameString)).ToList();

            while (_schemeInfo.HeroesInVillainDeck.Count < numberOfHeroesWithNameString && namedHeroes.Count > 0)
            {
                var randomIndex = rnd.Next(namedHeroes.Count);
                _schemeInfo.HeroesInVillainDeck.Add(namedHeroes[randomIndex]);
                namedHeroes.RemoveAt(randomIndex);
            }
            _schemeInfo.IsHeroesInVillainDeck = true;

            return this;
        }

        public SchemeInfoBuilder SetVillainCount(List<int> villainList)
        {
            _schemeInfo.Villains = villainList;
            return this;
        }

        public SchemeInfoBuilder SetTyrantVillains()
        {
            _schemeInfo.NumberExtraMasterminds = 3;
            _schemeInfo.IsExtraMasterminds = true;
            _schemeInfo.IsTyrantVillain = true;
            return this;
        }

        public SchemeInfoBuilder SidekicksInVillainDeck(int villainSidekicks)
        {
            _schemeInfo.SidekicksInVillainDeck = villainSidekicks;
            _schemeInfo.IsSidekickInVillainDeck = true;
            return this;
        }

        public SchemeInfoBuilder SetVillainOfficers(int villainOfficerCount)
        {
            _schemeInfo.VillainOfficerCount = villainOfficerCount;
            _schemeInfo.IsVillainOfficer = true;
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
            var randomIndex = rnd.Next(villainList.Count);
            var villain = villainList[randomIndex];
            _schemeInfo.RequiredVillains = new List<string> { villain };
            villainList.Remove(villain);
            _schemeInfo.VillainsNotAllowed = villainList;

            return this;
        }

        public SchemeInfoBuilder MastermindTacticsInVillainDeck()
        {
            _schemeInfo.IsTacticsInVillainDeck = true;
            return this;
        }

        public SchemeInfoBuilder DoubleVillains()
        {
            _schemeInfo.Villains = _schemeInfo.Villains.Select(x => x * 2).ToList();
            return this;
        }

        public SchemeInfoBuilder MarvelZombieVillains(int numberOfVillains, Keywords keyword)
        {
            _schemeInfo.NumberOfSchemeVillains = numberOfVillains;
            _schemeInfo.IsMarvelZombies = true;
            _schemeInfo.ZombieKeyword = keyword;
            return this;
        }

        public SchemeInfoBuilder SetLovedOnesDeck()
        {
            _schemeInfo.IsLovedOne = true;
            return this;
        }

        public SchemeInfoBuilder AddAdditionalHero(int additionalHeroes)
        {
            _schemeInfo.Heroes = _schemeInfo.Heroes.Select(heroCount => heroCount + additionalHeroes).ToList();
            return this;
        }

        public SchemeInfoBuilder SetHeroCount(int heroCount)
        {
            _schemeInfo.Heroes = Enumerable.Repeat(heroCount, 5).ToList();
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

        public SchemeInfoBuilder SetNumberOfHeroWithNameLike(int numberOfHeroesWithNameString, string nameString)
        {
            _schemeInfo.IsHeroNameLimit = true;
            _schemeInfo.NumberOfHeroesWithNameString = numberOfHeroesWithNameString;
            _schemeInfo.CustomNameString = nameString;
            return this;
        }

        public SchemeInfoBuilder SetRequiredHeroes(string heroGroup)
        {
            _schemeInfo.RequiredHeroes = new List<string> { heroGroup };
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
            return this;
        }

        public SchemeInfo Build()
        {
            return _schemeInfo;
        }

        public SchemeInfoBuilder NoDuplicates()
        {
            //TODO - work on this
            return this;
        }
    }
}
