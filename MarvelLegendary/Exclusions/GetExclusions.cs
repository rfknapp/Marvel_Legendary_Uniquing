using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;

namespace MarvelLegendary.Exclusions
{
    static class GetExclusions
    {
        public class GameExclusions
        {
            public List<string> SchemeList { get; set; }
            public List<string> MastermindList { get; set; }
            public List<string> HenchmenList { get; set; }
            public List<string> HeroList { get; set; }
            public List<string> VillainList { get; set; }
        }

        #region GetMastermindExclusion
        public static GameExclusions GetMastermindExclusion(string mastermindName)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("By Mastermind");
            var listOfMasterminds = spreadsheetInfo.First();
            var test = listOfMasterminds.ItemArray.ToList();
            var mastermindIndex = test.IndexOf(mastermindName);
            
            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var schemeList = new List<string>();
            var villainList = new List<string>();
            var henchmenList = new List<string>();
            var heroList = new List<string>();
            var schemeSection = true;
            var villainSection = false;
            var henchmenSection = false;
            var heroSection = false;
            
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
            
                if (combinationList[mastermindIndex].ToString() == "X")
                {
                    if (schemeSection)
                        schemeList.Add(name);
                    if (villainSection)
                        villainList.Add(name);
                    if (henchmenSection)
                        henchmenList.Add(name);
                    if (heroSection)
                        heroList.Add(name);
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

        public static GameExclusions GetMastermindExclusion(List<string> masterminds)
        {
            var returnSchemeList = new List<string>();
            var returnVillainList = new List<string>();
            var returnHenchmenList = new List<string>();
            var returnHeroList = new List<string>();

            foreach (var mastermind in masterminds)
            {
                var tempExclusion = GetMastermindExclusion(mastermind);
                foreach (var scheme in tempExclusion.SchemeList)
                {
                    if(!returnSchemeList.Contains(scheme))
                        returnSchemeList.Add(scheme);
                }
                foreach (var villain in tempExclusion.VillainList)
                {
                    if (!returnVillainList.Contains(villain))
                        returnVillainList.Add(villain);
                }
                foreach (var henchmen in tempExclusion.HenchmenList)
                {
                    if (!returnHenchmenList.Contains(henchmen))
                        returnHenchmenList.Add(henchmen);
                }
                foreach (var hero in tempExclusion.HeroList)
                {
                    if (!returnHeroList.Contains(hero))
                        returnHeroList.Add(hero);
                }
            }

            return new GameExclusions()
            {
                SchemeList = returnSchemeList,
                VillainList = returnVillainList,
                HenchmenList = returnHenchmenList,
                HeroList = returnHeroList
            };
        }
        #endregion

        #region GetMastermindByMastermindExclusions
        public static List<string> GetMastermindByMastermindExclusions(string mastermindName)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Mastermind x Mastermind");
            var listOfMasterminds = spreadsheetInfo.First();
            var mastermindIndex = listOfMasterminds.ItemArray.ToList().IndexOf(mastermindName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var mastermindList = new List<string>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (combinationList[mastermindIndex].ToString() == "X" && !mastermindList.Contains(name))
                {
                    mastermindList.Add(name);
                }
            }

            return mastermindList;
        }

        public static List<string> GetMastermindByMastermindExclusions(List<string> mastermindNames)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Mastermind x Mastermind");
            var listOfMasterminds = spreadsheetInfo.First();
            var mastermindList = new List<string>();

            foreach (var mastermindName in mastermindNames)
            {
                var mastermindIndex = listOfMasterminds.ItemArray.ToList().IndexOf(mastermindName);

                var combinations = spreadsheetInfo.ToList();
                combinations.RemoveAt(0);

                foreach (var combination in combinations)
                {
                    var combinationList = combination.ItemArray.ToList();
                    var name = combinationList[2].ToString();

                    if (combinationList[mastermindIndex].ToString() == "X" && !mastermindList.Contains(name))
                    {
                        mastermindList.Add(name);
                    }
                }
            }

            return mastermindList;
        }
        #endregion

        public static GameExclusions GetSchemeExclusions(string schemeName)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("By Scheme");
            if (schemeName.Contains('('))
            {
                schemeName = schemeName.Split('(')[1].Split(')')[0];
            }
            var listOfSchemes = spreadsheetInfo.First();
            var schemeArrayList = listOfSchemes.ItemArray.ToList();
            var schemeIndex = schemeArrayList.IndexOf(schemeName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var mastermindList = new List<string>();
            var villainList = new List<string>();
            var henchmenList = new List<string>();
            var heroList = new List<string>();
            var villainSection = false;
            var henchmenSection = false;
            var heroSection = true;
            var mastermindSection = false;

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

                if (combinationList[schemeIndex].ToString() != "X") continue;
                if (mastermindSection)
                    mastermindList.Add(name);
                if (villainSection)
                    villainList.Add(name);
                if (henchmenSection)
                    henchmenList.Add(name);
                if (heroSection)
                    heroList.Add(name);
            }

            var gameExclusions = new GameExclusions()
            {
                MastermindList = new List<string>(mastermindList),
                VillainList = new List<string>(villainList),
                HenchmenList = new List<string>(henchmenList),
                HeroList = new List<string>(heroList),
            };
            return gameExclusions;
        }

        #region GetVillainExclusion
        public static GameExclusions GetVillainExclusion(string villainName)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("By Villain");
            var listOfMasterminds = spreadsheetInfo.First();
            var mastermindIndex = listOfMasterminds.ItemArray.ToList().IndexOf(villainName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var schemeList = new List<string>();
            var mastermindList = new List<string>();
            var henchmenList = new List<string>();
            var heroList = new List<string>();
            var schemeSection = true;
            var mastermindSection = false;
            var henchmenSection = false;
            var heroSection = false;

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

                if (combinationList[mastermindIndex].ToString() == "X")
                {
                    if (schemeSection)
                        schemeList.Add(name);
                    if (mastermindSection)
                        mastermindList.Add(name);
                    if (henchmenSection)
                        henchmenList.Add(name);
                    if (heroSection)
                        heroList.Add(name);
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

        public static GameExclusions GetVillainExclusion(List<string> villains)
        {
            var returnSchemeList = new List<string>();
            var returnMastermindList = new List<string>();
            var returnHenchmenList = new List<string>();
            var returnHeroList = new List<string>();

            foreach (var villain in villains)
            {
                var tempExclusion = GetVillainExclusion(villain);
                foreach (var scheme in tempExclusion.SchemeList)
                {
                    if (!returnSchemeList.Contains(scheme))
                        returnSchemeList.Add(scheme);
                }
                foreach (var mastermind in tempExclusion.MastermindList)
                {
                    if (!returnMastermindList.Contains(mastermind))
                        returnMastermindList.Add(mastermind);
                }
                foreach (var henchmen in tempExclusion.HenchmenList)
                {
                    if (!returnHenchmenList.Contains(henchmen))
                        returnHenchmenList.Add(henchmen);
                }
                foreach (var hero in tempExclusion.HeroList)
                {
                    if (!returnHeroList.Contains(hero))
                        returnHeroList.Add(hero);
                }
            }

            return new GameExclusions()
            {
                SchemeList = returnSchemeList,
                MastermindList = returnMastermindList,
                HenchmenList = returnHenchmenList,
                HeroList = returnHeroList
            };
        }
        #endregion

        #region GetVillainByVillainExclusion
        public static List<string> GetVillainByVillainExclusion(List<string> villainNames)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Villain x Villain");
            var listOfVillains = spreadsheetInfo.First();
            var villainList = new List<string>();

            foreach (var villainName in villainNames)
            {
                var villainIndex = listOfVillains.ItemArray.ToList().IndexOf(villainName);

                var combinations = spreadsheetInfo.ToList();
                combinations.RemoveAt(0);

                foreach (var combination in combinations)
                {
                    var combinationList = combination.ItemArray.ToList();
                    var name = combinationList[2].ToString();

                    if (combinationList[villainIndex].ToString() == "X" && !villainList.Contains(name))
                    {
                        villainList.Add(name);
                    }
                }
            }

            return villainList;
        }
        #endregion

        #region GetHenchmenExclusion
        public static GameExclusions GetHenchmenExclusion(string henchmenGroup)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("By Henchmen");
            var henchmen = new Henchmen(henchmenGroup);
            henchmenGroup = henchmen.HenchmenName;
            
            var listOfHenchmen = spreadsheetInfo.First();
            var henchmenIndex = listOfHenchmen.ItemArray.ToList().IndexOf(henchmenGroup);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var schemeList = new List<string>();
            var mastermindList = new List<string>();
            var villainList = new List<string>();
            var heroList = new List<string>();
            var schemeSection = true;
            var mastermindSection = false;
            var villainSection = false;
            var heroSection = false;

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

                if (combinationList[henchmenIndex].ToString() == "X")
                {
                    if (schemeSection)
                        schemeList.Add(name);
                    if (mastermindSection)
                        mastermindList.Add(name);
                    if (villainSection)
                        villainList.Add(name);
                    if (heroSection)
                        heroList.Add(name);
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
        
        public static GameExclusions GetHenchmenExclusion(List<string> henchmenGroups)
        {
            var returnSchemeList = new List<string>();
            var returnMastermindList = new List<string>();
            var returnVillainList = new List<string>();
            var returnHeroList = new List<string>();

            foreach (var henchmenGroup in henchmenGroups)
            {
                var tempExclusion = GetHenchmenExclusion(henchmenGroup);
                foreach (var scheme in tempExclusion.SchemeList)
                {
                    if (!returnSchemeList.Contains(scheme))
                        returnSchemeList.Add(scheme);
                }
                foreach (var mastermind in tempExclusion.MastermindList)
                {
                    if (!returnMastermindList.Contains(mastermind))
                        returnMastermindList.Add(mastermind);
                }
                foreach (var villain in tempExclusion.VillainList)
                {
                    if (!returnVillainList.Contains(villain))
                        returnVillainList.Add(villain);
                }
                foreach (var hero in tempExclusion.HeroList)
                {
                    if (!returnHeroList.Contains(hero))
                        returnHeroList.Add(hero);
                }
            }

            return new GameExclusions()
            {
                SchemeList = returnSchemeList,
                MastermindList = returnMastermindList,
                VillainList = returnVillainList,
                HeroList = returnHeroList
            };
        }
        #endregion

        #region GetHenchmenByHenchmenExclusions
        public static List<string> GetHenchmenByHenchmenExclusions(List<string> henchmenGroups)
        {
            var henchmenList = new List<string>();
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Henchmen x Henchmen");
            var listOfHenchmen = spreadsheetInfo.First();

            foreach (var henchmenGroup in henchmenGroups)
            {
                var henchmenName = henchmenGroup;
                var henchmenIndex = listOfHenchmen.ItemArray.ToList().IndexOf(henchmenName);

                var combinations = spreadsheetInfo.ToList();
                combinations.RemoveAt(0);
                //var henchmenList = new List<string>();

                foreach (var combination in combinations)
                {
                    var combinationList = combination.ItemArray.ToList();
                    var name = combinationList[2].ToString();

                    if (combinationList[henchmenIndex].ToString() == "X" && !henchmenList.Contains(name))
                    {
                        henchmenList.Add(name);
                    }
                }
            }

            return henchmenList;
        }
        #endregion

        #region GetHeroExclusion
        public static GameExclusions GetHeroExclusion(string heroGroup)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("By Hero");
            var henchmen = new Hero(heroGroup);
            heroGroup = henchmen.HeroName;

            var listOfHenchmen = spreadsheetInfo.First();
            var henchmenIndex = listOfHenchmen.ItemArray.ToList().IndexOf(heroGroup);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var schemeList = new List<string>();
            var mastermindList = new List<string>();
            var villainList = new List<string>();
            var henchmenList = new List<string>();
            var schemeSection = true;
            var mastermindSection = false;
            var villainSection = false;
            var henchmenSection = false;

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

                if (combinationList[henchmenIndex].ToString() == "X")
                {
                    if (schemeSection)
                        schemeList.Add(name);
                    if (mastermindSection)
                        mastermindList.Add(name);
                    if (villainSection)
                        villainList.Add(name);
                    if (henchmenSection)
                        henchmenList.Add(name);
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

        public static GameExclusions GetHeroExclusion(List<string> heroGroups)
        {
            var returnSchemeList = new List<string>();
            var returnMastermindList = new List<string>();
            var returnVillainList = new List<string>();
            var returnHenchmenList = new List<string>();

            foreach (var heroGroup in heroGroups)
            {
                var tempExclusion = GetHeroExclusion(heroGroup);
                foreach (var scheme in tempExclusion.SchemeList)
                {
                    if (!returnSchemeList.Contains(scheme))
                        returnSchemeList.Add(scheme);
                }
                foreach (var mastermind in tempExclusion.MastermindList)
                {
                    if (!returnMastermindList.Contains(mastermind))
                        returnMastermindList.Add(mastermind);
                }
                foreach (var villain in tempExclusion.VillainList)
                {
                    if (!returnVillainList.Contains(villain))
                        returnVillainList.Add(villain);
                }
                foreach (var henchmen in tempExclusion.HenchmenList)
                {
                    if (!returnHenchmenList.Contains(henchmen))
                        returnHenchmenList.Add(henchmen);
                }
            }

            return new GameExclusions()
            {
                SchemeList = returnSchemeList,
                MastermindList = returnMastermindList,
                VillainList = returnVillainList,
                HenchmenList = returnHenchmenList
            };
        }
        #endregion



        #region GetHeroByHeroExclusions
        public static List<string> GetHeroByHeroExclusions(string heroName)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Hero x Hero");
            var listOfHeroes = spreadsheetInfo.First();
            var heroIndex = listOfHeroes.ItemArray.ToList().IndexOf(heroName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var heroList = new List<string>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (combinationList[heroIndex].ToString() == "X")
                {
                    heroList.Add(name);
                }
            }

            return heroList;
        }
        
        public static List<string> GetHeroByHeroExclusions(List<string> heroNames)
        {
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Hero x Hero");
            var listOfHeroes = spreadsheetInfo.First();
            var heroName = heroNames[0];
            var heroIndex = listOfHeroes.ItemArray.ToList().IndexOf(heroName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var heroList = new List<string>();

            foreach (var combination in combinations)
            {
                var combinationList = combination.ItemArray.ToList();
                var name = combinationList[2].ToString();

                if (combinationList[heroIndex].ToString() == "X")
                {
                    heroList.Add(name);
                }
            }

            return heroList;
        }
        #endregion

        public static List<HeroInfo> GetHeroExclusionsByMastermindSchemeVillainsAndHenchmen(GameInfo game, int numberOfHeroesNeeded, int numberOfHeroesExclusionsLeft, HeroTeam heroTeam = HeroTeam.None)
        {
            var allMasterminds = new List<Mastermind>(game.AllMastermindsInGame);
            var allVillains = new List<Villain>(game.AllVillainsInGame);
            var allHenchmenGroups = new List<Henchmen>(game.AllHenchmenInGame);
            var allHeroes = new List<Hero>(game.AllHeroesInGame);
            var scheme = game.Scheme.SchemeName;
            var spreadsheetInfo = GetSpreadsheet.GetSpreadsheetInfo("Hero x Hero");
            var listOfHeroes = spreadsheetInfo.First();

            var heroList = new List<string>();

            var exclusions = new Hero().ModifyHeroList(heroList);
            if (game.Scheme.SchemeInfo.Is3v3 || (game.Scheme.SchemeInfo.Is4v2 && game.GameIncludeHeroTeam))
            {
                exclusions = new Hero().ModifyHeroListOnlyHeroTeam(exclusions, heroTeam);
            }
            else if (game.Scheme.SchemeInfo.Is4v2 && !game.GameIncludeHeroTeam)
            {
                exclusions = new Hero().ModifyHeroListWithoutHeroTeam(exclusions, heroTeam);
            }

            if (numberOfHeroesExclusionsLeft == 0) return exclusions;

            for (var i = allMasterminds.Count; i >= 0; i--)
            {
                var masterminds = allMasterminds.Take(i).Select(x => x.MastermindName).ToList();
                var mastermindExcludedHeroGroups = GetMastermindExclusion(masterminds).HeroList;
                exclusions = new Hero().ModifyHeroList(mastermindExcludedHeroGroups);

                if (exclusions.Count == 0) continue;

                for (var l = 0; l < 1; l++)
                {
                    var schemeExcludedHeroGroups = GetSchemeExclusions(scheme).HeroList;

                    var oldExclusionsFromMasterminds = new List<string>(mastermindExcludedHeroGroups);
                    var newExclusionsFromSchemes = oldExclusionsFromMasterminds.Union(schemeExcludedHeroGroups).ToList();
                    var exclusionsFromScheme = new Hero().ModifyHeroList(newExclusionsFromSchemes);

                    if (exclusionsFromScheme.Count == 0) continue;

                    for (var j = allVillains.Count; j >= 0; j--)
                    {
                        var villains = allVillains.Take(j).Select(x => x.VillainName).ToList();
                        var villainExcludedHeroGroups = GetVillainExclusion(villains).HeroList;

                        var oldExclusionsFromSchemes = new List<string>(newExclusionsFromSchemes);
                        var newExclusionsFromVillains = oldExclusionsFromSchemes.Union(villainExcludedHeroGroups).ToList();
                        var exclusionsFromVillains = new Hero().ModifyHeroList(newExclusionsFromVillains);

                        if (exclusionsFromVillains.Count == 0) continue;

                        for (var k = allHenchmenGroups.Count; k > 0; k--)
                        {
                            var henchmenGroups = allHenchmenGroups.Take(k).Select(x => x.HenchmenName).ToList();
                            var henchmenExcludedHeroGroups = GetHenchmenExclusion(henchmenGroups).HeroList;

                            var oldExclusionsFromVillains = new List<string>(newExclusionsFromVillains);
                            var newExclusionsFromHenchmen = oldExclusionsFromVillains.Union(henchmenExcludedHeroGroups).ToList();
                            var exclusionsFromHenchmen = new Hero().ModifyHeroList(newExclusionsFromHenchmen);

                            if (exclusionsFromHenchmen.Count == 0) continue;

                            for (var m = allHeroes.Count; m > 0; m--)
                            {
                                var heroGroups = allHeroes.Take(m).Select(x=>x.HeroName).ToList();

                                var heroExcludedHeroGroups = GetHeroByHeroExclusions(heroGroups);
                                var oldExclusionsFromHenchmen = new List<string>(newExclusionsFromHenchmen);
                                var newExclusionsFromHeroes = oldExclusionsFromHenchmen.Union(heroExcludedHeroGroups).ToList();

                                foreach (var hero in allHeroes)
                                {
                                    if (!newExclusionsFromHeroes.Contains(hero.HeroName))
                                    {
                                        newExclusionsFromHeroes.Add(hero.HeroName);
                                    }
                                }

                                var exclusionsFromHeroes = new Hero().ModifyHeroList(newExclusionsFromHeroes);

                                if (newExclusionsFromHeroes.Count >= numberOfHeroesNeeded)
                                {
                                    return exclusionsFromHeroes;
                                }
                            }

                            if (exclusionsFromHenchmen.Count >= numberOfHeroesNeeded)
                            {
                                return exclusionsFromHenchmen;
                            }
                        }

                        if (exclusionsFromVillains.Count >= numberOfHeroesNeeded)
                        {
                            return exclusionsFromVillains;
                        }
                    }

                    if (exclusionsFromScheme.Count >= numberOfHeroesNeeded)
                    {
                        return exclusionsFromScheme;
                    }
                }
                if (exclusions.Count >= numberOfHeroesNeeded)
                {
                    return exclusions;
                }

                return GetHeroExclusionsByMastermindSchemeVillainsAndHenchmen(game, numberOfHeroesNeeded, numberOfHeroesExclusionsLeft - 1);
            }

            for (var i = 0; i < numberOfHeroesExclusionsLeft; i++)
            {
                heroList.Add(allHeroes[i].HeroName);
                var heroName = allHeroes[i].HeroName;
                var heroIndex = listOfHeroes.ItemArray.ToList().IndexOf(heroName);

                var combinations = spreadsheetInfo.ToList();
                combinations.RemoveAt(0);

                foreach (var combination in combinations)
                {
                    var combinationList = combination.ItemArray.ToList();
                    var name = combinationList[2].ToString();

                    if (combinationList[heroIndex].ToString() == "X" && !heroList.Contains(name))
                    {
                        heroList.Add(name);
                    }
                }
            }

            var finalExclusion = new Hero().ModifyHeroList(heroList);
            if (exclusions.Count >= numberOfHeroesNeeded) return finalExclusion;

            return GetHeroExclusionsByMastermindSchemeVillainsAndHenchmen(game, numberOfHeroesNeeded, numberOfHeroesExclusionsLeft - 1);
        }
    }
}
