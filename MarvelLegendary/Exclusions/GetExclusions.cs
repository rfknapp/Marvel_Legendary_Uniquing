using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using static MarvelLegendary.Exclusions.GetExclusions;

namespace MarvelLegendary.Exclusions
{
    public interface IGetExclusions
    {
        GameExclusions GetMastermindExclusion(string mastermindName);
        GameExclusions GetMastermindExclusion(List<string> masterminds);
        List<string> GetMastermindByMastermindExclusions(string mastermindName);
        List<string> GetMastermindByMastermindExclusions(List<string> mastermindNames);
        GameExclusions GetSchemeExclusions(string schemeName);
        GameExclusions GetVillainExclusion(string villainName);
        GameExclusions GetVillainExclusion(List<string> villains);
        List<string> GetVillainByVillainExclusion(List<string> villainNames);
        GameExclusions GetHenchmenExclusion(string henchmenGroup);
        GameExclusions GetHenchmenExclusion(List<string> henchmenGroups);
        List<string> GetHenchmenByHenchmenExclusions(List<string> henchmenGroups);
        GameExclusions GetHeroExclusion(string heroGroup);
        GameExclusions GetHeroExclusion(List<string> heroGroups);
        List<string> GetHeroByHeroExclusions(string heroName);
        List<string> GetHeroByHeroExclusions(List<string> heroNames);
    }

    public class GetExclusions : IGetExclusions
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
        public GameExclusions GetMastermindExclusion(string mastermindName)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Mastermind");
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

        public GameExclusions GetMastermindExclusion(List<string> masterminds)
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
        public List<string> GetMastermindByMastermindExclusions(string mastermindName)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Mastermind x Mastermind");
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

        public List<string> GetMastermindByMastermindExclusions(List<string> mastermindNames)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Mastermind x Mastermind");
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

        public GameExclusions GetSchemeExclusions(string schemeName)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Scheme");
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
        public GameExclusions GetVillainExclusion(string villainName)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Villain");
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

        public GameExclusions GetVillainExclusion(List<string> villains)
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
        public List<string> GetVillainByVillainExclusion(List<string> villainNames)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Villain x Villain");
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
        public GameExclusions GetHenchmenExclusion(string henchmenGroup)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Henchmen");
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
        
        public GameExclusions GetHenchmenExclusion(List<string> henchmenGroups)
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
        public List<string> GetHenchmenByHenchmenExclusions(List<string> henchmenGroups)
        {
            var henchmenList = new List<string>();
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Henchmen x Henchmen");
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
        public GameExclusions GetHeroExclusion(string heroGroup)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Hero");
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

        public GameExclusions GetHeroExclusion(List<string> heroGroups)
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
        public List<string> GetHeroByHeroExclusions(string heroName)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Hero x Hero");
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
        
        public List<string> GetHeroByHeroExclusions(List<string> heroNames)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Hero x Hero");
            var listOfHeroes = spreadsheetInfo.First();
            var heroList = new List<string>();

            foreach (var heroName in heroNames)
            {
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

            return heroList;
        }
        #endregion

    }
}
