using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MarvelLegendary
{
    public class HeroConnection 
    {
        public string Letter { get; set; }
        public string HeroName { get; set; }

        public HeroConnection(string letter, string heroName)
        {
            Letter = letter;
            HeroName = heroName;
        }
    }

    public enum HeroTeam
    {
        None,
        [Description("Avengers")]
        Avengers,
        [Description("X-Men")]
        XMen,
        [Description("S.H.I.E.L.D.")]
        SHIELD,
        [Description("Spider Friends")]
        SpiderFriends,
        [Description("Marvel Knights")]
        MarvelKnights,
        [Description("X-Force")]
        XForce,
        [Description("Fantastic Four")]
        FantasticFour,
        [Description("Crime Syndicate")]
        CrimeSyndicate,
        [Description("Sinister Six")]
        SinisterSix,
        [Description("Foes Of Asgard")]
        FoesOfAsgard,
        [Description("Brotherhood")]
        Brotherhood,
        [Description("Guardians Of The Galaxy")]
        GuardiansOfTheGalaxy,
        [Description("H.Y.D.R.A.")]
        HYDRA,
        [Description("Illuminati")]
        Illuminati,
        [Description("Cabal")]
        Cabal,
        [Description("New Warriors")]
        NewWarriors,
        [Description("Mercs For Money")]
        MercsForMoney,
        [Description("Champions")]
        Champions,
        [Description("Warbound")]
        Warbound,
        [Description("Venomverse")]
        Venomverse,
        [Description("Unaffiliated")]
        Unaffiliated,
        [Description("Heroes of Asgard")]
        HeroesOfAsgard
    };

    public class HeroInfo
    {
        public string HeroName { get; set; }
        public GameInfo.Set SetName { get; set; }
        public HeroTeam HeroTeam { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeNewRecruits { get; set; }
        public bool IncludeBindings { get; set; }
        public bool IncludeMadameHydra { get; set; }

        public HeroInfo(string name, GameInfo.Set setName, HeroTeam heroTeam, bool includeNewRecruits = false, bool includeBindings = false, bool includeMadameHydra = false)
        {
            HeroName = name;
            SetName = setName;
            HeroTeam = heroTeam;
            IsDuplicate = false;
            DuplicateName = "";
            IncludeNewRecruits = includeNewRecruits;
            IncludeBindings = includeBindings;
            IncludeMadameHydra = includeMadameHydra;
        }
    }

    public class Hero
    {
        public string HeroName { get; set; }
        public GameInfo.Set SetName { get; set; }
        public HeroTeam HeroTeam { get; set; }
        public HeroInfo HeroInfo { get; set; }
        public int Order { get; set; }

        private readonly List<HeroInfo> _heroes = new List<HeroInfo>()
        {
            new HeroInfo("Black Widow", GameInfo.Set.Core, HeroTeam.Avengers),
            new HeroInfo("Captain America", GameInfo.Set.Core, HeroTeam.Avengers),
            new HeroInfo("Cyclops", GameInfo.Set.Core, HeroTeam.XMen),
            new HeroInfo("Deadpool", GameInfo.Set.Core, HeroTeam.Unaffiliated),
            new HeroInfo("Emma Frost", GameInfo.Set.Core, HeroTeam.XMen),
            new HeroInfo("Gambit", GameInfo.Set.Core, HeroTeam.XMen),
            new HeroInfo("Hawkeye", GameInfo.Set.Core, HeroTeam.Avengers),
            new HeroInfo("Hulk", GameInfo.Set.Core, HeroTeam.Avengers),
            new HeroInfo("Iron Man", GameInfo.Set.Core, HeroTeam.Avengers),
            new HeroInfo("Nick Fury", GameInfo.Set.Core, HeroTeam.SHIELD),
            new HeroInfo("Rogue", GameInfo.Set.Core, HeroTeam.XMen),
            new HeroInfo("Spider-Man", GameInfo.Set.Core, HeroTeam.SpiderFriends),
            new HeroInfo("Storm", GameInfo.Set.Core, HeroTeam.XMen),
            new HeroInfo("Thor", GameInfo.Set.Core, HeroTeam.Avengers),
            new HeroInfo("Wolverine", GameInfo.Set.Core, HeroTeam.XMen),

            new HeroInfo("Angel", GameInfo.Set.Dc, HeroTeam.XMen),
            new HeroInfo("Bishop", GameInfo.Set.Dc, HeroTeam.XMen),
            new HeroInfo("Blade", GameInfo.Set.Dc, HeroTeam.MarvelKnights),
            new HeroInfo("Cable", GameInfo.Set.Dc, HeroTeam.XForce),
            new HeroInfo("Colossus", GameInfo.Set.Dc, HeroTeam.XForce),
            new HeroInfo("Daredevil", GameInfo.Set.Dc, HeroTeam.MarvelKnights),
            new HeroInfo("Domino", GameInfo.Set.Dc, HeroTeam.XForce),
            new HeroInfo("Elektra", GameInfo.Set.Dc, HeroTeam.MarvelKnights),
            new HeroInfo("Forge", GameInfo.Set.Dc, HeroTeam.XForce),
            new HeroInfo("Ghost Rider", GameInfo.Set.Dc, HeroTeam.MarvelKnights),
            new HeroInfo("Ice Man", GameInfo.Set.Dc, HeroTeam.XMen),
            new HeroInfo("Iron Fist", GameInfo.Set.Dc, HeroTeam.MarvelKnights),
            new HeroInfo("Jean Grey", GameInfo.Set.Dc, HeroTeam.XMen),
            new HeroInfo("Nightcrawler", GameInfo.Set.Dc, HeroTeam.XMen),
            new HeroInfo("Professor X", GameInfo.Set.Dc, HeroTeam.XMen),
            new HeroInfo("Punisher", GameInfo.Set.Dc, HeroTeam.MarvelKnights),
            new HeroInfo("Wolverine (X-Force)", GameInfo.Set.Dc, HeroTeam.XForce),

            new HeroInfo("Human Torch", GameInfo.Set.Ff, HeroTeam.FantasticFour),
            new HeroInfo("Invisible Woman", GameInfo.Set.Ff, HeroTeam.FantasticFour),
            new HeroInfo("Mr. Fantastic", GameInfo.Set.Ff, HeroTeam.FantasticFour),
            new HeroInfo("Silver Surfer", GameInfo.Set.Ff, HeroTeam.Unaffiliated),
            new HeroInfo("Thing", GameInfo.Set.Ff, HeroTeam.FantasticFour),
            
            new HeroInfo("Black Cat", GameInfo.Set.PttR, HeroTeam.SpiderFriends),
            new HeroInfo("Moon Knight", GameInfo.Set.PttR, HeroTeam.MarvelKnights),
            new HeroInfo("Scarlet Spider", GameInfo.Set.PttR, HeroTeam.SpiderFriends),
            new HeroInfo("Spider-Woman", GameInfo.Set.PttR, HeroTeam.SpiderFriends),
            new HeroInfo("Symbiote Spider-Man", GameInfo.Set.PttR, HeroTeam.SpiderFriends),
            
            new HeroInfo("Bullseye", GameInfo.Set.Villains, HeroTeam.CrimeSyndicate),
            new HeroInfo("Dr. Octopus", GameInfo.Set.Villains, HeroTeam.SinisterSix),
            new HeroInfo("Electro", GameInfo.Set.Villains, HeroTeam.SinisterSix),
            new HeroInfo("Enchantress", GameInfo.Set.Villains, HeroTeam.FoesOfAsgard, true),
            new HeroInfo("Green Goblin", GameInfo.Set.Villains, HeroTeam.SinisterSix),
            new HeroInfo("Juggernaut", GameInfo.Set.Villains, HeroTeam.Brotherhood),
            new HeroInfo("Kingpin", GameInfo.Set.Villains, HeroTeam.CrimeSyndicate, true),
            new HeroInfo("Kraven", GameInfo.Set.Villains, HeroTeam.SinisterSix),
            new HeroInfo("Loki", GameInfo.Set.Villains, HeroTeam.FoesOfAsgard, true, true),
            new HeroInfo("Magneto", GameInfo.Set.Villains, HeroTeam.Brotherhood, false, true),
            new HeroInfo("Mysterio", GameInfo.Set.Villains, HeroTeam.SinisterSix),
            new HeroInfo("Mystique", GameInfo.Set.Villains, HeroTeam.Brotherhood),
            new HeroInfo("Sabretooth", GameInfo.Set.Villains, HeroTeam.Brotherhood),
            new HeroInfo("Ultron", GameInfo.Set.Villains, HeroTeam.Unaffiliated),
            new HeroInfo("Venom", GameInfo.Set.Villains, HeroTeam.SinisterSix),
            
            new HeroInfo("Drax the Destroyer", GameInfo.Set.GotG, HeroTeam.GuardiansOfTheGalaxy),
            new HeroInfo("Gamora", GameInfo.Set.GotG, HeroTeam.GuardiansOfTheGalaxy),
            new HeroInfo("Groot", GameInfo.Set.GotG, HeroTeam.GuardiansOfTheGalaxy),
            new HeroInfo("Rocket Raccoon", GameInfo.Set.GotG, HeroTeam.GuardiansOfTheGalaxy),
            new HeroInfo("Star-Lord", GameInfo.Set.GotG, HeroTeam.GuardiansOfTheGalaxy),
            
            new HeroInfo("Greithoth, Breaker of Wills", GameInfo.Set.Fi, HeroTeam.FoesOfAsgard),
            new HeroInfo("Kuurth, Breaker of Stone", GameInfo.Set.Fi, HeroTeam.FoesOfAsgard),
            new HeroInfo("Nerkkod, Breaker of Oceans", GameInfo.Set.Fi, HeroTeam.FoesOfAsgard, true),
            new HeroInfo("Nul, Breaker of Worlds", GameInfo.Set.Fi, HeroTeam.FoesOfAsgard, false, true),
            new HeroInfo("Skadi", GameInfo.Set.Fi, HeroTeam.HYDRA, false, false, true),
            new HeroInfo("Skirn, Breaker of Men", GameInfo.Set.Fi, HeroTeam.FoesOfAsgard, true),
            
            new HeroInfo("Apocalyptic Kitty Pryde", GameInfo.Set.Sw1, HeroTeam.XMen),
            new HeroInfo("Black Bolt", GameInfo.Set.Sw1, HeroTeam.Illuminati),
            new HeroInfo("Black Panther", GameInfo.Set.Sw1, HeroTeam.Illuminati),
            new HeroInfo("Captain Marvel", GameInfo.Set.Sw1, HeroTeam.Avengers),
            new HeroInfo("Dr. Strange", GameInfo.Set.Sw1, HeroTeam.Illuminati),
            new HeroInfo("Lady Thor", GameInfo.Set.Sw1, HeroTeam.Avengers),
            new HeroInfo("Magik", GameInfo.Set.Sw1, HeroTeam.XMen),
            new HeroInfo("Maximus", GameInfo.Set.Sw1, HeroTeam.Cabal),
            new HeroInfo("Namor", GameInfo.Set.Sw1, HeroTeam.Cabal),
            new HeroInfo("Old Man Logan", GameInfo.Set.Sw1, HeroTeam.XMen),
            new HeroInfo("Proxima Midnight", GameInfo.Set.Sw1, HeroTeam.Cabal),
            new HeroInfo("Superior Iron Man", GameInfo.Set.Sw1, HeroTeam.Illuminati),
            new HeroInfo("Thanos", GameInfo.Set.Sw1, HeroTeam.Cabal),
            new HeroInfo("Ultimate Spider-Man", GameInfo.Set.Sw1, HeroTeam.SpiderFriends),
            
            new HeroInfo("Agent Venom", GameInfo.Set.Sw2, HeroTeam.SpiderFriends),
            new HeroInfo("Arkon the Magnificent", GameInfo.Set.Sw2, HeroTeam.Unaffiliated),
            new HeroInfo("Beast", GameInfo.Set.Sw2, HeroTeam.Illuminati),
            new HeroInfo("Black Swan", GameInfo.Set.Sw2, HeroTeam.Cabal),
            new HeroInfo("The Captain and the Devil", GameInfo.Set.Sw2, HeroTeam.Avengers),
            new HeroInfo("Captain Britain", GameInfo.Set.Sw2, HeroTeam.Illuminati),
            new HeroInfo("Corvus Glaive", GameInfo.Set.Sw2, HeroTeam.Cabal),
            new HeroInfo("Dr. Punisher, Soldier Supreme", GameInfo.Set.Sw2, HeroTeam.MarvelKnights),
            new HeroInfo("Elsa Bloodstone", GameInfo.Set.Sw2, HeroTeam.SHIELD),
            new HeroInfo("Phoenix Force Cyclops", GameInfo.Set.Sw2, HeroTeam.XMen),
            new HeroInfo("Ruby Summers", GameInfo.Set.Sw2, HeroTeam.XMen),
            new HeroInfo("Shang-Chi", GameInfo.Set.Sw2, HeroTeam.MarvelKnights),
            new HeroInfo("Silk", GameInfo.Set.Sw2, HeroTeam.SpiderFriends),
            new HeroInfo("Soulsword Colossus", GameInfo.Set.Sw2, HeroTeam.XMen),
            new HeroInfo("Spider-Gwen", GameInfo.Set.Sw2, HeroTeam.SpiderFriends),
            new HeroInfo("Time-Traveling Jean Grey", GameInfo.Set.Sw2, HeroTeam.XMen),
            
            new HeroInfo("Agent X-13", GameInfo.Set.Ca, HeroTeam.SHIELD),
            new HeroInfo("Captain America 1941", GameInfo.Set.Ca, HeroTeam.Avengers),
            new HeroInfo("Captain America (Falcon)", GameInfo.Set.Ca, HeroTeam.Avengers),
            new HeroInfo("Steve Rogers, Director of S.H.I.E.L.D.", GameInfo.Set.Ca, HeroTeam.SHIELD),
            new HeroInfo("Winter Soldier", GameInfo.Set.Ca, HeroTeam.Unaffiliated),
            
            new HeroInfo("Captain America, Secret Avenger", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Cloak & Dagger", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Daredevil (Iron Fist)", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Falcon", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Goliath", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Hercules", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Hulkling", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Luke Cage", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Patriot", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Peter Parker", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Speedball", GameInfo.Set.Cw, HeroTeam.NewWarriors),
            new HeroInfo("Stature", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Storm & Black Panther", GameInfo.Set.Cw, HeroTeam.XMen),
            new HeroInfo("Tigra", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Vision", GameInfo.Set.Cw, HeroTeam.Avengers),
            new HeroInfo("Wiccan", GameInfo.Set.Cw, HeroTeam.Avengers),
            
            new HeroInfo("Howard the Duck", GameInfo.Set.ThreeD, HeroTeam.Unaffiliated),
            new HeroInfo("Man-Thing", GameInfo.Set.ThreeD, HeroTeam.Unaffiliated),
            
            new HeroInfo("Bob, Agent of HYDRA", GameInfo.Set.Deadpool, HeroTeam.HYDRA),
            new HeroInfo("Deadpool (Mercs for Money)", GameInfo.Set.Deadpool, HeroTeam.MercsForMoney),
            new HeroInfo("Slapstick", GameInfo.Set.Deadpool, HeroTeam.MercsForMoney),
            new HeroInfo("Solo", GameInfo.Set.Deadpool, HeroTeam.MercsForMoney),
            new HeroInfo("Stingray", GameInfo.Set.Deadpool, HeroTeam.MercsForMoney),
            
            new HeroInfo("Angel Noir", GameInfo.Set.Noir, HeroTeam.XMen),
            new HeroInfo("Daredevil Noir", GameInfo.Set.Noir, HeroTeam.MarvelKnights),
            new HeroInfo("Iron Man Noir", GameInfo.Set.Noir, HeroTeam.Avengers),
            new HeroInfo("Luke Cage Noir", GameInfo.Set.Noir, HeroTeam.MarvelKnights),
            new HeroInfo("Spider-Man Noir", GameInfo.Set.Noir, HeroTeam.SpiderFriends),
            
            new HeroInfo("Aurora & Northstar", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Banshee", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Beast", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Cannonball", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Colossus & Wolverine", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Dazzler", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Havok", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Jubilee", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Kitty Pryde", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Legion", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Longshot", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Phoenix", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Polaris", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("Psylocke", GameInfo.Set.XMen, HeroTeam.XMen),
            new HeroInfo("X-23", GameInfo.Set.XMen, HeroTeam.XMen),
            
            new HeroInfo("Happy Hogan", GameInfo.Set.Sm, HeroTeam.Unaffiliated),
            new HeroInfo("High-Tech Spider-Man", GameInfo.Set.Sm, HeroTeam.SpiderFriends),
            new HeroInfo("Peter's Allies", GameInfo.Set.Sm, HeroTeam.SpiderFriends),
            new HeroInfo("Peter Parker, Homecoming", GameInfo.Set.Sm, HeroTeam.SpiderFriends),
            new HeroInfo("Tony Stark", GameInfo.Set.Sm, HeroTeam.Avengers),
            
            new HeroInfo("Gwenpool", GameInfo.Set.Champions, HeroTeam.Champions),
            new HeroInfo("Ms. Marvel", GameInfo.Set.Champions, HeroTeam.Champions),
            new HeroInfo("Nova", GameInfo.Set.Champions, HeroTeam.Champions),
            new HeroInfo("Totally Awesome Hulk", GameInfo.Set.Champions, HeroTeam.Champions),
            new HeroInfo("Viv Vision", GameInfo.Set.Champions, HeroTeam.Champions),
            
            new HeroInfo("Amadeus Cho", GameInfo.Set.Wwh, HeroTeam.Champions),
            new HeroInfo("Bruce Banner", GameInfo.Set.Wwh, HeroTeam.Avengers),
            new HeroInfo("Caiera", GameInfo.Set.Wwh, HeroTeam.Warbound),
            new HeroInfo("Gladiator Hulk", GameInfo.Set.Wwh, HeroTeam.Warbound),
            new HeroInfo("Hiroim", GameInfo.Set.Wwh, HeroTeam.Warbound),
            new HeroInfo("Hulkbuster Iron Man", GameInfo.Set.Wwh, HeroTeam.Avengers),
            new HeroInfo("Joe Fixit, Grey Hulk", GameInfo.Set.Wwh, HeroTeam.CrimeSyndicate),
            new HeroInfo("Korg", GameInfo.Set.Wwh, HeroTeam.Warbound),
            new HeroInfo("Miek, The Unhived", GameInfo.Set.Wwh, HeroTeam.Warbound),
            new HeroInfo("Namora", GameInfo.Set.Wwh, HeroTeam.Champions),
            new HeroInfo("No-Name, Brood Queen", GameInfo.Set.Wwh, HeroTeam.Warbound),
            new HeroInfo("Rick Jones", GameInfo.Set.Wwh, HeroTeam.SHIELD),
            new HeroInfo("Sentry", GameInfo.Set.Wwh, HeroTeam.Avengers),
            new HeroInfo("She-Hulk", GameInfo.Set.Wwh, HeroTeam.Avengers),
            new HeroInfo("Skaar, Son Of Hulk", GameInfo.Set.Wwh, HeroTeam.Avengers),
            
            new HeroInfo("Ant-Man", GameInfo.Set.Antman, HeroTeam.Avengers),
            new HeroInfo("Black Knight", GameInfo.Set.Antman, HeroTeam.Avengers),
            new HeroInfo("Jocasta", GameInfo.Set.Antman, HeroTeam.Avengers),
            new HeroInfo("Wasp", GameInfo.Set.Antman, HeroTeam.Avengers),
            new HeroInfo("Wonder Man", GameInfo.Set.Antman, HeroTeam.Avengers),
            
            new HeroInfo("Carnage", GameInfo.Set.Venom, HeroTeam.Venomverse),
            new HeroInfo("Venom (Venomverse)", GameInfo.Set.Venom, HeroTeam.Venomverse),
            new HeroInfo("Venom Rocket", GameInfo.Set.Venom, HeroTeam.Venomverse),
            new HeroInfo("Venomized Dr. Strange", GameInfo.Set.Venom, HeroTeam.Venomverse),
            new HeroInfo("Venompool", GameInfo.Set.Venom, HeroTeam.Venomverse),
            
            new HeroInfo("Jessica Jones", GameInfo.Set.Dimensions, HeroTeam.MarvelKnights),
            new HeroInfo("Ms. America", GameInfo.Set.Dimensions, HeroTeam.Avengers),
            new HeroInfo("Squirrel Girl", GameInfo.Set.Dimensions, HeroTeam.Avengers),
            
            new HeroInfo("Captain Marvel, Agent of S.H.I.E.L.D.", GameInfo.Set.Revelations, HeroTeam.SHIELD),
            new HeroInfo("Darkhawk", GameInfo.Set.Revelations, HeroTeam.Avengers),
            new HeroInfo("Hellcat", GameInfo.Set.Revelations, HeroTeam.Avengers),
            new HeroInfo("Photon", GameInfo.Set.Revelations, HeroTeam.Avengers),
            new HeroInfo("Quicksilver", GameInfo.Set.Revelations, HeroTeam.Avengers),
            new HeroInfo("Ronin", GameInfo.Set.Revelations, HeroTeam.Avengers),
            new HeroInfo("Scarlet Witch", GameInfo.Set.Revelations, HeroTeam.Avengers),
            new HeroInfo("Speed", GameInfo.Set.Revelations, HeroTeam.SHIELD),
            new HeroInfo("War Machine", GameInfo.Set.Revelations, HeroTeam.Avengers),
            
            new HeroInfo("Agent Phil Coulson", GameInfo.Set.Shield, HeroTeam.SHIELD),
            new HeroInfo("Deathlok", GameInfo.Set.Shield, HeroTeam.SHIELD),
            new HeroInfo("Mockingbird", GameInfo.Set.Shield, HeroTeam.SHIELD),
            new HeroInfo("Quake", GameInfo.Set.Shield, HeroTeam.SHIELD),

            new HeroInfo("Beta Ray Bill", GameInfo.Set.Asgard, HeroTeam.HeroesOfAsgard),
            new HeroInfo("Lady Sif", GameInfo.Set.Asgard, HeroTeam.HeroesOfAsgard),
            new HeroInfo("Thor (Asgard)", GameInfo.Set.Asgard, HeroTeam.HeroesOfAsgard),
            new HeroInfo("Valkyrie", GameInfo.Set.Asgard, HeroTeam.HeroesOfAsgard),
            new HeroInfo("The Warriors Three", GameInfo.Set.Asgard, HeroTeam.HeroesOfAsgard),

            new HeroInfo("Karma", GameInfo.Set.NewMutants, HeroTeam.XMen),
            new HeroInfo("Mirage", GameInfo.Set.NewMutants, HeroTeam.XMen),
            new HeroInfo("Sunspot", GameInfo.Set.NewMutants, HeroTeam.XMen),
            new HeroInfo("Warlock", GameInfo.Set.NewMutants, HeroTeam.XMen),
            new HeroInfo("Wolfsbane", GameInfo.Set.NewMutants, HeroTeam.XMen),

            new HeroInfo("Adam Warlock", GameInfo.Set.Cosmos, HeroTeam.Avengers),
            new HeroInfo("Captain Mar-Vell", GameInfo.Set.Cosmos, HeroTeam.Avengers),
            new HeroInfo("Moondragon", GameInfo.Set.Cosmos, HeroTeam.Avengers),
            new HeroInfo("Nebula", GameInfo.Set.Cosmos, HeroTeam.GuardiansOfTheGalaxy),
            new HeroInfo("Nova", GameInfo.Set.Cosmos, HeroTeam.Avengers),
            new HeroInfo("Quasar", GameInfo.Set.Cosmos, HeroTeam.Avengers),
            new HeroInfo("Ronan the Accuser", GameInfo.Set.Cosmos, HeroTeam.Unaffiliated),
            new HeroInfo("Phyla-Vell", GameInfo.Set.Cosmos, HeroTeam.GuardiansOfTheGalaxy),
            new HeroInfo("Yondu", GameInfo.Set.Cosmos, HeroTeam.GuardiansOfTheGalaxy)
        };

        private readonly List<HeroConnection> _heroConnections = new List<HeroConnection>()
        {
            new HeroConnection("a", "Black Widow"),
            new HeroConnection("b", "Captain America"),
            new HeroConnection("c", "Cyclops"),
            new HeroConnection("d", "Deadpool"),
            new HeroConnection("e", "Emma Frost"),
            new HeroConnection("f", "Gambit"),
            new HeroConnection("g", "Hawkeye"),
            new HeroConnection("h", "Hulk"),
            new HeroConnection("i", "Iron Man"),
            new HeroConnection("j", "Nick Fury"),
            new HeroConnection("k", "Rogue"),
            new HeroConnection("l", "Spider-Man"),
            new HeroConnection("m", "Storm"),
            new HeroConnection("n", "Thor"),
            new HeroConnection("o", "Wolverine"),
            new HeroConnection("p", "Angel"),
            new HeroConnection("q", "Bishop"),
            new HeroConnection("r", "Blade"),
            new HeroConnection("s", "Cable"),
            new HeroConnection("t", "Colossus"),
            new HeroConnection("u", "Daredevil"),
            new HeroConnection("v", "Domino"),
            new HeroConnection("w", "Elektra"),
            new HeroConnection("x", "Forge"),
            new HeroConnection("y", "Ghost Rider"),
            new HeroConnection("z", "Ice Man"),
            new HeroConnection("aa", "Iron Fist"),
            new HeroConnection("ab", "Jean Grey"),
            new HeroConnection("ac", "Nightcrawler"),
            new HeroConnection("ad", "Professor X"),
            new HeroConnection("ae", "Punisher"),
            new HeroConnection("af", "Wolverine (X-Force)"),
            new HeroConnection("ag", "Human Torch"),
            new HeroConnection("ah", "Invisible Woman"),
            new HeroConnection("ai", "Mr. Fantastic"),
            new HeroConnection("aj", "Silver Surfer"),
            new HeroConnection("ak", "Thing"),
            new HeroConnection("al", "Black Cat"),
            new HeroConnection("am", "Moon Knight"),
            new HeroConnection("an", "Scarlet Spider"),
            new HeroConnection("ao", "Spider-Woman"),
            new HeroConnection("ap", "Symbiote Spider-Man"),
            new HeroConnection("aq", "Bullseye"),
            new HeroConnection("ar", "Dr. Octopus"),
            new HeroConnection("as", "Electro"),
            new HeroConnection("at", "Enchantress")
        };

        public List<string> GetHeroNameList(List<string> letters)
        {
            var returnList = new List<string>();
            
            foreach (var letter in letters)
            {
                returnList.Add(_heroConnections.First(x => x.Letter == letter).HeroName);
            }

            return returnList;
        }

        public bool IsEnoughHeroes(List<HeroTeam> heroTeams, int heroesPerTeam, List<string> exclusionHeroes)
        {
            foreach (var heroTeam in heroTeams)
            {
                var heroesForTeams = _heroes.Where(x => x.HeroTeam == heroTeam).ToList();
                foreach (var exclusionHero in exclusionHeroes)
                {
                    var itemToRemove = heroesForTeams.SingleOrDefault(x => x.HeroName == exclusionHero);
                    if(itemToRemove != null)
                        heroesForTeams.Remove(itemToRemove);
                }
                if (heroesForTeams.Count < heroesPerTeam)
                    return false;
            }

            return true;
        }

        public bool IsEnoughHeroesWithout(HeroTeam heroTeam, int heroesPerTeam, List<string> exclusionHeroes)
        {
            var heroesForTeams = _heroes.Where(x => x.HeroTeam != heroTeam).ToList();
            foreach (var exclusionHero in exclusionHeroes)
            {
                var itemToRemove = heroesForTeams.SingleOrDefault(x => x.HeroName == exclusionHero);
                if (itemToRemove != null)
                    heroesForTeams.Remove(itemToRemove);
            }
            return heroesForTeams.Count >= heroesPerTeam;
        }

        public Hero()
        {
            var heroInfo = _heroes[new Random().Next(_heroes.Count)];

            HeroName = heroInfo.HeroName;
            SetName = heroInfo.SetName;
            HeroTeam = heroInfo.HeroTeam;
            HeroInfo = heroInfo;
        }

        public Hero(string heroName)
        {
            var heroInfo = _heroes.First(x => x.HeroName == heroName);

            HeroName = heroName;
            SetName = heroInfo.SetName;
            HeroTeam = heroInfo.HeroTeam;
            HeroInfo = heroInfo;
        }

        public Hero(List<string> exclusionHeroes)
        {
            var heroList = _heroes.Select(x => x.HeroName).ToList();
            heroList.Except(exclusionHeroes);
            var heroName = heroList[new Random().Next(heroList.Count)];
            var hero = _heroes.First(x => x.HeroName == heroName);

            HeroName = hero.HeroName;
            SetName = hero.SetName;
            HeroTeam = hero.HeroTeam;
            HeroInfo = hero;
        }

        public Hero(bool contains, string heroNamePart)
        {
            var heroList = new List<HeroInfo>();
            heroList = _heroes.Where(x => x.HeroName.Contains(heroNamePart)).ToList();

            if (heroNamePart == "Hulk")
            {
                heroList.Add(_heroes.First(x => x.HeroName == "Nul, Breaker of Worlds"));
            }

            var heroInfo = heroList[new Random().Next(heroList.Count)];

            HeroName = heroInfo.HeroName;
            SetName = heroInfo.SetName;
            HeroTeam = heroInfo.HeroTeam;
            HeroInfo = heroInfo;
        }

        public Hero(HeroTeam heroTeam, bool inTeam = true)
        {
            var heroInfoList = inTeam ? _heroes.Where(x => x.HeroTeam == heroTeam).ToList() : _heroes.Where(x => x.HeroTeam != heroTeam).ToList();
            var heroInfo = heroInfoList[new Random().Next(heroInfoList.Count)];

            HeroName = heroInfo.HeroName;
            SetName = heroInfo.SetName;
            HeroTeam = heroInfo.HeroTeam;
            HeroInfo = heroInfo;
        }

        public Hero(List<HeroInfo> newHeroList)
        {
            var heroInfo = newHeroList[new Random().Next(newHeroList.Count)];

            HeroName = heroInfo.HeroName;
            SetName = heroInfo.SetName;
            HeroTeam = heroInfo.HeroTeam;
            HeroInfo = heroInfo;
        }

        public List<HeroTeam> GetHeroTeams(int numberOfHeroTeams, bool is3v3 = false)
        {
            var returnList = new List<HeroTeam>();

            var heroTeams = Enum.GetValues(typeof(HeroTeam));

            while (returnList.Count < numberOfHeroTeams)
            {
                var heroTeam = (HeroTeam)heroTeams.GetValue(new Random().Next(heroTeams.Length));
                while(returnList.Any(x=>x.Equals(heroTeam)))
                {
                    heroTeam = (HeroTeam)heroTeams.GetValue(new Random().Next(heroTeams.Length));
                }

                if (!is3v3)
                {
                    returnList.Add(heroTeam);
                }
                else
                {
                    if (_heroes.Count(x => x.HeroTeam == heroTeam) >= 3 && heroTeam != HeroTeam.Unaffiliated)
                    {
                        returnList.Add(heroTeam);
                    }
                }
            }

            return returnList;
        }

        public int GetNumberOfHeroes()
        {
            return _heroes.Count;
        }

        public List<HeroInfo> ModifyHeroList(IEnumerable<string> heroExclusions)
        {
            var returnList = new List<HeroInfo>(_heroes);

            foreach (var heroExclusion in heroExclusions)
            {
                while (returnList.Any(x => x.HeroName.Split('_')[0] == heroExclusion))
                {
                    var itemToRemove = returnList.First(x => x.HeroName.Split('_')[0] == heroExclusion);
                    returnList.Remove(itemToRemove);
                }
            }

            return returnList;
        }

        public List<HeroInfo> ModifyHeroListOnlyHeroTeam(List<HeroInfo> heroExclusions, HeroTeam heroTeam)
        {
            var returnList = new List<HeroInfo>(heroExclusions);

            while (returnList.Any(x => x.HeroTeam != heroTeam))
            {
                var itemToRemove = returnList.First(x => x.HeroTeam != heroTeam);
                returnList.Remove(itemToRemove);
            }

            return returnList;
        }

        public List<HeroInfo> ModifyHeroListWithoutHeroTeam(List<HeroInfo> heroExclusions, HeroTeam heroTeam)
        {
            var returnList = new List<HeroInfo>(heroExclusions);

            while (returnList.Any(x => x.HeroTeam == heroTeam))
            {
                var itemToRemove = returnList.First(x => x.HeroTeam == heroTeam);
                returnList.Remove(itemToRemove);
            }

            return returnList;
        }

        public int GetHeroTeamMemberCount(HeroTeam heroTeam)
        {
            var returnList = new List<HeroInfo>(_heroes).Where(x=>x.HeroTeam==heroTeam);

            return returnList.Count();
        }

        public string ToString(List<Hero> heroList)
        {
            var returnString = "\r\n";
            var counter = 1;

            for (int i = 0; i < heroList.Count; i++)
            {
                heroList[i].Order = i+1;                
            }

            var orderedHeroList = heroList.OrderBy(x => (int) x.SetName).ToList();

            if (orderedHeroList.Count == 0) return returnString;
            foreach (var hero in orderedHeroList)
            {
                returnString = $"{returnString}{counter}) ({hero.HeroTeam.GetDescription()}) {hero.HeroName.Split('_').First()}, {hero.SetName.GetDescription()} ({hero.Order})\r\n";
                counter++;
            }
            returnString = $"{returnString.Remove(returnString.Length - 2)}\r\n";

            return returnString;
        }

        public List<string> GetListOfHeroes()
        {
            return _heroes.ToList().Select(x => x.HeroName).ToList();
        }

        public List<HeroInfo> SetHeroList(List<string> heroNames)
        {
            var returnList = _heroes.Where(x => heroNames.Contains(x.HeroName)).ToList();

            return returnList;
        }
    }
}
