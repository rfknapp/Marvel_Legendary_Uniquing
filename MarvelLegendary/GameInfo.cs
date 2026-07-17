using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using MarvelLegendary.Exclusions;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;

namespace MarvelLegendary
{
    public class GameInfo
    {
        public Mastermind Mastermind { get; set; }
        public List<Mastermind> ExtraMasterminds { get; set; }
        public List<Mastermind> AllMastermindsInGame { get; set; }

        public List<Hero> VillainHeroes { get; set; }
        public List<Hero> RandomVillainHeroes { get; set; }
        public List<Hero> TeamHeroes { get; set; }
        public List<Hero> Heroes { get; set; }
        public List<Hero> SchemeHeroes { get; set; }
        public List<Hero> AllHeroesInGame { get; set; }
        public Hero DarkLoyaltyHero { get; set; }

        public List<Villain> Villains { get; set; }
        public List<Villain> MonsterPitVillains { get; set; }
        public List<Villain> AllVillainsInGame { get; set; }
        public List<Villain> QuantumRealmVillains { get; set; }
        public List<Villain> MarvelZombieVillains { get; set; }
        public List<Villain> SchemeVillains { get; set; }

        public List<Henchmen> HenchmenList { get; set; }
        public List<Henchmen> SchemeHenchmen { get; set; }
        public List<Henchmen> InfectedHenchmen { get; set; }
        public List<Henchmen> AllHenchmenInGame { get; set; }

        public Scheme Scheme { get; set; }
        public UnveiledScheme UnveiledScheme { get; set; }

        public int PlayerCount { get; set; }
        public int MastermindCount { get; set; }
        public int HeroCount { get; set; }
        public int VillainCount { get; set; }
        public int HenchmenCount { get; set; }
        public bool CustomWoundNumber { get; set; }
        public int BindingNumber { get; set; }
        public int WoundNumber { get; set; }
        public int NumberHenchmenNextToScheme { get; set; }
        public bool GameIncludeHeroTeam { get; set; }

        public GameInfo(int players)
        {
            ExtraMasterminds = new List<Mastermind>();
            AllMastermindsInGame = new List<Mastermind>();

            TeamHeroes = new List<Hero>();
            SchemeHeroes = new List<Hero>();
            Heroes = new List<Hero>();
            AllHeroesInGame = new List<Hero>();

            VillainHeroes = new List<Hero>();
            RandomVillainHeroes = new List<Hero>();
            MonsterPitVillains = new List<Villain>();
            MarvelZombieVillains = new List<Villain>();
            Villains = new List<Villain>();
            AllVillainsInGame = new List<Villain>();
            SchemeVillains = new List<Villain>();

            HenchmenList = new List<Henchmen>();
            SchemeHenchmen = new List<Henchmen>();
            InfectedHenchmen = new List<Henchmen>();
            AllHenchmenInGame = new List<Henchmen>();

            PlayerCount = players;
            MastermindCount = 0;
            HeroCount = 0;
            VillainCount = 0;
            HenchmenCount = 0;
            GameIncludeHeroTeam = false;
            UnveiledScheme = null;
        }

        public void SetMastermind(string mastermindName = "", Set ?set = null)
        {
            Mastermind = Mastermind.GetNewMastermind(mastermindName, set);
            AllMastermindsInGame.Add(Mastermind);
        }

        public void SetScheme(string schemeName = null, Set ?set = null)
        {
            if(schemeName == null || set == null )
            {
                Scheme = Scheme.GetNewScheme(PlayerCount, Mastermind, schemeName);
            }
            else
            {
                var schemeInfo = SchemeRepository.All.FirstOrDefault(s => s.SchemeName == schemeName && s.SetName == set);
                Scheme = Scheme.ProcessSchemeInfo(PlayerCount, schemeInfo, Mastermind);
            }

            PlayerCount = Scheme.NumberOfPlayers;

            WoundNumber = GetWoundInformation(Scheme.CustomWoundNumber, Scheme.Wounds);
            CustomWoundNumber = Scheme.CustomWoundNumber;
            BindingNumber = Scheme.SchemeInfo.BindingPerPlayer[PlayerCount - 1];
            NumberHenchmenNextToScheme = Scheme.SchemeInfo.HenchmenNextToSchemePerPlayer[PlayerCount - 1];
        }

        public void SetUnVeiledScheme(string schemeName = "")
        {
            UnveiledScheme = new UnveiledScheme(schemeName);
        }

        public void SetExtraMasterminds()
        {
            for (int i = 0; i < Scheme.SchemeInfo.NumberExtraMasterminds; i++)
            {
                var mastermind = Mastermind.GetExtraMastermind(Scheme, AllMastermindsInGame);
                AllMastermindsInGame.Add(mastermind);
            }
        }

        public void SetVillains(List<Villain> villainNames = null)
        {
            var schemeInfo = Scheme.SchemeInfo;
            var setAsideVillains = new List<Villain>();

            //villainNames is a list of villains to be included in the game
            villainNames = villainNames ?? new List<Villain>();

            //Ultimately I'm refactoring this to use SchemeVillains instead of individual lists like MonsterPitVillains, MarvelZombieVillians, 
            //QuantumRealmVillains, etc. This also is going to change that the schemes will set a SchemeVillainName variable so in the end I can
            //just use one if statement to set up the scheme villains
            if (schemeInfo.IsMonsterPitDeck || schemeInfo.IsQuantumRealmDeck)
            {
                var schemeVillain = schemeInfo.SchemeVillain;
                SchemeVillains.Add(schemeVillain);
                setAsideVillains.Add(schemeVillain);
            }

            if (schemeInfo.IsDrainedMastermind && schemeInfo.DrainedMastermind.DoesLeadVillain)
            {
                var schemeVillain = schemeInfo.DrainedMastermind.LeadsVillain;
                Scheme.RequiredVillains.Add(schemeVillain);
                Scheme.NumberOfVillains += 1;
                villainNames.Add(schemeVillain);
            }

            //This will add all Villain objects matching the VillianName strings
            Villains.AddRange(villainNames);

            Villains = GetVillains(Scheme.NumberOfVillains + Mastermind.MastermindInfo.MastermindNumberOfVillains, Villains, setAsideVillains);

            AllVillainsInGame = Villains.Concat(Scheme.RequiredVillains).ToList();
        }

        public void SetHenchmen(List<Henchmen> henchmenNames = null)
        {
            henchmenNames = henchmenNames ?? new List<Henchmen>();

            //This comes from Cytoplasm Spike Invasion. The Cytoplasm Spikes Henchmen group is set aside,
            //so it isn't going to be one of the henchmen groups
            if (Scheme.SchemeInfo.IsInfectedDeck)
            {
                var henchmen = Henchmen.GetNewHenchmen("Cytoplasm Spikes", Set.Wwh);
                InfectedHenchmen.Add(henchmen);
                SchemeHenchmen.Add(henchmen);
            }

            //This comes from Cage Villains in Power-Suppressing Cells. The Cops henchmen group is set aside,
            //so it isn't going to be one of the henchmen groups
            //Right now it is only Cops but another scheme could use this
            if (Scheme.SchemeInfo.IsHenchmenNextToScheme)
            {
                SchemeHenchmen.Add(Scheme.SchemeInfo.HenchmenNextToScheme);
            }

            //This is for Symbiotic Absorption. It says to add to add the Drained Mastermind's "Always Leads Villians" as an extra villain group.
            //There was no clarification if it just meant villains or also henchmen, so this just assumes that it will add the henchmen if that
            //mastermind is chosen
            if (Scheme.SchemeInfo.IsDrainedMastermind && Scheme.SchemeInfo.DrainedMastermind.DoesLeadHenchmen)
            {
                Scheme.RequiredHenchmen.Add(ExtraMasterminds.First().LeadsHenchmen);
                Scheme.NumberOfHenchmen += 1;
                HenchmenList.Add(ExtraMasterminds.First().LeadsHenchmen);
            }

            //This will add all the henchmen from the list, if it is passed, and ignore duplicates
            HenchmenList = HenchmenList.Concat(henchmenNames).GroupBy(h => h.Id).Select(g => g.First()).ToList();

            //Henchmen is the list of henchmen in the villain deck
            HenchmenList = GetHenchmen(Scheme.NumberOfHenchmen, HenchmenList, SchemeHenchmen);

            //Smuggler adds an extra henchmen to the deck
            //HenchmenInHeroDeck adds a henchmen group to the hero deck
            //AnnihilationHenchmen adds a henchmen group to the KO pile
            //IsXerogenHenchmen adds an extra henchmen to the deck
            //IsVampireNeonaniteHenchmen adds an extra henchmen to the deck
            if (Scheme.SchemeInfo.IsSmugglerHenchmen || Scheme.SchemeInfo.IsHenchmenInHeroDeck || Scheme.SchemeInfo.HasAnnihilationHenchmen
                || Scheme.SchemeInfo.IsXerogenHenchmen || Scheme.SchemeInfo.IsVampireNeonaniteHenchmen)
            {
                var extraHenchmen = GetExtraHenchmen(1, HenchmenList, SchemeHenchmen).FirstOrDefault();
                SchemeHenchmen.Add(extraHenchmen);

                if (Scheme.SchemeInfo.IsSmugglerHenchmen || Scheme.SchemeInfo.IsXerogenHenchmen || Scheme.SchemeInfo.IsVampireNeonaniteHenchmen)
                    HenchmenList.Add(extraHenchmen);
            }

            AllHenchmenInGame = HenchmenList.Concat(SchemeHenchmen)
                .Concat(Scheme.RequiredHenchmen.Select(schemeRequiredHenchmen => schemeRequiredHenchmen))
                             .ToList();
        }

        public void SetHeroes(List<Hero> heroNames = null)
        {
            //
            heroNames = heroNames ?? new List<Hero>();
            if (Scheme.SchemeInfo.Is4v2 || Scheme.SchemeInfo.Is3v3)
            {
                Heroes = GetHeroesByTeam();
            }
            else
            {
                //This will replace heroNames with an empty list of strings if heroNames is null, otherwise use what was passed in
                //And then it will add each item in the list as a new hero to Heroes.
                Heroes.AddRange(heroNames);

                //This will get all the heroes needed for the game
                Heroes = GetHeroes();
            }

            if (Scheme.SchemeInfo.IsRandomHeroesInVillainDeck)
            {
                VillainHeroes = GetHeroes(Scheme.SchemeInfo.NumberOfHeroesInVillainDeck);
                SchemeHeroes.AddRange(from item in VillainHeroes select item);
            }

            if (Scheme.SchemeInfo.IsHeroesInVillainDeck)
            {
                VillainHeroes = Scheme.HeroesInVillainDeck;
                SchemeHeroes.AddRange(from item in VillainHeroes select item);
            }

            if (Scheme.SchemeInfo.IsSoulsHero)
            {
                SchemeHeroes.Add(Scheme.SchemeInfo.SoulsHero);
            }

            if (Scheme.SchemeInfo.IsShrinkTechHero)
            {
                SchemeHeroes.Add(Scheme.SchemeInfo.ShrinkTechHero);
            }

            if (Scheme.SchemeInfo.IsDarkLoyalty)
            {
                DarkLoyaltyHero = Hero.GetNewHero();
                Scheme.SchemeInfo.DarkLoyaltyHero = DarkLoyaltyHero.HeroName;
                SchemeHeroes.Add(DarkLoyaltyHero);
            }

            if (Scheme.SchemeInfo.IsMutationDeck || Scheme.SchemeInfo.IsHulkDeck)
            {
                var newHero = Hero.GetNewHeroByContainsString("Hulk");
                SchemeHeroes.Add(newHero);
            }

            //Sets aside 2 random heroes for the scheme
            if (Scheme.SchemeInfo.IsRoyalWedding)
            {
                var excludedHeroes = new List<string>();
                excludedHeroes.AddRange(from item in SchemeHeroes select item.HeroName);
                excludedHeroes.AddRange(from item in Heroes select item.HeroName);

                //var royalWeddingHeroes = GetHeroes(Scheme.SchemeInfo.RoyalWeddingHeroCount, SchemeHeroes, allHeroes, excludedHeroes, getExclusions);
                var royalWeddingHeroes = GetHeroes(Scheme.SchemeInfo.NumberOfHeroesInVillainDeck);

                SchemeHeroes.AddRange(royalWeddingHeroes);
            }
        }

        #region Villains
        //currentVillains is the list of villains to be included in the villain deck
        //schemeVillains is the list of villains that are set aside but needed to avoid being added to the list of villains
        //to include in the villain deck
        private List<Villain> GetVillains(int numberOfVillains, List<Villain> currentVillains, List<Villain> schemeVillains)
        {
            var schemeInfo = Scheme.SchemeInfo;

            //This will be the list of villians to include in the villain deck
            var villainList = new List<Villain>();

            var allVillains = VillainRepository.AllVillains.ToList();

            //villainsNotAllowed is a list of villains that are not allowed to be in the game based on the scheme
            var villainsNotAllowed = Scheme.SchemeInfo.VillainsNotAllowed;

            //This will remove all the villians not allowed by the scheme from the list of villians to choose from
            allVillains = allVillains.Except(villainsNotAllowed).ToList();

            //This will remove all the villains coming from the schemes from the list of villains to choose from
            allVillains = allVillains.Except(schemeVillains).ToList();

            villainList.AddRange(from item in currentVillains select item);

            //If the game isn't a solo game or the Mastermind has his Leads Villain even when solo
            //if the number of villains required for the player count hasn't been reached
            //then it will add the mastermind leads villain group
            if ((Mastermind.MastermindInfo.AlwaysLeadsOnSolo || PlayerCount > 1) && Mastermind.DoesLeadVillain
                && numberOfVillains > villainList.Count)
            {
                //If the masterminds leads one of the villains brought in through the scheme, it won't be added twice
                var mastermindLeadsVillain = Mastermind.LeadsVillain;
                var mastermindLeadsVillainName = mastermindLeadsVillain.VillainName;
                if (villainList.All(x => x.VillainName != mastermindLeadsVillainName))
                {
                    villainList.Add(mastermindLeadsVillain);
                }
            }

            if (schemeInfo.IsMarvelZombies)
            {
                var allKeywordVillains = Villain.GetListOfVillainsWithKeyword(schemeInfo.ZombieKeyword);
                var mastermindVillain = Mastermind.MastermindInfo.LeadsVillain;

                //If the Mastermind leads a villain group with the "Rise of the Living Dead" keyword then this shouldn't grab another one
                if (allKeywordVillains.Any(v => v.VillainName == mastermindVillain.VillainName))
                {
                    allKeywordVillains.Remove(mastermindVillain);
                    schemeInfo.VillainsNotAllowed = allKeywordVillains;
                }
                else
                {
                    var marvelZombieVillains = GetMarvelZombieVillains(schemeInfo.NumberOfSchemeVillains, schemeInfo.ZombieKeyword);
                    var remainingKeywordVillains = allKeywordVillains.Except(marvelZombieVillains).ToList();

                    //This will add the Marvel Zombie villain to the deck
                    villainList.AddRange(marvelZombieVillains);

                    //This scheme only allows one group with "Rise of the Living Dead" keyword
                    schemeInfo.VillainsNotAllowed = remainingKeywordVillains;
                }
            }

            var currentVillainCount = villainList.Count;
            var numRemainingVillains = numberOfVillains - currentVillainCount;

            //This will return the list if the number of villains coming from the scheme and the mastermind reaches or
            //exceeds the required number of villains for the player count
            if (numRemainingVillains <= 0) return villainList;

            var returnList = new List<Villain>();
            var villainsInGame = villainList;

            while (numRemainingVillains > 0)
            {
                var newVillain = Villain.GetNewVillain(AllMastermindsInGame, Scheme, villainsInGame);
                villainsInGame.Add(newVillain);
                numRemainingVillains--;
            }

            returnList.AddRange(villainsInGame);

            return returnList;
        }

        private List<Villain> GetMarvelZombieVillains(int numberOfVillains, Keywords keyword)
        {
            var returnList = new List<Villain>();
            var keywordVillains = Villain.GetListOfVillainsWithKeyword(keyword);

            while (returnList.Count < numberOfVillains && keywordVillains.Count > 0)
            {
                var zombieVillain = keywordVillains[RandomHelper.Instance.Next(keywordVillains.Count)];
                returnList.Add(zombieVillain);
                keywordVillains.Remove(zombieVillain);
            }

            return returnList;
        }
        #endregion

        #region Henchmen
        //schemeHenchmenGroups is the list of henchmen that are set aside but needed to avoid being added to the list of henchmen
        //to include in the henchmen deck
        //currentHenchmen is the list of henchmen to be included in the henchmen deck
        private List<Henchmen> GetHenchmen(int numberOfHenchmen, List<Henchmen> currentHenchmen, List<Henchmen> schemeHenchmen)
        {
            //var schemeHenchmenNames = schemeHenchmen.Select(x => x.HenchmenName).ToList();

            //This will be the list of henchmen to include in the villain deck
            var henchmenList = new List<Henchmen>();

            var allHenchmen = Henchmen.GetAllHenchmen();

            henchmenList.AddRange(currentHenchmen);

            //This will remove the henchmen that are currently in the game from the pool to choose from
            var idsInGame = new HashSet<int>(schemeHenchmen.Select(h => h.Id));
            var remainingHenchmen = allHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();

            //This will remove all the henchmen that come in from the scheme from the list of henchmen to choose
            allHenchmen = allHenchmen.Except(schemeHenchmen).ToList();

            //If the scheme didn't bring in any henchmen or there are more than one henchmen group included we then include from the Mastermind
            //If this is a solo game, then the Mastermind Leads is ignored
            if ((Mastermind.MastermindInfo.AlwaysLeadsOnSolo || PlayerCount > 1) && Mastermind.DoesLeadHenchmen
                && numberOfHenchmen > henchmenList.Count)
            {
                //If the masterminds leads one of the henchmen brought in through the scheme twist, it won't be added twice
                var mastermindLeadsHenchmen = Mastermind.LeadsHenchmen; 
                if (henchmenList.All(x => x.HenchmenName != mastermindLeadsHenchmen.HenchmenName))
                {
                    henchmenList.Add(mastermindLeadsHenchmen);
                }
            }

            var currentHenchmenCount = henchmenList.Count;
            var numRemainingHenchmen = numberOfHenchmen - currentHenchmenCount;

            //If the required number of henchmen from schemes and mastermind lead abilities hasn't reached the number of
            //henchmen for the player count, it will do this
            if (numRemainingHenchmen <= 0) return henchmenList;

            var returnList = new List<Henchmen>();

            while(numRemainingHenchmen > 0)
            {
                var henchmen = Henchmen.GetNewHenchmen(AllMastermindsInGame, Scheme, Villains, henchmenList);
                henchmenList.Add(henchmen);
                numRemainingHenchmen--;
            }

            return henchmenList;
        }

        //schemeHenchmenGroups is the list of henchmen that are set aside but needed to avoid being added to the list of henchmen
        //to include in the henchmen deck
        //currentHenchmen is the list of henchmen to be included in the henchmen deck
        private List<Henchmen> GetExtraHenchmen(int numberOfHenchmen, List<Henchmen> currentHenchmen, List<Henchmen> schemeHenchmen)
        {
            var schemeHenchmenNames = schemeHenchmen.Select(x => x.HenchmenName).ToList();

            //This will be the list of henchmen to include in the villain deck
            var henchmenList = new List<Henchmen>(currentHenchmen);
            var henchmentToExclude = new List<Henchmen>(currentHenchmen).Concat(schemeHenchmen).ToList();

            var allHenchmen = Henchmen.GetAllHenchmen();

            //This will remove the henchmen that are currently in the game from the pool to choose from
            var idsInGame = new HashSet<int>(henchmenList.Select(h => h.Id));
            var remainingHenchmen = allHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();

            //This will remove the henchmen that are currently brought in by the scheme from the pool to choose from
            idsInGame = new HashSet<int>(schemeHenchmen.Select(h => h.Id));
            remainingHenchmen = remainingHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();

            var currentHenchmenCount = henchmenList.Count;
            var totalHenchmenInGame = Scheme.NumberOfHenchmen + Scheme.SchemeInfo.NumberExtraHenchmenGroups;
            var numRemainingHenchmen = totalHenchmenInGame - currentHenchmenCount;

            //If the required number of henchmen from schemes and mastermind lead abilities hasn't reached the number of
            //henchmen for the player count, it will do this
            if (numRemainingHenchmen <= 0) return henchmenList;

            var returnList = new List<Henchmen>();

            while (numRemainingHenchmen > 0)
            {
                var henchmen = Henchmen.GetNewHenchmen(AllMastermindsInGame, Scheme, Villains, henchmentToExclude);
                returnList.Add(henchmen);
                numRemainingHenchmen--;
            }

            return returnList;
        }
        #endregion

        #region Heroes
        private List<Hero> GetHeroes(int numberOfHeroes)
        {
            var heroList = new List<Hero>(AllHeroesInGame); // Initialize heroList with AllHeroesInGame
            
            for (int i = 0; i < numberOfHeroes; i++)
            {
                //var newHero = Hero.GetNewHero(exclusionHeroes);
                var newHero = Hero.GetNewHero(AllMastermindsInGame, Scheme, AllVillainsInGame, AllHenchmenInGame, heroList);
                heroList.Add(newHero);
            }

            return heroList;
        }

        public List<Hero> GetHeroes(List<Hero> heroList, List<Hero> availableHeroes, List<Hero> excludedHeroes, IGetExclusions getExclusions)
        {
            return new List<Hero>();
        }

        public List<Hero> GetHeroes(int numberOfHeroes, HeroTeam heroTeam, List<Hero> availableHeroes, List<Hero> exclusionList = null)
        {
            var returnList = new List<Hero>();

            //This will set exclusion list to an empty list if it comes in as null otherwise it will keep the passed in value
            exclusionList = exclusionList ?? new List<Hero>();

            var currentHeroCount = 0;
            if (numberOfHeroes > currentHeroCount)
            {
                for (int i = currentHeroCount; i < numberOfHeroes; i++)
                {
                    var newHero = Hero.GetNewHeroByTeam(heroTeam, availableHeroes);
                    var heroName = newHero.HeroName;

                    //This will make sure there are no duplicate heroes in the list.
                    //It will also ignore any heroes that will be included in the villain deck
                    while (returnList.Any(x => x.HeroName == heroName && x.SetName == newHero.SetName) || exclusionList.Any(h => h.HeroName == heroName && h.SetName == newHero.SetName))
                    {
                        newHero = Hero.GetNewHeroByTeam(heroTeam, availableHeroes);
                        heroName = newHero.HeroName;
                    }

                    returnList.Add(newHero);
                }
            }

            return returnList;
        }

        private List<Hero> getHeroesNotInTeam(int numberOfHeroes, HeroTeam heroTeam, List<string> exclusionList, List<string> availableHeroes)
        {
            var returnList = new List<Hero>();

            var currentHeroCount = 0;
            if (numberOfHeroes > currentHeroCount)
            {
                for (int i = currentHeroCount; i < numberOfHeroes; i++)
                {
                    var newHero = Hero.GetNewHeroByTeam(heroTeam, availableHeroes, false);
                    var heroName = newHero.HeroName;
                    //This will make sure there are no duplicate heroes in the list. It will also ignore any heroes that will be included in the villain deck
                    while (returnList.Any(x => x.HeroName == heroName || exclusionList.Contains(heroName)))
                    {
                        newHero = Hero.GetNewHeroByTeam(heroTeam, availableHeroes, false);
                        heroName = newHero.HeroName;
                    }
                    returnList.Add(newHero);
                }
            }

            return returnList;
        }

        //exclusionHeroes is the list of heroes that have played with the mastermind
        //this will be going away, but documenting what it is
        private List<Hero> GetHeroesByTeam()
        {
            var availableHeroes = Hero.GetAllHeroes();
            var returnList = new List<Hero>();
            var usedHeroTeams = new List<HeroTeam>();

            var teamOneMembers = Scheme.SchemeInfo.Is4v2 ? 4 : 3;
            var teamTwoMembers = Scheme.SchemeInfo.Is4v2 ? 2 : 3;

            //Need to get all the hero teams that have teamOneMembers or more members
            var heroTeam = Hero.GetHeroTeam(teamOneMembers);
            usedHeroTeams.Add(heroTeam);

            //Need to get teamOneMembers heroes from that team
            //TODO
            //Need to bring in Scheme, Mastermind, Villain, and Henchmen data to unique these heroes
            //It could mean that another team would have to be chosen if not enough heroes are left in the team after uniquing
            //or choosing any hero once it gets down to not enough heroes left in the team after uniquing
            //Can use the function IsEnoughHeroes to check for this
            var heroList = GetHeroes(teamOneMembers, heroTeam, availableHeroes);

            returnList.AddRange(heroList);
            AllHeroesInGame.AddRange(heroList);

            //Need to get another hero team that has teamTwoMembers or more members
            heroTeam = Hero.GetHeroTeam(teamTwoMembers, usedHeroTeams);
            usedHeroTeams.Add(heroTeam);

            //Need to get teamTwoMembers heroes from that team
            heroList = GetHeroes(teamTwoMembers, heroTeam, availableHeroes);

            returnList.AddRange(heroList);
            AllHeroesInGame.AddRange(heroList);

            return returnList;
        }

        //This is kept around until I can fix the integration tests
        public List<Hero> GetHeroes(List<Hero> exclusionHeroes, List<Hero> schemeHeroGroups, List<Hero> currentHeroes, List<Hero> availableHeroes, IGetExclusions getExclusions)
        {
            var heroList = new List<Hero>(currentHeroes);

            //The second variable will only be a non-zero number if Alchemax Executives are the Mastermind
            var numberOfHeroes = Scheme.NumberOfHeroes + Mastermind.MastermindInfo.MastermindNumberOfHeroes;

            var excludedHeroes = new List<Hero>();

            AllHeroesInGame.AddRange(from item in schemeHeroGroups select item);
            AllHeroesInGame.AddRange(from item in currentHeroes select item);

            excludedHeroes.AddRange(from item in schemeHeroGroups select item);
            excludedHeroes.AddRange(from item in currentHeroes select item);

            if (Scheme.SchemeInfo.IsIncludeHeroTeam)
            {
                var heroGroupList = GetHeroes(Scheme.SchemeInfo.NumberOfHeroesFromTeam, Scheme.SchemeInfo.IncludeHeroTeam, availableHeroes, exclusionHeroes);
                foreach (var heroGroup in heroGroupList)
                {
                    SchemeHeroes.Add(heroGroup);
                    heroList.Add(heroGroup);
                    AllHeroesInGame.Add(heroGroup);
                }
            }

            if (Scheme.SchemeInfo.IsHeroNameLimit)
            {
                var nameLimitHeroes = GetNameLimitHeroes(Scheme.SchemeInfo.CustomNameString, availableHeroes, Scheme.SchemeInfo.NumberOfHeroesWithNameString);
                heroList.AddRange(from item in nameLimitHeroes select item);
                AllHeroesInGame.AddRange(from item in nameLimitHeroes select item);

                var heroesToExclude = Hero.GetListOfHeroes().Where(x => x.Contains(Scheme.SchemeInfo.CustomNameString)).ToList();
                excludedHeroes.AddRange(from item in nameLimitHeroes select item);
            }

            var currentHeroCount = heroList.Count;
            var numRemainingHeroes = numberOfHeroes - currentHeroCount;

            //If the required number of heroes from schemes reached the number of heroes for the player count, it will return the list
            if (numRemainingHeroes <= 0) return heroList;

            var returnList = new List<Hero>();
            var heroes = new List<Hero>();

            for (int i = numRemainingHeroes; i > 0; i--)
            {
                heroes.Concat(GetHeroes(heroList, availableHeroes, excludedHeroes, getExclusions));
            }

            //heroes = GetHeroes(numRemainingHeroes, heroList, availableHeroes, excludedHeroes, getExclusions);

            //returnList.AddRange(from item in heroesInGame select new Hero(item));
            returnList.AddRange(heroes);

            return returnList;
        }
        
        public List<Hero> GetHeroes()
        {
            var heroList = new List<Hero>(Heroes);
            var availableHeroes = Hero.GetAllHeroes();

            //Alchemax Executives are the only Mastermind that brings in hero groups
            var numberOfHeroes = Scheme.NumberOfHeroes + Mastermind.MastermindInfo.MastermindNumberOfHeroes;

            //This will add all the current heroes brought in from the scheme to a list of heroes in the game.
            AllHeroesInGame.AddRange(from item in SchemeHeroes select item);
            //There are some instances where specific heroes are set to be in the game, this is mainly used for testing.
            //If those heroes are set, this will add them to a list of heroes in the game
            AllHeroesInGame.AddRange(from item in heroList select item);
            //This will create a list of hero names for all heroes in the game
            var excludedHeroes = new List<Hero>(AllHeroesInGame);

            //This will remove any of the excluded heroes from the list of heroes to choose from
            var idsInGame = new HashSet<int>(excludedHeroes.Select(h => h.HeroInfo.Id));
            var remainingHeroes = availableHeroes.Where(h => !idsInGame.Contains(h.HeroInfo.Id)).ToList();

            //Schemes like "Distract the Hero" say to include X number of heroes from a certain team.
            //Should use GetHeroes(teamOneMembers, heroTeam, availableHeroes) to get the hero

            var test = new List<string>();
            if (Scheme.SchemeInfo.IsIncludeHeroTeam)
            {
                var heroGroupList = GetHeroes(Scheme.SchemeInfo.NumberOfHeroesFromTeam, Scheme.SchemeInfo.IncludeHeroTeam, remainingHeroes);
                foreach (var heroGroup in heroGroupList)
                {
                    SchemeHeroes.Add(heroGroup);
                    heroList.Add(heroGroup);
                    
                    AllHeroesInGame.Add(heroGroup);
                    excludedHeroes.Add(heroGroup);
                }
            }

            //This will remove any of the newly added heroes from the list of heroes to choose from
            availableHeroes = availableHeroes.Except(excludedHeroes).ToList();

            //Schemes like "Deadpool Writes a Scheme" say to include a certain hero.
            //Schemes like "Fall of the Hulks" say to use only a certain number of heroes with a certain keyword in their name
            //If the second is true I need to add a method to remove all others that weren't chosen from the main list
            //Possibly create a method to remove heroes that match a certain string
            if (Scheme.SchemeInfo.IsHeroNameLimit)
            {
                //This will add the number of heroes with the name to the game
                var nameLimitHeroes = GetNameLimitHeroes(Scheme.SchemeInfo.CustomNameString, availableHeroes, Scheme.SchemeInfo.NumberOfHeroesWithNameString);
                heroList.AddRange(from item in nameLimitHeroes select item);
                AllHeroesInGame.AddRange(from item in nameLimitHeroes select item);

                //This will remove all other heroes with that name. Deadpool Writes a Scheme doesn't require only the one Deadpool hero
                if (Scheme.SchemeInfo.SchemeName != "Deadpool Writes a Scheme")
                {
                    var heroesToExclude = Hero.ConvertToHeroList(HeroRepository.All.Where(x => x.HeroName.Contains(Scheme.SchemeInfo.CustomNameString)).ToList());
                    excludedHeroes.AddRange(heroesToExclude);
                }
            }

            var currentHeroCount = heroList.Count;
            var numRemainingHeroes = numberOfHeroes - currentHeroCount;

            //If the required number of heroes from schemes reached the number of heroes for the player count, it will return the list
            if (numRemainingHeroes <= 0) return heroList;
            
            //This will chose the remaining heroes for the setup. It will then add them to the main heroList
            var heroes = GetHeroes(numRemainingHeroes, heroList);
            heroList.AddRange(heroes);

            return heroList;
        }

        //This will replace the above function. It will be the main function to get the list of heroes for the game.
        public List<Hero> GetHeroes(int heroCount, List<Hero> heroList)
        {
            var heroesInGame = new List<Hero>(heroList);

            //If the required number of heroes from schemes hasn't reached the number of heroes for the player count, it will do this
            for (int i = 0; i < heroCount; i++)
            {
                var hero = Hero.GetNewHero(AllMastermindsInGame, Scheme, AllVillainsInGame, AllHenchmenInGame, heroList);
                heroesInGame.Add(hero);
            }

            return heroesInGame;
        }

        public List<Hero> GetNameLimitHeroes(string heroNamePart, List<Hero> availableHeroes, int numberOfHeroes)
        {
            var heroList = new List<Hero>();
            var listOfHeroes = Hero.GetAllHeroesByNamePart(heroNamePart, availableHeroes);
            var newListOfHeroes = new List<Hero>(listOfHeroes);
            for (int i = 0; i < numberOfHeroes; i++)
            {
                var hero = newListOfHeroes[RandomHelper.Instance.Next(newListOfHeroes.Count)];
                heroList.Add(hero);
                newListOfHeroes.Remove(hero);
            }
            return heroList;
        }
        #endregion

        private int GetWoundInformation(bool customWoundNumber, List<int> wounds)
        {
            if (customWoundNumber)
            {
                return wounds[PlayerCount - 1];
            }
            else
            {
                return -1;
            }
        }
    }

    public static class EnumDescription
    { 
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name == null) return null;

            var field = type.GetField(name);
            if (field == null) return null;

            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attr?.Description;
        }
    }

}
