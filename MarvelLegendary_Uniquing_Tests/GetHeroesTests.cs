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
        public void TestGetHeroesThirdHero(List<int> heroesToInclude, string expectedHero)
        {
            var mastermindExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 1, 2, 3 });
            var schemeExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 3, 4, 5, 11, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 });
            var oneVillainExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 5, 6, 7, 17, 18, 28, 29, 30, 41, 42, 43, 44, 45, 46 });
            var twoVillainExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 5, 6, 7, 8, 9, 17, 18, 23, 24, 25, 26, 27, 28, 29, 30, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 });
            var henchmenExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 7, 9, 10, 11, 17, 18, 21, 22, 25, 26, 27, 30, 33, 34, 38, 39, 40, 44, 45, 46 });
            var oneHeroExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 13, 14, 15, 22, 24, 28, 29, 30, 32, 34, 37, 40, 43, 46 });
            var twoHeroExclusionHeroes = new Hero().GetHeroNameList(new List<int>() { 13, 14, 15, 17, 18, 19, 21, 22, 24, 27, 29, 30, 32, 34, 37, 40, 43, 46 });

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

            var allHeroes = new Hero().GetHeroNameList(new List<int>() { 1, 2, 3, 28 });
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

        [TestCase("Nul, Breaker of Worlds", "Hulkling")]
        [TestCase("Hulk", "Totally Awesome Hulk")]
        [TestCase("Gladiator Hulk", "Hulkbuster Iron Man")]
        [TestCase("Joe Fixit, Grey Hulk", "She-Hulk")]
        [TestCase("Skaar, Son Of Hulk", "Nul, Breaker of Worlds")]
        public void TestIsHeroNameLimit(string hulkHeroName1, string hulkHeroName2)
        {
            var newGameInfo = new GameInfo(1);
            newGameInfo.Mastermind = new Mastermind("Loki");
            newGameInfo.AllMastermindsInGame = new List<Mastermind>() { newGameInfo.Mastermind };
            newGameInfo.Scheme = new Scheme(1, "Fall of the Hulks");
            newGameInfo.Villains = new List<Villain>() { new Villain("Enemies of Asgard"), new Villain("HYDRA") };
            newGameInfo.Henchmen = new List<Henchmen>() { new Henchmen("Doombot Legion") };
            newGameInfo.Heroes = new List<Hero>();

            var allHeroes = new List<string>() { hulkHeroName1, hulkHeroName2, "Black Widow", "Captain America", "Cyclops" };
            var mastermindExclusionHeroes = new List<string>() { hulkHeroName1, hulkHeroName2 };
            var schemeExclusionHeroes = new List<string>() { "Black Widow" };
            var twoVillainExclusionHeroes = new List<string>() { "Captain America" };
            var heroListString = new List<string>();

            var testMoq = new Mock<IGetExclusions>();
            var mastermindExclusions = new GameExclusions();
            mastermindExclusions.HeroList = mastermindExclusionHeroes;

            var schemeExclusions = new GameExclusions();
            schemeExclusions.HeroList = schemeExclusionHeroes;

            var oneVillainExclusions = new GameExclusions();
            oneVillainExclusions.HeroList = new List<string>();

            var twoVillainExclusions = new GameExclusions();
            twoVillainExclusions.HeroList = twoVillainExclusionHeroes;

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
            testMoq.Setup(x => x.GetSchemeExclusions("Fall of the Hulks")).Returns(schemeExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(new List<string>() { "Enemies of Asgard" })).Returns(oneVillainExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(twoVillainStrings)).Returns(twoVillainExclusions);
            testMoq.Setup(x => x.GetHenchmenExclusion(new List<string>() { "Doombot Legion" })).Returns(henchmenExclusions);
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { hulkHeroName1 })).Returns(new List<string>());
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { hulkHeroName2 })).Returns(new List<string>());
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { hulkHeroName1, hulkHeroName2 })).Returns(new List<string>());
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { hulkHeroName2, hulkHeroName1 })).Returns(new List<string>());

            var heroList = newGameInfo.GetHeroes(mastermindExclusionHeroes, new List<Hero>(), newGameInfo.Heroes, allHeroes, testMoq.Object);
            heroListString.AddRange(from item in heroList select item.HeroName);

            CollectionAssert.AreEquivalent(new List<string>() { hulkHeroName1, hulkHeroName2, "Cyclops" }, heroListString);
        }

        [TestCase("Everybody Hates Deadpool", "Deadpool (Mercs for Money)")]
        [TestCase("Distract the Hero", "Spider-Man Noir")]
        public void TestIsIncludeHeroTeam(string schemeName, string heroName)
        {
            var newGameInfo = new GameInfo(1);
            newGameInfo.Mastermind = new Mastermind("Loki");
            newGameInfo.AllMastermindsInGame = new List<Mastermind>() { newGameInfo.Mastermind };
            newGameInfo.Scheme = new Scheme(1, schemeName);
            newGameInfo.Villains = new List<Villain>() { new Villain("Enemies of Asgard"), new Villain("HYDRA") };
            newGameInfo.Henchmen = new List<Henchmen>() { new Henchmen("Doombot Legion") };
            newGameInfo.Heroes = new List<Hero>();

            var allHeroes = new List<string>() { heroName, "Black Widow", "Captain America", "Cyclops" };
            var mastermindExclusionHeroes = new List<string>() { heroName };
            var schemeExclusionHeroes = new List<string>() { "Black Widow" };
            var twoVillainExclusionHeroes = new List<string>() { "Captain America" };
            var heroListString = new List<string>();

            var testMoq = new Mock<IGetExclusions>();
            var mastermindExclusions = new GameExclusions();
            mastermindExclusions.HeroList = mastermindExclusionHeroes;

            var schemeExclusions = new GameExclusions();
            schemeExclusions.HeroList = schemeExclusionHeroes;

            var oneVillainExclusions = new GameExclusions();
            oneVillainExclusions.HeroList = new List<string>();

            var twoVillainExclusions = new GameExclusions();
            twoVillainExclusions.HeroList = twoVillainExclusionHeroes;

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
            testMoq.Setup(x => x.GetSchemeExclusions(schemeName)).Returns(schemeExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(new List<string>() { "Enemies of Asgard" })).Returns(oneVillainExclusions);
            testMoq.Setup(x => x.GetVillainExclusion(twoVillainStrings)).Returns(twoVillainExclusions);
            testMoq.Setup(x => x.GetHenchmenExclusion(new List<string>() { "Doombot Legion" })).Returns(henchmenExclusions);
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { heroName })).Returns(new List<string>());
            testMoq.Setup(x => x.GetHeroByHeroExclusions(new List<string>() { heroName, "Cyclops" })).Returns(new List<string>());

            var heroList = newGameInfo.GetHeroes(mastermindExclusionHeroes, new List<Hero>(), newGameInfo.Heroes, allHeroes, testMoq.Object);

            Assert.AreEqual(heroName, heroList.First().HeroName);
        }

        private static readonly object[] _sourceLists =
        {
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20}, "Colossus"}, //case 1
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19}, "Cable"}, //case 2
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 16}, "Storm"}, //case 3
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 16}, "Nick Fury"}, //case 4
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 16, 21}, "Daredevil"}, //case 5
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 16, 22}, "Domino"}, //case 6
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 16}, "Hulk"}, //case 7
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 9, 12, 16, 23}, "Elektra"}, //case 8
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 9, 12, 16, 24}, "Forge"}, //case 9
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 12, 16, 25}, "Ghost Rider"}, //case 10
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 12, 16, 26}, "Ice Man"}, //case 11
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 6, 7, 12, 16, 27}, "Iron Fist"}, //case 12
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 7, 12, 16, 28}, "Jean Grey"}, //case 13
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 11, 12, 16, 17}, "Bishop"}, //case 14
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 7, 11, 12, 16, 29}, "Nightcrawler"}, //case 15
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 7, 11, 12, 16}, "Hawkeye"}, //case 16
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 11, 12, 16, 18}, "Blade"}, //case 17
            new object [] {new List<int>() { 1, 2, 3, 4, 5, 11, 12, 16, 30}, "Professor X"}, //case 18
            new object [] {new List<int>() { 1, 2, 4, 5, 11, 12, 16}, "Deadpool"}, //case 19
            new object [] {new List<int>() { 1, 2, 5, 11, 12, 16, 31}, "Punisher"}, //case 20
            new object [] {new List<int>() { 1, 2, 5, 11, 12, 16, 32}, "Wolverine (X-Force)"}, //case 21
            new object [] {new List<int>() { 1, 2, 5, 11, 12, 16}, "Rogue"}, //case 22
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 33}, "Human Torch"}, //case 23
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 34}, "Invisible Woman"}, //case 24
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 35}, "Mr. Fantastic"}, //case 25
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 36}, "Silver Surfer"}, //case 26
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 37}, "Thing"}, //case 27
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 38}, "Black Cat"}, //case 28
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 39}, "Moon Knight"}, //case 29
            new object [] {new List<int>() { 1, 2, 5, 12, 16, 40}, "Scarlet Spider"}, //case 30
            new object [] {new List<int>() { 1, 2, 12, 16, 41}, "Spider-Woman"}, //case 31
            new object [] {new List<int>() { 1, 2, 12, 16, 42}, "Symbiote Spider-Man"}, //case 32
            new object [] {new List<int>() { 1, 2, 12, 16, 43}, "Bullseye"}, //case 33
            new object [] {new List<int>() { 1, 2, 12, 16, 44}, "Dr. Octopus"}, //case 34
            new object [] {new List<int>() { 1, 2, 12, 16, 45}, "Electro"}, //case 35
            new object [] {new List<int>() { 1, 2, 12, 16, 46}, "Enchantress"}, //case 36
            new object [] {new List<int>() { 2, 12, 16}, "Captain America"} //case 37
        };
    }
}
