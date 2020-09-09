using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MarvelLegendary.Exclusions;

namespace MarvelLegendary.DetermineLists
{
    class DetermineHenchmen
    {
        public static List<string> DetermineHenchmenList(List<Villain> villainsInGame, List<Mastermind> mastermindsInGame, List<string> henchmenInGame, Scheme scheme)
        {
            var masterminds = mastermindsInGame.Select(x => x.MastermindName).ToList();
            var villains = villainsInGame.Select(x => x.VillainName).ToList();
            var availableHenchmen = new Henchmen().GetListOfHenchmen();
            var returnList = new List<string>();
            var getExclusions = new GetExclusions();

            for (int i = masterminds.Count - 1; i >= 0; i--)
            {
                var exclusions = getExclusions.GetMastermindExclusion(masterminds);
                var mastermindExcludedHenchmen = availableHenchmen.Except(exclusions.HenchmenList).Except(henchmenInGame).ToList();

                var allExclusions = new List<string>(exclusions.HenchmenList);
                allExclusions.AddRange(from item in henchmenInGame select item);

                var schemeExcludedHenchmenGroups = getExclusions.GetSchemeExclusions(scheme.SchemeName).HenchmenList;
                var schemeExcludedHenchmen = mastermindExcludedHenchmen.Except(schemeExcludedHenchmenGroups).ToList();

                if (schemeExcludedHenchmen.Count != 0)
                {
                    var allExclusionsWithScheme = new List<string>(allExclusions);
                    allExclusionsWithScheme.AddRange(from item in schemeExcludedHenchmenGroups select item);
                    returnList = GetHenchmenExclusionsByMastermindSchemeAndVillain(villains, schemeExcludedHenchmen, henchmenInGame, exclusions.HenchmenList, availableHenchmen, allExclusionsWithScheme);
                    if (returnList.Count != 0)
                    {
                        return returnList;
                    }
                }

                //This will check Mastermind/Villain/Henchmen when no henchmen matches with the scheme exclusions
                returnList = GetHenchmenExclusionsByMastermindAndVillain(villains, mastermindExcludedHenchmen, henchmenInGame, exclusions.HenchmenList);
                if (returnList.Count != 0)
                {
                    return returnList;
                }

                //This will check Mastermind exclusions 
                var mastermindCompareList = availableHenchmen.Except(henchmenInGame).Except(exclusions.HenchmenList).ToList();
                if (mastermindCompareList.Count > 0)
                {
                    returnList.AddRange(from item in exclusions.HenchmenList select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }

                masterminds.RemoveAt(i);
            }

            return new List<string>();
        }

        private static List<string> GetHenchmenExclusionsByMastermindSchemeAndVillain(List<string> villainsInGame, List<string> exclusionHenchmen, List<string> henchmenInGame,
            List<string> exclusionsHenchmenList, List<string> availableHenchmen, List<string> allExclusions)
        {
            var returnList = new List<string>();
            var getExclusions = new GetExclusions();

            var villainsToExcludeWith = new List<string>(villainsInGame);
            for (int j = villainsToExcludeWith.Count - 1; j >= 0; j--)
            {
                var villainExcludedHenchmenGroups = getExclusions.GetVillainExclusion(villainsToExcludeWith).HenchmenList;
                var villainExcludedHenchmen = exclusionHenchmen.Except(villainExcludedHenchmenGroups).ToList();

                if (villainExcludedHenchmen.Count != 0)
                {
                    if (henchmenInGame.Count != 0)
                    {
                        var newExclusions = new List<string>(villainExcludedHenchmenGroups);
                        newExclusions.AddRange(from item in allExclusions select item);
                        returnList = GetHenchmenExclusionByMastermindSchemeVillainAndHenchmen(henchmenInGame, villainExcludedHenchmen, exclusionsHenchmenList, newExclusions);
                        if (returnList.Count != 0)
                        {
                            return returnList;
                        }
                    }

                    returnList = new List<string>(allExclusions);
                    returnList.AddRange(from item in villainExcludedHenchmenGroups select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }

                villainsToExcludeWith.RemoveAt(j);
            }

            if (henchmenInGame.Count != 0)
            {
                //This is checking to see Mastermind/Scheme/Henchmen exclusions
                returnList = GetHenchmenExclusionByMastermindSchemeAndHenchmen(henchmenInGame, exclusionHenchmen, exclusionsHenchmenList, allExclusions);
                if (returnList.Count != 0)
                {
                    return returnList;
                }
            }

            //This is checking to see Mastermind/Scheme exclusions
            var villainCompareList = availableHenchmen.Except(henchmenInGame).Except(allExclusions).ToList();
            if (villainCompareList.Count > 0)
            {
                return allExclusions;
            }

            return new List<string>();
        }

        private static List<string> GetHenchmenExclusionByMastermindSchemeVillainAndHenchmen(List<string> henchmenInGame, List<string> exclusionHenchmen, List<string> exclusionsHenchmenList,
            List<string> allExclusions)
        {
            var henchmenToExclude = new List<string>(henchmenInGame);
            var returnList = new List<string>();
            var getExclusions = new GetExclusions();

            for (int j = henchmenToExclude.Count - 1; j >= 0; j--)
            {
                var henchmenExcludedHenchmenGroups = getExclusions.GetHenchmenByHenchmenExclusions(henchmenInGame);
                var henchmenExcludedHenchmen = exclusionHenchmen.Except(henchmenExcludedHenchmenGroups).ToList();

                var henchByHenchExcludedHenchmen = getExclusions.GetHenchmenByHenchmenExclusions(henchmenToExclude);
                var excludeList = new List<string>(henchByHenchExcludedHenchmen);
                excludeList.AddRange(from item in exclusionsHenchmenList select item);
                excludeList.AddRange(from item in allExclusions select item);

                var henchmenCompareList = henchmenExcludedHenchmen.Except(henchmenInGame).Except(excludeList).ToList();
                if (henchmenCompareList.Count > 0)
                {
                    returnList = new List<string>(allExclusions);
                    returnList.AddRange(from item in henchByHenchExcludedHenchmen select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }
                henchmenToExclude.RemoveAt(j);
            }

            return new List<string>();
        }

        private static List<string> GetHenchmenExclusionByMastermindSchemeAndHenchmen(List<string> henchmenInGame, List<string> exclusionHenchmen, List<string> exclusionsHenchmenList,
           List<string> allExclusions)
        {
            var henchmenToExclude = new List<string>(henchmenInGame);
            var returnList = new List<string>();
            var getExclusions = new GetExclusions();

            for (int j = henchmenToExclude.Count - 1; j >= 0; j--)
            {
                var henchmenExcludedHenchmenGroups = getExclusions.GetHenchmenByHenchmenExclusions(henchmenInGame);
                var henchmenExcludedHenchmen = exclusionHenchmen.Except(henchmenExcludedHenchmenGroups).ToList();

                var henchByHenchExcludedHenchmen = getExclusions.GetHenchmenByHenchmenExclusions(henchmenToExclude);
                var excludeList = new List<string>(henchByHenchExcludedHenchmen);
                excludeList.AddRange(from item in exclusionsHenchmenList select item);
                excludeList.AddRange(from item in allExclusions select item);

                var henchmenCompareList = henchmenExcludedHenchmen.Except(henchmenInGame).Except(excludeList).ToList();
                if (henchmenCompareList.Count > 0)
                {
                    returnList = new List<string>(allExclusions);
                    returnList.AddRange(from item in henchByHenchExcludedHenchmen select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }
                henchmenToExclude.RemoveAt(j);
            }

            return new List<string>();
        }

        private static List<string> GetHenchmenExclusionsByMastermindAndVillain(List<string> villainsInGame, List<string> henchmenAfterMastermindExclusions, List<string> henchmenInGame,
            List<string> henchmenExclusionsFromMasterminds)
        {
            var returnList = new List<string>();
            var allExclusions = new List<string>(henchmenExclusionsFromMasterminds);
            allExclusions.AddRange(from item in henchmenInGame select item);
            var getExclusions = new GetExclusions();

            var villainsToExcludeWith = new List<string>(villainsInGame);
            for (int j = villainsToExcludeWith.Count - 1; j >= 0; j--)
            {
                var villainExcludedHenchmenGroups = getExclusions.GetVillainExclusion(villainsToExcludeWith).HenchmenList;
                var villainExcludedHenchmen = henchmenAfterMastermindExclusions.Except(villainExcludedHenchmenGroups).ToList();

                //If there are henchmen left after excluding the villain exlusions in addition to the mastermind ones it will do the following
                if (villainExcludedHenchmen.Count != 0)
                {
                    if (henchmenInGame.Count != 0)
                    {
                        var newExclusions = new List<string>(villainExcludedHenchmenGroups);
                        newExclusions.AddRange(from item in allExclusions select item);
                        returnList = GetHenchmenExclusionByMastermindVillainAndHenchmen(henchmenInGame, villainExcludedHenchmen, henchmenExclusionsFromMasterminds, newExclusions);
                        if (returnList.Count != 0)
                        {
                            return returnList;
                        }
                    }

                    returnList = new List<string>(allExclusions);
                    returnList.AddRange(from item in villainExcludedHenchmenGroups select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }

                villainsToExcludeWith.RemoveAt(j);
            }

            //This is checking to see Mastermind/Henchmen exclusions
            returnList = GetHenchmenExclusionByMastermindAndHenchmen(henchmenInGame, henchmenAfterMastermindExclusions, henchmenExclusionsFromMasterminds, allExclusions);
            if (returnList.Count != 0)
            {
                return returnList;
            }

            return new List<string>();
        }

        private static List<string> GetHenchmenExclusionByMastermindVillainAndHenchmen(List<string> henchmenInGame, List<string> henchmenAfterMastermindExclusions,
             List<string> henchmenExclusionsFromMasterminds, List<string> allExclusions)
        {
            var henchmenToExclude = new List<string>(henchmenInGame);
            var returnList = new List<string>();
            var getExclusions = new GetExclusions();

            for (int j = henchmenToExclude.Count - 1; j >= 0; j--)
            {
                var henchmenExcludedHenchmenGroups = getExclusions.GetHenchmenByHenchmenExclusions(henchmenInGame);
                var henchmenExcludedHenchmen = henchmenAfterMastermindExclusions.Except(henchmenExcludedHenchmenGroups).ToList();

                var henchByHenchExcludedHenchmen = getExclusions.GetHenchmenByHenchmenExclusions(henchmenToExclude);
                var excludeList = new List<string>(henchByHenchExcludedHenchmen);
                excludeList.AddRange(from item in henchmenExclusionsFromMasterminds select item);
                excludeList.AddRange(from item in allExclusions select item);

                var henchmenCompareList = henchmenExcludedHenchmen.Except(henchmenInGame).Except(excludeList).ToList();
                if (henchmenCompareList.Count > 0)
                {
                    returnList = new List<string>(allExclusions);
                    returnList.AddRange(from item in henchByHenchExcludedHenchmen select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }
                henchmenToExclude.RemoveAt(j);
            }

            return new List<string>();
        }

        private static List<string> GetHenchmenExclusionByMastermindAndHenchmen(List<string> henchmenInGame, List<string> exclusionHenchmen, List<string> exclusionsHenchmenList,
            List<string> allExclusions)
        {
            var henchmenToExclude = new List<string>(henchmenInGame);
            var returnList = new List<string>();
            var getExclusions = new GetExclusions();

            for (int j = henchmenToExclude.Count - 1; j >= 0; j--)
            {
                var henchmenExcludedHenchmenGroups = getExclusions.GetHenchmenByHenchmenExclusions(henchmenInGame);
                var henchmenExcludedHenchmen = exclusionHenchmen.Except(henchmenExcludedHenchmenGroups).ToList();

                var henchByHenchExcludedHenchmen = getExclusions.GetHenchmenByHenchmenExclusions(henchmenToExclude);
                var excludeList = new List<string>(henchByHenchExcludedHenchmen);
                excludeList.AddRange(from item in exclusionsHenchmenList select item);
                excludeList.AddRange(from item in allExclusions select item);

                var henchmenCompareList = henchmenExcludedHenchmen.Except(henchmenInGame).Except(excludeList).ToList();
                if (henchmenCompareList.Count > 0)
                {
                    returnList = new List<string>(allExclusions);
                    returnList.AddRange(from item in henchByHenchExcludedHenchmen select item);
                    returnList.AddRange(from item in henchmenInGame select item);
                    return returnList;
                }
                henchmenToExclude.RemoveAt(j);
            }

            return new List<string>();
        }
    }
}
