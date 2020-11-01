using System;
using System.Collections.Generic;
using System.Linq;

namespace MarvelLegendary
{
    class Program
    {
        static void Main(string[] args)
        {
            int output;
            Console.WriteLine("How many players are playing? (1-5)");
            var playerCount = Console.ReadLine();

            while (int.TryParse(playerCount, out output) && int.Parse(playerCount) > 0 && int.Parse(playerCount) < 6)
            {
                var game = new GameInfo(int.Parse(playerCount));
                game.SetMastermind();
                game.SetScheme();

                if (game.Scheme.SchemeInfo.NumberExtraMasterminds > 0)
                    game.SetExtraMasterminds();

                game.SetVillains(new List<string>());
                game.SetHenchmen(new List<string>());
                game.SetHeroes(new List<string>());

                var gameText = GameTextBuilder(game);
                Console.Clear();
                Console.Out.Write(gameText);

                Console.WriteLine("How many players are playing? (0 to quit)");
                playerCount = Console.ReadLine();
            }
        }

        private static string GameTextBuilder(GameInfo game)
        {
            var scheme = game.Scheme;
            var schemeInfo = game.Scheme.SchemeInfo;

            var playerCount = $"{game.PlayerCount} players take on\r\n";
            var mastermindOutput = $"Mastermind is {new Mastermind().ToString(new List<Mastermind> { game.Mastermind })}\r\n";
            var schemeOutput = $"Whose scheme is\r\n1) {scheme.SchemeName}, {scheme.SetName}\r\n\r\n";
            var villainOutput = $"Villains are {new Villain().ToString(game.Villains)}\r\n";
            var villainHeroOutput = game.Scheme.SchemeInfo.IsHeroesInVillainDeck || game.Scheme.SchemeInfo.IsRandomHeroesInVillainDeck ? $"Heroes in Villain Deck are {new Hero().ToString(game.VillainHeroes)}\r\n" : "";
            var henchmenOutput = "Henchmen " + (game.Henchmen.Count==1 ? "is" : "are") + $" {new Henchmen().ToString(game.Henchmen)}\r\n";
            var xerogenHenchmenOutput = game.Scheme.SchemeInfo.IsXerogenHenchmen ? $"All {new Henchmen().ToString(game.SchemeHenchmen)} are Xerogen Expiraments\r\n" : "";
            var heroesOutput = $"Heroes are {new Hero().ToString(game.Heroes)}\r\n";
            var royalWeddingHeroes = schemeInfo.IsRoyalWedding ? $"\r\nInclude the following heroes as Wedding Heroes: {new Hero().ToString(game.SchemeHeroes)}\r\n" : "";
            var twistsBystanderAndMasterStrikeOutput = $"Include {scheme.Twists} Scheme Twists, 5 Master Strikes, and {scheme.BystandersInVillainDeck} Bystanders in the Villain deck.\r\n";
            var woundsOutput = game.CustomWoundNumber ? $"There are {game.WoundNumber} wounds in the wound deck.\r\n": "";
            var twistsNextToScheme = scheme.IsSchemeTwistsNextToScheme ? $"Place {scheme.NumberTwistsNextToScheme} Twists next to the Scheme\r\n": "";
            var heroBystandersOutput = scheme.IsBystandersInHeroDeck ? $"Place {scheme.BystandersInHeroDeck} Bystanders in the Hero deck.\r\n" : "";
            var heroesInVillainDeck = schemeInfo.IsHeroesInVillainDeck || game.Scheme.SchemeInfo.IsRandomHeroesInVillainDeck ? $"Include the following Heroes in the Villain deck:{new Hero().ToString(game.VillainHeroes)}\r\n" : "";
            var heroHenchmen = schemeInfo.IsHenchmenInHeroDeck ? $"Include 6 cards from the following Henchmen group to the Hero deck:{new Henchmen().ToString(game.SchemeHenchmen)}\r\n" : "";
            var bindingsInGame = schemeInfo.CustomBindingCount && !schemeInfo.HasBetryalDeck ? $"The Bindings stack holds {game.BindingNumber} Bindings.\r\n" : "";
            var henchmenNextToScheme = schemeInfo.IsHenchmenNextToScheme ? $"Stack {game.NumberHenchmenNextToScheme} of the following Henchmen next to the plot.{new Henchmen().ToString(game.SchemeHenchmen)}\r\n" : "";
            var villainCardNextToScheme = schemeInfo.IsVillainCardNextToScheme ? $"From the Avengers Adversary group in Villains, put the following Adversary next to this plot:\r\n{schemeInfo.VillainCardNextToScheme}\r\n" : "";
            var bystandersNextToScheme = schemeInfo.IsBystandersNextToScheme ? $"Put {schemeInfo.BystandersNextToScheme} Bystanders next to this plot.\r\n" : "";
            var shardCount = schemeInfo.IsShardCount? $"Put {schemeInfo.ShardCount} Shards in the supply.\r\n" : "";
            var betrayalDeck = schemeInfo.HasBetryalDeck ? $"Shuffle a \'Betrayal Deck\' of {game.BindingNumber} Bindings and a 9th Twist.\r\n" : "";
            var annihilationHenchmen = schemeInfo.HasAnnihilationHenchmen ? $"Put 10 extra of the following Henchmen in the KO pile.{new Henchmen().ToString(game.SchemeHenchmen)}\r\n" : "";
            var villainSidekicks = schemeInfo.IsSidekickInVillainDeck ? $"Add {schemeInfo.SidekicksInVillainDeck} Sidekicks to the Villain deck.\r\n" : "";
            var darkAllianceMastermind = schemeInfo.IsDarkAllianceMastermind ? $"Set aside the following Mastermind and two Mastermind Tactics:{new Mastermind().ToString(game.ExtraMasterminds)} \r\n" : "";
            var tyrantVillain = schemeInfo.IsTyrantVillain ? $"Shuffle the 12 Tactics from the following Masterminds into the Villain deck:{new Mastermind().ToString(game.ExtraMasterminds)}\r\n" : "";
            var secretWarsMasterminds = schemeInfo.IsSecretWarsMasterminds ? $"Set aside the following Masterminds with one Tactic each:{new Mastermind().ToString(game.ExtraMasterminds)}\r\n" : "";
            var ambitions = schemeInfo.HasAmbitions ? "Add 10 random Ambition cards to the Villain deck.\r\n" : "";
            var villainOfficers = schemeInfo.IsVillainOfficer ? $"Add {schemeInfo.VillainOfficerCount} S.H.I.E.L.D. Officers to the Villain deck.\r\n" : "";
            var tacticsInVillainDeck = schemeInfo.IsTacticsInVillainDeck ? "Shuffle the Mastermind Tactics into the Villain deck.\r\n" : "";
            var monumentDeck = schemeInfo.IsMonumentDeck ? "Shuffle 18 Bystanders and 14 Wounds, then deal them evenly into eight decks.\r\n" : "";
            var smugglerHenchmen = schemeInfo.IsSmugglerHenchmen ? $"Include the following Henchmen as Smugglers with the Striker ability.{new Henchmen().ToString(game.SchemeHenchmen)}\r\n" : "";
            var monsterDeck = schemeInfo.IsMonsterPitDeck ? $"Shuffle 8 of the Villains into a face-down \"Monster Pit\" deck.\r\n{game.MonsterPitVillains[0].VillainName}, {game.MonsterPitVillains[0].SetName}\r\n" : "";
            var infectedDeck = schemeInfo.IsInfectedDeck ? $"Shuffle together 20 Bystanders and 10 of the following Henchmen as an \"Infected Deck.\"{new Henchmen().ToString(game.SchemeHenchmen)}\r\n" : "";
            var mutationDeck = schemeInfo.IsMutationDeck ? $"Take 14 cards from the following hero and put them in a face-up \"Mutation Pile\".:{new Hero().ToString(game.SchemeHeroes)}\r\n" : "";
            var hulkDeck = schemeInfo.IsHulkDeck ? $"Take 14 cards from the following Hero and shuffle them into a \"Hulk Deck\":{new Hero().ToString(game.SchemeHeroes)}\r\n" : "";
            var worldWarHulkMasterminds = schemeInfo.IsWorldWarHulkMasterminds ? $"Put the following Masterminds out of play lurking. All four Masterminds have two Mastermind Tactics: {new Mastermind().ToString(game.ExtraMasterminds)}\r\n" : "";
            var drainedMastermind = schemeInfo.IsDrainedMastermind ? $"Set aside the following as a \"Drained Mastermind\" and its 4 Tactics out of play:{new Mastermind().ToString(game.Scheme.SchemeInfo.DrainedMastermind)}\r\n" : "";
            var hasBindings = game.Heroes.Any(x => x.HeroInfo.IncludeBindings) || game.AllVillainsInGame.Any(x => x.VillainInfo.IncludeBindings) || game.AllMastermindsInGame.Any(x=>x.MastermindInfo.IncludeBindings) ? "Include Bindings.\r\n" : "";
            var hasNewRecruits = game.Heroes.Any(x => x.HeroInfo.IncludeNewRecruits) || game.AllHenchmenInGame.Any(x=>x.HenchmenInfo.IncludeNewRecruits) || game.Scheme.SchemeInfo.IncludeNewRecruits ? "Include New Recruits.\r\n" : "";
            var hasMadameHydra = game.Heroes.Any(x => x.HeroInfo.IncludeMadameHydra) || game.AllMastermindsInGame.Any(x => x.MastermindInfo.IncludeMadameHydra) || game.Scheme.SchemeInfo.IncludeMadameHydra ? "Include Madame Hydra.\r\n" : "";
            var hasHorrors = game.Mastermind.MastermindInfo.IncludeHorrors || game.Scheme.SchemeInfo.IncludeHorrors ? "Include horrors.\r\n" : "";
            var hasDarkLoyalty = game.Scheme.SchemeInfo.IsDarkLoyalty ? $"Include 5 cards that cost 5 or less from the hero {game.Scheme.SchemeInfo.DarkLoyaltyHero}.\r\n" : "";
            var isContestOfChampions = game.Scheme.SchemeInfo.SchemeName == "The Contest of Champions" ? "Put 11 random cards from the Hero Deck face up in a Contest Row\r\n" : "";
            var isInvasionHero = game.Scheme.SchemeInfo.SchemeName.Contains("Skrull Shapeshifters") ? "Shuffle 12 random Heroes from the Hero Deck into the Villain Deck.\r\n" : "";
            var xerogenHenchmen = schemeInfo.IsXerogenHenchmen ? $"Include 10 cards from the following Henchmen group as a Xerogen Expirament:{new Henchmen().ToString(game.SchemeHenchmen)}\r\n" : "";

            var returnString = playerCount + mastermindOutput + schemeOutput + villainOutput + villainHeroOutput + henchmenOutput + xerogenHenchmenOutput + heroesOutput + royalWeddingHeroes + twistsBystanderAndMasterStrikeOutput
                + woundsOutput  + twistsNextToScheme + heroBystandersOutput + heroesInVillainDeck + heroHenchmen + bindingsInGame + henchmenNextToScheme + villainCardNextToScheme
                + bystandersNextToScheme + shardCount + betrayalDeck + annihilationHenchmen + villainSidekicks + darkAllianceMastermind + tyrantVillain + secretWarsMasterminds + ambitions
                + villainOfficers + tacticsInVillainDeck + monumentDeck + smugglerHenchmen + monsterDeck + infectedDeck + mutationDeck + hulkDeck + worldWarHulkMasterminds + drainedMastermind
                + hasBindings + hasNewRecruits + hasMadameHydra + hasHorrors + hasDarkLoyalty + isContestOfChampions + isInvasionHero + xerogenHenchmen + "\r\n\r\n";

            return returnString;
        }


    }
}
