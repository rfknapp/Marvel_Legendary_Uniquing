using MarvelLegendary;
using MarvelLegendary.Exclusions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static MarvelLegendary.Exclusions.GetExclusions;

namespace MarvelLegendary_Uniquing_Tests
{
    [TestFixture]
    public class GetHeroesTests
    {
        [TestCaseSource("_sourceLists")]
        public void TestGetHeroesThirdHero(List<string> heroesToInclude, string expectedHero)
        {
            var mastermindExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "a", "b", "c" });
            var schemeExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "c", "d", "e", "k", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at" });
            var oneVillainExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "e", "f", "g", "q", "r", "ab", "ac", "ad", "ao", "ap", "aq", "ar", "as", "at" });
            var twoVillainExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "e", "f", "g", "h", "i", "q", "r", "w", "x", "y", "z", "aa", "ab", "ac", "ad", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at" });
            var henchmenExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "g", "i", "j", "k", "r", "q", "r", "u", "v", "y", "z", "aa", "ad", "ag", "ah", "al", "am", "an", "ar", "as", "at" });
            var oneHeroExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "m", "n", "o", "v", "x", "aa", "ac", "ad", "af", "ah", "ak", "an", "aq", "at" });
            var twoHeroExclusionHeroes = new Hero().GetHeroNameList(new List<string>() { "m", "n", "o", "q", "r", "s", "u", "v", "x", "aa", "ac", "ad", "af", "ah", "ak", "an", "aq", "at" });

            var allHeroes = new Hero().GetHeroNameList(heroesToInclude);
            var testMoq = new Mock<IGetExclusions>();
            var mastermindExclusions = new GameExclusions();
            mastermindExclusions.HeroList = mastermindExclusionHeroes;

            var schemeExclusions = new GameExclusions();
            schemeExclusions.HeroList = schemeExclusionHeroes;

            var oneVillainExclusions = new GameExclusions();
            oneVillainExclusions.HeroList = oneVillainExclusionHeroes;

            var twoVillainExclusions = new GameExclusions();
            twoVillainExclusions.HeroList = twoVillainExclusionHeroes;

            var henchmenExclusions = new GameExclusions();
            henchmenExclusions.HeroList = henchmenExclusionHeroes;

            var newGameInfo = new GameInfo(1);
            newGameInfo.Mastermind = new Mastermind("Loki");
            newGameInfo.AllMastermindsInGame = new List<Mastermind>() { newGameInfo.Mastermind };
            newGameInfo.Scheme = new Scheme(1, "Steal the Weaponized Plutonium");
            newGameInfo.Villains = new List<Villain>() { new Villain("Enemies of Asgard"), new Villain("HYDRA") };
            newGameInfo.Henchmen = new List<Henchmen>() { new Henchmen("Doombot Legion") };
            newGameInfo.Heroes = new List<Hero>() { new Hero("Spider-Man"), new Hero("Angel") };

            var mastermindStrings = new List<string>();
            foreach (var item in newGameInfo.AllMastermindsInGame)
            {
                mastermindStrings.Add(item.MastermindName);
            }
            var twoVillainStrings = new List<string>();
            foreach (var item in newGameInfo.Villains)
            {
                twoVillainStrings.Add(item.VillainName);
            }

            testMoq.Setup(x => x.GetMastermindExclusion(mastermindStrings)).Returns(mastermindExclusions);
            testMoq.Setup(x => x.GetSchemeExclusions("Steal the Weaponized Plutonium")).Returns(schemeExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(new List<string>() { "Enemies of Asgard" })).Returns(oneVillainExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(twoVillainStrings)).Returns(twoVillainExclusions);
            testMoq.Setup(x => x.GetHenchmenExclusion(new List<string>() { "Doombot Legion" })).Returns(henchmenExclusions);
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { "Spider-Man" })).Returns(oneHeroExclusionHeroes);
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { "Spider-Man", "Angel" })).Returns(twoHeroExclusionHeroes);

            var heroList = newGameInfo.GetHeroes(mastermindExclusionHeroes, new List<Hero>(), newGameInfo.Heroes, allHeroes, testMoq.Object);

            Assert.AreEqual(expectedHero, heroList.Last().HeroName);
        }

        [Test]
        public void TestIsHeroesInVillainDeck()
        {
            var newGameInfo = new GameInfo(1);
            newGameInfo.Mastermind = new Mastermind("Loki");
            newGameInfo.AllMastermindsInGame = new List<Mastermind>() { newGameInfo.Mastermind };
            newGameInfo.Scheme = new Scheme(1, "The Dark Phoenix Saga");
            newGameInfo.Villains = new List<Villain>() { new Villain("Enemies of Asgard"), new Villain("HYDRA") };
            newGameInfo.Henchmen = new List<Henchmen>() { new Henchmen("Doombot Legion") };
            newGameInfo.Heroes = new List<Hero>();

            var allHeroes = new Hero().GetHeroNameList(new List<string>() { "ab", "a", "b", "c"});
            var mastermindExclusionHeroes = new List<string>() { "Black Widow"};
            var schemeExclusionHeroes = new List<string>() { "Captain America" };
            var schemeHeroes = new List<Hero>() { new Hero("Jean Grey") };
            var heroListString = new List<string>();

            var testMoq = new Mock<IGetExclusions>();
            var mastermindExclusions = new GameExclusions();
            mastermindExclusions.HeroList = mastermindExclusionHeroes;

            var schemeExclusions = new GameExclusions();
            schemeExclusions.HeroList = schemeExclusionHeroes;

            var oneVillainExclusions = new GameExclusions();
            oneVillainExclusions.HeroList = new List<string>();

            var twoVillainExclusions = new GameExclusions();
            twoVillainExclusions.HeroList = new List<string>();

            var henchmenExclusions = new GameExclusions();
            henchmenExclusions.HeroList = new List<string>();

            var mastermindStrings = new List<string>();
            foreach (var item in newGameInfo.AllMastermindsInGame)
            {
                mastermindStrings.Add(item.MastermindName);
            }
            var twoVillainStrings = new List<string>();
            foreach (var item in newGameInfo.Villains)
            {
                twoVillainStrings.Add(item.VillainName);
            }

            testMoq.Setup(x => x.GetMastermindExclusion(mastermindStrings)).Returns(mastermindExclusions);
            testMoq.Setup(x => x.GetSchemeExclusions("The Dark Phoenix Saga")).Returns(schemeExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(new List<string>() { "Enemies of Asgard" })).Returns(oneVillainExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(twoVillainStrings)).Returns(twoVillainExclusions);
            testMoq.Setup(x => x.GetHenchmenExclusion(new List<string>() { "Doombot Legion" })).Returns(henchmenExclusions);
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { "Cyclops" })).Returns(new List<string>());
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { "Cyclops", "Captain America" })).Returns(new List<string>());

            var heroList = newGameInfo.GetHeroes(mastermindExclusionHeroes, schemeHeroes, newGameInfo.Heroes, allHeroes, testMoq.Object);
            heroListString.AddRange(from item in heroList select item.HeroName);

            CollectionAssert.DoesNotContain(heroListString, "Jean Grey");
        }

        private static readonly object[] _sourceLists =
        {
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t"}, "Colossus"}, //case 1
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s"}, "Cable"}, //case 2
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "p"}, "Storm"}, //case 3
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "l", "p"}, "Nick Fury"}, //case 4
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "l", "p", "u"}, "Daredevil"}, //case 5
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "l", "p", "v"}, "Domino"}, //case 6
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "l", "p"}, "Hulk"}, //case 7
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "i", "l", "p", "w"}, "Elektra"}, //case 8
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "i", "l", "p", "x"}, "Forge"}, //case 9
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "l", "p", "y"}, "Ghost Rider"}, //case 10
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "l", "p", "z"}, "Ice Man"}, //case 11
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "f", "g", "l", "p", "aa"}, "Iron Fist"}, //case 12
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "g", "l", "p", "ab"}, "Jean Grey"}, //case 13
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "k", "l", "p", "q"}, "Bishop"}, //case 14
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "g", "k", "l", "p", "ac"}, "Nightcrawler"}, //case 15
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "g", "k", "l", "p"}, "Hawkeye"}, //case 16
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "k", "l", "p", "r"}, "Blade"}, //case 17
            new object [] {new List<string>() { "a", "b", "c", "d", "e", "k", "l", "p", "ad"}, "Professor X"}, //case 18
            new object [] {new List<string>() { "a", "b", "d", "e", "k", "l", "p"}, "Deadpool"}, //case 19
            new object [] {new List<string>() { "a", "b", "e", "k", "l", "p", "ae"}, "Punisher"}, //case 20
            new object [] {new List<string>() { "a", "b", "e", "k", "l", "p", "af"}, "Wolverine (X-Force)"}, //case 21
            new object [] {new List<string>() { "a", "b", "e", "k", "l", "p"}, "Rogue"}, //case 22
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "ag"}, "Human Torch"}, //case 23
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "ah"}, "Invisible Woman"}, //case 24
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "ai"}, "Mr. Fantastic"}, //case 25
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "aj"}, "Silver Surfer"}, //case 26
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "ak"}, "Thing"}, //case 27
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "al"}, "Black Cat"}, //case 28
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "am"}, "Moon Knight"}, //case 29
            new object [] {new List<string>() { "a", "b", "e", "l", "p", "an"}, "Scarlet Spider"}, //case 30
            new object [] {new List<string>() { "a", "b", "l", "p", "ao"}, "Spider-Woman"}, //case 31
            new object [] {new List<string>() { "a", "b", "l", "p", "ap"}, "Symbiote Spider-Man"}, //case 32
            new object [] {new List<string>() { "a", "b", "l", "p", "aq"}, "Bullseye"}, //case 33
            new object [] {new List<string>() { "a", "b", "l", "p", "ar"}, "Dr. Octopus"}, //case 34
            new object [] {new List<string>() { "a", "b", "l", "p", "as"}, "Electro"}, //case 35
            new object [] {new List<string>() { "a", "b", "l", "p", "at"}, "Enchantress"}, //case 36
            new object [] {new List<string>() { "b", "l", "p"}, "Captain America"} //case 37
        };
    }
}
