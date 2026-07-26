using MarvelLegendary.Enums;
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
        //Need to rework this now that there is a different db schema
        public static void ConvertTrackedGames()
        {
            ConvertMastermindGames();
            ConvertSchemeGames();
            //ConvertVillainGames();
            //ConvertHenchmenGames();
            //ConvertHeroGames();
        }

        private static void ConvertMastermindGames()
        {
            var listOfMasterminds = Mastermind.ConvertToMastermindList(MastermindRepository.All.ToList());

            foreach (var mastermind in listOfMasterminds)
            {
                var newMastermind = mastermind;
                var setName = newMastermind.SetName;
        
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetMastermindExclusion(mastermind);
        
                var schemeExclusions = exclusions.SchemeList;
                var villainExclusions = exclusions.VillainList;
                var henchmenExclusions = exclusions.HenchmenList;
                var heroExclusions = exclusions.HeroList;
                var mastermindExclusions = getExclusions.GetMastermindByMastermindExclusions(mastermind);

                var mastermindCard = new Card
                {
                    CardName = mastermind.Name,
                    CardType = (int)CardType.Mastermind,
                    SetId = (int)mastermind.SetName
                };

                foreach (var scheme in schemeExclusions)
                {
                    var schemeCard = new Card
                    {
                        CardName = scheme.Name,
                        CardType = (int)CardType.Scheme,
                        SetId = (int)scheme.SetName
                    };

                    InsertIntoCardRelationshipTable(mastermindCard, schemeCard);
                }
        
                foreach (var villain in villainExclusions)
                {
                    var villainCard = new Card
                    {
                        CardName = villain.Name,
                        CardType = (int)CardType.Villain,
                        SetId = (int)villain.SetName
                    };

                    InsertIntoCardRelationshipTable(mastermindCard, villainCard);
                }

                foreach (var henchmen in henchmenExclusions)
                {
                    var henchmenCard = new Card
                    {
                        CardName = henchmen.Name,
                        CardType = (int)CardType.Henchmen,
                        SetId = (int)henchmen.SetName
                    };

                    InsertIntoCardRelationshipTable(mastermindCard, henchmenCard);
                }

                foreach (var hero in heroExclusions)
                {
                    var heroCard = new Card
                    {
                        CardName = hero.Name,
                        CardType = (int)CardType.Hero,
                        SetId = (int)hero.SetName
                    };

                    InsertIntoCardRelationshipTable(mastermindCard, heroCard);
                }

                foreach (var mastermindItem in mastermindExclusions)
                {
                    var mastermindItemCard = new Card
                    {
                        CardName = mastermindItem.Name,
                        CardType = (int)CardType.Mastermind,
                        SetId = (int)mastermindItem.SetName
                    };

                    InsertIntoCardRelationshipTable(mastermindCard, mastermindItemCard);
                }
            }
        }

        private static void ConvertSchemeGames()
        {
            var listOfSchemeInfo = SchemeRepository.All.ToList();
        
            foreach (var schemeInfo in listOfSchemeInfo)
            {
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetSchemeExclusions(schemeInfo);
        
                var mastermindExclusions = exclusions.MastermindList;
                var villainExclusions = exclusions.VillainList;
                var henchmenExclusions = exclusions.HenchmenList;
                var heroExclusions = exclusions.HeroList;

                var schemeCard = new Card
                {
                    CardName = schemeInfo.Name,
                    CardType = (int)CardType.Scheme,
                    SetId = (int)schemeInfo.SetName
                };

                foreach (var mastermind in mastermindExclusions)
                {
                    var mastermindItemCard = new Card
                    {
                        CardName = mastermind.Name,
                        CardType = (int)CardType.Mastermind,
                        SetId = (int)mastermind.SetName
                    };

                    InsertIntoCardRelationshipTable(schemeCard, mastermindItemCard);
                }

                foreach (var villain in villainExclusions)
                {
                    var villainCard = new Card
                    {
                        CardName = villain.Name,
                        CardType = (int)CardType.Villain,
                        SetId = (int)villain.SetName
                    };

                    InsertIntoCardRelationshipTable(schemeCard, villainCard);
                }

                foreach (var henchmen in henchmenExclusions)
                {
                    var henchmenCard = new Card
                    {
                        CardName = henchmen.Name,
                        CardType = (int)CardType.Henchmen,
                        SetId = (int)henchmen.SetName
                    };

                    InsertIntoCardRelationshipTable(schemeCard, henchmenCard);
                }

                foreach (var hero in heroExclusions)
                {
                    var heroCard = new Card
                    {
                        CardName = hero.Name,
                        CardType = (int)CardType.Hero,
                        SetId = (int)hero.SetName
                    };

                    InsertIntoCardRelationshipTable(schemeCard, heroCard);
                }
            }
        }

        //private static void ConvertVillainGames()
        //{
        //    var outputVbM = "";
        //    var outputVbS = "";
        //    var outputVbV = "";
        //    var outputVbH = "";
        //    var outputVbHo = "";
        //    var listOfVillains = VillainRepository.All.ToList();
        //
        //    foreach (var villain in listOfVillains)
        //    {
        //        var newVillain = Villain.GetNewVillain(villain);
        //        var setName = newVillain.SetName;
        //
        //        if (!targetSets.Contains(setName))
        //        {
        //            var getExclusions = new GetExclusions();
        //            var exclusions = getExclusions.GetVillainExclusion(villain.VillainName);
        //
        //            var mastermindExclusions = exclusions.MastermindList;
        //            var schemeExclusions = exclusions.SchemeList;
        //            var henchmenExclusions = exclusions.HenchmenList;
        //            var heroExclusions = exclusions.HeroList;
        //            var villainExclusions = getExclusions.GetVillainByVillainExclusion(new List<string>() { villain.VillainName });
        //
        //            foreach (var item2 in mastermindExclusions)
        //            {
        //                outputVbM = $"{outputVbM}{villain}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in schemeExclusions)
        //            {
        //                outputVbS = $"{outputVbS}{villain}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in henchmenExclusions)
        //            {
        //                outputVbH = $"{outputVbH}{villain}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in heroExclusions)
        //            {
        //                outputVbHo = $"{outputVbHo}{villain}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in villainExclusions)
        //            {
        //                outputVbV = $"{outputVbV}{villain}, {item2}\r\n";
        //            }
        //        }
        //    }
        //}

        //private static void ConvertHenchmenGames()
        //{
        //    var newTargetSets = new HashSet<Enums.Set>(targetSets);
        //    newTargetSets.Add(Enums.Set.P1);
        //
        //    var outputHbM = "";
        //    var outputHbS = "";
        //    var outputHbV = "";
        //    var outputHbH = "";
        //    var outputHbHo = "";
        //    var henchmenList = Henchmen.ConvertToHenchmenList(HenchmenRepository.All.ToList());
        //
        //    foreach (var henchmen in henchmenList)
        //    {
        //        var newHenchmen = Henchmen.GetNewHenchmen(henchmen.HenchmenName, henchmen.HenchmenSet);
        //        var setName = newHenchmen.HenchmenSet;
        //
        //        if (!newTargetSets.Contains(setName))
        //        {
        //            var getExclusions = new GetExclusions();
        //            var exclusions = getExclusions.GetHenchmenExclusion(henchmen.HenchmenName, henchmen.HenchmenSet);
        //
        //            var mastermindExclusions = exclusions.MastermindList;
        //            var schemeExclusions = exclusions.SchemeList;
        //            var villainExclusions = exclusions.VillainList;
        //            var heroExclusions = exclusions.HeroList;
        //            var henchmenExclusions = getExclusions.GetHenchmenByHenchmenExclusions(new List<Henchmen>() { henchmen });
        //
        //            foreach (var item2 in mastermindExclusions)
        //            {
        //                outputHbM = $"{outputHbM}{henchmen}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in schemeExclusions)
        //            {
        //                outputHbS = $"{outputHbS}{henchmen}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in villainExclusions)
        //            {
        //                outputHbV = $"{outputHbV}{henchmen}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in heroExclusions)
        //            {
        //                outputHbHo = $"{outputHbHo}{henchmen}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in henchmenExclusions)
        //            {
        //                outputHbH = $"{outputHbH}{henchmen}, {item2}\r\n";
        //            }
        //        }
        //    }
        //}

        //private static void ConvertHeroGames()
        //{
        //    var outputHobM = "";
        //    var outputHobS = "";
        //    var outputHobV = "";
        //    var outputHobH = "";
        //    var outputHobHo = "";
        //    var heroList = Hero.GetListOfHeroes();
        //
        //    foreach (var hero in heroList)
        //    {
        //        var newHero = Hero.GetNewHero(hero);
        //        var setName = newHero.SetName;
        //
        //        if (!targetSets.Contains(setName))
        //        {
        //            var getExclusions = new GetExclusions();
        //            var exclusions = getExclusions.GetHeroExclusion(hero);
        //
        //            var mastermindExclusions = exclusions.MastermindList;
        //            var schemeExclusions = exclusions.SchemeList;
        //            var villainExclusions = exclusions.VillainList;
        //            var henchmenExclusions = exclusions.HenchmenList;
        //            var heroExclusions = getExclusions.GetHeroByHeroExclusions(new List<string>() { hero });
        //
        //            foreach (var item2 in mastermindExclusions)
        //            {
        //                outputHobM = $"{outputHobM}{hero}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in schemeExclusions)
        //            {
        //                outputHobS = $"{outputHobS}{hero}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in villainExclusions)
        //            {
        //                outputHobV = $"{outputHobV}{hero}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in henchmenExclusions)
        //            {
        //                outputHobH = $"{outputHobH}{hero}, {item2}\r\n";
        //            }
        //
        //            foreach (var item2 in heroExclusions)
        //            {
        //                outputHobHo = $"{outputHobHo}{hero}, {item2}\r\n";
        //            }
        //        }
        //    }
        //}

        ////If someone wants to put Magnito playing with "The Legacy Virus" into the MastermindByScheme table, they would pass in 
        ////tablePrefix = Mastermind
        ////tableSuffix = Scheme
        ////prefixItem = Magnito
        ////suffixItem = The Legacy Virus
        //private static void EnterIntoByTable(string tablePrefix, string tableSuffix, string prefixItem, string suffixItem)
        //{
        //    var prefixTableName = (tablePrefix == "Henchmen") ? "Henchmen" : (tablePrefix == "Hero" ? "Heroes" : (prefixItem.StartsWith(".") ? "UnveiledSchemes" : $"{tablePrefix}s"));
        //    var updatedPrefixItem = prefixItem.Contains("'") ? prefixItem.Replace("'", "''") : prefixItem;
        //    var suffixTableName = (tableSuffix == "Henchmen") ? "Henchmen" : (tableSuffix == "Hero" ? "Heroes" : (suffixItem.StartsWith(".") ? "UnveiledSchemes" : $"{tableSuffix}s"));
        //    var updatedSuffixItem = suffixItem.Contains("'") ? suffixItem.Replace("'", "''") : suffixItem;
        //    var updatedTablePrefix = prefixItem.StartsWith(".") ? "UnveiledScheme" : $"{tablePrefix}";
        //    var updatedTableSuffix = suffixItem.StartsWith(".") ? "UnveiledScheme" : $"{tableSuffix}";
        //    //If this is a table like MastermindByMastermind then the suffixId has to be Mastermind2Id
        //    var tableSuffixId = tablePrefix == tableSuffix ? $"{updatedTableSuffix}2Id" : $"{updatedTableSuffix}Id";
        //
        //    var prefixItemId = new DatabaseHelper().GetResult($"SELECT ID FROM {prefixTableName} WHERE {updatedTablePrefix}Name = '{updatedPrefixItem}'");
        //    var suffixItemId = new DatabaseHelper().GetResult($"SELECT ID FROM {suffixTableName} WHERE {updatedTableSuffix}Name = '{updatedSuffixItem}'");
        //
        //    //This will check to see if that entry is already in the table
        //    var itemCount = new DatabaseHelper().GetResult($"SELECT COUNT(*) FROM {updatedTablePrefix}By{updatedTableSuffix} WHERE {updatedTablePrefix}Id = {prefixItemId} AND {tableSuffixId} = {suffixItemId}");
        //
        //    //The following code will run if the entry is not in the table
        //    if (itemCount == "0")
        //    {
        //        string sqlQuery = $"INSERT INTO {tablePrefix}By{tableSuffix} ({tablePrefix}Id, {tableSuffixId}) VALUES ({prefixItemId}, {suffixItemId})";
        //        new DatabaseHelper().InsertInto(sqlQuery);
        //    }
        //}

        private static void InsertIntoCardRelationshipTable(Card card1, Card card2)
        {
            using (var connection = SqlHelper.GetConnection())
            {
                connection.Open();

                //Steps
                //1. Get the id of the first card in the Card table
                var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT CardId
                  FROM Card
                  WHERE CardName = $name
                    AND SetId = $setId";

                command.Parameters.AddWithValue("$name", card1.CardName);
                command.Parameters.AddWithValue("$setId", card1.SetId);

                var card1Id = SqlHelper.RunCommandScalar<int>(command);

                //2. Get the id of the second card in the card game
                command.Parameters.Clear();
                command.CommandText =
                    @"SELECT CardId
                  FROM Card
                  WHERE CardName = $name
                    AND SetId = $setId";

                command.Parameters.AddWithValue("$name", card2.CardName);
                command.Parameters.AddWithValue("$setId", card2.SetId);

                var card2Id = SqlHelper.RunCommandScalar<int>(command);

                var cardsToEnter = (First: Math.Min(card1Id, card2Id), Second: Math.Max(card1Id, card2Id));
                
                //3. Insert it into
                command.Parameters.Clear();
                command.CommandText =
                    @"INSERT INTO CardRelationship (Card1Id, Card2Id, TimesPlayed)
                  VALUES (@card1Id, @card2Id, 1)
                  ON CONFLICT(Card1Id, Card2Id)
                  DO UPDATE SET TimesPlayed = TimesPlayed + 1;";

                command.Parameters.AddWithValue("@card1Id", cardsToEnter.First);
                command.Parameters.AddWithValue("@card2Id", cardsToEnter.Second);

                command.ExecuteNonQuery();
            }
        }
    }
}
