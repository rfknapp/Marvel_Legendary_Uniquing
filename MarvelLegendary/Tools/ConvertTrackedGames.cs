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
            ConvertSchemeGames();
            ConvertVillainGames();
            ConvertHenchmenGames();
            ConvertHeroGames();
        }

        private static void ConvertMastermindGames()
        {
            var outputMbS = "INSERT INTO MastermindByScheme (MastermindId, SchemeId) VALUES\r\n";
            var outputMbV = "INSERT INTO MastermindByVillain (MastermindId, VillainId) VALUES\r\n";
            var outputMbH = "INSERT INTO MastermindByHenchmen (MastermindId, HenchmenId) VALUES\r\n";
            var outputMbHo = "INSERT INTO MastermindByHero (MastermindId, HeroId) VALUES\r\n";
            var outputMbM = "";
            var listOfMasterminds = Mastermind.GetListOfMasterminds();

            foreach (var mastermind in listOfMasterminds)
            {
                var newMastermind = Mastermind.GetNewMastermind(mastermind);
                var setName = newMastermind.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetMastermindExclusion(mastermind);

                    var schemeExclusions = exclusions.SchemeList;
                    var villainExclusions = exclusions.VillainList;
                    var henchmenExclusions = exclusions.HenchmenList;
                    var heroExclusions = exclusions.HeroList;
                    var mastermindExclusions = getExclusions.GetMastermindByMastermindExclusions(mastermind);

                    var mastermindId = new DatabaseHelper().GetResult($"SELECT ID from Masterminds WHERE MastermindName = '{mastermind}'");

                    foreach (var item2 in schemeExclusions)
                    {
                        var schemeIdSql = "SELECT ID FROM Schemes WHERE SchemeName = @SchemeName";
                        var schemeIdParameters = new Dictionary<string, object>
                        {
                            { "@SchemeName", item2 }
                        };
                        var schemeId = new DatabaseHelper().GetResult(schemeIdSql, schemeIdParameters);

                        var countSql = @"SELECT COUNT(*) FROM MastermindByScheme WHERE MastermindId = @MastermindId AND SchemeId = @SchemeId";
                        var countParameters = new Dictionary<string, object>
                        {
                            { "@MastermindId", mastermindId },
                            { "@SchemeId", schemeId }
                        };
                        var itemCount = new DatabaseHelper().GetResult(countSql, countParameters);
                        //var itemCount = new SqlHelper().GetResult($"SELECT COUNT(*) FROM MastermindByScheme WHERE MastermindId = {mastermindId} AND SchemeId = {schemeId}");

                        if (itemCount == "0")
                        {
                            outputMbS = $"{outputMbS}({mastermindId}, {schemeId}),\r\n";
                        }

                        enterIntoByTable("Mastermind", "Scheme", mastermind, item2);
                    }

                    foreach (var item2 in villainExclusions)
                    {
                        var villainId = new DatabaseHelper().GetResult($"SELECT ID from Villains WHERE VillainName = '{item2}'");
                        var itemCount = new DatabaseHelper().GetResult($"SELECT COUNT(*) FROM MastermindByVillain WHERE MastermindId = {mastermindId} AND VillainId = {villainId}");

                        if (itemCount == "0")
                        {
                            outputMbV = $"{outputMbV}({mastermindId}, {villainId}),\r\n";
                        }

                        enterIntoByTable("Mastermind", "Villain", mastermind, item2);
                    }

                    foreach (var item2 in henchmenExclusions)
                    {
                        var henchmenId = new DatabaseHelper().GetResult($"SELECT ID from Henchmen WHERE HenchmenName = '{item2}'");
                        var itemCount = new DatabaseHelper().GetResult($"SELECT COUNT(*) FROM MastermindByHenchmen WHERE MastermindId = {mastermindId} AND HenchmenId = {henchmenId}");

                        if (itemCount == "0")
                        {
                            outputMbH = $"{outputMbH}({mastermindId}, {henchmenId}),\r\n";
                        }

                        enterIntoByTable("Mastermind", "Henchmen", mastermind, item2);
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        var heroId = new DatabaseHelper().GetResult($"SELECT ID from Heroes WHERE HeroName = '{item2}'");
                        var itemCount = new DatabaseHelper().GetResult($"SELECT COUNT(*) FROM MastermindByHero WHERE MastermindId = {mastermindId} AND HeroId = {heroId}");

                        if (itemCount == "0")
                        {
                            outputMbHo = $"{outputMbHo}({mastermindId}, {heroId}),\r\n";
                        }

                        enterIntoByTable("Mastermind", "Hero", mastermind, item2);
                    }

                    foreach (var item2 in mastermindExclusions)
                    {
                        var mastermind2Id = new DatabaseHelper().GetResult($"SELECT ID from Masterminds WHERE MastermindName = '{item2}'");
                        var itemCount = new DatabaseHelper().GetResult($"SELECT COUNT(*) FROM MastermindByMastermind WHERE MastermindId = {mastermindId} AND Mastermind2Id = {mastermind2Id}");

                        if (itemCount == "0")
                        {
                            outputMbM = $"{outputMbM}({mastermindId}, {mastermind2Id}),\r\n";
                        }

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
            var prefixTableName = (tablePrefix == "Henchmen") ? "Henchmen" : (tablePrefix == "Hero" ? "Heroes" : (prefixItem.StartsWith(".") ? "UnveiledSchemes": $"{tablePrefix}s"));
            var updatedPrefixItem = prefixItem.Contains("'") ? prefixItem.Replace("'", "''") : prefixItem;
            var suffixTableName = (tableSuffix == "Henchmen") ? "Henchmen" : (tableSuffix == "Hero" ? "Heroes" : (suffixItem.StartsWith(".") ? "UnveiledSchemes" : $"{tableSuffix}s"));
            var updatedSuffixItem = suffixItem.Contains("'") ? suffixItem.Replace("'", "''") : suffixItem;
            var updatedTablePrefix = prefixItem.StartsWith(".") ? "UnveiledScheme" : $"{tablePrefix}";
            var updatedTableSuffix = suffixItem.StartsWith(".") ? "UnveiledScheme" : $"{tableSuffix}";
            //If this is a table like MastermindByMastermind then the suffixId has to be Mastermind2Id
            var tableSuffixId = tablePrefix == tableSuffix ? $"{updatedTableSuffix}2Id" : $"{updatedTableSuffix}Id";

            var prefixItemId = new DatabaseHelper().GetResult($"SELECT ID FROM {prefixTableName} WHERE {updatedTablePrefix}Name = '{updatedPrefixItem}'");
            var suffixItemId = new DatabaseHelper().GetResult($"SELECT ID FROM {suffixTableName} WHERE {updatedTableSuffix}Name = '{updatedSuffixItem}'");

            //This will check to see if that entry is already in the table
            var itemCount = new DatabaseHelper().GetResult($"SELECT COUNT(*) FROM {updatedTablePrefix}By{updatedTableSuffix} WHERE {updatedTablePrefix}Id = {prefixItemId} AND {tableSuffixId} = {suffixItemId}");
            
            //The following code will run if the entry is not in the table
            if(itemCount == "0")
            {
                string sqlQuery = $"INSERT INTO {tablePrefix}By{tableSuffix} ({tablePrefix}Id, {tableSuffixId}) VALUES ({prefixItemId}, {suffixItemId})";
                new DatabaseHelper().InsertInto(sqlQuery);
            }
        }

        private static void ConvertSchemeGames()
        {
            var s = new Scheme();
            var outputSbM = "";
            var outputSbV = "";
            var outputSbH = "";
            var outputSbHo = "";
            var listOfSchemes = s.GetListOfSchemes();

            foreach (var scheme in listOfSchemes)
            {
                var newScheme = s.GetNewScheme(scheme);
                var setName = newScheme.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetSchemeExclusions(scheme);

                    var mastermindExclusions = exclusions.MastermindList;
                    var villainExclusions = exclusions.VillainList;
                    var henchmenExclusions = exclusions.HenchmenList;
                    var heroExclusions = exclusions.HeroList;

                    foreach (var item2 in mastermindExclusions)
                    {
                        outputSbM = $"{outputSbM}{scheme}, {item2}\r\n";
                        enterIntoByTable("Scheme", "Mastermind", scheme, item2);
                    }

                    foreach (var item2 in villainExclusions)
                    {
                        outputSbV = $"{outputSbV}{scheme}, {item2}\r\n";
                        enterIntoByTable("Scheme", "Villain", scheme, item2);
                    }

                    foreach (var item2 in henchmenExclusions)
                    {
                        outputSbH = $"{outputSbH}{scheme}, {item2}\r\n";
                        enterIntoByTable("Scheme", "Henchmen", scheme, item2);
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        outputSbHo = $"{outputSbHo}{scheme}, {item2}\r\n";
                        enterIntoByTable("Scheme", "Hero", scheme, item2);
                    }
                }
            }
        }

        private static void ConvertVillainGames()
        {
            var v = new Villain();
            var outputVbM = "";
            var outputVbS = "";
            var outputVbV = "";
            var outputVbH = "";
            var outputVbHo = "";
            var listOfVillains = v.GetListOfVillains();

            foreach (var villain in listOfVillains)
            {
                var newVillain = v.GetNewVillain(villain);
                var setName = newVillain.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetVillainExclusion(villain);

                    var mastermindExclusions = exclusions.MastermindList;
                    var schemeExclusions = exclusions.SchemeList;
                    var henchmenExclusions = exclusions.HenchmenList;
                    var heroExclusions = exclusions.HeroList;
                    var villainExclusions = getExclusions.GetVillainByVillainExclusion(new List<string>() { villain });

                    foreach (var item2 in mastermindExclusions)
                    {
                        outputVbM = $"{outputVbM}{villain}, {item2}\r\n";
                    }

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

                    foreach (var item2 in villainExclusions)
                    {
                        outputVbV = $"{outputVbV}{villain}, {item2}\r\n";
                    }
                }
            }
        }

        private static void ConvertHenchmenGames()
        {
            var h = new Henchmen();
            var newTargetSets = new HashSet<Enums.Set>(targetSets);
            newTargetSets.Add(Enums.Set.P1);

            var outputHbM = "";
            var outputHbS = "";
            var outputHbV = "";
            var outputHbH = "";
            var outputHbHo = "";
            var henchmenList = h.GetListOfHenchmen();

            foreach (var henchmen in henchmenList)
            {
                var newHenchmen = h.GetNewHenchmen(henchmen);
                var setName = newHenchmen.HenchmenSet;

                if (!newTargetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetHenchmenExclusion(henchmen);

                    var mastermindExclusions = exclusions.MastermindList;
                    var schemeExclusions = exclusions.SchemeList;
                    var villainExclusions = exclusions.VillainList;
                    var heroExclusions = exclusions.HeroList;
                    var henchmenExclusions = getExclusions.GetHenchmenByHenchmenExclusions(new List<string>() { henchmen });

                    foreach (var item2 in mastermindExclusions)
                    {
                        outputHbM = $"{outputHbM}{henchmen}, {item2}\r\n";
                    }

                    foreach (var item2 in schemeExclusions)
                    {
                        outputHbS = $"{outputHbS}{henchmen}, {item2}\r\n";
                    }

                    foreach (var item2 in villainExclusions)
                    {
                        outputHbV = $"{outputHbV}{henchmen}, {item2}\r\n";
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        outputHbHo = $"{outputHbHo}{henchmen}, {item2}\r\n";
                    }

                    foreach (var item2 in henchmenExclusions)
                    {
                        outputHbH = $"{outputHbH}{henchmen}, {item2}\r\n";
                    }
                }
            }
        }

        private static void ConvertHeroGames()
        {
            var h = new Hero();
            var outputHobM = "";
            var outputHobS = "";
            var outputHobV = "";
            var outputHobH = "";
            var outputHobHo = "";
            var heroList = h.GetListOfHeroes();

            foreach (var hero in heroList)
            {
                var newHero = h.GetNewHero(hero);
                var setName = newHero.SetName;

                if (!targetSets.Contains(setName))
                {
                    var getExclusions = new GetExclusions();
                    var exclusions = getExclusions.GetHeroExclusion(hero);

                    var mastermindExclusions = exclusions.MastermindList;
                    var schemeExclusions = exclusions.SchemeList;
                    var villainExclusions = exclusions.VillainList;
                    var henchmenExclusions = exclusions.HenchmenList;
                    var heroExclusions = getExclusions.GetHeroByHeroExclusions(new List<string>() { hero });

                    foreach (var item2 in mastermindExclusions)
                    {
                        outputHobM = $"{outputHobM}{hero}, {item2}\r\n";
                    }

                    foreach (var item2 in schemeExclusions)
                    {
                        outputHobS = $"{outputHobS}{hero}, {item2}\r\n";
                    }

                    foreach (var item2 in villainExclusions)
                    {
                        outputHobV = $"{outputHobV}{hero}, {item2}\r\n";
                    }

                    foreach (var item2 in henchmenExclusions)
                    {
                        outputHobH = $"{outputHobH}{hero}, {item2}\r\n";
                    }

                    foreach (var item2 in heroExclusions)
                    {
                        outputHobHo = $"{outputHobHo}{hero}, {item2}\r\n";
                    }
                }
            }
        }
    }
}
