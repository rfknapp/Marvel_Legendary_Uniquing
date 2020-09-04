using MarvelLegendary.Exclusions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary.DetermineLists
{
    class DetermineHeroes
    {
        public static List<string> DetermineHeroList(List<Villain> villainsInGame, List<Mastermind> mastermindsInGame, List<Henchmen> henchmenInGame, Scheme scheme, 
            List<string> heroesInGame, List<string> availableHeroes)
        {
            var masterminds = mastermindsInGame.Select(x => x.MastermindName).ToList();
            var villains = villainsInGame.Select(x => x.VillainName).ToList();
            var henchmen = henchmenInGame.Select(x => x.HenchmenName).ToList();

            var returnList = new List<string>();

            for (int i = 0; i < masterminds.Count; i++)
            {
                var exclusions = GetExclusions.GetMastermindExclusion(masterminds); //heroes excluded from mastermind(s)
                var mastermindExcludedHeroes = availableHeroes.Except(exclusions.HeroList).Except(heroesInGame).ToList(); //heroes left after taking out mastermind exclusions

                var allExclusions = new List<string>(exclusions.HeroList);
                allExclusions.AddRange(from item in heroesInGame select item);

                var schemeExcludedHeroGroups = GetExclusions.GetSchemeExclusions(scheme.SchemeName).HeroList; //heroes excluded from scheme(s)
                var schemeExcludedHeroes = mastermindExcludedHeroes.Except(schemeExcludedHeroGroups).ToList(); //heroes left after taking out mastermind exclusions and scheme exclusions

                if (schemeExcludedHeroes.Count != 0)
                {
                    var allExclusionsWithScheme = new List<string>(allExclusions);
                    allExclusionsWithScheme.AddRange(from item in schemeExcludedHeroGroups select item); //heroes excluded from mastermind(s) and scheme(s)
                    allExclusionsWithScheme = allExclusionsWithScheme.Distinct().ToList(); //this removes all duplicates

                    var test = GetHeroExclusionsByVillainHenchmenAndHeroes(villains, schemeExcludedHeroes, allExclusionsWithScheme, heroesInGame, henchmen);
                    if (test.Count != 0)
                        return test;

                    //var villainsToExcludeWith = new List<string>(villains);
                    //
                    //for (int j = villainsToExcludeWith.Count - 1; j >= 0; j--)
                    //{
                    //    var villainExcludedHeroGroups = GetExclusions.GetVillainExclusion(villainsToExcludeWith).HeroList; //heroes excluded from villain(s)
                    //    var villainExcludedHeroes = schemeExcludedHeroes.Except(villainExcludedHeroGroups).ToList(); //heroes left after taking out mastermind, scheme, and villain exclusions
                    //
                    //    //Skip over all this if villainExcludedHeroes.Count = 0
                    //    //It will only go into this block if there are heroes left after taking the exclusions of masterminds and schemes
                    //    if (villainExcludedHeroes.Count != 0)
                    //    {
                    //        var newVillainExclusions = new List<string>(allExclusionsWithScheme);
                    //        newVillainExclusions.AddRange(from item in villainExcludedHeroGroups select item);
                    //        newVillainExclusions = newVillainExclusions.Distinct().ToList(); //This combines the exclusions of the mastermind, scheme, and villains into one list
                    //
                    //        var mastermindSchemeVillainHenchmenToExclude = new List<string>(henchmen);
                    //
                    //        for (int k = mastermindSchemeVillainHenchmenToExclude.Count - 1; k >= 0; k--)
                    //        {
                    //            var henchmenExcludedHeroGroups = GetExclusions.GetHenchmenExclusion(mastermindSchemeVillainHenchmenToExclude).HeroList; //heroes excluded from henchmen
                    //            var henchmenExcludedHeroes = villainExcludedHeroes.Except(henchmenExcludedHeroGroups).ToList(); //heroes left after taking out mastermind, scheme, villains, and henchmen
                    //
                    //            if (henchmenExcludedHeroes.Count != 0)
                    //            {
                    //                var newHenchmenExclusions = new List<string>(newVillainExclusions);
                    //                newHenchmenExclusions.AddRange(from item in henchmenExcludedHeroGroups select item);
                    //                newHenchmenExclusions = newHenchmenExclusions.Distinct().ToList();
                    //
                    //                var heroesToExcludeWith = new List<string>(heroesInGame);
                    //
                    //                if (heroesInGame.Count != 0)
                    //                {
                    //                    for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                    //                    {
                    //                        var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                    //                        var heroExcludedHeroes = henchmenExcludedHeroes.Except(heroByHeroExclusions).ToList();
                    //
                    //                        if (heroExcludedHeroes.Count != 0)
                    //                        {
                    //                            returnList = new List<string>(newHenchmenExclusions);
                    //                            returnList.AddRange(from item in heroByHeroExclusions select item);
                    //                            return returnList;
                    //                        }
                    //
                    //                        heroesToExcludeWith.RemoveAt(l);
                    //                    }
                    //                }
                    //
                    //                return newHenchmenExclusions;
                    //            }
                    //
                    //            mastermindSchemeVillainHenchmenToExclude.RemoveAt(k);
                    //        }
                    //
                    //        //This will check for mastermind, scheme, villains, and hero exclusions. No heroes are left after henchmen exclusions
                    //        if (heroesInGame.Count != 0)
                    //        {
                    //            var newHeroExclusions = new List<string>(newVillainExclusions);
                    //            newHeroExclusions.AddRange(from item in villainExcludedHeroGroups select item);
                    //            newHeroExclusions = newHeroExclusions.Distinct().ToList();
                    //
                    //            var heroesToExcludeWith = new List<string>(heroesInGame);
                    //
                    //            for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                    //            {
                    //                var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                    //                var heroExcludedHeroes = villainExcludedHeroes.Except(heroByHeroExclusions).ToList();
                    //
                    //                if (heroExcludedHeroes.Count != 0)
                    //                {
                    //                    returnList = new List<string>(newHeroExclusions);
                    //                    returnList.AddRange(from item in heroByHeroExclusions select item);
                    //                    return returnList;
                    //                }
                    //
                    //                heroesToExcludeWith.RemoveAt(l);
                    //            }
                    //        }
                    //
                    //        //Since we are in this loop, there are heroes left after mastermind, scheme, and villain exclusions. This will return the exclusion list.
                    //        returnList = new List<string>(newVillainExclusions);
                    //        return returnList;
                    //
                    //    }
                    //    villainsToExcludeWith.RemoveAt(j);
                    //}

                    //Need to do Mastermind, Scheme, Henchmen, Heroes here
                    var mastermindSchemeHenchmenToExcludeWith = new List<string>(henchmen);

                    //do if henchmen exclusion count != 0
                    for (int k = mastermindSchemeHenchmenToExcludeWith.Count - 1; k >= 0; k--)
                    {
                        var henchmenExcludedHeroGroups = GetExclusions.GetHenchmenExclusion(mastermindSchemeHenchmenToExcludeWith).HeroList; //heroes excluded from henchmen
                        var henchmenExcludedHeroes = schemeExcludedHeroes.Except(henchmenExcludedHeroGroups).ToList(); //heroes left after taking out mastermind, scheme, villains, and henchmen

                        var newHenchmenExclusions = new List<string>(allExclusionsWithScheme);
                        newHenchmenExclusions.AddRange(from item in henchmenExcludedHeroGroups select item);
                        newHenchmenExclusions = newHenchmenExclusions.Distinct().ToList();

                        var heroesToExcludeWith = new List<string>(heroesInGame);

                        if (henchmenExcludedHeroes.Count != 0)
                        {
                            if (heroesInGame.Count != 0)
                            {
                                for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                                {
                                    var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                                    var heroExcludedHeroes = henchmenExcludedHeroes.Except(heroByHeroExclusions).ToList();

                                    if (heroExcludedHeroes.Count != 0)
                                    {
                                        returnList = new List<string>(newHenchmenExclusions);
                                        returnList.AddRange(from item in heroByHeroExclusions select item);
                                        return returnList;
                                    }

                                    heroesToExcludeWith.RemoveAt(l);
                                }
                            }

                            return newHenchmenExclusions;
                        }

                        mastermindSchemeHenchmenToExcludeWith.RemoveAt(k);
                    }

                    //This will check for mastemrind, scheme, villains, and hero exclusions. No heroes are left after henchmen exclusions
                    if (heroesInGame.Count != 0)
                    {
                        var newHeroExclusions = new List<string>(allExclusionsWithScheme);
                        newHeroExclusions.AddRange(from item in schemeExcludedHeroGroups select item);
                        newHeroExclusions = newHeroExclusions.Distinct().ToList();

                        var heroesToExcludeWith = new List<string>(heroesInGame);

                        for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                        {
                            var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                            var heroExcludedHeroes = schemeExcludedHeroes.Except(heroByHeroExclusions).ToList();

                            if (heroExcludedHeroes.Count != 0)
                            {
                                returnList = new List<string>(newHeroExclusions);
                                returnList.AddRange(from item in heroByHeroExclusions select item);
                                return returnList;
                            }

                            heroesToExcludeWith.RemoveAt(l);
                        }
                    }

                    //Since we are in this loop, there are heroes left after mastermind, scheme, and villain exclusions. This will return the exclusion list.
                    returnList = new List<string>(allExclusionsWithScheme);
                    return returnList;
                }

                else
                {
                    //This will check Mastermind/Villain/Henchmen/Hero when no heroes match with the scheme exclusions
                    var noSchemeReturnList = GetHeroExclusionsByVillainHenchmenAndHeroes(villains, mastermindExcludedHeroes, allExclusions, heroesInGame, henchmen);
                    if (noSchemeReturnList.Count != 0)
                        return noSchemeReturnList;

                    //Need one for mastermind, henchmen, and hero exclusions
                    var henchmenToExclude = new List<string>(henchmen);

                    //do if henchmen exclusion count != 0
                    for (int k = henchmenToExclude.Count - 1; k >= 0; k--)
                    {
                        var henchmenExcludedHeroGroups = GetExclusions.GetHenchmenExclusion(henchmenToExclude).HeroList; //heroes excluded from henchmen
                        var henchmenExcludedHeroes = mastermindExcludedHeroes.Except(henchmenExcludedHeroGroups).ToList(); //heroes left after taking out mastermind and henchmen

                        var newHenchmenExclusions = new List<string>(allExclusions);
                        newHenchmenExclusions.AddRange(from item in henchmenExcludedHeroGroups select item);
                        newHenchmenExclusions = newHenchmenExclusions.Distinct().ToList();

                        var heroesToExcludeWith = new List<string>(heroesInGame);

                        if (henchmenExcludedHeroes.Count != 0)
                        {
                            if (heroesInGame.Count != 0)
                            {
                                for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                                {
                                    var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                                    var heroExcludedHeroes = henchmenExcludedHeroes.Except(heroByHeroExclusions).ToList();

                                    if (heroExcludedHeroes.Count != 0)
                                    {
                                        returnList = new List<string>(newHenchmenExclusions);
                                        returnList.AddRange(from item in heroByHeroExclusions select item);
                                        return returnList;
                                    }

                                    heroesToExcludeWith.RemoveAt(l);
                                }
                            }

                            return newHenchmenExclusions;
                        }

                        henchmenToExclude.RemoveAt(k);
                    }

                    //This will check for mastemrind and hero exclusions
                    //No Scheme, Villains, and Henchmen
                    if (heroesInGame.Count != 0)
                    {
                        var heroesToExcludeWith = new List<string>(heroesInGame);

                        for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                        {
                            var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                            var heroExcludedHeroes = mastermindExcludedHeroes.Except(heroByHeroExclusions).ToList();

                            if (heroExcludedHeroes.Count != 0)
                            {
                                returnList = new List<string>(allExclusions);
                                returnList.AddRange(from item in heroByHeroExclusions select item);
                                return returnList;
                            }

                            heroesToExcludeWith.RemoveAt(l);
                        }
                    }

                }

                //This will check Mastermind exclusions 
                var mastermindCompareList = availableHeroes.Except(heroesInGame).Except(exclusions.HeroList).ToList();
                if (mastermindCompareList.Count > 0)
                {
                    returnList.AddRange(from item in exclusions.HeroList select item);
                    returnList.AddRange(from item in heroesInGame select item);
                    return returnList;
                }

                masterminds.RemoveAt(i);
            }

            return new List<string>();
        }

        private static List<string> GetHeroExclusionsByVillainHenchmenAndHeroes(List<string> villainsInGame, List<string> previousExcludedHeroes, List<string> previousExclusions, List<string> heroesInGame, List<string> henchmen)
        {
            var villainsToExcludeWith = new List<string>(villainsInGame);
            var returnList = new List<string>();

            for (int j = villainsToExcludeWith.Count - 1; j >= 0; j--)
            {
                var villainExcludedHeroGroups = GetExclusions.GetVillainExclusion(villainsToExcludeWith).HeroList; //heroes excluded from villain(s)
                var villainExcludedHeroes = previousExcludedHeroes.Except(villainExcludedHeroGroups).ToList(); //heroes left after taking out mastermind, scheme, and villain exclusions

                //Skip over all this if villainExcludedHeroes.Count = 0
                //It will only go into this block if there are heroes left after taking the exclusions of villains and previous exclusions
                if (villainExcludedHeroes.Count != 0)
                {
                    var newVillainExclusions = new List<string>(previousExclusions);
                    newVillainExclusions.AddRange(from item in villainExcludedHeroGroups select item);
                    newVillainExclusions = newVillainExclusions.Distinct().ToList(); //This combines the exclusions of the mastermind, scheme, and villains into one list

                    var henchmenToExcludeWith = new List<string>(henchmen);

                    //do if henchmen exclusion count != 0
                    for (int k = henchmenToExcludeWith.Count - 1; k >= 0; k--)
                    {
                        var henchmenExcludedHeroGroups = GetExclusions.GetHenchmenExclusion(henchmenToExcludeWith).HeroList; //heroes excluded from henchmen
                        var henchmenExcludedHeroes = villainExcludedHeroes.Except(henchmenExcludedHeroGroups).ToList(); //heroes left after taking out mastermind, scheme, villains, and henchmen

                        if (henchmenExcludedHeroes.Count != 0)
                        {
                            var newHenchmenExclusions = new List<string>(newVillainExclusions);
                            newHenchmenExclusions.AddRange(from item in henchmenExcludedHeroGroups select item);
                            newHenchmenExclusions = newHenchmenExclusions.Distinct().ToList();

                            var heroesToExcludeWith = new List<string>(heroesInGame);

                            if (heroesInGame.Count != 0)
                            {
                                for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                                {
                                    var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                                    var heroExcludedHeroes = henchmenExcludedHeroes.Except(heroByHeroExclusions).ToList();

                                    if (heroExcludedHeroes.Count != 0)
                                    {
                                        returnList = new List<string>(newHenchmenExclusions);
                                        returnList.AddRange(from item in heroByHeroExclusions select item);
                                        return returnList;
                                    }

                                    heroesToExcludeWith.RemoveAt(l);
                                }
                            }

                            return newHenchmenExclusions;
                        }

                        henchmenToExcludeWith.RemoveAt(k);
                    }

                    //This will check for mastermind, scheme, villains, and hero exclusions. No heroes are left after henchmen exclusions
                    //No Henchmen
                    if (heroesInGame.Count != 0)
                    {
                        var newHeroExclusions = new List<string>(newVillainExclusions);
                        newHeroExclusions.AddRange(from item in villainExcludedHeroGroups select item);
                        newHeroExclusions = newHeroExclusions.Distinct().ToList();

                        var heroesToExcludeWith = new List<string>(heroesInGame);

                        for (int l = heroesToExcludeWith.Count - 1; l >= 0; l--)
                        {
                            var heroByHeroExclusions = GetExclusions.GetHeroByHeroExclusions(heroesToExcludeWith);
                            var heroExcludedHeroes = villainExcludedHeroes.Except(heroByHeroExclusions).ToList();

                            if (heroExcludedHeroes.Count != 0)
                            {
                                returnList = new List<string>(newHeroExclusions);
                                returnList.AddRange(from item in heroByHeroExclusions select item);
                                return returnList;
                            }

                            heroesToExcludeWith.RemoveAt(l);
                        }
                    }

                    //Since we are in this loop, there are heroes left after mastermind, scheme, and villain exclusions. This will return the exclusion list.
                    returnList = new List<string>(newVillainExclusions);
                    return returnList;

                }
                villainsToExcludeWith.RemoveAt(j);
            }

            return new List<string>();
        }
    }
}
