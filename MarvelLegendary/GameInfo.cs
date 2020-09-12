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

        public List<Henchmen> Henchmen { get; set; }
        public List<Henchmen> SchemeHenchmen { get; set; }
        public List<Henchmen> InfectedHenchmen { get; set; }
        public List<Henchmen> AllHenchmenInGame { get; set; }

        public Scheme Scheme { get; set; }

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

        public enum Set
        {
            [Description("Core")]
            Core,
            [Description("Dark City")]
            Dc,
            [Description("Fantastic Four")]
            Ff,
            [Description("Paint The Town Red")]
            PttR,
            [Description("Villains")]
            Villains,
            [Description("Guardians Of The Galaxy")]
            GotG,
            [Description("Fear Itself")]
            Fi,
            [Description("Secret Wars Volume 1")]
            Sw1,
            [Description("Secret Wars Volume 2")]
            Sw2,
            [Description("Captain America 75th Anniversary")]
            Ca,
            [Description("Civil War")]
            Cw,
            [Description("3D")]
            ThreeD,
            [Description("Deadpool")]
            Deadpool,
            [Description("Noir")]
            Noir,
            [Description("X-Men")]
            XMen,
            [Description("Spider-Man Homecoming")]
            Sm,
            [Description("Champions")]
            Champions,
            [Description("World War Hulk")]
            Wwh,
            [Description("Phase 1")]
            P1,
            [Description("Ant-Man")]
            Antman,
            [Description("Venom")]
            Venom,
            [Description("Dimensions")]
            Dimensions,
            [Description("Revelations")]
            Revelations,
            [Description("S.H.I.E.L.D.")]
            Shield,
            [Description("Heroes of Asgard")]
            Asgard,
            [Description("The New Mutants")]
            NewMutants,
            [Description("Into the Cosmos")]
            Cosmos
        }

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
        }

        public void SetMastermind(string mastermindName = "")
        {
            Mastermind = mastermindName == "" ? new Mastermind() : new Mastermind(mastermindName);
            AllMastermindsInGame.Add(Mastermind);
        }

        public void SetScheme(string schemenName = "")
        {
            Scheme = schemenName == "" ? new Scheme(PlayerCount, Mastermind) : new Scheme(PlayerCount, schemenName);
            PlayerCount = Scheme.NumberOfPlayers;

            WoundNumber = GetWoundInformation(Scheme.CustomWoundNumber, Scheme.Wounds);
            CustomWoundNumber = Scheme.CustomWoundNumber;
            BindingNumber = Scheme.SchemeInfo.BindingPerPlayer[PlayerCount - 1];
            NumberHenchmenNextToScheme = Scheme.SchemeInfo.HenchmenNextToSchemePerPlayer[PlayerCount - 1];
        }

        public void SetExtraMasterminds()
        {
            ExtraMasterminds = GetMasterminds(Scheme, Mastermind);
            AllMastermindsInGame.Concat(ExtraMasterminds).ToList();
        }

        public void SetVillains(List<string> villainNames = null)
        {
            var getExclusions = new GetExclusions();
            var masterminds = AllMastermindsInGame.Select(x => x.MastermindName).ToList();
            var exclusions = getExclusions.GetMastermindExclusion(masterminds);

            if (Scheme.SchemeInfo.IsMonsterPitDeck)
            {
                MonsterPitVillains.Add(new Villain("Monsters Unleashed"));
            }

            if (Scheme.SchemeInfo.IncludeExtraAlwaysLeadsVillains && Scheme.SchemeInfo.DrainedMastermind.DoesLeadVillain)
            {
                Scheme.RequiredVillains.Add(Scheme.SchemeInfo.DrainedMastermind.LeadsVillain);
                Scheme.NumberOfVillains += 1;
            }

            foreach (var villainNotAllowed in Scheme.SchemeInfo.VillainsNotAllowed)
            {
                exclusions.VillainList.Add(villainNotAllowed);
            }

            if (villainNames != null)
            {
                Villains.AddRange(from item in villainNames select new Villain(item));
            }

            Villains = GetVillains(Scheme.NumberOfVillains, Scheme.RequiredVillains, Villains);

            AllVillainsInGame = new List<Villain>(Villains);

            foreach (var schemeRequiredVillain in Scheme.RequiredVillains)
            {
                AllVillainsInGame.Add(new Villain(schemeRequiredVillain));
            }
        }

        public void SetHenchmen(List<string> henchmenNames = null)
        {
            var getExclusions = new GetExclusions();
            var masterminds = AllMastermindsInGame.Select(x => x.MastermindName).ToList();
            var exclusions = getExclusions.GetMastermindExclusion(masterminds);

            if (Scheme.SchemeInfo.IsInfectedDeck)
            {
                InfectedHenchmen.Add(new Henchmen("Cytoplasm Spikes"));
            }

            //This is for Symbiotic Absorbtion
            if (Scheme.SchemeInfo.IncludeExtraAlwaysLeadsVillains && Scheme.SchemeInfo.DrainedMastermind.DoesLeadHenchmen)
            {
                Scheme.RequiredHenchmen.Add(ExtraMasterminds.First().LeadsHenchmen);
                Scheme.NumberOfHenchmen += 1;
            }

            var henchmen = Scheme.SchemeInfo.IsHenchmenNextToScheme ? Scheme.SchemeInfo.HenchmenNextToScheme :
                Scheme.SchemeInfo.IsInfectedDeck ? "Cytoplasm Spikes" : null;
            if (henchmen != null)
            {
                SchemeHenchmen.Add(new Henchmen(henchmen));
            }

            if (Scheme.SchemeInfo.IsSmugglerHenchmen || Scheme.SchemeInfo.IsHenchmenInHeroDeck || Scheme.SchemeInfo.HasAnnihilationHenchmen)
            {
                SchemeHenchmen.Add(new Henchmen(exclusions.HenchmenList));
            }

            if (henchmenNames != null)
            {
                Henchmen.AddRange(from item in henchmenNames select new Henchmen(item));
            }

            Henchmen = GetHenchmen(Scheme.RequiredHenchmen, SchemeHenchmen, Mastermind, Henchmen);
            AllHenchmenInGame = new List<Henchmen>(Henchmen).Concat(SchemeHenchmen).ToList();
            foreach (var schemeRequiredHenchmen in Scheme.RequiredHenchmen)
            {
                AllHenchmenInGame.Add(new Henchmen(schemeRequiredHenchmen));
            }
        }

        public void SetHeroes(List<string> heroNames = null)
        {
            var getExclusions = new GetExclusions();
            var masterminds = AllMastermindsInGame.Select(x => x.MastermindName).ToList();
            var exclusions = getExclusions.GetMastermindExclusion(masterminds);
            var allHeroesInGame = new List<Hero>();
            var allHeroes = new Hero().GetListOfHeroes();

            if (Scheme.SchemeInfo.IsRandomHeroesInVillainDeck)
            {
                VillainHeroes = getHeroes(Scheme.SchemeInfo.NumberOfHeroesInVillainDeck, exclusions.HeroList);
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

            if (Scheme.SchemeInfo.IsDarkLoyalty)
            {
                DarkLoyaltyHero = GetDarkLoyaltyHero(SchemeHeroes);
                Scheme.SchemeInfo.DarkLoyaltyHero = DarkLoyaltyHero.HeroName;
                SchemeHeroes.Add(DarkLoyaltyHero);
            }

            if (Scheme.SchemeInfo.IsMutationDeck || Scheme.SchemeInfo.IsHulkDeck)
            {
                var hero = new Hero(true, "Hulk", allHeroes);
                SchemeHeroes.Add(hero);
            }

            if (heroNames != null)
            {
                Heroes.AddRange(from item in heroNames select new Hero(item));
            }

            if (Scheme.SchemeInfo.Is4v2 || Scheme.SchemeInfo.Is3v3)
            {
                Heroes = GetHeroesByTeam(exclusions.HeroList, allHeroes);
            }
            else
            {
                Heroes = GetHeroes(exclusions.HeroList, SchemeHeroes, Heroes, allHeroes, getExclusions);
            }
        }

        #region Masterminds
        private static List<Mastermind> GetMasterminds(Scheme scheme, Mastermind mainMastermind)
        {
            var returnList = new List<Mastermind>();
            var mastermindsInGame = new List<string> { mainMastermind.MastermindName };
            var extraMasterminds = new List<string>();
            var mastermindList = new Mastermind().GetListOfMasterminds();

            for (int i = 0; i < scheme.NumberOfMasterminds; i++)
            {
                var remainingMasterminds = mastermindList.Except(mastermindsInGame).ToList();
                var exclusions = DetermineMastermindList(mastermindsInGame);
                var mastermindsToChooseFrom = remainingMasterminds.Except(exclusions).ToList();

                var newMastermind = mastermindsToChooseFrom[new Random().Next(mastermindsToChooseFrom.Count)];
                mastermindsInGame.Add(newMastermind);
                extraMasterminds.Add(newMastermind);
            }

            returnList.AddRange(from item in extraMasterminds
                                select new Mastermind(item));

            if (scheme.SchemeInfo.IsDrainedMastermind)
            {
                scheme.SchemeInfo.DrainedMastermind = new Mastermind(extraMasterminds.First());
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
        private List<Villain> GetVillains(int numberOfVillains, List<string> requiredVillains, List<Villain> currentVillains)
        {
            var villainList = new List<Villain>();
            var allVillainsInGame = new List<Villain>();

            villainList.AddRange(from item in currentVillains select item);
            allVillainsInGame.AddRange(from item in currentVillains select item);

            //Bring in the Monster Pit villain if it that scheme
            villainList.AddRange(from item in MonsterPitVillains select item);
            allVillainsInGame.AddRange(from item in MonsterPitVillains select item);

            //There is one scheme that brings in two villains, so this will actually allow 2 villain groups in a solo game
            villainList.AddRange(from item in requiredVillains select new Villain(item));
            allVillainsInGame.AddRange(from item in requiredVillains select new Villain(item));

            //If this is a solo game, then the Mastermind Leads is ignored
            //If the number of villains required for the player count hasn't been reached, then it will add the mastermind leads villain group
            if (PlayerCount > 1 && Mastermind.DoesLeadVillain && numberOfVillains > villainList.Count)
            {
                //If the masterminds leads one of the villains brought in through the scheme, it won't be added twice
                var mastermindLeadsVillain = new Villain(Mastermind.LeadsVillain);
                var mastermindLeadsVillainName = mastermindLeadsVillain.VillainName;
                if (allVillainsInGame.All(x => x.VillainName != mastermindLeadsVillainName))
                {
                    villainList.Add(mastermindLeadsVillain);
                    allVillainsInGame.Add(mastermindLeadsVillain);
                }
            }

            if (villainList.Count == 0)
            {
                var villain = new Villain(AllMastermindsInGame);
                villainList.Add(villain);
                allVillainsInGame.Add(villain);
            }

            var currentVillainCount = villainList.Count;
            var numRemainingVillains = numberOfVillains - currentVillainCount;

            //This will return the list if the number of villains coming from the scheme and the mastermind reaches or
            //exceeds the required number of villains for the player count
            if (numRemainingVillains <= 0) return villainList;

            var returnList = new List<Villain>();
            var villainsInGame = new List<string>(villainList.Select(x => x.VillainName));
            var allVillains = new Villain().GetListOfVillains();

            for (int i = 0; i < numRemainingVillains; i++)
            {
                var exclusions = DetermineVillainList(villainsInGame, AllMastermindsInGame);
            
                var remainingVillains = allVillains.Except(villainsInGame).ToList();
                var villainsToChooseFrom = remainingVillains.Except(exclusions).ToList();
            
                var newVillain = villainsToChooseFrom[new Random().Next(villainsToChooseFrom.Count)];
                villainsInGame.Add(newVillain);
            }

            returnList.AddRange(from item in villainsInGame
                                select new Villain(item));

            return returnList;
        }

        private static List<string> DetermineVillainList(List<string> villainsInGame, List<Mastermind> mastermindsInGame)
        {
            var masterminds = mastermindsInGame.Select(x => x.MastermindName).ToList();
            var villainList = new Villain().GetListOfVillains();
            var getExclusions = new GetExclusions();

            for (int i = masterminds.Count - 1; i >= 0; i--)
            {
                var villainsToExcludeWith = new List<string>(villainsInGame);
                var exclusions = getExclusions.GetMastermindExclusion(masterminds);

                if (villainList.Except(exclusions.VillainList).Except(villainsInGame).ToList().Count != 0)
                {
                    for (int j = villainsToExcludeWith.Count - 1; j >= 0; j--)
                    {
                        var excludedVillains = getExclusions.GetVillainByVillainExclusion(villainsToExcludeWith);
                        var excludeList = new List<string>(excludedVillains);
                        excludeList.AddRange(from item in exclusions.VillainList
                                             select item);

                        var villainCompareList = villainList.Except(villainsInGame).Except(excludedVillains).Except(exclusions.VillainList).ToList();
                        if (villainCompareList.Count > 0)
                        {
                            return excludeList;
                        }
                        villainsToExcludeWith.RemoveAt(j);
                    }

                    var mastermindCompareList = villainList.Except(villainsInGame).Except(exclusions.VillainList).ToList();
                    if (mastermindCompareList.Count > 0)
                    {
                        return exclusions.VillainList;
                    }
                }
                masterminds.RemoveAt(i);
            }

            return new List<string>();
        }
        #endregion

        #region Henchmen
        private List<Henchmen> GetHenchmen(List<string> requiredHenchmenString, List<Henchmen> schemeHenchmenGroups, Mastermind mastermind, List<Henchmen> currentHenchmen)
        {
            var henchmenList = new List<Henchmen>();
            var allHenchmenInGame = new List<Henchmen>();
            var requiredHenchmen = new List<Henchmen>();
            var numberOfHenchmen = Scheme.NumberOfHenchmen;
            requiredHenchmen.AddRange(from item in requiredHenchmenString select new Henchmen(item));

            henchmenList.AddRange(from item in currentHenchmen select item);
            allHenchmenInGame.AddRange(from item in currentHenchmen select item);

            henchmenList.AddRange(from schemeHenchmen in schemeHenchmenGroups select schemeHenchmen);
            allHenchmenInGame.AddRange(from schemeHenchmen in schemeHenchmenGroups select schemeHenchmen);

            //Adds all required henchmen from the scheme to the list
            henchmenList.AddRange(from item in requiredHenchmen select item);
            allHenchmenInGame.AddRange(from item in requiredHenchmen select item);

            //If the scheme didn't bring in any henchmen or there are more than one henchmen group included we then include from the Mastermind
            //If this is a solo game, then the Mastermind Leads is ignored
            if (PlayerCount != 1 && numberOfHenchmen > henchmenList.Count && mastermind.DoesLeadHenchmen)
            {
                //If the masterminds leads one of the henchmen brought in through the scheme twist, it won't be added twice
                var mastermindLeadsHenchmen = new Henchmen(mastermind.LeadsHenchmen);
                if (allHenchmenInGame.All(x => x.HenchmenName != mastermindLeadsHenchmen.HenchmenName))
                {
                    henchmenList.Add(mastermindLeadsHenchmen);
                    allHenchmenInGame.Add(mastermindLeadsHenchmen);
                }
            }

            var currentHenchmenCount = henchmenList.Count;
            var numRemainingHenchmen = numberOfHenchmen - currentHenchmenCount;

            //If the required number of henchmen from schemes and mastermind lead abilities hasn't reached the number of
            //henchmen for the player count, it will do this
            if (numRemainingHenchmen <= 0) return henchmenList;

            var returnList = new List<Henchmen>();
            var henchmenInGame = new List<string>(henchmenList.Select(x => x.HenchmenName));
            var allHenchmen = new Henchmen().GetListOfHenchmen();

            for (int i = 0; i < numRemainingHenchmen; i++)
            {
                var exclusions = DetermineHenchmen.DetermineHenchmenList(Villains, AllMastermindsInGame, henchmenInGame, Scheme);
                
                var exclusionsWithoutHenchmenInGame = allHenchmen.Except(henchmenInGame).ToList();
                var henchmenToChooseFrom = exclusionsWithoutHenchmenInGame.Except(exclusions).ToList();
                var henchmenName = henchmenToChooseFrom[new Random().Next(henchmenToChooseFrom.Count)];

                henchmenInGame.Add(henchmenName);
            }

            returnList.AddRange(from item in henchmenInGame
                                select new Henchmen(item));

            return returnList;
        }
        #endregion

        #region Heroes
        private Hero GetDarkLoyaltyHero(List<Hero> allHeroesInGame)
        {
            List<string> heroes = new List<string>();
            foreach (var heroInGame in allHeroesInGame)
            {
                heroes.Add(heroInGame.HeroName);
            }
            var newHero = new Hero(heroes);

            return newHero;
        }

        private List<Hero> getHeroes(int randomHeroesInVillainDeck, List<string> exclusionHeroes)
        {
            var heroList = new List<Hero>();
            var getExclusions = new GetExclusions();

            var hero = new Hero(exclusionHeroes);
            heroList.Add(hero);

            var heroByHeroExclusions = getExclusions.GetHeroByHeroExclusions(heroList.First().HeroName);

            if (randomHeroesInVillainDeck > heroList.Count)
            {
                for (int i = 0; i < randomHeroesInVillainDeck; i++)
                {
                    hero = new Hero(heroByHeroExclusions);
                    var heroName = hero.HeroName;
                    while (heroList.Any(x => x.HeroName == heroName) || (heroByHeroExclusions.Count < new Hero().GetNumberOfHeroes() - 1 && heroByHeroExclusions.Any(x => x == heroName)))
                    {
                        hero = new Hero(heroByHeroExclusions);
                        heroName = hero.HeroName;
                    }
                    heroList.Add(hero);
                }
            }

            return heroList;
        }

        private List<Hero> getHeroes(List<string> heroesInVillainDeck)
        {
            var heroList = new List<Hero>();

            foreach (var hero in heroesInVillainDeck)
            {
                heroList.Add(new Hero(hero));
            }

            return heroList;
        }

        private List<Hero> getHeroes(int numberOfHeroes, HeroTeam heroTeam, List<string> exclusionList, List<string> availableHeroes)
        {
            var returnList = new List<Hero>();

            var currentHeroCount = 0;
            if (numberOfHeroes > currentHeroCount)
            {
                for (int i = currentHeroCount; i < numberOfHeroes; i++)
                {
                    var hero = new Hero(heroTeam, availableHeroes);
                    var heroName = hero.HeroName;
                    //This will make sure there are no duplicate heroes in the list. It will also ignore any heroes that will be included in the villain deck
                    while (returnList.Any(x => x.HeroName == heroName || exclusionList.Contains(heroName)))
                    {
                        hero = new Hero(heroTeam, availableHeroes);
                        heroName = hero.HeroName;
                    }
                    returnList.Add(hero);
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
                    var hero = new Hero(heroTeam, availableHeroes, false);
                    var heroName = hero.HeroName;
                    //This will make sure there are no duplicate heroes in the list. It will also ignore any heroes that will be included in the villain deck
                    while (returnList.Any(x => x.HeroName == heroName || exclusionList.Contains(heroName)))
                    {
                        hero = new Hero(heroTeam, availableHeroes, false);
                        heroName = hero.HeroName;
                    }
                    returnList.Add(hero);
                }
            }

            return returnList;
        }

        private List<Hero> GetHeroesByTeam(List<string> exclusionHeroes, List<string> availableHeroes)
        {
            var returnList = new List<Hero>();

            if (Scheme.SchemeInfo.Is4v2)
            {
                var heroTeams = Enum.GetValues(typeof(HeroTeam));
                var randomHeroTeam = (HeroTeam) heroTeams.GetValue(new Random().Next(heroTeams.Length));
                while (new Hero().GetHeroTeamMemberCount(randomHeroTeam) < 4 || randomHeroTeam == HeroTeam.Unaffiliated)
                {
                    randomHeroTeam = (HeroTeam)heroTeams.GetValue(new Random().Next(heroTeams.Length));
                }
                var enoughHeroes = new Hero().IsEnoughHeroes(new List<HeroTeam>{randomHeroTeam}, 4, exclusionHeroes);
                var exclusionCompare = enoughHeroes ? exclusionHeroes : new List<string>();

                var heroList = getHeroes(4, randomHeroTeam, exclusionCompare, availableHeroes);

                foreach (var item in heroList)
                {
                    returnList.Add(item);
                    AllHeroesInGame.Add(item);
                }
                

                enoughHeroes = new Hero().IsEnoughHeroesWithout(randomHeroTeam, 2, exclusionHeroes);
                exclusionCompare = enoughHeroes ? exclusionHeroes : new List<string>();

                heroList = getHeroesNotInTeam(2, randomHeroTeam, exclusionCompare, availableHeroes);

                foreach (var item in heroList)
                {
                    returnList.Add(item);
                    AllHeroesInGame.Add(item);
                }
                
            }

            else
            {
                var heroTeams = new Hero().GetHeroTeams(2, true);
                var enoughHeroes = new Hero().IsEnoughHeroes(heroTeams, 3, exclusionHeroes);
                var exclusionCompare = enoughHeroes ? exclusionHeroes : new List<string>();

                foreach (var heroTeam in heroTeams)
                {
                    var heroList = getHeroes(3, heroTeam, exclusionCompare, availableHeroes);

                    foreach (var item in heroList)
                    {
                        returnList.Add(item);
                        AllHeroesInGame.Add(item);
                    }
                }
            }

            return returnList;
        }

        public List<Hero> GetHeroes(List<string> exclusionHeroes, List<Hero> schemeHeroGroups, List<Hero> currentHeroes, List<string> availableHeroes, IGetExclusions getExclusions)
        {
            var heroList = new List<Hero>(currentHeroes);
            var numberOfHeroes = Scheme.NumberOfHeroes;
            var excludedHeroes = new List<string>();

            AllHeroesInGame.AddRange(from item in schemeHeroGroups select item);
            AllHeroesInGame.AddRange(from item in currentHeroes select item);

            excludedHeroes.AddRange(from item in schemeHeroGroups select item.HeroName);
            excludedHeroes.AddRange(from item in currentHeroes select item.HeroName);

            if (Scheme.SchemeInfo.IsIncludeHeroTeam)
            {
                var heroGroupList = getHeroes(Scheme.SchemeInfo.NumberOfHeroesFromTeam, Scheme.SchemeInfo.IncludeHeroTeam, exclusionHeroes, availableHeroes);
                foreach (var heroGroup in heroGroupList)
                {
                    SchemeHeroes.Add(heroGroup);
                    heroList.Add(heroGroup);
                    AllHeroesInGame.Add(heroGroup);
                }
            }

            if (Scheme.SchemeInfo.IsHeroNameLimit)
            {
                for (var i = 0; i < Scheme.SchemeInfo.NumberOfHeroesWithNameString; i++)
                {
                    var hero = new Hero(true, Scheme.SchemeInfo.CustomNameString, availableHeroes);
                    while (heroList.Any(x => x.HeroName == hero.HeroName))
                    {
                        hero = new Hero(true, Scheme.SchemeInfo.CustomNameString, availableHeroes);
                    }
                    heroList.Add(hero);
                    AllHeroesInGame.Add(hero);
                }

                var heroesToExclude = new Hero().GetListOfHeroes().Where(x => x.Contains(Scheme.SchemeInfo.CustomNameString)).ToList();
                excludedHeroes.AddRange(from item in heroesToExclude select item);

                if (Scheme.SchemeInfo.CustomNameString == "Hulk" && availableHeroes.Contains("Nul, Breaker of Worlds"))
                    excludedHeroes.Add(new Hero().GetListOfHeroes().First(x => x == "Nul, Breaker of Worlds"));
            }

            var currentHeroCount = heroList.Count;
            var numRemainingHeroes = numberOfHeroes - currentHeroCount;

            //If the required number of heroes from schemes reached the number of heroes for the player count, it will return the list
            if (numRemainingHeroes <= 0) return heroList;

            var returnList = new List<Hero>();
            var heroesInGame = new List<string>(heroList.Select(x => x.HeroName));

            var availableHeroesWithoutExcludedHeroes = new List<string>(availableHeroes);
            availableHeroesWithoutExcludedHeroes = availableHeroesWithoutExcludedHeroes.Except(excludedHeroes).ToList();

            //If the required number of heroes from schemes hasn't reached the number of heroes for the player count, it will do this
            for (int i = 0; i < numRemainingHeroes; i++)
            {
                var exclusions = DetermineHeroes.DetermineHeroList(Villains, AllMastermindsInGame, Henchmen, Scheme, heroesInGame, availableHeroesWithoutExcludedHeroes, getExclusions);

                var exclusionsWithoutHeroesInGame = availableHeroesWithoutExcludedHeroes.Except(heroesInGame).ToList();
                var heroesToChooseFrom = exclusionsWithoutHeroesInGame.Except(exclusions).ToList();
                var heroName = heroesToChooseFrom[new Random().Next(heroesToChooseFrom.Count)];

                heroesInGame.Add(heroName);
            }

            returnList.AddRange(from item in heroesInGame select new Hero(item));

            return returnList;
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
