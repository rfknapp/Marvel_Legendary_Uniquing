using MarvelLegendary.Exclusions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary.Tools
{
    public static class ConvertGames
    {
        public static HashSet<Enums.Set> targetSets = new HashSet<Enums.Set>(new[]
        {
            Enums.Set.MidnightSons,
            Enums.Set.WhatIf,
            Enums.Set.AntmanWasp,
            Enums.Set.TwentyNintyNine
        });

        public static void ConvertTrackedGames()
        {
            ConvertMastermindGames();
            ConvertVillainGames();
            ConvertHenchmenGames();
            ConvertHeroGames();
        }

        private static void ConvertMastermindGames()
        {
            var outputMbS = "";
            var outputMbV = "";
            var outputMbH = "";
            var outputMbHo = "";
            var outputMbM = "";
            foreach (var mastermind in new Mastermind().GetListOfMasterminds())
            {
                var newMastermind = new Mastermind().GetNewMastermind(mastermind);
                var setName = newMastermind.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetMastermindExclusion(mastermind);
                    var exclusions2 = getExclusions.GetMastermindByMastermindExclusions(mastermind);

                    var schemeExclusions = exclusions.SchemeList;
                    var villainExclusions = exclusions.VillainList;
                    var henchmenExclusions = exclusions.HenchmenList;
                    var heroExclusions = exclusions.HeroList;
                    var mastermindExclusions = exclusions.MastermindList;

                    foreach (var item2 in schemeExclusions)
                    {
                        outputMbS = $"{outputMbS}{mastermind}, {item2}\r\n";
                        enterIntoByTable("Scheme", "Mastermind", item2, mastermind);
                        enterIntoByTable("Mastermind", "Scheme", mastermind, item2);
                    }

                    foreach (var item2 in villainExclusions)
                    {
                        outputMbV = $"{outputMbV}{mastermind}, {item2}\r\n";
                        enterIntoByTable("Villain", "Mastermind", item2, mastermind);
                        enterIntoByTable("Mastermind", "Villain", mastermind, item2);
                    }

                    foreach (var item2 in henchmenExclusions)
                    {
                        outputMbH = $"{outputMbH}{mastermind}, {item2}\r\n";
                        enterIntoByTable("Henchmen", "Mastermind", item2, mastermind);
                        enterIntoByTable("Mastermind", "Henchmen", mastermind, item2);
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        outputMbHo = $"{outputMbHo}{mastermind}, {item2}\r\n";
                        enterIntoByTable("Hero", "Mastermind", item2, mastermind);
                        enterIntoByTable("Mastermind", "Hero", mastermind, item2);
                    }

                    foreach (var item2 in exclusions2)
                    {
                        outputMbM = $"{outputMbM}{mastermind}, {item2}\r\n";
                        enterIntoByTable("Mastermind", "Mastermind", mastermind, item2);
                    }
                }
            }
        }

        //If someone wants to put Magnito playing with "The Legacy Virus" into the MastermindByScheme table, they would pass in 
        //tablePrefix = Mastermind
        //tableSuffix = Scheme
        //prefixItem = Magnito
        //suffixItem = The Legacy Virus
        private static void enterIntoByTable(string tablePrefix, string tableSuffix, string prefixItem, string suffixItem)
        {
            var prefixTableName = (tablePrefix == "Henchmen") ? "Henchmen" : (tablePrefix == "Hero" ? "Heroes" : $"{tablePrefix}s");
            var updatedPrefixItem = prefixItem.Contains("'") ? prefixItem.Replace("'", "''") : prefixItem;
            var suffixTableName = (tableSuffix == "Henchmen") ? "Henchmen" : (tableSuffix == "Hero" ? "Heroes" : $"{tableSuffix}s");
            var updatedSuffixItem = suffixItem.Contains("'") ? suffixItem.Replace("'", "''") : suffixItem;

            var prefixItemId = new SqlHelper().GetResult($"SELECT ID FROM {prefixTableName} WHERE {tablePrefix}Name == '{updatedPrefixItem}'");
            var suffixItemId = new SqlHelper().GetResult($"SELECT ID FROM {suffixTableName} WHERE {tableSuffix}Name == '{updatedSuffixItem}'");

            //This will check to see if that entry is already in the table
            var itemCount = new SqlHelper().GetResult($"SELECT COUNT(*) FROM {tablePrefix}By{tableSuffix} WHERE {tablePrefix}Id == {prefixItemId} && {tableSuffix}Id == {suffixItemId}");
            
            //The following code will run if the entry is not in the table
            if(itemCount == "0")
            {
                string sqlQuery = $"INSERT INTO {tablePrefix}By{tableSuffix} ({tablePrefix}Id, {tableSuffix}Id) VALUES ({prefixItemId}, {suffixItemId})";
                new SqlHelper().InsertInto(sqlQuery);
            }
        }

        private static void ConvertVillainGames()
        {
            var outputVbS = "";
            var outputVbV = "";
            var outputVbH = "";
            var outputVbHo = "";
            foreach (var villain in new Villain().GetListOfVillains())
            {
                var newVillain = new Villain().GetNewVillain(villain);
                var setName = newVillain.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetVillainExclusion(villain);
                    var exclusions2 = getExclusions.GetVillainByVillainExclusion(new List<string>() { villain });

                    var schemeExclusions = exclusions.SchemeList;
                    var henchmenExclusions = exclusions.HenchmenList;
                    var heroExclusions = exclusions.HeroList;

                    foreach (var item2 in schemeExclusions)
                    {
                        outputVbS = $"{outputVbS}{villain}, {item2}\r\n";
                    }

                    foreach (var item2 in henchmenExclusions)
                    {
                        outputVbH = $"{outputVbH}{villain}, {item2}\r\n";
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        outputVbHo = $"{outputVbHo}{villain}, {item2}\r\n";
                    }

                    foreach (var item2 in exclusions2)
                    {
                        outputVbV = $"{outputVbV}{villain}, {item2}\r\n";
                    }
                }
            }
        }

        private static void ConvertHenchmenGames()
        {
            var newTargetSets = new HashSet<Enums.Set>(targetSets);
            newTargetSets.Add(Enums.Set.P1);

            var outputHbS = "";
            var outputHbH = "";
            var outputHbHo = "";
            foreach (var henchmen in new Henchmen().GetListOfHenchmen())
            {
                var newHenchmen = new Henchmen().GetNewHenchmen(henchmen);
                var setName = newHenchmen.HenchmenSet;

                if (!newTargetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetHenchmenExclusion(henchmen);
                    var exclusions2 = getExclusions.GetHenchmenByHenchmenExclusions(new List<string>() { henchmen });

                    var schemeExclusions = exclusions.SchemeList;
                    var heroExclusions = exclusions.HeroList;

                    foreach (var item2 in schemeExclusions)
                    {
                        outputHbS = $"{outputHbS}{henchmen}, {item2}\r\n";
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        outputHbHo = $"{outputHbHo}{henchmen}, {item2}\r\n";
                    }

                    foreach (var item2 in exclusions2)
                    {
                        outputHbH = $"{outputHbH}{henchmen}, {item2}\r\n";
                    }
                }
            }
        }

        private static void ConvertHeroGames()
        {
            var outputHobS = "";
            var outputHobHo = "";
            foreach (var hero in new Hero().GetListOfHeroes())
            {
                var newHero = new Hero().GetNewHero(hero);
                var setName = newHero.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetHeroExclusion(hero);
                    var exclusions2 = getExclusions.GetHeroByHeroExclusions(new List<string>() { hero });

                    var schemeExclusions = exclusions.SchemeList;

                    foreach (var item2 in schemeExclusions)
                    {
                        outputHobS = $"{outputHobS}{hero}, {item2}\r\n";
                    }

                    foreach (var item2 in exclusions2)
                    {
                        outputHobHo = $"{outputHobHo}{hero}, {item2}\r\n";
                    }
                }
            }
        }
    }
}
