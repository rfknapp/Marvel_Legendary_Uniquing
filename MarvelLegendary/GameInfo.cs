using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using MarvelLegendary.Exclusions;
using MarvelLegendary.DetermineLists;
using System.Xml.Linq;

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

        public List<Henchmen> Henchmen { get; set; }
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

            Henchmen = new List<Henchmen>();
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

        public void SetMastermind(string mastermindName = "")
        {
            Mastermind = new Mastermind().GetNewMastermind(mastermindName);
            AllMastermindsInGame.Add(Mastermind);
        }

        public void SetScheme(string schemeName = "")
        {
            Scheme = new Scheme().GetNewScheme(PlayerCount, Mastermind, schemeName);
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
            ExtraMasterminds = new Mastermind().GetExtraMasterminds(Scheme, Mastermind);
            AllMastermindsInGame.Concat(ExtraMasterminds).ToList();
        }

        public void SetVillains(List<string> villainNames = null)
        {
            //villainNames is a list of villains to be included in the game
            if (villainNames == null)
            {
                villainNames = new List<string>();
            }

            var setAsideVillains = new List<string>();

            if (Scheme.SchemeInfo.IsMonsterPitDeck)
            {
                var monsterPitVillain = new Villain().GetNewVillain("Monsters Unleashed");
                MonsterPitVillains.Add(monsterPitVillain);
                setAsideVillains.Add(monsterPitVillain.VillainName);
            }

            if (Scheme.SchemeInfo.IsMarvelZombies)
            {
                var marvelZombieVillain = new Villain().GetNewVillain(Scheme.SchemeInfo.MarvelZombiesGroup.FirstOrDefault());
                MarvelZombieVillains.Add(marvelZombieVillain);
                setAsideVillains.Add(marvelZombieVillain.VillainName);
            }

            if (Scheme.SchemeInfo.IsQuantumRealmDeck)
            {
                var quantumRealmVillain = new Villain().GetNewVillain("Quantum Realm");
                QuantumRealmVillains.Add(quantumRealmVillain);
                setAsideVillains.Add(quantumRealmVillain.VillainName);
            }

            if (Scheme.SchemeInfo.IsDrainedMastermind && Scheme.SchemeInfo.DrainedMastermind.DoesLeadVillain)
            {
                var schemeVillain = Scheme.SchemeInfo.DrainedMastermind.LeadsVillain;
                Scheme.RequiredVillains.Add(schemeVillain);
                Scheme.NumberOfVillains += 1;
                villainNames.Add(schemeVillain);
            }

            //This will add all Villain objects matching the VillianName strings
            Villains.AddRange(from item in villainNames select new Villain().GetNewVillain(item));

            Villains = GetVillains(Scheme.NumberOfVillains, Villains, setAsideVillains);

            AllVillainsInGame = new List<Villain>(Villains);

            foreach (var schemeRequiredVillain in Scheme.RequiredVillains)
            {
                AllVillainsInGame.Add(new Villain().GetNewVillain(schemeRequiredVillain));
            }
        }

        public void SetHenchmen(List<string> henchmenNames = null)
        {
            if (henchmenNames == null)
            {
                henchmenNames = new List<string>();
            }

            if (Scheme.SchemeInfo.IsInfectedDeck)
            {
                InfectedHenchmen.Add(new Henchmen().GetNewHenchmen("Cytoplasm Spikes"));
                SchemeHenchmen.Add(new Henchmen().GetNewHenchmen("Cytoplasm Spikes"));
            }

            if (Scheme.SchemeInfo.IsHenchmenNextToScheme)
            {
                SchemeHenchmen.Add(new Henchmen().GetNewHenchmen(Scheme.SchemeInfo.HenchmenNextToScheme));
            }

            //This is for Symbiotic Absorption
            if (Scheme.SchemeInfo.IsDrainedMastermind && Scheme.SchemeInfo.DrainedMastermind.DoesLeadHenchmen)
            {
                Scheme.RequiredHenchmen.Add(ExtraMasterminds.First().LeadsHenchmen);
                Scheme.NumberOfHenchmen += 1;
                Henchmen.Add(new Henchmen().GetNewHenchmen(ExtraMasterminds.First().LeadsHenchmen));
            }

            if (henchmenNames != null)
            {
                Henchmen.AddRange(from item in henchmenNames select new Henchmen().GetNewHenchmen(item));
            }

            //Henchmen is the list of henchmen in the villain deck
            Henchmen = GetHenchmen(Scheme.NumberOfHenchmen, Henchmen, SchemeHenchmen);

            //Smuggler adds an extra henchmen to the deck
            //HenchmenInHeroDeck adds a henchmen group to the hero deck
            //AnnihilationHenchmen adds a henchmen group to the KO pile
            //IsXerogenHenchmen adds an extra henchmen to the deck
            //IsVampireNeonaniteHenchmen adds an extra henchmen to the deck
            if (Scheme.SchemeInfo.IsSmugglerHenchmen || Scheme.SchemeInfo.IsHenchmenInHeroDeck || Scheme.SchemeInfo.HasAnnihilationHenchmen
                || Scheme.SchemeInfo.IsXerogenHenchmen || Scheme.SchemeInfo.IsVampireNeonaniteHenchmen)
            {
                var extraHenchmen = GetHenchmen(1, Henchmen, SchemeHenchmen).FirstOrDefault();
                SchemeHenchmen.Add(extraHenchmen);

                if (Scheme.SchemeInfo.IsSmugglerHenchmen || Scheme.SchemeInfo.IsXerogenHenchmen || Scheme.SchemeInfo.IsVampireNeonaniteHenchmen)
                    Henchmen.Add(extraHenchmen);
            }

            AllHenchmenInGame = new List<Henchmen>(Henchmen).Concat(SchemeHenchmen).ToList();
            foreach (var schemeRequiredHenchmen in Scheme.RequiredHenchmen)
            {
                AllHenchmenInGame.Add(new Henchmen().GetNewHenchmen(schemeRequiredHenchmen));
            }
        }

        public void SetHeroes(List<string> heroNames = null)
        {
            var hero = new Hero();
            
            if (Scheme.SchemeInfo.Is4v2 || Scheme.SchemeInfo.Is3v3)
            {
                Heroes = GetHeroesByTeam();
            }
            else
            {
                //This will replace heroNames with an empty list of strings if heroNames is null, otherwise use what was passed in
                //And then it will add each item in the list as a new hero to Heroes.
                Heroes.AddRange((heroNames ?? new List<string>()).Select(item => hero.GetNewHero(item)));

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
                VillainHeroes = getHeroes(Scheme.HeroesInVillainDeck);
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
                DarkLoyaltyHero = hero.GetNewHero();
                Scheme.SchemeInfo.DarkLoyaltyHero = DarkLoyaltyHero.HeroName;
                SchemeHeroes.Add(DarkLoyaltyHero);
            }

            if (Scheme.SchemeInfo.IsMutationDeck || Scheme.SchemeInfo.IsHulkDeck)
            {
                var newHero = hero.GetNewHeroByContainsString("Hulk");
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

                SchemeHeroes.AddRange(from item in royalWeddingHeroes select hero.GetNewHero(item.HeroName));
            }
        }

        #region Masterminds
        private static List<Mastermind> GetMasterminds(Scheme scheme, Mastermind mainMastermind)
        {
            var returnList = new List<Mastermind>();
            var mastermindsInGame = new List<string> { mainMastermind.MastermindName };
            var extraMasterminds = new List<string>();
            var mastermindList = new Mastermind().GetListOfMasterminds();

            for (int i = 0; i < scheme.SchemeInfo.NumberExtraMasterminds; i++)
            {
                var remainingMasterminds = mastermindList.Except(mastermindsInGame).ToList();
                var exclusions = DetermineMastermindList(mastermindsInGame);
                var mastermindsToChooseFrom = remainingMasterminds.Except(exclusions).ToList();

                var newMastermind = mastermindsToChooseFrom[new Random().Next(mastermindsToChooseFrom.Count)];
                mastermindsInGame.Add(newMastermind);
                extraMasterminds.Add(newMastermind);
            }

            returnList.AddRange(from item in extraMasterminds
                                select new Mastermind().GetNewMastermind(item));

            if (scheme.SchemeInfo.IsDrainedMastermind)
            {
                scheme.SchemeInfo.DrainedMastermind = new Mastermind().GetNewMastermind(extraMasterminds.First());
            }

            return returnList;
        }

        private static List<string> DetermineMastermindList(List<string> mastermindsInGame)
        {
            var mastermindsToExcludeWith = new List<string>(mastermindsInGame);
            var getExclusions = new GetExclusions();

            for (int i = mastermindsToExcludeWith.Count - 1; i >= 0; i--)
            {
                var test = getExclusions.GetMastermindByMastermindExclusions(mastermindsToExcludeWith);
                var mastermindList = new Mastermind().GetListOfMasterminds();
                var compareList = mastermindList.Except(mastermindsInGame).Except(test).ToList();
                if (compareList.Count > 0)
                {
                    return test;
                }
                mastermindsToExcludeWith.RemoveAt(i);
            }

            return new List<string>();
        }
        #endregion

        #region Villains
        //currentVillains is the list of villains to be included in the villain deck
        //schemeVillains is the list of villains that are set aside but needed to avoid being added to the list of villains
        //to include in the villain deck
        private List<Villain> GetVillains(int numberOfVillains, List<Villain> currentVillains, List<string> schemeVillains)
        {            
            //This will be the list of villians to include in the villain deck
            var villainList = new List<Villain>();

            var allVillains = new Villain().GetListOfVillains();

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
                var mastermindLeadsVillain = new Villain().GetNewVillain(Mastermind.LeadsVillain);
                var mastermindLeadsVillainName = mastermindLeadsVillain.VillainName;
                if (villainList.All(x => x.VillainName != mastermindLeadsVillainName))
                {
                    villainList.Add(mastermindLeadsVillain);
                }
            }

            var currentVillainCount = villainList.Count;
            var numRemainingVillains = numberOfVillains - currentVillainCount;

            //This will return the list if the number of villains coming from the scheme and the mastermind reaches or
            //exceeds the required number of villains for the player count
            if (numRemainingVillains <= 0) return villainList;

            var returnList = new List<Villain>();
            var villainsInGame = new List<string>(villainList.Select(x => x.VillainName));

            while (numRemainingVillains > 0)
            {
                var villain = new Villain().GetNewVillain(AllMastermindsInGame, Scheme, villainsInGame);
                villainsInGame.Add(villain.VillainName);
                numRemainingVillains--;
            }

            returnList.AddRange(from item in villainsInGame
                                select new Villain().GetNewVillain(item));

            return returnList;
        }
        #endregion

        #region Henchmen
        //schemeHenchmenGroups is the list of henchmen that are set aside but needed to avoid being added to the list of henchmen
        //to include in the henchmen deck
        //currentHenchmen is the list of henchmen to be included in the henchmen deck
        private List<Henchmen> GetHenchmen(int numberOfHenchmen, List<Henchmen> currentHenchmen, List<Henchmen> schemeHenchmen)
        {
            var schemeHenchmenNames = schemeHenchmen.Select(x => x.HenchmenName).ToList();

            //This will be the list of henchmen to include in the villain deck
            var henchmenList = new List<Henchmen>();

            var allHenchmen = new Henchmen().GetListOfHenchmen();

            henchmenList.AddRange(from item in currentHenchmen select item);

            //This will remove all the henchmen that come in from the scheme from the list of henchmen to choose
            allHenchmen = allHenchmen.Except(schemeHenchmenNames).ToList();

            //If the scheme didn't bring in any henchmen or there are more than one henchmen group included we then include from the Mastermind
            //If this is a solo game, then the Mastermind Leads is ignored
            if ((Mastermind.MastermindInfo.AlwaysLeadsOnSolo || PlayerCount > 1) && Mastermind.DoesLeadHenchmen
                && numberOfHenchmen > henchmenList.Count)
            {
                //If the masterminds leads one of the henchmen brought in through the scheme twist, it won't be added twice
                var mastermindLeadsHenchmen = new Henchmen().GetNewHenchmen(Mastermind.LeadsHenchmen);
                var mastermindLeadsHenchmenName = mastermindLeadsHenchmen.HenchmenName;
                if (henchmenList.All(x => x.HenchmenName != mastermindLeadsHenchmenName))
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
            var henchmenInGame = new List<string>(henchmenList.Select(x => x.HenchmenName));

            while(numRemainingHenchmen > 0)
            {
                var henchmen = new Henchmen().GetNewHenchmen(AllMastermindsInGame, Scheme, Villains, henchmenInGame);
                henchmenInGame.Add(henchmen.HenchmenName);
                numRemainingHenchmen--;
            }

            returnList.AddRange(from item in henchmenInGame
                                select new Henchmen().GetNewHenchmen(item));

            return returnList;
        }
        #endregion

        #region Heroes
        private List<Hero> GetHeroes(int numberOfHeroes)
        {
            var heroList = new List<Hero>(AllHeroesInGame); // Initialize heroList with AllHeroesInGame
            var hero = new Hero(); // Create a new hero instance

            for (int i = 0; i < numberOfHeroes; i++)
            {
                //var newHero = hero.GetNewHero(exclusionHeroes);
                var newHero = hero.GetNewHero(AllMastermindsInGame, Scheme, AllVillainsInGame, AllHenchmenInGame, heroList);
                heroList.Add(newHero);
            }

            return heroList;
        }

        private List<Hero> getHeroes(List<string> heroesInVillainDeck)
        {
            var newHero = new Hero();
            var heroList = new List<Hero>();

            foreach (var hero in heroesInVillainDeck)
            {
                heroList.Add(newHero.GetNewHero(hero));
            }

            return heroList;
        }

        private List<Hero> GetHeroes(int numberOfHeroes, HeroTeam heroTeam, List<string> availableHeroes, List<string> exclusionList = null)
        {
            var hero = new Hero();
            var returnList = new List<Hero>();

            //This will set exclusion list to an empty list if it comes in as null otherwise it will keep the passed in value
            exclusionList = exclusionList ?? new List<string>();

            var currentHeroCount = 0;
            if (numberOfHeroes > currentHeroCount)
            {
                for (int i = currentHeroCount; i < numberOfHeroes; i++)
                {
                    var newHero = hero.GetNewHeroByTeam(heroTeam, availableHeroes);
                    var heroName = newHero.HeroName;
                    
                    //This will make sure there are no duplicate heroes in the list.
                    //It will also ignore any heroes that will be included in the villain deck
                    while (returnList.Any(x => x.HeroName == heroName) || exclusionList.Contains(heroName))
                    {
                        newHero = hero.GetNewHeroByTeam(heroTeam, availableHeroes);
                        heroName = newHero.HeroName;
                    }

                    returnList.Add(newHero);
                }
            }

            return returnList;
        }

        private List<Hero> getHeroesNotInTeam(int numberOfHeroes, HeroTeam heroTeam, List<string> exclusionList, List<string> availableHeroes)
        {
            var hero = new Hero();
            var returnList = new List<Hero>();

            var currentHeroCount = 0;
            if (numberOfHeroes > currentHeroCount)
            {
                for (int i = currentHeroCount; i < numberOfHeroes; i++)
                {
                    var newHero = hero.GetNewHeroByTeam(heroTeam, availableHeroes, false);
                    var heroName = newHero.HeroName;
                    //This will make sure there are no duplicate heroes in the list. It will also ignore any heroes that will be included in the villain deck
                    while (returnList.Any(x => x.HeroName == heroName || exclusionList.Contains(heroName)))
                    {
                        newHero = hero.GetNewHeroByTeam(heroTeam, availableHeroes, false);
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
            var hero = new Hero();
            var availableHeroes = hero.GetListOfHeroes();
            var returnList = new List<Hero>();
            var usedHeroTeams = new List<HeroTeam>();

            var teamOneMembers = Scheme.SchemeInfo.Is4v2 ? 4 : 3;
            var teamTwoMembers = Scheme.SchemeInfo.Is4v2 ? 2 : 3;

            //Need to get all the hero teams that have teamOneMembers or more members
            var heroTeam = hero.GetHeroTeam(teamOneMembers);
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
            heroTeam = hero.GetHeroTeam(teamTwoMembers, usedHeroTeams);
            usedHeroTeams.Add(heroTeam);

            //Need to get teamTwoMembers heroes from that team
            heroList = GetHeroes(teamTwoMembers, heroTeam, availableHeroes);

            returnList.AddRange(heroList);
            AllHeroesInGame.AddRange(heroList);

            return returnList;
        }

        //This is kept around until I can fix the integration tests
        public List<Hero> GetHeroes(List<string> exclusionHeroes, List<Hero> schemeHeroGroups, List<Hero> currentHeroes, List<string> availableHeroes, IGetExclusions getExclusions)
        {
            var hero = new Hero();
            var heroList = new List<Hero>(currentHeroes);

            //The second variable will only be a non-zero number if Alchemax Executives are the Mastermind
            var numberOfHeroes = Scheme.NumberOfHeroes + Mastermind.MastermindInfo.MastermindNumberOfHeroes;

            var excludedHeroes = new List<string>();

            AllHeroesInGame.AddRange(from item in schemeHeroGroups select item);
            AllHeroesInGame.AddRange(from item in currentHeroes select item);

            excludedHeroes.AddRange(from item in schemeHeroGroups select item.HeroName);
            excludedHeroes.AddRange(from item in currentHeroes select item.HeroName);

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

                var heroesToExclude = hero.GetListOfHeroes().Where(x => x.Contains(Scheme.SchemeInfo.CustomNameString)).ToList();
                excludedHeroes.AddRange(from item in nameLimitHeroes select item.HeroName);
            }

            var currentHeroCount = heroList.Count;
            var numRemainingHeroes = numberOfHeroes - currentHeroCount;

            //If the required number of heroes from schemes reached the number of heroes for the player count, it will return the list
            if (numRemainingHeroes <= 0) return heroList;

            var returnList = new List<Hero>();
            var heroes = GetHeroes(numRemainingHeroes, heroList, availableHeroes, excludedHeroes, getExclusions);

            //returnList.AddRange(from item in heroesInGame select new Hero(item));
            returnList.AddRange(from item in heroes select hero.GetNewHero(item));

            return returnList;
        }
        
        public List<Hero> GetHeroes()
        {
            var hero = new Hero();
            var excludedHeroes = new List<string>();
            var heroList = new List<Hero>(Heroes);
            var availableHeroes = hero.GetListOfHeroes();

            //Alchemax Executives are the only Mastermind that brings in hero groups
            var numberOfHeroes = Scheme.NumberOfHeroes + Mastermind.MastermindInfo.MastermindNumberOfHeroes;

            //This will add all the current heroes brought in from the scheme to a list of heroes in the game.
            AllHeroesInGame.AddRange(from item in SchemeHeroes select item);
            //There are some instances where specific heroes are set to be in the game, this is mainly used for testing.
            //If those heroes are set, this will add them to a list of heroes in the game
            AllHeroesInGame.AddRange(from item in heroList select item);
            //This will create a list of hero names for all heroes in the game
            excludedHeroes.AddRange(from item in AllHeroesInGame select item.HeroName);

            //This will remove any of the excluded heroes from the list of heroes to choose from
            availableHeroes = availableHeroes.Except(excludedHeroes).ToList();

            //Schemes like "Distract the Hero" say to include X number of heroes from a certain team.
            //Should use GetHeroes(teamOneMembers, heroTeam, availableHeroes) to get the hero
            if (Scheme.SchemeInfo.IsIncludeHeroTeam)
            {
                var heroGroupList = GetHeroes(Scheme.SchemeInfo.NumberOfHeroesFromTeam, Scheme.SchemeInfo.IncludeHeroTeam, availableHeroes);
                foreach (var heroGroup in heroGroupList)
                {
                    SchemeHeroes.Add(heroGroup);
                    heroList.Add(heroGroup);
                    
                    AllHeroesInGame.Add(heroGroup);
                    excludedHeroes.Add(heroGroup.HeroName);
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
                    var heroesToExclude = hero.GetListOfHeroes().Where(x => x.Contains(Scheme.SchemeInfo.CustomNameString)).ToList();
                    excludedHeroes.AddRange(from item in nameLimitHeroes select item.HeroName);
                }
            }

            var currentHeroCount = heroList.Count;
            var numRemainingHeroes = numberOfHeroes - currentHeroCount;

            //If the required number of heroes from schemes reached the number of heroes for the player count, it will return the list
            if (numRemainingHeroes <= 0) return heroList;
            
            //This will chose the remaining heroes for the setup. It will then add them to the main heroList
            var heroes = GetHeroes(numRemainingHeroes, heroList);
            heroList.AddRange(from item in heroes select hero.GetNewHero(item));

            return heroList;
        }

        public List<string> GetHeroes(int heroCount, List<Hero> heroList, List<string> availableHeroes, List<string> excludedHeroes, IGetExclusions getExclusions)
        {
            var heroesInGame = new List<string>(heroList.Select(x => x.HeroName));

            var availableHeroesWithoutExcludedHeroes = new List<string>(availableHeroes);
            availableHeroesWithoutExcludedHeroes = availableHeroesWithoutExcludedHeroes.Except(excludedHeroes).ToList();

            //If the required number of heroes from schemes hasn't reached the number of heroes for the player count, it will do this
            for (int i = 0; i < heroCount; i++)
            {
                var exclusions = DetermineHeroes.DetermineHeroList(Villains, AllMastermindsInGame, Henchmen, Scheme, heroesInGame, availableHeroesWithoutExcludedHeroes, getExclusions);

                var exclusionsWithoutHeroesInGame = availableHeroesWithoutExcludedHeroes.Except(heroesInGame).ToList();
                var heroesToChooseFrom = exclusionsWithoutHeroesInGame.Except(exclusions).ToList();
                var heroName = heroesToChooseFrom[new Random().Next(heroesToChooseFrom.Count)];

                heroesInGame.Add(heroName);
            }

            return heroesInGame;
        }

        //This will replace the above function. It will be the main function to get the list of heroes for the game.
        public List<string> GetHeroes(int heroCount, List<Hero> heroList)
        {
            var newHero = new Hero();
            var heroesInGame = new List<string>(heroList.Select(x => x.HeroName));

            //If the required number of heroes from schemes hasn't reached the number of heroes for the player count, it will do this
            for (int i = 0; i < heroCount; i++)
            {
                var hero = newHero.GetNewHero(AllMastermindsInGame, Scheme, AllVillainsInGame, AllHenchmenInGame, heroList);
                heroesInGame.Add(hero.HeroName);
            }

            return heroesInGame;
        }

        public List<Hero> GetNameLimitHeroes(string heroNamePart, List<string> availableHeroes, int numberOfHeroes)
        {
            var newHero = new Hero();
            var heroList = new List<Hero>();
            var listOfHeroes = newHero.GetAllHeroesByNamePart(heroNamePart, availableHeroes);
            var newListOfHeroes = new List<Hero>(listOfHeroes);
            for (int i = 0; i < numberOfHeroes; i++)
            {
                var hero = newListOfHeroes[new Random().Next(newListOfHeroes.Count)];
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
