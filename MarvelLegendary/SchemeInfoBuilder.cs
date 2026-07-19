using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;
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
                Id = 0,
                DuplicateSchemeIds = null,
                IsDuplicate = false,
                NumberOfPlayers = 0,
                IsEnabled = true,

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
                RequiredHenchmen = new List<Henchmen>(),
                NumberHenchmenInHeroDeck = 0,
                HenchmenNextToSchemePerPlayer = new List<int>() { 0, 0, 0, 0, 0 },
                HenchmenNextToScheme = null,
                HasAnnihilationHenchmen = false,
                IsHenchmenInHeroDeck = false,
                IsHenchmenNextToScheme = false,
                IsSmugglerHenchmen = false,
                IsXerogenHenchmen = false,
                IsVampireNeonaniteHenchmen = false,
                NumberExtraHenchmenGroups = 0,

                //Villains
                Villains = new List<int> { 1, 2, 3, 3, 4 },
                RequiredVillains = new List<Villain>(),
                VillainCardNextToScheme = "",
                IsVillainCardNextToScheme = false,
                IsMonsterPitDeck = false,
                IsQuantumRealmDeck = false,
                VillainsNotAllowed = new List<Villain>(),
                IsMarvelZombies = false,
                MarvelZombiesGroup = new List<string>(),
                SchemeVillain = null,
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
                IsEnshroudedMastermind = false,

                //Heroes
                Heroes = new List<int> { 3, 5, 5, 5, 6 },
                RequiredHeroes = new List<string>(),
                HeroesInVillainDeck = new List<Hero>(),
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
                IsRandomHeroCardsInVillainDeck = false,
                NumberRandomHeroCardsInVillainDeck = 0,
                NoDuplicates = false,

                //Sidekicks
                SidekicksInVillainDeck = 0,
                IsSidekickInVillainDeck = false,

                //Officers
                OfficersNextToMastermind = 0,
                IsOfficersNextToMastermind = false
            };
        }

        public SchemeInfoBuilder Duplicates(List<int> duplicateCardIds)
        {
            _schemeInfo.DuplicateSchemeIds = duplicateCardIds;
            _schemeInfo.IsDuplicate = true;
            return this;
        }

        public SchemeInfoBuilder SchemeId(int id)
        {
            _schemeInfo.Id = id;
            return this;
        }

        public SchemeInfoBuilder SetTwistsNextToScheme(int twistsNextToScheme)
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
            _schemeInfo.SchemeVillain = Villain.GetNewVillain("Monsters Unleashed", Set.Champions);
            return this;
        }

        public SchemeInfoBuilder IncludeQuantumRealmDeck()
        {
            _schemeInfo.IsQuantumRealmDeck = true;
            _schemeInfo.SchemeVillain = Villain.GetNewVillain("Quantum Realm", Set.AntmanWasp);
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

        public SchemeInfoBuilder SetSoulsDeck(string heroName, Set set)
        {
            _schemeInfo.SoulsHero = Hero.GetNewHero(heroName, set);
            _schemeInfo.IsSoulsHero = true;
            return this;
        }

        public SchemeInfoBuilder SetShrinkTechDeck()
        {
            var keywordHeroes = Hero.GetListOfHeroesWithKeyword(Keywords.Size);

            var randomIndex = RandomHelper.Instance.Next(keywordHeroes.Count);
            _schemeInfo.ShrinkTechHero = keywordHeroes[randomIndex];
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

        public SchemeInfoBuilder NumberHenchmenNextToScheme(int henchmenNumber, string henchmenName, Set setName)
        {
            _schemeInfo.HenchmenNextToSchemePerPlayer = Enumerable.Range(1, 5).Select(i => i * henchmenNumber).ToList();
            _schemeInfo.HenchmenNextToScheme = Henchmen.GetNewHenchmen(henchmenName, setName);
            _schemeInfo.IsHenchmenNextToScheme = true;
            return this;
        }

        public SchemeInfoBuilder SetRequiredHenchmen(Henchmen henchmen)
        {
            _schemeInfo.RequiredHenchmen = new List<Henchmen> { henchmen };
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

        public SchemeInfoBuilder HeroesInVillainDeck(string heroName, Set set)
        {
            _schemeInfo.HeroesInVillainDeck = new List<Hero> { Hero.GetNewHero(heroName, set) };
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
            _schemeInfo.NumberOfHeroesInVillainDeck = numberOfHeroesWithNameString;
            _schemeInfo.IsRandomHeroesInVillainDeck = true;

            var heroesInfo = Hero.GetAllHeroesInfo();
            var namedHeroes = Hero.ConvertToHeroList(heroesInfo.Where(x => x.HeroName.Contains(nameString)).ToList());

            while (_schemeInfo.HeroesInVillainDeck.Count < numberOfHeroesWithNameString && namedHeroes.Count > 0)
            {
                var randomIndex = RandomHelper.Instance.Next(namedHeroes.Count);
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

        public SchemeInfoBuilder SetRequiredVillains(string villainGroup, Set villainSetName)
        {
            _schemeInfo.RequiredVillains = new List<Villain> { Villain.GetNewVillain(villainGroup, villainSetName) };
            return this;
        }

        public SchemeInfoBuilder SetRequiredVillains(List<Villain> villainGroup)
        {
            _schemeInfo.RequiredVillains = villainGroup;
            return this;
        }

        public SchemeInfoBuilder SetOneButNotOther(List<Villain> villainList)
        {
            var villain = villainList[RandomHelper.Instance.Next(villainList.Count)];
            _schemeInfo.RequiredVillains.Add(villain);
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
            _schemeInfo.NoDuplicates = true;
            return this;
        }

        public SchemeInfoBuilder SetEnshroudedGame()
        {
            _schemeInfo.IsEnshroudedMastermind = true;
            _schemeInfo.OfficersNextToMastermind = 3;
            _schemeInfo.IsOfficersNextToMastermind = true;
            return this;
        }

        public SchemeInfoBuilder SetRandomHeroCardsInVillainDeck(int randomHeroCardsInVillainDeck)
        {
            _schemeInfo.IsRandomHeroCardsInVillainDeck = true;
            _schemeInfo.NumberRandomHeroCardsInVillainDeck = randomHeroCardsInVillainDeck;
            return this;
        }

        public SchemeInfoBuilder Disable()
        {
            _schemeInfo.IsEnabled = false;
            return this;
        }
    }
}
