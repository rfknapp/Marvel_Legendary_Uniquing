using MarvelLegendary.Enums;
using MarvelLegendary.Exclusions;
using System;
using System.Diagnostics;
using System.Linq;
using static MarvelLegendary.Exclusions.GetExclusions;

namespace MarvelLegendary.Tools
{
    public static class ConvertGames
    {
        public static void ConvertTrackedGames()
        {
            ConvertMastermindGames();
            ConvertSchemeGames();
            ConvertUnveiledSchemeGames();
            ConvertVillainGames();
            ConvertHenchmenGames();
            ConvertHeroGames();
        }

        private static void ConvertMastermindGames()
        {
            var stopwatch = Stopwatch.StartNew();
            var listOfMasterminds = Mastermind.ConvertToMastermindList(MastermindRepository.All.ToList());

            var mastermindSpreadsheet = new GetSpreadsheet();
            var mastermindSpreadsheetInfo = mastermindSpreadsheet.GetSpreadsheetInfo("By Mastermind").ToList();
            mastermindSpreadsheetInfo.RemoveAt(0);
            var mastermindByMastermindSpreadsheet = new GetSpreadsheet();
            var mastermindByMastermindSpreadsheetInfo = mastermindByMastermindSpreadsheet.GetSpreadsheetInfo("Mastermind x Mastermind").ToList();
            mastermindByMastermindSpreadsheetInfo.RemoveAt(0);

            foreach (var mastermind in listOfMasterminds)
            {
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetMastermindExclusion(mastermindSpreadsheetInfo, mastermind);
                exclusions.MastermindList = getExclusions.GetMastermindByMastermindExclusions(mastermindByMastermindSpreadsheetInfo, mastermind);

                var mastermindCard = new Card
                {
                    CardName = mastermind.Name,
                    CardType = (int)CardType.Mastermind,
                    SetId = (int)mastermind.SetName
                };

                ConvertGamesInsert(mastermindCard, exclusions);
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void ConvertSchemeGames()
        {
            var stopwatch = Stopwatch.StartNew();
            var listOfSchemeInfo = SchemeRepository.All.ToList();

            var schemeSpreadsheet = new GetSpreadsheet();
            var schemeSpreadsheetInfo = schemeSpreadsheet.GetSpreadsheetInfo("By Scheme").ToList();
            schemeSpreadsheetInfo.RemoveAt(0);

            foreach (var schemeInfo in listOfSchemeInfo)
            {
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetSchemeExclusions(schemeSpreadsheetInfo, schemeInfo);

                var schemeCard = new Card
                {
                    CardName = schemeInfo.Name,
                    CardType = (int)CardType.Scheme,
                    SetId = (int)schemeInfo.SetName
                };

                ConvertGamesInsert(schemeCard, exclusions);
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void ConvertUnveiledSchemeGames()
        {
            var stopwatch = Stopwatch.StartNew();
            var listOfUnveiledSchemeInfo = UnveiledScheme.All.ToList();

            var schemeBySchemeSpreadsheet = new GetSpreadsheet();
            var schemeBySchemeSpreadsheetInfo = schemeBySchemeSpreadsheet.GetSpreadsheetInfo("Scheme x Scheme").ToList();
            schemeBySchemeSpreadsheetInfo.RemoveAt(0);

            foreach (var unveiledSchemeInfo in listOfUnveiledSchemeInfo)
            {
                var getExclusions = new GetExclusions();
                var schemeExclusions = getExclusions.GetSchemeBySchemeExclusions(schemeBySchemeSpreadsheetInfo, unveiledSchemeInfo);

                var schemeCard = new Card
                {
                    CardName = unveiledSchemeInfo.Name,
                    CardType = (int)CardType.Scheme,
                    SetId = (int)unveiledSchemeInfo.SetName
                };

                foreach (var scheme in schemeExclusions)
                {
                    var unveiledSchemeCard = new Card
                    {
                        CardName = scheme.Name,
                        CardType = (int)CardType.Scheme,
                        SetId = (int)scheme.SetName
                    };

                    InsertIntoCardRelationshipTable(schemeCard, unveiledSchemeCard, true);
                }
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void ConvertVillainGames()
        {
            var stopwatch = Stopwatch.StartNew();
            var listOfVillains = Villain.ConvertToVillainList(VillainRepository.All.ToList(), true);

            var villainSpreadsheet = new GetSpreadsheet();
            var villainSpreadsheetInfo = villainSpreadsheet.GetSpreadsheetInfo("By Villain").ToList();
            villainSpreadsheetInfo.RemoveAt(0);
            var villainByVillainSpreadsheet = new GetSpreadsheet();
            var villainByVillainSpreadsheetInfo = villainByVillainSpreadsheet.GetSpreadsheetInfo("Villain x Villain").ToList();
            villainByVillainSpreadsheetInfo.RemoveAt(0);

            foreach (var villain in listOfVillains)
            {
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetVillainExclusion(villainSpreadsheetInfo, villain);
                exclusions.VillainList = getExclusions.GetVillainByVillainExclusions(villainByVillainSpreadsheetInfo, villain);

                var villainCard = new Card
                {
                    CardName = villain.Name,
                    CardType = (int)CardType.Villain,
                    SetId = (int)villain.SetName
                };

                ConvertGamesInsert(villainCard, exclusions);
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void ConvertHenchmenGames()
        {
            var stopwatch = Stopwatch.StartNew();
            var listOfHenchmen = Henchmen.ConvertToHenchmenList(HenchmenRepository.All.ToList(), true);

            var henchmenSpreadsheet = new GetSpreadsheet();
            var henchmenSpreadsheetInfo = henchmenSpreadsheet.GetSpreadsheetInfo("By Henchmen").ToList();
            henchmenSpreadsheetInfo.RemoveAt(0);
            var henchmenByHenchmenSpreadsheet = new GetSpreadsheet();
            var henchmenByHenchmenSpreadsheetInfo = henchmenByHenchmenSpreadsheet.GetSpreadsheetInfo("Henchmen x Henchmen").ToList();
            henchmenByHenchmenSpreadsheetInfo.RemoveAt(0);

            foreach (var henchmen in listOfHenchmen)
            {
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetHenchmenExclusion(henchmenSpreadsheetInfo, henchmen);
                exclusions.HenchmenList = getExclusions.GetHenchmenByHenchmenExclusions(henchmenByHenchmenSpreadsheetInfo, henchmen);

                var henchmenCard = new Card
                {
                    CardName = henchmen.Name,
                    CardType = (int)CardType.Henchmen,
                    SetId = (int)henchmen.SetName
                };

                ConvertGamesInsert(henchmenCard, exclusions);
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void ConvertHeroGames()
        {
            var stopwatch = Stopwatch.StartNew();
            var listOfHeroes = Hero.ConvertToHeroList(HeroRepository.All.ToList(), true);

            var heroSpreadsheet = new GetSpreadsheet();
            var heroSpreadsheetInfo = heroSpreadsheet.GetSpreadsheetInfo("By Hero").ToList();
            heroSpreadsheetInfo.RemoveAt(0);
            var heroByHeroSpreadsheet = new GetSpreadsheet();
            var heroByHeroSpreadsheetInfo = heroByHeroSpreadsheet.GetSpreadsheetInfo("Hero x Hero").ToList();
            heroByHeroSpreadsheetInfo.RemoveAt(0);

            foreach (var hero in listOfHeroes)
            {
                var getExclusions = new GetExclusions();
                var exclusions = getExclusions.GetHeroExclusion(heroSpreadsheetInfo, hero);
                exclusions.HeroList = getExclusions.GetHeroByHeroExclusions(heroByHeroSpreadsheetInfo, hero);

                var heroCard = new Card
                {
                    CardName = hero.Name,
                    CardType = (int)CardType.Hero,
                    SetId = (int)hero.SetName
                };

                ConvertGamesInsert(heroCard, exclusions);
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void InsertIntoCardRelationshipTable(Card card1, Card card2, bool doNotIncrement = false)
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
                    $@"INSERT INTO CardRelationship (Card1Id, Card2Id, TimesPlayed)
                    VALUES (@card1Id, @card2Id, 1)
                    ON CONFLICT(Card1Id, Card2Id)
                    {(doNotIncrement
                        ? "DO NOTHING"
                        : "DO UPDATE SET TimesPlayed = TimesPlayed + 1")}; ";

                command.Parameters.AddWithValue("@card1Id", cardsToEnter.First);
                command.Parameters.AddWithValue("@card2Id", cardsToEnter.Second);

                command.ExecuteNonQuery();
            }
        }

        private static void ConvertGamesInsert(Card mainCard, GameExclusions exclusions)
        {
            foreach (var mastermindItem in exclusions.MastermindList)
            {
                var mastermindItemCard = new Card
                {
                    CardName = mastermindItem.Name,
                    CardType = (int)CardType.Mastermind,
                    SetId = (int)mastermindItem.SetName
                };

                InsertIntoCardRelationshipTable(mainCard, mastermindItemCard, true);
            }

            foreach (var scheme in exclusions.SchemeList)
            {
                var schemeCard = new Card
                {
                    CardName = scheme.Name,
                    CardType = (int)CardType.Scheme,
                    SetId = (int)scheme.SetName
                };

                InsertIntoCardRelationshipTable(mainCard, schemeCard, true);
            }

            foreach (var villain in exclusions.VillainList)
            {
                var villainCard = new Card
                {
                    CardName = villain.Name,
                    CardType = (int)CardType.Villain,
                    SetId = (int)villain.SetName
                };

                InsertIntoCardRelationshipTable(mainCard, villainCard, true);
            }

            foreach (var henchmen in exclusions.HenchmenList)
            {
                var henchmenCard = new Card
                {
                    CardName = henchmen.Name,
                    CardType = (int)CardType.Henchmen,
                    SetId = (int)henchmen.SetName
                };

                InsertIntoCardRelationshipTable(mainCard, henchmenCard, true);
            }

            foreach (var hero in exclusions.HeroList)
            {
                var heroCard = new Card
                {
                    CardName = hero.Name,
                    CardType = (int)CardType.Hero,
                    SetId = (int)hero.SetName
                };

                InsertIntoCardRelationshipTable(mainCard, heroCard, true);
            }
        }
    }
}
