using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MarvelLegendary.Enums;

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

    public class Hero
    {
        public string HeroName { get; set; }
        public Set SetName { get; set; }
        public HeroTeam HeroTeam { get; set; }
        public HeroInfo HeroInfo { get; set; }
        public int Order { get; set; }

        private readonly List<HeroInfo> _heroes = new List<HeroInfo>()
        {
            new HeroInfoBuilder().SetHeroName("Black Widow").Build(),
            new HeroInfoBuilder().SetHeroName("Captain America").Build(),
            new HeroInfoBuilder().SetHeroName("Cyclops").SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Deadpool").SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Emma Frost").SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Gambit").SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Hawkeye").Build(),
            new HeroInfoBuilder().SetHeroName("Hulk").Build(),
            new HeroInfoBuilder().SetHeroName("Iron Man").Build(),
            new HeroInfoBuilder().SetHeroName("Nick Fury").SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Rogue").SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Spider-Man").SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Storm").SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Thor").Build(),
            new HeroInfoBuilder().SetHeroName("Wolverine").SetHeroTeam(HeroTeam.XMen).Build(),

            new HeroInfoBuilder().SetHeroName("Angel").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Bishop").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Blade").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Cable").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XForce).Build(),
            new HeroInfoBuilder().SetHeroName("Colossus").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XForce).Build(),
            new HeroInfoBuilder().SetHeroName("Daredevil").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Domino").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XForce).Build(),
            new HeroInfoBuilder().SetHeroName("Elektra").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Forge").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XForce).Build(),
            new HeroInfoBuilder().SetHeroName("Ghost Rider").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Ice Man").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Iron Fist").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Jean Grey").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Nightcrawler").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Professor X").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Punisher").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Wolverine (X-Force)").SetGameSet(Set.Dc).SetHeroTeam(HeroTeam.XForce).Build(),

            new HeroInfoBuilder().SetHeroName("Human Torch").SetGameSet(Set.Ff).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            new HeroInfoBuilder().SetHeroName("Invisible Woman").SetGameSet(Set.Ff).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            new HeroInfoBuilder().SetHeroName("Mr. Fantastic").SetGameSet(Set.Ff).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            new HeroInfoBuilder().SetHeroName("Silver Surfer").SetGameSet(Set.Ff).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Thing").SetGameSet(Set.Ff).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            
            new HeroInfoBuilder().SetHeroName("Black Cat").SetGameSet(Set.PttR).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Moon Knight").SetGameSet(Set.PttR).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Scarlet Spider").SetGameSet(Set.PttR).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Spider-Woman").SetGameSet(Set.PttR).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Symbiote Spider-Man").SetGameSet(Set.PttR).SetHeroTeam(HeroTeam.SpiderFriends).Build(),

            new HeroInfoBuilder().SetHeroName("Bullseye").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.CrimeSyndicate).Build(),
            new HeroInfoBuilder().SetHeroName("Dr. Octopus").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.SinisterSix).Build(),
            new HeroInfoBuilder().SetHeroName("Electro").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.SinisterSix).Build(),
            new HeroInfoBuilder().SetHeroName("Enchantress").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.FoesOfAsgard).IncludeNewRecruits().Build(),
            new HeroInfoBuilder().SetHeroName("Green Goblin").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.SinisterSix).Build(),
            new HeroInfoBuilder().SetHeroName("Juggernaut").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.Brotherhood).Build(),
            new HeroInfoBuilder().SetHeroName("Kingpin").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.CrimeSyndicate).IncludeNewRecruits().Build(),
            new HeroInfoBuilder().SetHeroName("Kraven").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.SinisterSix).Build(),
            new HeroInfoBuilder().SetHeroName("Loki").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.FoesOfAsgard).IncludeNewRecruits().IncludeBindings().Build(),
            new HeroInfoBuilder().SetHeroName("Magneto").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.Brotherhood).IncludeBindings().Build(),
            new HeroInfoBuilder().SetHeroName("Mysterio").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.SinisterSix).Build(),
            new HeroInfoBuilder().SetHeroName("Mystique").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.Brotherhood).Build(),
            new HeroInfoBuilder().SetHeroName("Sabretooth").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.Brotherhood).Build(),
            new HeroInfoBuilder().SetHeroName("Ultron").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Venom").SetGameSet(Set.Villains).SetHeroTeam(HeroTeam.SinisterSix).Build(),
            
            new HeroInfoBuilder().SetHeroName("Drax the Destroyer").SetGameSet(Set.GotG).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Gamora").SetGameSet(Set.GotG).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Groot").SetGameSet(Set.GotG).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Rocket Raccoon").SetGameSet(Set.GotG).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Star-Lord").SetGameSet(Set.GotG).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            
            new HeroInfoBuilder().SetHeroName("Greithoth, Breaker of Wills").SetGameSet(Set.Fi).SetHeroTeam(HeroTeam.FoesOfAsgard).Build(),
            new HeroInfoBuilder().SetHeroName("Kuurth, Breaker of Stone").SetGameSet(Set.Fi).SetHeroTeam(HeroTeam.FoesOfAsgard).Build(),
            new HeroInfoBuilder().SetHeroName("Nerkkod, Breaker of Oceans").SetGameSet(Set.Fi).SetHeroTeam(HeroTeam.FoesOfAsgard).IncludeNewRecruits().Build(),
            new HeroInfoBuilder().SetHeroName("Nul, Breaker of Worlds").SetGameSet(Set.Fi).SetHeroTeam(HeroTeam.FoesOfAsgard).IncludeBindings().Build(),
            new HeroInfoBuilder().SetHeroName("Skadi").SetGameSet(Set.Fi).SetHeroTeam(HeroTeam.HYDRA).IncludeMadameHydra().Build(),
            new HeroInfoBuilder().SetHeroName("Skirn, Breaker of Men").SetGameSet(Set.Fi).SetHeroTeam(HeroTeam.FoesOfAsgard).IncludeNewRecruits().Build(),
            
            new HeroInfoBuilder().SetHeroName("Apocalyptic Kitty Pryde").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Black Bolt").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Illuminati).Build(),
            new HeroInfoBuilder().SetHeroName("Black Panther").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Illuminati).Build(),
            new HeroInfoBuilder().SetHeroName("Captain Marvel").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Dr. Strange").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Illuminati).Build(),
            new HeroInfoBuilder().SetHeroName("Lady Thor").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Magik").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Maximus").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Cabal).Build(),
            new HeroInfoBuilder().SetHeroName("Namor").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Cabal).Build(),
            new HeroInfoBuilder().SetHeroName("Old Man Logan").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Proxima Midnight").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Cabal).Build(),
            new HeroInfoBuilder().SetHeroName("Superior Iron Man").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Illuminati).Build(),
            new HeroInfoBuilder().SetHeroName("Thanos").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.Cabal).Build(),
            new HeroInfoBuilder().SetHeroName("Ultimate Spider-Man").SetGameSet(Set.Sw1).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            
            new HeroInfoBuilder().SetHeroName("Agent Venom").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Arkon the Magnificent").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Beast").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.Illuminati).Build(),
            new HeroInfoBuilder().SetHeroName("Black Swan").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.Cabal).Build(),
            new HeroInfoBuilder().SetHeroName("The Captain and the Devil").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Captain Britain").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.Illuminati).Build(),
            new HeroInfoBuilder().SetHeroName("Corvus Glaive").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.Cabal).Build(),
            new HeroInfoBuilder().SetHeroName("Dr. Punisher, Soldier Supreme").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Elsa Bloodstone").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Phoenix Force Cyclops").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Ruby Summers").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Shang-Chi").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Silk").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Soulsword Colossus").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Spider-Gwen").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Time-Traveling Jean Grey").SetGameSet(Set.Sw2).SetHeroTeam(HeroTeam.XMen).Build(),
            
            new HeroInfoBuilder().SetHeroName("Agent X-13").SetGameSet(Set.Ca).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Captain America 1941").SetGameSet(Set.Ca).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Captain America (Falcon)").SetGameSet(Set.Ca).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Steve Rogers, Director of S.H.I.E.L.D.").SetGameSet(Set.Ca).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Winter Soldier").SetGameSet(Set.Ca).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            
            new HeroInfoBuilder().SetHeroName("Captain America, Secret Avenger").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Cloak & Dagger").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Daredevil (Iron Fist)").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Falcon").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Goliath").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Divided}).Build(),
            new HeroInfoBuilder().SetHeroName("Hercules").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Hulkling").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Divided}).Build(),
            new HeroInfoBuilder().SetHeroName("Luke Cage").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Patriot").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Peter Parker").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Speedball").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.NewWarriors).Build(),
            new HeroInfoBuilder().SetHeroName("Stature").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Divided}).Build(),
            new HeroInfoBuilder().SetHeroName("Storm & Black Panther").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Tigra").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Vision").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{Keywords.Phasing, Keywords.Size, Keywords.Divided}).Build(),
            new HeroInfoBuilder().SetHeroName("Wiccan").SetGameSet(Set.Cw).SetHeroTeam(HeroTeam.Avengers).Build(),
            
            new HeroInfoBuilder().SetHeroName("Bob, Agent of HYDRA").SetGameSet(Set.Deadpool).SetHeroTeam(HeroTeam.HYDRA).Build(),
            new HeroInfoBuilder().SetHeroName("Deadpool (Mercs for Money)").SetGameSet(Set.Deadpool).SetHeroTeam(HeroTeam.MercsForMoney).Build(),
            new HeroInfoBuilder().SetHeroName("Slapstick").SetGameSet(Set.Deadpool).SetHeroTeam(HeroTeam.MercsForMoney).Build(),
            new HeroInfoBuilder().SetHeroName("Solo").SetGameSet(Set.Deadpool).SetHeroTeam(HeroTeam.MercsForMoney).Build(),
            new HeroInfoBuilder().SetHeroName("Stingray").SetGameSet(Set.Deadpool).SetHeroTeam(HeroTeam.MercsForMoney).Build(),
            
            new HeroInfoBuilder().SetHeroName("Angel Noir").SetGameSet(Set.Noir).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Daredevil Noir").SetGameSet(Set.Noir).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Iron Man Noir").SetGameSet(Set.Noir).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Luke Cage Noir").SetGameSet(Set.Noir).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Spider-Man Noir").SetGameSet(Set.Noir).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            
            new HeroInfoBuilder().SetHeroName("Aurora & Northstar").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Banshee").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Beast").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Cannonball").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Colossus & Wolverine").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Dazzler").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Havok").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Jubilee").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Kitty Pryde").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Legion").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Longshot").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Phoenix").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Polaris").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Psylocke").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("X-23").SetGameSet(Set.XMen).SetHeroTeam(HeroTeam.XMen).Build(),
            
            new HeroInfoBuilder().SetHeroName("Happy Hogan").SetGameSet(Set.Sm).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("High-Tech Spider-Man").SetGameSet(Set.Sm).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Peter's Allies").SetGameSet(Set.Sm).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Peter Parker, Homecoming").SetGameSet(Set.Sm).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
            new HeroInfoBuilder().SetHeroName("Tony Stark").SetGameSet(Set.Sm).SetHeroTeam(HeroTeam.Avengers).Build(),
            
            new HeroInfoBuilder().SetHeroName("Gwenpool").SetGameSet(Set.Champions).SetHeroTeam(HeroTeam.Champions).SetKeywords(new List<Keywords>{ Keywords.Versatile, Keywords.Size, Keywords.Cheering, Keywords.Demolish}).Build(),
            new HeroInfoBuilder().SetHeroName("Ms. Marvel").SetGameSet(Set.Champions).SetHeroTeam(HeroTeam.Champions).SetKeywords(new List<Keywords>{ Keywords.Versatile, Keywords.Size, Keywords.Cheering}).Build(),
            new HeroInfoBuilder().SetHeroName("Nova").SetGameSet(Set.Champions).SetHeroTeam(HeroTeam.Champions).SetKeywords(new List<Keywords>{ Keywords.Versatile, Keywords.Size, Keywords.Cheering}).Build(),
            new HeroInfoBuilder().SetHeroName("Totally Awesome Hulk").SetGameSet(Set.Champions).SetHeroTeam(HeroTeam.Champions).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Cheering}).Build(),
            new HeroInfoBuilder().SetHeroName("Viv Vision").SetGameSet(Set.Champions).SetHeroTeam(HeroTeam.Champions).SetKeywords(new List<Keywords>{ Keywords.Versatile, Keywords.Size, Keywords.Cheering}).Build(),

            new HeroInfoBuilder().SetHeroName("Amadeus Cho").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Champions).Build(),
            new HeroInfoBuilder().SetHeroName("Bruce Banner").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Caiera").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Warbound).Build(),
            new HeroInfoBuilder().SetHeroName("Gladiator Hulk").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Warbound).Build(),
            new HeroInfoBuilder().SetHeroName("Hiroim").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Warbound).Build(),
            new HeroInfoBuilder().SetHeroName("Hulkbuster Iron Man").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Joe Fixit, Grey Hulk").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.CrimeSyndicate).Build(),
            new HeroInfoBuilder().SetHeroName("Korg").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Warbound).Build(),
            new HeroInfoBuilder().SetHeroName("Miek, The Unhived").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Warbound).Build(),
            new HeroInfoBuilder().SetHeroName("Namora").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Champions).Build(),
            new HeroInfoBuilder().SetHeroName("No-Name, Brood Queen").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Warbound).Build(),
            new HeroInfoBuilder().SetHeroName("Rick Jones").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Sentry").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("She-Hulk").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Skaar, Son Of Hulk").SetGameSet(Set.Wwh).SetHeroTeam(HeroTeam.Avengers).Build(),
            
            new HeroInfoBuilder().SetHeroName("Ant-Man").SetGameSet(Set.Antman).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size }).Build(),
            new HeroInfoBuilder().SetHeroName("Black Knight").SetGameSet(Set.Antman).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Jocasta").SetGameSet(Set.Antman).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Empowered }).Build(),
            new HeroInfoBuilder().SetHeroName("Wasp").SetGameSet(Set.Antman).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size }).Build(),
            new HeroInfoBuilder().SetHeroName("Wonder Man").SetGameSet(Set.Antman).SetHeroTeam(HeroTeam.Avengers).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Empowered }).Build(),

            new HeroInfoBuilder().SetHeroName("Carnage").SetGameSet(Set.Venom).SetHeroTeam(HeroTeam.Venomverse).Build(),
            new HeroInfoBuilder().SetHeroName("Venom (Venomverse)").SetGameSet(Set.Venom).SetHeroTeam(HeroTeam.Venomverse).Build(),
            new HeroInfoBuilder().SetHeroName("Venom Rocket").SetGameSet(Set.Venom).SetHeroTeam(HeroTeam.Venomverse).Build(),
            new HeroInfoBuilder().SetHeroName("Venomized Dr. Strange").SetGameSet(Set.Venom).SetHeroTeam(HeroTeam.Venomverse).Build(),
            new HeroInfoBuilder().SetHeroName("Venompool").SetGameSet(Set.Venom).SetHeroTeam(HeroTeam.Venomverse).Build(),

            new HeroInfoBuilder().SetHeroName("Howard the Duck").SetGameSet(Set.Dimensions).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Jessica Jones").SetGameSet(Set.Dimensions).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Man-Thing").SetGameSet(Set.Dimensions).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Ms. America").SetGameSet(Set.Dimensions).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Squirrel Girl").SetGameSet(Set.Dimensions).SetHeroTeam(HeroTeam.Avengers).Build(),
            
            new HeroInfoBuilder().SetHeroName("Captain Marvel, Agent of S.H.I.E.L.D.").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Darkhawk").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Hellcat").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Photon").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Quicksilver").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Ronin").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Scarlet Witch").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Speed").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("War Machine").SetGameSet(Set.Revelations).SetHeroTeam(HeroTeam.Avengers).Build(),
            
            new HeroInfoBuilder().SetHeroName("Agent Phil Coulson").SetGameSet(Set.Shield).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Deathlok").SetGameSet(Set.Shield).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Mockingbird").SetGameSet(Set.Shield).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Quake").SetGameSet(Set.Shield).SetHeroTeam(HeroTeam.SHIELD).Build(),

            new HeroInfoBuilder().SetHeroName("Beta Ray Bill").SetGameSet(Set.Asgard).SetHeroTeam(HeroTeam.HeroesOfAsgard).Build(),
            new HeroInfoBuilder().SetHeroName("Lady Sif").SetGameSet(Set.Asgard).SetHeroTeam(HeroTeam.HeroesOfAsgard).Build(),
            new HeroInfoBuilder().SetHeroName("Thor (Asgard)").SetGameSet(Set.Asgard).SetHeroTeam(HeroTeam.HeroesOfAsgard).Build(),
            new HeroInfoBuilder().SetHeroName("Valkyrie").SetGameSet(Set.Asgard).SetHeroTeam(HeroTeam.HeroesOfAsgard).Build(),
            new HeroInfoBuilder().SetHeroName("The Warriors Three").SetGameSet(Set.Asgard).SetHeroTeam(HeroTeam.HeroesOfAsgard).Build(),

            new HeroInfoBuilder().SetHeroName("Karma").SetGameSet(Set.NewMutants).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Mirage").SetGameSet(Set.NewMutants).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Sunspot").SetGameSet(Set.NewMutants).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Warlock").SetGameSet(Set.NewMutants).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Wolfsbane").SetGameSet(Set.NewMutants).SetHeroTeam(HeroTeam.XMen).Build(),

            new HeroInfoBuilder().SetHeroName("Adam Warlock").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Captain Mar-Vell").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Moondragon").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Nebula").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Nova (Cosmos)").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Quasar").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.Avengers).Build(),
            new HeroInfoBuilder().SetHeroName("Ronan the Accuser").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Phyla-Vell").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Yondu").SetGameSet(Set.Cosmos).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),

            new HeroInfoBuilder().SetHeroName("Black Bolt (Inhumans)").SetGameSet(Set.Inhumans).SetHeroTeam(HeroTeam.Inhumans).Build(),
            new HeroInfoBuilder().SetHeroName("Crystal").SetGameSet(Set.Inhumans).SetHeroTeam(HeroTeam.Inhumans).Build(),
            new HeroInfoBuilder().SetHeroName("Gorgon").SetGameSet(Set.Inhumans).SetHeroTeam(HeroTeam.Inhumans).Build(),
            new HeroInfoBuilder().SetHeroName("Karnak").SetGameSet(Set.Inhumans).SetHeroTeam(HeroTeam.Inhumans).Build(),
            new HeroInfoBuilder().SetHeroName("Medusa").SetGameSet(Set.Inhumans).SetHeroTeam(HeroTeam.Inhumans).Build(),

            new HeroInfoBuilder().SetHeroName("Brainstorm").SetGameSet(Set.Annihilation).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            new HeroInfoBuilder().SetHeroName("Fantastic Four United").SetGameSet(Set.Annihilation).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            new HeroInfoBuilder().SetHeroName("Heralds of Galactus").SetGameSet(Set.Annihilation).SetHeroTeam(HeroTeam.Unaffiliated).Build(),
            new HeroInfoBuilder().SetHeroName("Psi-Lord").SetGameSet(Set.Annihilation).SetHeroTeam(HeroTeam.FantasticFour).Build(),
            new HeroInfoBuilder().SetHeroName("Super-Skrull").SetGameSet(Set.Annihilation).SetHeroTeam(HeroTeam.Unaffiliated).Build(),

            new HeroInfoBuilder().SetHeroName("M").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XFactor).Build(),
            new HeroInfoBuilder().SetHeroName("Multiple Man").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XFactor).Build(),
            new HeroInfoBuilder().SetHeroName("Rictor").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XFactor).Build(),
            new HeroInfoBuilder().SetHeroName("Shatterstar").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XForce).Build(),
            new HeroInfoBuilder().SetHeroName("Siryn").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XFactor).Build(),
            new HeroInfoBuilder().SetHeroName("Stepford Cuckoos").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XMen).Build(),
            new HeroInfoBuilder().SetHeroName("Strong Guy").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XFactor).Build(),
            new HeroInfoBuilder().SetHeroName("Warpath").SetGameSet(Set.Messiah).SetHeroTeam(HeroTeam.XForce).Build(),

            new HeroInfoBuilder().SetHeroName("The Ancient One").SetGameSet(Set.Strange).SetHeroTeam(HeroTeam.None).Build(),
            new HeroInfoBuilder().SetHeroName("Clea").SetGameSet(Set.Strange).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Doctor Strange").SetGameSet(Set.Strange).Build(),
            new HeroInfoBuilder().SetHeroName("Doctor Voodoo").SetGameSet(Set.Strange).Build(),
            new HeroInfoBuilder().SetHeroName("The Vishanti").SetGameSet(Set.Strange).SetHeroTeam(HeroTeam.None).Build(),

            new HeroInfoBuilder().SetHeroName("Drax").SetGameSet(Set.Guardians).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Gamora").SetGameSet(Set.Guardians).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Mantis").SetGameSet(Set.Guardians).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Rocket & Groot").SetGameSet(Set.Guardians).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),
            new HeroInfoBuilder().SetHeroName("Star-Lord").SetGameSet(Set.Guardians).SetHeroTeam(HeroTeam.GuardiansOfTheGalaxy).Build(),

            new HeroInfoBuilder().SetHeroName("General Okoye").SetGameSet(Set.BlackPanther).SetHeroTeam(HeroTeam.Wakanda).Build(),
            new HeroInfoBuilder().SetHeroName("King Black Panther").SetGameSet(Set.BlackPanther).SetHeroTeam(HeroTeam.Wakanda).Build(),
            new HeroInfoBuilder().SetHeroName("Princess Shuri").SetGameSet(Set.BlackPanther).SetHeroTeam(HeroTeam.Wakanda).Build(),
            new HeroInfoBuilder().SetHeroName("Queen Storm of Wakanda").SetGameSet(Set.BlackPanther).SetHeroTeam(HeroTeam.Wakanda).Build(),
            new HeroInfoBuilder().SetHeroName("White Wolf").SetGameSet(Set.BlackPanther).SetHeroTeam(HeroTeam.Wakanda).Build(),

            new HeroInfoBuilder().SetHeroName("Black Widow").SetGameSet(Set.BlackWidow).SetHeroTeam(HeroTeam.SHIELD).Build(),
            new HeroInfoBuilder().SetHeroName("Falcon and the Winter Soldier").SetGameSet(Set.BlackWidow).Build(),
            new HeroInfoBuilder().SetHeroName("Red Guardian").SetGameSet(Set.BlackWidow).SetHeroTeam(HeroTeam.None).Build(),
            new HeroInfoBuilder().SetHeroName("White Tiger").SetGameSet(Set.BlackWidow).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Yelena Belova").SetGameSet(Set.BlackWidow).SetHeroTeam(HeroTeam.SHIELD).Build(),

            new HeroInfoBuilder().SetHeroName("Black Panther").SetGameSet(Set.InfinitySaga).Build(),
            new HeroInfoBuilder().SetHeroName("Bruce Banner").SetGameSet(Set.InfinitySaga).Build(),
            new HeroInfoBuilder().SetHeroName("Captain Marvel").SetGameSet(Set.InfinitySaga).Build(),
            new HeroInfoBuilder().SetHeroName("Doctor Strange").SetGameSet(Set.InfinitySaga).Build(),
            new HeroInfoBuilder().SetHeroName("Wanda & Vision").SetGameSet(Set.InfinitySaga).Build(),

            new HeroInfoBuilder().SetHeroName("Blade, Daywalker").SetGameSet(Set.MidnightSons).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Elsa Bloodstone").SetGameSet(Set.MidnightSons).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Morbius").SetGameSet(Set.MidnightSons).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Werewolf by Night").SetGameSet(Set.MidnightSons).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Wong,Master of the Mystic Arts").SetGameSet(Set.MidnightSons).SetHeroTeam(HeroTeam.MarvelKnights).Build(),

            new HeroInfoBuilder().SetHeroName("Apocalyptic Black Widow").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("Captain Carter (Guardians of the Multiverse)").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("Doctor Strange Supreme").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("Gamora, Destroyer of Thanos").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("Killmonger, Special Ops").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("Party Thor").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("T'Challa Star-Lord").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),
            new HeroInfoBuilder().SetHeroName("The Watcher").SetGameSet(Set.WhatIf).SetHeroTeam(HeroTeam.Multiverse).Build(),

            new HeroInfoBuilder().SetHeroName("Ant Army").SetGameSet(Set.AntmanWasp).SetHeroTeam(HeroTeam.None).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Antics, Keywords.Heist }).Build(),
            new HeroInfoBuilder().SetHeroName("Ant-Man (Ant-Man and the Wasp)").SetGameSet(Set.AntmanWasp).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Antics, Keywords.Heist }).Build(),
            new HeroInfoBuilder().SetHeroName("Cassie Lang").SetGameSet(Set.AntmanWasp).SetKeywords(new List<Keywords>{ Keywords.Size }).Build(),
            new HeroInfoBuilder().SetHeroName("Freedom Fighters").SetGameSet(Set.AntmanWasp).SetHeroTeam(HeroTeam.None).Build(),
            new HeroInfoBuilder().SetHeroName("Janet Van Dyne").SetGameSet(Set.AntmanWasp).SetHeroTeam(HeroTeam.None).SetKeywords(new List<Keywords>{ Keywords.Size, Keywords.Explore }).Build(),
            new HeroInfoBuilder().SetHeroName("Jentorra").SetGameSet(Set.AntmanWasp).SetHeroTeam(HeroTeam.None).Build(),
            new HeroInfoBuilder().SetHeroName("Scott Lang, Cat Burglar").SetGameSet(Set.AntmanWasp).SetHeroTeam(HeroTeam.CrimeSyndicate).Build(),
            new HeroInfoBuilder().SetHeroName("Wasp (Ant-Man and the Wasp)").SetGameSet(Set.AntmanWasp).SetKeywords(new List<Keywords>{ Keywords.Size }).Build(),

            new HeroInfoBuilder().SetHeroName("Doctor Doom 2099").SetGameSet(Set.TwentyNintyNine).SetHeroTeam(HeroTeam.None).Build(),
            new HeroInfoBuilder().SetHeroName("Ghost Rider 2099").SetGameSet(Set.TwentyNintyNine).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Hulk 2099").SetGameSet(Set.TwentyNintyNine).SetHeroTeam(HeroTeam.MarvelKnights).Build(),
            new HeroInfoBuilder().SetHeroName("Ravage 2099").SetGameSet(Set.TwentyNintyNine).SetHeroTeam(HeroTeam.None).Build(),
            new HeroInfoBuilder().SetHeroName("Spider-Man 2099").SetGameSet(Set.TwentyNintyNine).SetHeroTeam(HeroTeam.SpiderFriends).Build(),
        };

        public List<string> GetHeroNameList(List<int> indicies)
        {
            return (from index in indicies select _heroes.ElementAt(index-1).HeroName).ToList();
        }

        public List<Hero> GetAllHeroesByNamePart(string namePart, List<string> availableHeroes)
        {
            var heroes = availableHeroes.Where(x => x.Contains(namePart)).ToList();

            if (namePart == "Hulk" && availableHeroes.Any(x=>x == "Nul, Breaker of Worlds"))
            {
                heroes.Add(availableHeroes.First(x => x == "Nul, Breaker of Worlds"));
            }

            var returnList = (from item in heroes select GetNewHero(item)).ToList();

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

        public Hero() {}

        public Hero GetNewHero(string heroName = "")
        {
            var newHero = new Hero();
            var hero = heroName;
            if (string.IsNullOrEmpty(hero))
            {
                var allHeroes = GetListOfHeroes();
                hero = allHeroes[new Random().Next(allHeroes.Count)];
            }

            var heroInfo = _heroes.FirstOrDefault(h => h.HeroName == hero);

            newHero.HeroName = heroInfo.HeroName;
            newHero.SetName = heroInfo.SetName;
            newHero.HeroTeam = heroInfo.HeroTeam;
            newHero.HeroInfo = heroInfo;

            return newHero;
        }

        public Hero GetNewHero(List<string> excludedHeroes)
        {
            var newHero = new Hero();
            var heroList = GetListOfHeroes();
            heroList.Except(excludedHeroes);
            var heroName = heroList[new Random().Next(heroList.Count)];
            var hero = _heroes.FirstOrDefault(x => x.HeroName == heroName);

            newHero.HeroName = hero.HeroName;
            newHero.SetName = hero.SetName;
            newHero.HeroTeam = hero.HeroTeam;
            newHero.HeroInfo = hero;

            return newHero;
        }

        //This is replacing the DetermineLists functionality
        public Hero GetNewHero(List<Mastermind> allMastermindsInGame, Scheme scheme, List<Villain> villainsInGame, List<Henchmen> henchmenInGame, List<Hero> heroesInGame)
        {
            var newHero = new Hero();
            
            //Get Heroes
            var heroList = GetListOfHeroes();

            //Remove all Heroes currently in the game from the list
            var remainingHeroes = heroList.Except(heroesInGame.Select(hero => hero.HeroName)).ToList();

            //Get Heroes that have played with the Scheme
            var heroesByScheme = GetListOfHeroesByX("Scheme", scheme.SchemeName);

            //Remove all Heroes that have played with the scheme from the list
            remainingHeroes = remainingHeroes.Except(heroesByScheme).ToList();

            //Get Heroes that have played with each of the Masterminds
            foreach (var mastermind in allMastermindsInGame)
            {
                var heroesByMastermind = GetListOfHeroesByX("Mastermind", mastermind.MastermindName);
                //Remove all Heroes that have played with the Mastermind(s)
                remainingHeroes = remainingHeroes.Except(heroesByMastermind).ToList();
            }

            //Get Heroes that have played with each of the Villains
            foreach (var villain in villainsInGame)
            {
                var heroesByVillain = GetListOfHeroesByX("Villain", villain.VillainName);
                //Remove all Heroes that have played with the Villains
                remainingHeroes = remainingHeroes.Except(heroesByVillain).ToList();
            }

            //Get Heroes that have played with each of the Henchmen
            foreach (var henchmen in henchmenInGame)
            {
                var heroesByHenchmen = GetListOfHeroesByX("Henchmen", henchmen.HenchmenName);
                //Remove all Heroes that have played with the Henchmen
                remainingHeroes = remainingHeroes.Except(heroesByHenchmen).ToList();
            }

            //Get Heroes that have played with each of the Heroes
            foreach (var hero in heroesInGame)
            {
                var heroesByHero = GetListOfHeroesByX("Hero", hero.HeroName);
                //Remove all Heroes that have played with the Hero
                remainingHeroes = remainingHeroes.Except(heroesByHero).ToList();
            }

            //Select Hero from remaining list
            var heroName = remainingHeroes[new Random().Next(remainingHeroes.Count)];
            var heroInfo = _heroes.First(h => h.HeroName == heroName);

            //Set HeroName
            newHero.HeroName = heroName;

            //Set SetName
            newHero.SetName = heroInfo.SetName;

            //Set HeroInfo
            newHero.HeroInfo = heroInfo;

            return newHero;
        }

        public Hero GetNewHeroByContainsString(string heroNamePart)
        {
            var newHero = new Hero();
            var availableHeroes = GetListOfHeroes();
            var heroList = availableHeroes.Where(x => x.Contains(heroNamePart)).ToList();

            if (heroNamePart == "Hulk" && availableHeroes.Contains("Nul, Breaker of Worlds"))
            {
                heroList.Add(availableHeroes.First(x => x == "Nul, Breaker of Worlds"));
            }

            var heroInfo = heroList[new Random().Next(heroList.Count)];
            var hero = GetNewHero(heroInfo);

            newHero.HeroName = hero.HeroName;
            newHero.SetName = hero.SetName;
            newHero.HeroTeam = hero.HeroTeam;
            newHero.HeroInfo = hero.HeroInfo;

            return newHero;
        }

        public Hero GetNewHeroByTeam(HeroTeam heroTeam, List<string> availableHeroes, bool inTeam = true)
        {
            var newHero = new Hero();
            var heroes = (from item in availableHeroes select GetNewHero(item)).ToList();
            var heroInfoList = inTeam ? heroes.Where(x => x.HeroTeam == heroTeam).ToList() : heroes.Where(x => x.HeroTeam != heroTeam).ToList();
            var heroInfo = heroInfoList[new Random().Next(heroInfoList.Count)];

            newHero.HeroName = heroInfo.HeroName;
            newHero.SetName = heroInfo.SetName;
            newHero.HeroTeam = heroInfo.HeroTeam;
            newHero.HeroInfo = heroInfo.HeroInfo;

            return newHero;
        }

        public Hero GetNewHeroByTeam(HeroTeam heroTeam, List<string> availableHeroes, List<string> excludedHeroes, bool inTeam = true)
        {
            var newHero = new Hero();
            var heroes = (from item in availableHeroes select GetNewHero(item)).ToList();
            var heroInfoList = inTeam ? heroes.Where(x => x.HeroTeam == heroTeam).ToList() : heroes.Where(x => x.HeroTeam != heroTeam).ToList();
            var heroInfo = heroInfoList[new Random().Next(heroInfoList.Count)];

            newHero.HeroName = heroInfo.HeroName;
            newHero.SetName = heroInfo.SetName;
            newHero.HeroTeam = heroInfo.HeroTeam;
            newHero.HeroInfo = heroInfo.HeroInfo;

            return newHero;
        }

        //This will return a hero team that contains at least the number of heroes
        public HeroTeam GetHeroTeam(int numberOfHeroes, List<HeroTeam> includedTeams = null)
        {
            //This will set heroTeamsList to a list of HeroTeams while removing what is coming in from includedTeams
            //If includedTeams is not passed, it will come in as null, so the ?? is checking to see if it is null
            //If it is null, it will use an empty list in the Except LINQ statement
            //If it is not null, it will either be empty or have data. Either way that will be used in the Except LINQ statement
            //The Except LINQ statement will remove all items from includedTeams from the values coming back from GetListOfHeroTeams
            var heroTeamsList = GetListOfHeroTeams().Except(includedTeams ?? new List<HeroTeam>()).ToList();

            //This will chose a random HeroTeam
            var heroTeam = heroTeamsList[new Random().Next(heroTeamsList.Count)];

            //This will get the number of heroes that are in the team
            var heroesByTeam = GetHeroTeamMemberCount(heroTeam);

            //If the team doesn't have enough heroes
            //the team will be removed from the list of teams
            //a new team will be randomly chosen
            //and it will get the number of heroes in the team
            while(heroesByTeam < numberOfHeroes)
            {
                heroTeamsList.Remove(heroTeam);
                heroTeam = heroTeamsList[new Random().Next(heroTeamsList.Count)];
                heroesByTeam = GetHeroTeamMemberCount(heroTeam);
            }

            return heroTeam;
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

        /*public List<HeroInfo> ModifyHeroList(IEnumerable<string> heroExclusions)
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
        }*/

        public int GetHeroTeamMemberCount(HeroTeam heroTeam)
        {
            var returnList = new List<HeroInfo>(_heroes).Where(x=>x.HeroTeam==heroTeam);

            return returnList.Count();
        }

        public List<HeroTeam> GetListOfHeroTeams()
        {
            var heroTeamsList = new List<HeroTeam>();
            var heroTeams = Enum.GetValues(typeof(HeroTeam));

            //This will convert the array to a list
            foreach (var item in heroTeams)
            {
                heroTeamsList.Add((HeroTeam)item);
            }
            return heroTeamsList;
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

        public List<string> GetListOfHeroesWithKeyword(Keywords keyword)
        {
            var returnList = _heroes
                .Where(hero => hero.KeywordsList.Contains(keyword))
                .Select(hero => hero.HeroName).ToList();

            return returnList;
        }

        public List<string> GetListOfHeroesByX(string cardType, string name)
        {
            //cardType can be Henchmen, Scheme, Hero, Villain, or Mastermind
            var heroByTable = $"HeroBy{cardType}";
            var tableName = (cardType == "Hechmen") ? "Hechmen" : $"{cardType}s";
            var updatedName = name.Contains("'") ? name.Replace("'", "''") : name;

            var allHeroesBy = $@"select h.HeroName from Heroes h
                    inner join {heroByTable} hb ON h.Id = hb.HeroId
                    inner join {tableName} t ON t.Id = hb.{cardType}Id
                    where t.{cardType}Name = '{updatedName}'";

            var allHeroesByX = new SqlHelper().GetList(allHeroesBy);
            return allHeroesByX;
        }

        public List<HeroInfo> SetHeroList(List<string> heroNames)
        {
            var returnList = _heroes.Where(x => heroNames.Contains(x.HeroName)).ToList();

            return returnList;
        }
    }
}
