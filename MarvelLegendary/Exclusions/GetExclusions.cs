using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using static MarvelLegendary.Exclusions.GetExclusions;
using MarvelLegendary.Enums;

namespace MarvelLegendary.Exclusions
{
    public interface IGetExclusions
    {
        List<Mastermind> GetMastermindByMastermindExclusions(Mastermind mastermindName);
        //List<string> GetMastermindByMastermindExclusions(List<string> mastermindNames);
        GameExclusions GetSchemeExclusions(SchemeInfo schemeName);
        //GameExclusions GetVillainExclusion(string villainName);
        //GameExclusions GetVillainExclusion(List<Villain> villains);
        //List<string> GetVillainByVillainExclusion(List<string> villainNames);
        //GameExclusions GetHenchmenExclusion(string henchmenGroup, Set set);
        //GameExclusions GetHenchmenExclusion(List<Henchmen> henchmenGroups);
        //GameExclusions GetHeroByHenchmenExclusion(List<Henchmen> henchmenGroups);
        //List<string> GetHenchmenByHenchmenExclusions(List<string> henchmenGroups);
        //List<string> GetHenchmenByHenchmenExclusions(List<Henchmen> henchmenGroups);
        //GameExclusions GetHeroExclusion(string heroGroup);
        //GameExclusions GetHeroExclusion(List<string> heroGroups);
        //List<string> GetHeroByHeroExclusions(string heroName);
        //List<string> GetHeroByHeroExclusions(List<string> heroNames);
    }

    public class GetExclusions : IGetExclusions
    {
        public class GameExclusions
        {
            public List<Scheme> SchemeList { get; set; }
            public List<Villain> VillainList { get; set; }
            public List<Henchmen> HenchmenList { get; set; }
            public List<Hero> HeroList { get; set; }
            public List<Mastermind> MastermindList { get; set; }
        }

        #region GetMastermindExclusion
        public GameExclusions GetMastermindExclusion(Mastermind mastermind)
        {
            var schemeList = new List<Scheme>();
            var villainList = new List<Villain>();
            var henchmenList = new List<Henchmen>();
            var heroList = new List<Hero>();
            var schemeSection = true;
            var villainSection = false;
            var henchmenSection = false;
            var heroSection = false;

            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Mastermind");
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

        /*public GameExclusions GetMastermindExclusion(List<string> masterminds)
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
        }*/
        #endregion

        #region GetMastermindByMastermindExclusions
        public List<Mastermind> GetMastermindByMastermindExclusions(Mastermind mastermind)
        {
            //To get the id of the masterminds to associate it with the Mastermind object, just pass the index as the number for the set
            //If every mastermind is in the spreadsheet it should line up with the ids in the code
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("Mastermind x Mastermind");
            var listOfMasterminds = spreadsheetInfo.First().ItemArray.ToList();
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

        /*public List<string> GetMastermindByMastermindExclusions(List<string> mastermindNames)
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
        }*/
        #endregion

        #region GetSchemeExclusions
        public GameExclusions GetSchemeExclusions(SchemeInfo schemeInfo)
        {
            var mastermindList = new List<Mastermind>();
            var villainList = new List<Villain>();
            var henchmenList = new List<Henchmen>();
            var heroList = new List<Hero>();
            var villainSection = false;
            var henchmenSection = false;
            var heroSection = true;
            var mastermindSection = false;

            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Scheme");
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
                if (villainSection)
                    villainList.Add(Villain.GetNewVillain(name, set));
                if (henchmenSection)
                    henchmenList.Add(Henchmen.GetNewHenchmen(name, set));
                if (heroSection)
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

        #region GetVillainExclusion
        /*public GameExclusions GetVillainExclusion(Villain villainName)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Villain");
            var listOfMasterminds = spreadsheetInfo.First();
            var mastermindIndex = listOfMasterminds.ItemArray.ToList().IndexOf(villainName);

            var combinations = spreadsheetInfo.ToList();
            combinations.RemoveAt(0);
            var schemeList = new List<Scheme>();
            var mastermindList = new List<Mastermind>();
            var henchmenList = new List<Henchmen>();
            var heroList = new List<Hero>();
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
                        schemeList.Add(Scheme.GetNewScheme(name, Set.Core));
                    if (mastermindSection)
                        mastermindList.Add(Mastermind.GetNewMastermind(name, Set.Core));
                    if (henchmenSection)
                        henchmenList.Add(Henchmen.GetNewHenchmen(name, Set.Core));
                    if (heroSection)
                        heroList.Add(Hero.GetNewHero(name, Set.Core));
                }
            }

            var gameExclusions = new GameExclusions()
            {
                VillainxSchemeList = schemeList,
                VillainxMastermindList = mastermindList,
                VillainxHenchmenList = henchmenList,
                VillainxHeroList = heroList,
            };
            return gameExclusions;
        }

        public GameExclusions GetVillainExclusion(List<Villain> villains)
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
        }*/
        #endregion

        #region GetVillainByVillainExclusion
        /*public List<string> GetVillainByVillainExclusion(List<string> villainNames)
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
        }*/
        #endregion

        #region GetHenchmenExclusion
        /*public GameExclusions GetHenchmenExclusion(string henchmenGroup, Set set)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Henchmen");
            var henchmen = Henchmen.GetNewHenchmen(henchmenGroup, set);
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

            //foreach (var henchmenGroup in henchmenGroups)
            //{
            //    var tempExclusion = GetHenchmenExclusion(henchmenGroup);
            //    foreach (var scheme in tempExclusion.SchemeList)
            //    {
            //        if (!returnSchemeList.Contains(scheme))
            //            returnSchemeList.Add(scheme);
            //    }
            //    foreach (var mastermind in tempExclusion.MastermindList)
            //    {
            //        if (!returnMastermindList.Contains(mastermind))
            //            returnMastermindList.Add(mastermind);
            //    }
            //    foreach (var villain in tempExclusion.VillainList)
            //    {
            //        if (!returnVillainList.Contains(villain))
            //            returnVillainList.Add(villain);
            //    }
            //    foreach (var hero in tempExclusion.HeroList)
            //    {
            //        if (!returnHeroList.Contains(hero))
            //            returnHeroList.Add(hero);
            //    }
            //}
            
            return new GameExclusions()
            {
                SchemeList = returnSchemeList,
                MastermindList = returnMastermindList,
                VillainList = returnVillainList,
                HeroList = returnHeroList
            };
        }

        public GameExclusions GetHenchmenExclusion(List<Henchmen> henchmenGroups)
        {
            var returnSchemeList = new List<string>();
            var returnMastermindList = new List<string>();
            var returnVillainList = new List<string>();
            var returnHeroList = new List<string>();

            foreach (var henchmenGroup in henchmenGroups)
            {
                var tempExclusion = GetHenchmenExclusion(henchmenGroup.HenchmenName, henchmenGroup.HenchmenSet);
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

        public GameExclusions GetHeroByHenchmenExclusion(List<Henchmen> henchmenGroups)
        {
            var returnSchemeList = new List<Scheme>();
            var returnMastermindList = new List<Mastermind>();
            var returnVillainList = new List<Villain>();
            var returnHeroList = new List<Hero>();

            foreach (var henchmenGroup in henchmenGroups)
            {
                var tempExclusion = GetHenchmenExclusion(henchmenGroup.HenchmenName, henchmenGroup.HenchmenSet);
                foreach (var scheme in tempExclusion.HeroxSchemeList)
                {
                    if (!returnSchemeList.Contains(Scheme.GetNewScheme(scheme, Set.Core)))
                        returnSchemeList.Add(scheme);
                }
                foreach (var mastermind in tempExclusion.HeroxMastermindList)
                {
                    if (!returnMastermindList.Contains(mastermind))
                        returnMastermindList.Add(mastermind);
                }
                foreach (var villain in tempExclusion.HeroxVillainList)
                {
                    if (!returnVillainList.Contains(villain))
                        returnVillainList.Add(villain);
                }
                foreach (var hero in tempExclusion.HeroxHeroList)
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
        }*/
        #endregion

        #region GetHenchmenByHenchmenExclusions

        //TODO: Need to remove this
        /*public List<string> GetHenchmenByHenchmenExclusions(List<string> henchmenGroups)
        {
            return null;
        }

        public List<string> GetHenchmenByHenchmenExclusions(List<Henchmen> henchmenGroups)
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
        }*/
        #endregion

        #region GetHeroExclusion
        /*public GameExclusions GetHeroExclusion(string heroGroup)
        {
            var spreadsheet = new GetSpreadsheet();
            var spreadsheetInfo = spreadsheet.GetSpreadsheetInfo("By Hero");
            var hero = Hero.GetNewHero(heroGroup);
            heroGroup = hero.HeroName;

            var listOfHeroes = spreadsheetInfo.First();
            var heroIndex = listOfHeroes.ItemArray.ToList().IndexOf(heroGroup);

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

                if (combinationList[heroIndex].ToString() == "X")
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
        }*/
        #endregion

        #region GetHeroByHeroExclusions
        /*public List<string> GetHeroByHeroExclusions(string heroName)
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

        public GameExclusions GetVillainExclusion(string villainName)
        {
            throw new NotImplementedException();
        }*/
        #endregion

        #region Helpers

        private int GetIndex(EnumerableRowCollection<DataRow> spreadsheetInfo, string name, Set set)
        {
            var rows = (Names: spreadsheetInfo.ElementAtOrDefault(0).ItemArray, SetIds: spreadsheetInfo.ElementAtOrDefault(1).ItemArray);
            var index = Enumerable.Range(0, rows.Names.Length).Where(i => rows.Names[i]?.ToString() == name
                && rows.SetIds[i]?.ToString() == ((int)set).ToString()).DefaultIfEmpty(-1).First();

            return index;
        }
        #endregion
    }
}
