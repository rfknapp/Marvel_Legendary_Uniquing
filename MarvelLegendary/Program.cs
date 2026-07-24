using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarvelLegendary.Exclusions;
using MarvelLegendary.Enums;
using MarvelLegendary.Tools;
using Microsoft.Data.Sqlite;

namespace MarvelLegendary
{
    class Program
    {
        static void Main()
        {
            SqlHelper.SetupDatabase();
            ConvertGames.ConvertTrackedGames();

            Console.WriteLine("How many players are playing? (1-5)");
            var playerCount = Console.ReadLine();

            while (int.TryParse(playerCount, out _) && int.Parse(playerCount) > 0 && int.Parse(playerCount) < 6)
            {
                var game = new GameInfo(int.Parse(playerCount));
                game.SetMastermind();
                game.SetScheme();
                
                if (game.Scheme.SchemeInfo.NumberExtraMasterminds > 0)
                    game.SetExtraMasterminds();
                
                game.SetVillains();
                game.SetHenchmen();
                game.SetHeroes();
                
                var gameText = GameTextBuilder(game);
                
                Console.Clear();
                Console.Out.Write(gameText);
                
                if (game.Scheme.SchemeInfo.isVeiled)
                {
                    game.SetUnVeiledScheme();
                    Console.WriteLine("Press any key to reveal unveiled scheme.");
                    Console.ReadLine();
                    Console.WriteLine($"Unveiled scheme is\r\n1) {game.UnveiledScheme.SchemeName}, {game.UnveiledScheme.SetName}\r\n\r\n");
                }

                //WatchForDuplicates(game);

                //Need to rework this now that there is a different db schema
                //var test = new GetExclusions().GetMastermindByMastermindExclusions(game.Mastermind);

                Console.WriteLine("How many players are playing? (0 to quit)");
                playerCount = Console.ReadLine();
            }

            void WatchForDuplicates(GameInfo game)
            {
                if (game.Villains.GroupBy(v => new { v.VillainName, v.SetName }).Any(g => g.Count() > 1))
                {
                    var duplicates = game.Villains.GroupBy(v => new { v.VillainName, v.SetName }).Where(g => g.Count() > 1).SelectMany(g => g).ToList();
                    Console.WriteLine(Villain.ToString(duplicates));
                }

                if (game.HenchmenList.GroupBy(h => new { h.HenchmenName, h.HenchmenSet }).Any(g => g.Count() > 1))
                {
                    var duplicates = game.HenchmenList.GroupBy(h => new { h.HenchmenName, h.HenchmenSet }).Where(g => g.Count() > 1).SelectMany(g => g).ToList();
                    Console.WriteLine(Henchmen.ToString(duplicates));
                }

                if (game.Heroes.GroupBy(h => new { h.HeroName, h.SetName }).Any(g => g.Count() > 1))
                {
                    var duplicates = game.Heroes.GroupBy(h => new { h.HeroName, h.SetName }).Where(g => g.Count() > 1).SelectMany(g => g).ToList();
                    Console.WriteLine(Hero.ToString(duplicates));
                }
            }
        }

        private static string GameTextBuilder(GameInfo game)
        {
            var scheme = game.Scheme;
            var schemeInfo = game.Scheme.SchemeInfo;

            var playerCount = $"{game.PlayerCount} players take on\r\n";
            var mastermindOutput = $"Mastermind is {Mastermind.ToString(new List<Mastermind> { game.Mastermind })}\r\n";
            //var schemeOutput = $"Whose scheme is\r\n1) {scheme.SchemeName}, {scheme.SetName}\r\n\r\n";
            var schemeOutput = $"Whose scheme is\r\n1) {Scheme.ToString(scheme)}\r\n\r\n";
            var villainOutput = $"Villains are {Villain.ToString(game.Villains)}\r\n";
            var villainHeroOutput = game.Scheme.SchemeInfo.IsHeroesInVillainDeck || game.Scheme.SchemeInfo.IsRandomHeroesInVillainDeck ? $"Heroes in Villain Deck are {Hero.ToString(game.VillainHeroes)}\r\n" : "";
            var henchmenOutput = "Henchmen " + (game.HenchmenList.Count==1 ? "is" : "are") + $" {Henchmen.ToString(game.HenchmenList)}\r\n";
            var heroesOutput = $"Heroes are {Hero.ToString(game.Heroes)}\r\n";
            var twistsBystanderAndMasterStrikeOutput = $"Include {scheme.Twists} Scheme Twists, 5 Master Strikes, and {scheme.BystandersInVillainDeck} Bystanders in the Villain deck.\r\n";
            var woundsOutput = game.CustomWoundNumber ? $"There are {game.WoundNumber} wounds in the wound deck.\r\n": "";
            var twistsNextToScheme = scheme.IsSchemeTwistsNextToScheme ? $"Place {scheme.NumberTwistsNextToScheme} Twists next to the Scheme\r\n": "";
            var heroBystandersOutput = scheme.IsBystandersInHeroDeck ? $"Place {scheme.BystandersInHeroDeck} Bystanders in the Hero deck.\r\n" : "";
            var heroesInVillainDeck = schemeInfo.IsHeroesInVillainDeck || game.Scheme.SchemeInfo.IsRandomHeroesInVillainDeck ? $"Include the following Heroes in the Villain deck:{Hero.ToString(game.VillainHeroes)}\r\n" : "";
            var randomHeroeCardsInVillainDeck = schemeInfo.IsRandomHeroCardsInVillainDeck ? $"Shuffle {game.Scheme.SchemeInfo.NumberRandomHeroCardsInVillainDeck} random cards from the Hero Deck into the Villain Deck\r\n" : "";
            var heroHenchmen = schemeInfo.IsHenchmenInHeroDeck ? $"Include 6 cards from the following Henchmen group to the Hero deck:{Henchmen.ToString(game.SchemeHenchmen)}\r\n" : "";
            var bindingsInGame = schemeInfo.CustomBindingCount && !schemeInfo.HasBetryalDeck ? $"The Bindings stack holds {game.BindingNumber} Bindings.\r\n" : "";
            var henchmenNextToScheme = schemeInfo.IsHenchmenNextToScheme ? $"Stack {game.NumberHenchmenNextToScheme} of the following Henchmen next to the plot.{Henchmen.ToString(game.SchemeHenchmen)}\r\n" : "";
            var villainCardNextToScheme = schemeInfo.IsVillainCardNextToScheme ? $"From the Avengers Adversary group in Villains, put the following Adversary next to this plot:\r\n{schemeInfo.VillainCardNextToScheme}\r\n" : "";
            var bystandersNextToScheme = schemeInfo.IsBystandersNextToScheme ? $"Put {schemeInfo.BystandersNextToScheme} Bystanders next to this plot.\r\n" : "";
            var shardCount = schemeInfo.IsShardCount? $"Put {schemeInfo.ShardCount} Shards in the supply.\r\n" : "";
            var betrayalDeck = schemeInfo.HasBetryalDeck ? $"Shuffle a \'Betrayal Deck\' of {game.BindingNumber} Bindings and a 9th Twist.\r\n" : "";
            var annihilationHenchmen = schemeInfo.HasAnnihilationHenchmen ? $"Put 10 extra of the following Henchmen in the KO pile.{Henchmen.ToString(game.SchemeHenchmen)}\r\n" : "";
            var villainSidekicks = schemeInfo.IsSidekickInVillainDeck ? $"Add {schemeInfo.SidekicksInVillainDeck} Sidekicks to the Villain deck.\r\n" : "";
            var darkAllianceMastermind = schemeInfo.IsDarkAllianceMastermind ? $"Set aside the following Mastermind and two Mastermind Tactics:{Mastermind.ToString(game.ExtraMasterminds)} \r\n" : "";
            var tyrantVillain = schemeInfo.IsTyrantVillain ? $"Shuffle the 12 Tactics from the following Masterminds into the Villain deck:{Mastermind.ToString(game.ExtraMasterminds)}\r\n" : "";
            var secretWarsMasterminds = schemeInfo.IsSecretWarsMasterminds ? $"Set aside the following Masterminds with one Tactic each:{Mastermind.ToString(game.ExtraMasterminds)}\r\n" : "";
            var ambitions = schemeInfo.HasAmbitions ? "Add 10 random Ambition cards to the Villain deck.\r\n" : "";
            var villainOfficers = schemeInfo.IsVillainOfficer ? $"Add {schemeInfo.VillainOfficerCount} S.H.I.E.L.D. Officers to the Villain deck.\r\n" : "";
            var tacticsInVillainDeck = schemeInfo.IsTacticsInVillainDeck ? "Shuffle the Mastermind Tactics into the Villain deck.\r\n" : "";
            var monumentDeck = schemeInfo.IsMonumentDeck ? "Shuffle 18 Bystanders and 14 Wounds, then deal them evenly into eight decks.\r\n" : "";
            var smugglerHenchmen = schemeInfo.IsSmugglerHenchmen ? $"Include the following Henchmen as Smugglers with the Striker ability.{Henchmen.ToString(game.SchemeHenchmen)}\r\n" : "";
            var monsterDeck = schemeInfo.IsMonsterPitDeck ? $"Shuffle 8 of the Villains into a face-down \"Monster Pit\" deck.\r\n{game.SchemeVillains[0].VillainName}, {game.SchemeVillains[0].SetName}\r\n" : "";
            var infectedDeck = schemeInfo.IsInfectedDeck ? $"Shuffle together 20 Bystanders and 10 of the following Henchmen as an \"Infected Deck.\"{Henchmen.ToString(game.SchemeHenchmen)}\r\n" : "";
            var mutationDeck = schemeInfo.IsMutationDeck ? $"Take 14 cards from the following hero and put them in a face-up \"Mutation Pile\".:{Hero.ToString(game.SchemeHeroes)}\r\n" : "";
            var hulkDeck = schemeInfo.IsHulkDeck ? $"Take 14 cards from the following Hero and shuffle them into a \"Hulk Deck\":{Hero.ToString(game.SchemeHeroes)}\r\n" : "";
            var worldWarHulkMasterminds = schemeInfo.IsWorldWarHulkMasterminds ? $"Put the following Masterminds out of play lurking. All four Masterminds have two Mastermind Tactics: {Mastermind.ToString(game.ExtraMasterminds)}\r\n" : "";
            var drainedMastermind = schemeInfo.IsDrainedMastermind ? $"Set aside the following as a \"Drained Mastermind\" and its 4 Tactics out of play:{Mastermind.ToString(game.Scheme.SchemeInfo.DrainedMastermind)}\r\n" : "";
            var hasBindings = game.Heroes.Any(x => x.HeroInfo.IncludeBindings) || game.AllVillainsInGame.Any(x => x.VillainInfo.IncludeBindings) || game.AllMastermindsInGame.Any(x=>x.MastermindInfo.IncludeBindings) ? "Include Bindings.\r\n" : "";
            var hasNewRecruits = game.Heroes.Any(x => x.HeroInfo.IncludeNewRecruits) || game.AllHenchmenInGame.Any(x=>x.HenchmenInfo.IncludeNewRecruits) || game.Scheme.SchemeInfo.IncludeNewRecruits ? "Include New Recruits.\r\n" : "";
            var hasMadameHydra = game.Heroes.Any(x => x.HeroInfo.IncludeMadameHydra) || game.AllMastermindsInGame.Any(x => x.MastermindInfo.IncludeMadameHydra) || game.Scheme.SchemeInfo.IncludeMadameHydra ? "Include Madame Hydra.\r\n" : "";
            var hasHorrors = game.Mastermind.MastermindInfo.IncludeHorrors || game.Scheme.SchemeInfo.IncludeHorrors ? "Include horrors.\r\n" : "";
            var hasDarkLoyalty = game.Scheme.SchemeInfo.IsDarkLoyalty ? $"Include 5 cards that cost 5 or less from the hero {game.Scheme.SchemeInfo.DarkLoyaltyHero}.\r\n" : "";
            var isContestOfChampions = game.Scheme.SchemeInfo.SchemeName == "The Contest of Champions" ? "Put 11 random cards from the Hero Deck face up in a Contest Row\r\n" : "";
            var isInvasionHero = game.Scheme.SchemeInfo.SchemeName.Contains("Skrull Shapeshifters") ? "Shuffle 12 random Heroes from the Hero Deck into the Villain Deck.\r\n" : "";
            var sneakAttackString = game.Scheme.SchemeInfo.SchemeName == "Sneak Attack the Heroes" ? SneakAttackRuleGenerator(game.PlayerCount, game.Heroes) : "";
            var zombieVillainsString = game.Mastermind.MastermindInfo.IsZombieSoloVillain && game.PlayerCount == 1 ? "Treat the Villain group as having the Zombie keyword.\r\n" : "";
            var quantumRealmString = game.Scheme.SchemeInfo.IsQuantumRealmDeck ? $"Set aside the {game.SchemeVillains.FirstOrDefault().VillainName} ({game.SchemeVillains[0].SetName}) Villain Group as an extra group. Shuffle its Ambush Scheme into the Villain Deck.\r\n" : "";
            var pastHeroDeck = game.Scheme.SchemeName == "The Time Heist" ? "Set half of the hero groups in the main city. The other half of the hero groups make a Past Hero Deck." : "";
            var shrinkTechDeck = game.Scheme.SchemeInfo.IsShrinkTechHero ? $"Set aside all 14 cards of the {game.Scheme.SchemeInfo.ShrinkTechHero.HeroName} hero group as Shrink Tech.\r\n" : "";
            var lovedOnesDeck = game.Scheme.SchemeInfo.IsLovedOne ? $"Set aside a lowest-cost card for each hero Name, face up,w ith 2 face up Bystanders under it as Loved Ones.\r\n" : "";

            var returnString = playerCount + mastermindOutput + schemeOutput + villainOutput + villainHeroOutput + henchmenOutput + heroesOutput + twistsBystanderAndMasterStrikeOutput + woundsOutput
                + twistsNextToScheme + heroBystandersOutput + heroesInVillainDeck + heroHenchmen + bindingsInGame + henchmenNextToScheme + villainCardNextToScheme
                + bystandersNextToScheme + shardCount + betrayalDeck + annihilationHenchmen + villainSidekicks + darkAllianceMastermind + tyrantVillain + secretWarsMasterminds + ambitions
                + villainOfficers + tacticsInVillainDeck + monumentDeck + smugglerHenchmen + monsterDeck + infectedDeck + mutationDeck + hulkDeck + worldWarHulkMasterminds + drainedMastermind
                + hasBindings + hasNewRecruits + hasMadameHydra + hasHorrors + hasDarkLoyalty + isContestOfChampions + isInvasionHero + zombieVillainsString + pastHeroDeck + quantumRealmString
                + shrinkTechDeck + lovedOnesDeck + $"\r\n{sneakAttackString}\r\n";

            return returnString;
        }

        private static string SneakAttackRuleGenerator(int playerCount, List<Hero> heroes)
        {
            var returnString = "";
            for (int i = 0; i < playerCount; i++)
            {
                returnString = $"{returnString}Player {i + 1} chooses three non-rare cards with different names from the {heroes[i].HeroName} deck and three wounds and adds them to their deck.\r\n";
            }
            
            return returnString;
        }
    }

    public static class DatabasePaths
    {
        public static string DatabasePath
        {
            get
            {
                //var folder = Path.Combine(
                //    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                //    "MarvelLegendary_Uniquing");

                var folder = @"D:\git\Marvel_Legendary_Uniquing\MarvelLegendary\Data";

                Directory.CreateDirectory(folder);

                return Path.Combine(folder, "MarvelLegendary.db");
            }
        }
    }
}
