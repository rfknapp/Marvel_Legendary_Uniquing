using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static MarvelLegendary.Exclusions.GetExclusions;
using MarvelLegendary.Enums;

namespace MarvelLegendary.Exclusions
{
    public interface IGetExclusions
    {
        GameExclusions GetMastermindExclusion(List<DataRow> spreadsheetInfo, Mastermind mastermind);
        GameExclusions GetSchemeExclusions(List<DataRow> spreadsheetInfo, SchemeInfo schemeName);
        GameExclusions GetVillainExclusion(List<DataRow> spreadsheetInfo, Villain villain);
        GameExclusions GetHenchmenExclusion(List<DataRow> spreadsheetInfo, Henchmen henchmen);
        GameExclusions GetHeroExclusion(List<DataRow> spreadsheetInfo, Hero hero);


        List<Mastermind> GetMastermindByMastermindExclusions(List<DataRow> spreadsheetInfo, Mastermind mastermindName);
        List<SchemeInfo> GetSchemeBySchemeExclusions(List<DataRow> spreadsheetInfo, SchemeInfo schemeInfo);
        List<Villain> GetVillainByVillainExclusions(List<DataRow> spreadsheetInfo, Villain villain);
        List<Henchmen> GetHenchmenByHenchmenExclusions(List<DataRow> spreadsheetInfo, Henchmen henchmen);
        List<Hero> GetHeroByHeroExclusions(List<DataRow> spreadsheetInfo, Hero hero);
    }

    public class GetExclusions : IGetExclusions
    {
        public class GameExclusions
        {
            public List<Scheme> SchemeList { get; set; } = new List<Scheme>();
            public List<Villain> VillainList { get; set; } = new List<Villain>();
            public List<Henchmen> HenchmenList { get; set; } = new List<Henchmen>();
            public List<Hero> HeroList { get; set; } = new List<Hero>();
            public List<Mastermind> MastermindList { get; set; } = new List<Mastermind>();
        }

        #region GetMastermindExclusion
        public GameExclusions GetMastermindExclusion(List<DataRow> spreadsheetInfo, Mastermind mastermind)
        {
            var schemeList = new List<Scheme>();
            var villainList = new List<Villain>();
            var henchmenList = new List<Henchmen>();
            var heroList = new List<Hero>();
            var schemeSection = true;
            var villainSection = false;
            var henchmenSection = false;
            var heroSection = false;

            var index = GetIndex(spreadsheetInfo, mastermind.Name, mastermind.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();
            
                if (name.Equals("Villains"))
                {
                    schemeSection = false;
                    villainSection = true;
                }
                if (name.Equals("Henchmen"))
                {
                    villainSection = false;
                    henchmenSection = true;
                }
                if (name.Equals("Heroes"))
                {
                    henchmenSection = false;
                    heroSection = true;
                }
                
                if (combinationList[index].ToString() == "X")
                {
                    var set = (Set)Convert.ToInt32(combinationList[3]);

                    if (schemeSection)
                        schemeList.Add(Scheme.GetNewScheme(name, set));
                    else if (villainSection)
                        villainList.Add(Villain.GetNewVillain(name, set));
                    else if (henchmenSection)
                        henchmenList.Add(Henchmen.GetNewHenchmen(name, set));
                    else if (heroSection)
                        heroList.Add(Hero.GetNewHero(name, set));
                }
            }
        
            var gameExclusions = new GameExclusions()
            {
                SchemeList = schemeList,
                VillainList = villainList,
                HenchmenList = henchmenList,
                HeroList = heroList,
            };
            return gameExclusions;
        }
        #endregion

        #region GetMastermindByMastermindExclusions
        public List<Mastermind> GetMastermindByMastermindExclusions(List<DataRow> spreadsheetInfo, Mastermind mastermind)
        {
            var index = GetIndex(spreadsheetInfo, mastermind.Name, mastermind.SetName);
            
            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);
            var mastermindsInList = new List<Mastermind>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();
                if (name != "")
                {
                    var set = Convert.ToInt32(combinationList[3]);

                    if (combinationList[index].ToString() == "X" && !mastermindsInList.Any(x => x.Name == name && (int)x.SetName == set))
                    {
                        mastermindsInList.Add(Mastermind.GetNewMastermind(name, (Set)set));
                    }
                }
            }

            return mastermindsInList;
        }
        #endregion

        #region GetSchemeExclusions
        public GameExclusions GetSchemeExclusions(List<DataRow> spreadsheetInfo, SchemeInfo schemeInfo)
        {
            var mastermindList = new List<Mastermind>();
            var villainList = new List<Villain>();
            var henchmenList = new List<Henchmen>();
            var heroList = new List<Hero>();
            var villainSection = false;
            var henchmenSection = false;
            var heroSection = true;
            var mastermindSection = false;

            var index = GetIndex(spreadsheetInfo, schemeInfo.Name, schemeInfo.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (name.Equals("Villains"))
                {
                    heroSection = false;
                    villainSection = true;
                }
                if (name.Equals("Henchmen"))
                {
                    villainSection = false;
                    henchmenSection = true;
                }
                if (name.Equals("Masterminds"))
                {
                    henchmenSection = false;
                    mastermindSection = true;
                }

                if (combinationList[index].ToString() != "X") continue;

                var set = (Set)Convert.ToInt32(combinationList[3]);

                if (mastermindSection)
                    mastermindList.Add(Mastermind.GetNewMastermind(name, set));
                else if (villainSection)
                    villainList.Add(Villain.GetNewVillain(name, set));
                else if (henchmenSection)
                    henchmenList.Add(Henchmen.GetNewHenchmen(name, set));
                else if (heroSection)
                    heroList.Add(Hero.GetNewHero(name, set));
            }

            var gameExclusions = new GameExclusions()
            {
                MastermindList = mastermindList,
                VillainList = villainList,
                HenchmenList = henchmenList,
                HeroList = heroList
            };

            return gameExclusions;
        }
        #endregion

        #region GetSchemeBySchemeExclusions
        public List<SchemeInfo> GetSchemeBySchemeExclusions(List<DataRow> spreadsheetInfo, SchemeInfo schemeInfo)
        {
            var index = GetIndex(spreadsheetInfo, schemeInfo.Name, schemeInfo.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);
            var schemesInList = new List<SchemeInfo>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();
                if (name != "")
                {
                    var set = Convert.ToInt32(combinationList[3]);

                    if (combinationList[index].ToString() == "X" && !schemesInList.Any(x => x.Name == name && (int)x.SetName == set))
                    {
                        schemesInList.Add(Scheme.GetNewScheme(name, (Set)set).SchemeInfo);
                    }
                }
            }

            return schemesInList;
        }
        #endregion

        #region GetVillainExclusion
        public GameExclusions GetVillainExclusion(List<DataRow> spreadsheetInfo, Villain villain)
        {
            var schemeList = new List<Scheme>();
            var mastermindList = new List<Mastermind>();
            var henchmenList = new List<Henchmen>();
            var heroList = new List<Hero>();
            var schemeSection = true;
            var mastermindSection = false;
            var henchmenSection = false;
            var heroSection = false;

            var index = GetIndex(spreadsheetInfo, villain.Name, villain.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (name.Equals("Heroes"))
                {
                    schemeSection = false;
                    heroSection = true;
                }
                if (name.Equals("Henchmen"))
                {
                    heroSection = false;
                    henchmenSection = true;
                }
                if (name.Equals("Masterminds"))
                {
                    henchmenSection = false;
                    mastermindSection = true;
                }

                if (combinationList[index].ToString() == "X")
                {
                    var set = (Set)Convert.ToInt32(combinationList[3]);

                    if (schemeSection)
                        schemeList.Add(Scheme.GetNewScheme(name, set));
                    else if (mastermindSection)
                        mastermindList.Add(Mastermind.GetNewMastermind(name, set));
                    else if (henchmenSection)
                        henchmenList.Add(Henchmen.GetNewHenchmen(name, set));
                    else if (heroSection)
                        heroList.Add(Hero.GetNewHero(name, set));
                }
            }

            var gameExclusions = new GameExclusions()
            {
                SchemeList = schemeList,
                MastermindList = mastermindList,
                HenchmenList = henchmenList,
                HeroList = heroList,
            };
            return gameExclusions;
        }
        #endregion

        #region GetVillainByVillainExclusion
        public List<Villain> GetVillainByVillainExclusions(List<DataRow> spreadsheetInfo, Villain villain)
        {
            var index = GetIndex(spreadsheetInfo, villain.Name, villain.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);
            var villainInList = new List<Villain>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();
                if (name != "")
                {
                    var set = Convert.ToInt32(combinationList[3]);

                    if (combinationList[index].ToString() == "X" && !villainInList.Any(x => x.Name == name && (int)x.SetName == set))
                    {
                        villainInList.Add(Villain.GetNewVillain(name, (Set)set));
                    }
                }
            }

            return villainInList;
        }
        #endregion

        #region GetHenchmenExclusion
        public GameExclusions GetHenchmenExclusion(List<DataRow> spreadsheetInfo, Henchmen henchmen)
        {
            var schemeList = new List<Scheme>();
            var mastermindList = new List<Mastermind>();
            var villainList = new List<Villain>();
            var heroList = new List<Hero>();
            var schemeSection = true;
            var mastermindSection = false;
            var villainSection = false;
            var heroSection = false;

            var index = GetIndex(spreadsheetInfo, henchmen.Name, henchmen.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (name.Equals("Heroes"))
                {
                    schemeSection = false;
                    heroSection = true;
                }
                if (name.Equals("Villains"))
                {
                    heroSection = false;
                    villainSection = true;
                }
                if (name.Equals("Masterminds"))
                {
                    villainSection = false;
                    mastermindSection = true;
                }

                if (combinationList[index].ToString() == "X")
                {
                    var set = (Set)Convert.ToInt32(combinationList[3]);

                    if (schemeSection)
                        schemeList.Add(Scheme.GetNewScheme(name, set));
                    else if (mastermindSection)
                        mastermindList.Add(Mastermind.GetNewMastermind(name, set));
                    else if (villainSection)
                        villainList.Add(Villain.GetNewVillain(name, set));
                    else if (heroSection)
                        heroList.Add(Hero.GetNewHero(name, set));
                }
            }

            var gameExclusions = new GameExclusions()
            {
                SchemeList = schemeList,
                MastermindList = mastermindList,
                VillainList = villainList,
                HeroList = heroList,
            };
            return gameExclusions;
        }
        #endregion

        #region GetHenchmenByHenchmenExclusions
        public List<Henchmen> GetHenchmenByHenchmenExclusions(List<DataRow> spreadsheetInfo, Henchmen henchmen)
        {
            //var spreadsheet = new GetSpreadsheet();
            //var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Henchmen x Henchmen");
            var index = GetIndex(spreadsheetInfo, henchmen.Name, henchmen.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);
            var henchmenInList = new List<Henchmen>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();
                if (name != "")
                {
                    var set = Convert.ToInt32(combinationList[3]);

                    if (combinationList[index].ToString() == "X" && !henchmenInList.Any(x => x.Name == name && (int)x.SetName == set))
                    {
                        henchmenInList.Add(Henchmen.GetNewHenchmen(name, (Set)set));
                    }
                }
            }

            return henchmenInList;
        }
        #endregion

        #region GetHeroExclusion
        public GameExclusions GetHeroExclusion(List<DataRow> spreadsheetInfo, Hero hero)
        {
            var schemeList = new List<Scheme>();
            var mastermindList = new List<Mastermind>();
            var villainList = new List<Villain>();
            var henchmenList = new List<Henchmen>();
            var schemeSection = true;
            var mastermindSection = false;
            var villainSection = false;
            var henchmenSection = false;

            var index = GetIndex(spreadsheetInfo, hero.Name, hero.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (name.Equals("Villains"))
                {
                    schemeSection = false;
                    villainSection = true;
                }
                if (name.Equals("Henchmen"))
                {
                    villainSection = false;
                    henchmenSection = true;
                }
                if (name.Equals("Masterminds"))
                {
                    henchmenSection = false;
                    mastermindSection = true;
                }

                if (combinationList[index].ToString() == "X")
                {
                    var set = (Set)Convert.ToInt32(combinationList[3]);

                    if (schemeSection)
                        schemeList.Add(Scheme.GetNewScheme(name, set));
                    if (mastermindSection)
                        mastermindList.Add(Mastermind.GetNewMastermind(name, set));
                    if (villainSection)
                        villainList.Add(Villain.GetNewVillain(name, set));
                    if (henchmenSection)
                        henchmenList.Add(Henchmen.GetNewHenchmen(name, set));
                }
            }

            var gameExclusions = new GameExclusions()
            {
                SchemeList = schemeList,
                MastermindList = mastermindList,
                VillainList = villainList,
                HenchmenList = henchmenList,
            };

            return gameExclusions;
        }
        #endregion

        #region GetHeroByHeroExclusions
        public List<Hero> GetHeroByHeroExclusions(List<DataRow> spreadsheetInfo, Hero hero)
        {
            var index = GetIndex(spreadsheetInfo, hero.Name, hero.SetName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveRange(0, 2);
            var heroesInList = new List<Hero>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();
                if (name != "")
                {
                    var set = Convert.ToInt32(combinationList[3]);

                    if (combinationList[index].ToString() == "X" && !heroesInList.Any(x => x.Name == name && (int)x.SetName == set))
                    {
                        heroesInList.Add(Hero.GetNewHero(name, (Set)set, true));
                    }
                }
            }

            return heroesInList;
        }
        #endregion

        #region Helpers

        private int GetIndex(List<DataRow> spreadsheetInfo, string name, Set set)
        {
            var rows = (Names: spreadsheetInfo.ElementAtOrDefault(0).ItemArray, SetIds: spreadsheetInfo.ElementAtOrDefault(1).ItemArray);
            var index = Enumerable.Range(0, rows.Names.Length).Where(i => rows.Names[i]?.ToString() == name
                && rows.SetIds[i]?.ToString() == ((int)set).ToString()).DefaultIfEmpty(-1).First();

            return index;
        }
        #endregion
    }
}
