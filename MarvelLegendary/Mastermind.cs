using System.Collections.Generic;
using System.Linq;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;

namespace MarvelLegendary
{
    public static class MastermindRepository
    {
        private static readonly List<MastermindInfo> _masterminds = new List<MastermindInfo>()
        {
            new MastermindInfoBuilder().SetMastermindName("Dr. Doom").LeadsHenchmen("Doombot Legion", Set.Core).Duplicates(new List<int>{1, 158}).MastermindId(1).Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").LeadsVillain("Enemies of Asgard", Set.Core).Duplicates(new List<int>{2, 67, 162}).MastermindId(2).Build(),
            new MastermindInfoBuilder().SetMastermindName("Magneto").LeadsVillain("Brotherhood", Set.Core).Duplicates(new List<int>{3, 164}).MastermindId(3).Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull").LeadsVillain("HYDRA", Set.Core).Duplicates(new List<int>{4, 68, 166}).MastermindId(4).Build(),

            new MastermindInfoBuilder().SetMastermindName("Apocalypse").SetMastermindSet(Set.Dc).LeadsVillain("Four Horsemen", Set.Dc).MastermindId(5).Build(),
            new MastermindInfoBuilder().SetMastermindName("Kingpin").SetMastermindSet(Set.Dc).LeadsVillain("Streets of New York", Set.Dc).MastermindId(6).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mephisto").SetMastermindSet(Set.Dc).LeadsVillain("Underworld", Set.Dc).MastermindId(7).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mr. Sinister").SetMastermindSet(Set.Dc).LeadsVillain("Marauders", Set.Dc).MastermindId(8).Build(),
            new MastermindInfoBuilder().SetMastermindName("Stryfe").SetMastermindSet(Set.Dc).LeadsVillain("MLF", Set.Dc).MastermindId(9).Build(),

            new MastermindInfoBuilder().SetMastermindName("Galactus").SetMastermindSet(Set.Ff).LeadsVillain("Heralds of Galactus", Set.Ff).MastermindId(10).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mole Man").SetMastermindSet(Set.Ff).LeadsVillain("Subterranea", Set.Ff).MastermindId(11).Build(),

            new MastermindInfoBuilder().SetMastermindName("Carnage").SetMastermindSet(Set.PttR).LeadsVillain("Maximum Carnage", Set.PttR).MastermindId(12).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mysterio").SetMastermindSet(Set.PttR).LeadsVillain("Sinister Six", Set.PttR).MastermindId(13).Build(),

            new MastermindInfoBuilder().SetMastermindName("Dr. Strange").SetMastermindSet(Set.Villains).LeadsVillain("Defenders", Set.Villains).IncludeBindings().MastermindId(14).Build(),
            new MastermindInfoBuilder().SetMastermindName("Nick Fury").SetMastermindSet(Set.Villains).LeadsVillain("Avengers", Set.Villains).IncludeMadameHydra().MastermindId(15).Build(),
            new MastermindInfoBuilder().SetMastermindName("Odin").SetMastermindSet(Set.Villains).LeadsHenchmen("Asgardian Warriors", Set.Villains).IncludeBindings().MastermindId(16).Build(),
            new MastermindInfoBuilder().SetMastermindName("Professor X").SetMastermindSet(Set.Villains).LeadsVillain("X-Men First Class", Set.Villains).IncludeBindings().MastermindId(17).Build(),

            new MastermindInfoBuilder().SetMastermindName("Supreme Intelligence Of The Kree").SetMastermindSet(Set.GotG).LeadsVillain("Kree Starforce", Set.GotG).MastermindId(18).Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos").SetMastermindSet(Set.GotG).LeadsVillain("Infinity Gems", Set.GotG).MastermindId(19).Build(),

            new MastermindInfoBuilder().SetMastermindName("Uru-Enchanted Iron Man").SetMastermindSet(Set.Fi).LeadsVillain("The Mighty", Set.Fi).IncludeBindings().MastermindId(20).Build(),

            new MastermindInfoBuilder().SetMastermindName("Madelyne Pryor, Goblin Queen").SetMastermindSet(Set.Sw1).LeadsVillain("Limbo", Set.Sw1).MastermindId(21).Build(),
            new MastermindInfoBuilder().SetMastermindName("Nimrod, Super Sentinel").SetMastermindSet(Set.Sw1).LeadsVillain("Sentinel Territories", Set.Sw1).MastermindId(22).Build(),
            new MastermindInfoBuilder().SetMastermindName("Wasteland Hulk").SetMastermindSet(Set.Sw1).LeadsVillain("Wasteland", Set.Sw1).MastermindId(23).Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Green Goblin").SetMastermindSet(Set.Sw1).LeadsVillain("The Deadlands", Set.Sw1).MastermindId(24).Build(),

            new MastermindInfoBuilder().SetMastermindName("Immortal Emperor Zheng-Zhu").SetMastermindSet(Set.Sw2).LeadsVillain("K'un-Lun", Set.Sw2).MastermindId(25).Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hyperion").SetMastermindSet(Set.Sw2).LeadsVillain("Utopolis", Set.Sw2).MastermindId(26).Build(),
            new MastermindInfoBuilder().SetMastermindName("Shiklah, the Demon Bride").SetMastermindSet(Set.Sw2).LeadsVillain("Monster Metropolis", Set.Sw2).MastermindId(27).Build(),
            new MastermindInfoBuilder().SetMastermindName("Spider-Queen").SetMastermindSet(Set.Sw2).LeadsHenchmen("Spider-Infected", Set.Sw2).MastermindId(28).Build(),

            new MastermindInfoBuilder().SetMastermindName("Armin Zola").SetMastermindSet(Set.Ca).LeadsVillain("Zola's Creations", Set.Ca).MastermindId(29).Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Heinrich Zemo").SetMastermindSet(Set.Ca).LeadsVillain("Masters of Evil (WWII)", Set.Ca).MastermindId(30).Build(),

            new MastermindInfoBuilder().SetMastermindName("Authoritarian Iron Man").SetMastermindSet(Set.Cw).LeadsVillain("Superhuman Registration Act", Set.Cw).MastermindId(31).Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Helmut Zemo").SetMastermindSet(Set.Cw).LeadsVillain("Thunderbolts", Set.Cw).MastermindId(32).Build(),
            new MastermindInfoBuilder().SetMastermindName("Maria Hill, Director Of S.H.I.E.L.D").SetMastermindSet(Set.Cw).LeadsVillain("S.H.I.E.L.D. Elite", Set.Cw).MastermindId(33).Build(),
            new MastermindInfoBuilder().SetMastermindName("Misty Knight").SetMastermindSet(Set.Cw).LeadsVillain("Heroes for Hire", Set.Cw).MastermindId(34).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ragnarok").SetMastermindSet(Set.Cw).LeadsVillain("Registration Enforcers", Set.Cw).MastermindId(35).Build(),

            new MastermindInfoBuilder().SetMastermindName("Evil Deadpool").SetMastermindSet(Set.Deadpool).LeadsVillain("Evil Deadpool Corpse", Set.Deadpool).MastermindId(36).Build(),
            new MastermindInfoBuilder().SetMastermindName("Macho Gomez").SetMastermindSet(Set.Deadpool).LeadsVillain("Deadpool's \"Friends\"", Set.Deadpool).MastermindId(37).Build(),

            new MastermindInfoBuilder().SetMastermindName("Charles Xavier").SetMastermindSet(Set.Noir).LeadsVillain("X-Men Noir", Set.Noir).MastermindId(38).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Goblin, Underworld Boss").SetMastermindSet(Set.Noir).LeadsVillain("Goblin's Freak Show", Set.Noir).MastermindId(39).Build(),

            new MastermindInfoBuilder().SetMastermindName("Arcade").SetMastermindSet(Set.XMen).LeadsVillain("Murderworld", Set.XMen).MastermindId(40).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Arcade").SetMastermindSet(Set.XMen).LeadsVillain("Murderworld", Set.XMen).IncludeHorrors().MastermindId(41).Build(),
            new MastermindInfoBuilder().SetMastermindName("Dark Phoenix").SetMastermindSet(Set.XMen).LeadsVillain("Hellfire Club", Set.XMen).MastermindId(42).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dark Phoenix").SetMastermindSet(Set.XMen).LeadsVillain("Hellfire Club", Set.XMen).IncludeHorrors().MastermindId(43).Build(),
            new MastermindInfoBuilder().SetMastermindName("Deathbird").SetMastermindSet(Set.XMen).LeadsVillain("Shi'ar Imperial Guard", Set.XMen).LeadsHenchmen(new List<Henchmen> { Henchmen.GetNewHenchmen("Shi'ar Death Commandos", Set.XMen), Henchmen.GetNewHenchmen("Shi'ar Patrol Craft", Set.XMen)}).MastermindId(44).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Deathbird").SetMastermindSet(Set.XMen).LeadsVillain("Shi'ar Imperial Guard", Set.XMen).LeadsHenchmen(new List<Henchmen> { Henchmen.GetNewHenchmen("Shi'ar Death Commandos", Set.XMen), Henchmen.GetNewHenchmen("Shi'ar Patrol Craft", Set.XMen)}).IncludeHorrors().MastermindId(45).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mojo").SetMastermindSet(Set.XMen).LeadsVillain("Mojoverse", Set.XMen).MastermindId(46).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mojo").SetMastermindSet(Set.XMen).LeadsVillain("Mojoverse", Set.XMen).IncludeHorrors().MastermindId(47).Build(),
            new MastermindInfoBuilder().SetMastermindName("Onslaught").SetMastermindSet(Set.XMen).LeadsVillain("Dark Descendants", Set.XMen).MastermindId(48).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Onslaught").SetMastermindSet(Set.XMen).LeadsVillain("Dark Descendants", Set.XMen).IncludeHorrors().MastermindId(49).Build(),
            new MastermindInfoBuilder().SetMastermindName("Shadow King").SetMastermindSet(Set.XMen).LeadsVillain("Shadow-X", Set.XMen).MastermindId(50).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Shadow King").SetMastermindSet(Set.XMen).LeadsVillain("Shadow-X", Set.XMen).IncludeHorrors().MastermindId(51).Build(),

            new MastermindInfoBuilder().SetMastermindName("Adrian Toomes").SetMastermindSet(Set.Sm).LeadsVillain("Salvagers", Set.Sm).MastermindId(52).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Adrian Toomes").SetMastermindSet(Set.Sm).LeadsVillain("Salvagers", Set.Sm).MastermindId(53).Build(),
            new MastermindInfoBuilder().SetMastermindName("Vulture").SetMastermindSet(Set.Sm).LeadsVillain("Vulture Tech", Set.Sm).MastermindId(54).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Vulture").SetMastermindSet(Set.Sm).LeadsVillain("Vulture Tech", Set.Sm).MastermindId(55).Build(),

            new MastermindInfoBuilder().SetMastermindName("Fin Fang Foom").SetMastermindSet(Set.Champions).LeadsVillain("Monsters Unleashed", Set.Champions).MastermindId(56).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Fin Fang Foom").SetMastermindSet(Set.Champions).LeadsVillain("Monsters Unleashed", Set.Champions).MastermindId(57).Build(),
            new MastermindInfoBuilder().SetMastermindName("Pagliacci").SetMastermindSet(Set.Champions).LeadsVillain("Wrecking Crew", Set.Champions).MastermindId(58).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Pagliacci").SetMastermindSet(Set.Champions).LeadsVillain("Wrecking Crew", Set.Champions).MastermindId(59).Build(),

            new MastermindInfoBuilder().SetMastermindName("General Ross").SetMastermindSet(Set.Wwh).LeadsVillain("Code Red", Set.Wwh).MastermindId(60).Build(),
            new MastermindInfoBuilder().SetMastermindName("Illuminati, Secret Society").SetMastermindSet(Set.Wwh).LeadsVillain("Illuminati", Set.Wwh).MastermindId(61).Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hulk, Sakaarson").SetMastermindSet(Set.Wwh).LeadsVillain("Warbound", Set.Wwh).MastermindId(62).Build(),
            new MastermindInfoBuilder().SetMastermindName("M.O.D.O.K.").SetMastermindSet(Set.Wwh).LeadsVillain("Intelligencia", Set.Wwh).MastermindId(63).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Red King").SetMastermindSet(Set.Wwh).LeadsVillain("Sakaar Imperial Guard", Set.Wwh).MastermindId(64).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Sentry").SetMastermindSet(Set.Wwh).LeadsVillain("Aspects of the Void", Set.Wwh).MastermindId(65).Build(),

            new MastermindInfoBuilder().SetMastermindName("Iron Monger").SetMastermindSet(Set.P1).LeadsVillain("Iron Foes", Set.P1).MastermindId(66).Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").SetMastermindSet(Set.P1).LeadsVillain("Enemies of Asgard", Set.P1).Duplicates(new List<int>{2, 67, 162}).MastermindId(67).Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull").SetMastermindSet(Set.P1).LeadsVillain("HYDRA", Set.P1).Duplicates(new List<int>{4, 68, 166}).MastermindId(68).Build(),

            new MastermindInfoBuilder().SetMastermindName("Morgan Le Fay").SetMastermindSet(Set.Antman).LeadsVillain("Queen's Vengeance", Set.Antman).MastermindId(69).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Morgan Le Fay").SetMastermindSet(Set.Antman).LeadsVillain("Queen's Vengeance", Set.Antman).MastermindId(70).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron").SetMastermindSet(Set.Antman).LeadsVillain("Ultron's Legacy", Set.Antman).MastermindId(71).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron").SetMastermindSet(Set.Antman).LeadsVillain("Ultron's Legacy", Set.Antman).MastermindId(72).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hybrid").SetMastermindSet(Set.Venom).LeadsVillain("Life Foundation", Set.Venom).MastermindId(73).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hybrid").SetMastermindSet(Set.Venom).LeadsVillain("Life Foundation", Set.Venom).MastermindId(74).Build(),
            new MastermindInfoBuilder().SetMastermindName("Poison Thanos").SetMastermindSet(Set.Venom).LeadsVillain("Poisons", Set.Venom).MastermindId(75).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Poison Thanos").SetMastermindSet(Set.Venom).LeadsVillain("Poisons", Set.Venom).MastermindId(76).Build(),

            new MastermindInfoBuilder().SetMastermindName("J. Jonah Jameson").SetMastermindSet(Set.Dimensions).LeadsHenchmen("Spider-Slayer", Set.Dimensions).MastermindId(77).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic J. Jonah Jameson").SetMastermindSet(Set.Dimensions).LeadsHenchmen("Spider-Slayer", Set.Dimensions).MastermindId(78).Build(),

            new MastermindInfoBuilder().SetMastermindName("Grim Reaper").SetMastermindSet(Set.Revelations).LeadsVillain("Lethal Legion", Set.Revelations).MastermindId(79).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grim Reaper").SetMastermindSet(Set.Revelations).LeadsVillain("Lethal Legion", Set.Revelations).MastermindId(80).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Hood").SetMastermindSet(Set.Revelations).LeadsVillain("Hood's Gang", Set.Revelations).MastermindId(81).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Hood").SetMastermindSet(Set.Revelations).LeadsVillain("Hood's Gang", Set.Revelations).MastermindId(82).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mandarin").SetMastermindSet(Set.Revelations).LeadsHenchmen("Mandarin's Rings", Set.Revelations).MastermindId(83).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mandarin").SetMastermindSet(Set.Revelations).LeadsHenchmen("Mandarin's Rings", Set.Revelations).MastermindId(84).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hydra High Council").SetMastermindSet(Set.Shield).LeadsVillain("Hydra Elite", Set.Shield).MastermindId(85).Build(),
            new MastermindInfoBuilder().SetMastermindName("Hydra Super-Adaptoid").SetMastermindSet(Set.Shield).LeadsVillain("A.I.M., Hydra Offshoot", Set.Shield).MastermindId(86).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hela").SetMastermindSet(Set.Asgard).LeadsVillain("Omens of Ragnarok", Set.Asgard).MastermindId(87).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hela").SetMastermindSet(Set.Asgard).LeadsVillain("Omens of Ragnarok", Set.Asgard).MastermindId(88).Build(),
            new MastermindInfoBuilder().SetMastermindName("Malekith").SetMastermindSet(Set.Asgard).LeadsVillain("Dark Council", Set.Asgard).MastermindId(89).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Malekith").SetMastermindSet(Set.Asgard).LeadsVillain("Dark Council", Set.Asgard).MastermindId(90).Build(),

            new MastermindInfoBuilder().SetMastermindName("Belasco, Demon Lord of Limbo").SetMastermindSet(Set.NewMutants).LeadsVillain("Demons of Limbo", Set.NewMutants).MastermindId(91).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Belasco, Demon Lord of Limbo").SetMastermindSet(Set.NewMutants).LeadsVillain("Demons of Limbo", Set.NewMutants).MastermindId(92).Build(),
            new MastermindInfoBuilder().SetMastermindName("Emma Frost, The White Queen").SetMastermindSet(Set.NewMutants).LeadsVillain("Hellions", Set.NewMutants).MastermindId(93).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emma Frost, The White Queen").SetMastermindSet(Set.NewMutants).LeadsVillain("Hellions", Set.NewMutants).MastermindId(94).Build(),

            new MastermindInfoBuilder().SetMastermindName("The Beyonder").SetMastermindSet(Set.Cosmos).LeadsVillain("From Beyond", Set.Cosmos).MastermindId(95).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Beyonder").SetMastermindSet(Set.Cosmos).LeadsVillain("From Beyond", Set.Cosmos).MastermindId(96).Build(),
            new MastermindInfoBuilder().SetMastermindName("Grandmaster").SetMastermindSet(Set.Cosmos).LeadsVillain("Elders of the Universe", Set.Cosmos).MastermindId(97).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grandmaster").SetMastermindSet(Set.Cosmos).LeadsVillain("Elders of the Universe", Set.Cosmos).MastermindId(98).Build(),
            new MastermindInfoBuilder().SetMastermindName("Magus").SetMastermindSet(Set.Cosmos).LeadsHenchmen("Universal Church of Truth", Set.Cosmos).MastermindId(99).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Magus").SetMastermindSet(Set.Cosmos).LeadsHenchmen("Universal Church of Truth", Set.Cosmos).MastermindId(100).Build(),

            new MastermindInfoBuilder().SetMastermindName("Emperor Vulcan of the Shi'ar").SetMastermindSet(Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite", Set.Inhumans).MastermindId(101).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emperor Vulcan").SetMastermindSet(Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite", Set.Inhumans).MastermindId(102).Build(),
            new MastermindInfoBuilder().SetMastermindName("Maximus the Mad").SetMastermindSet(Set.Inhumans).LeadsVillain("Inhuman Rebellion", Set.Inhumans).MastermindId(103).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Maximus the Mad").SetMastermindSet(Set.Inhumans).LeadsVillain("Inhuman Rebellion", Set.Inhumans).MastermindId(104).Build(),

            new MastermindInfoBuilder().SetMastermindName("Annihilus").SetMastermindSet(Set.Annihilation).LeadsVillain("Annihilation Wave", Set.Annihilation).MastermindId(105).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Annihilus").SetMastermindSet(Set.Annihilation).LeadsVillain("Annihilation Wave", Set.Annihilation).MastermindId(106).Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang the Conqueror").SetMastermindSet(Set.Annihilation).LeadsVillain("Timelines of Kang", Set.Annihilation).MastermindId(107).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Kang the Conqueror").SetMastermindSet(Set.Annihilation).LeadsVillain("Timelines of Kang", Set.Annihilation).MastermindId(108).Build(),

            new MastermindInfoBuilder().SetMastermindName("Bastion, Fused Sentinel").SetMastermindSet(Set.Messiah).LeadsVillain("Purifiers", Set.Messiah).LeadsHenchmenByKind(new List<string> {"Sentinel" }).MastermindId(109).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Bastion, Fused Sentinel").SetMastermindSet(Set.Messiah).LeadsVillain("Purifiers", Set.Messiah).LeadsHenchmenByKind(new List<string> {"Sentinel" }).MastermindId(110).Build(),
            new MastermindInfoBuilder().SetMastermindName("Exodus").SetMastermindSet(Set.Messiah).LeadsVillain("Acolytes", Set.Messiah).MastermindId(111).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Exodus").SetMastermindSet(Set.Messiah).LeadsVillain("Acolytes", Set.Messiah).MastermindId(112).Build(),
            new MastermindInfoBuilder().SetMastermindName("Lady Deathstrike").SetMastermindSet(Set.Messiah).LeadsVillain("Reavers", Set.Messiah).MastermindId(113).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Lady Deathstrike").SetMastermindSet(Set.Messiah).LeadsVillain("Reavers", Set.Messiah).MastermindId(114).Build(),

            new MastermindInfoBuilder().SetMastermindName("Dormammu").SetMastermindSet(Set.Strange).LeadsVillain("Lords of the Netherworld", Set.Strange).MastermindId(115).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dormammu").SetMastermindSet(Set.Strange).LeadsVillain("Lords of the Netherworld", Set.Strange).MastermindId(116).Build(),
            new MastermindInfoBuilder().SetMastermindName("Nightmare").SetMastermindSet(Set.Strange).LeadsVillain("Fear Lords", Set.Strange).MastermindId(117).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Nightmare").SetMastermindSet(Set.Strange).LeadsVillain("Fear Lords", Set.Strange).MastermindId(118).Build(),

            new MastermindInfoBuilder().SetMastermindName("Ego, the Living Planet").SetMastermindSet(Set.Guardians).MastermindId(119).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ego, the Living Planet").SetMastermindSet(Set.Guardians).MastermindId(120).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ronan the Accuser").SetMastermindSet(Set.Guardians).LeadsVillain("Followers of Ronan", Set.Guardians).MastermindId(121).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ronan the Accuser").SetMastermindSet(Set.Guardians).LeadsVillain("Followers of Ronan", Set.Guardians).MastermindId(122).Build(),

            new MastermindInfoBuilder().SetMastermindName("Killmonger").SetMastermindSet(Set.BlackPanther).LeadsVillain("Killmonger's League", Set.BlackPanther).MastermindId(123).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Killmonger").SetMastermindSet(Set.BlackPanther).LeadsVillain("Killmonger's League", Set.BlackPanther).MastermindId(124).Build(),
            new MastermindInfoBuilder().SetMastermindName("Klaw").SetMastermindSet(Set.BlackPanther).LeadsVillain("Enemies of Wakanda", Set.BlackPanther).MastermindId(125).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Klaw").SetMastermindSet(Set.BlackPanther).LeadsVillain("Enemies of Wakanda", Set.BlackPanther).MastermindId(126).Build(),

            new MastermindInfoBuilder().SetMastermindName("Indestructible Man").SetMastermindSet(Set.BlackWidow).LeadsVillain("Elite Assassins", Set.BlackWidow).MastermindId(127).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Indestructible Man").SetMastermindSet(Set.BlackWidow).LeadsVillain("Elite Assassins", Set.BlackWidow).MastermindId(128).Build(),
            new MastermindInfoBuilder().SetMastermindName("Taskmaster").SetMastermindSet(Set.BlackWidow).LeadsVillain("Taskmaster's Thunderbolts", Set.BlackWidow).MastermindId(129).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Taskmaster").SetMastermindSet(Set.BlackWidow).LeadsVillain("Taskmaster's Thunderbolts", Set.BlackWidow).MastermindId(130).Build(),

            new MastermindInfoBuilder().SetMastermindName("Ebony Maw").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Children of Thanos", Set.InfinitySaga).MastermindId(131).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ebony Maw").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Children of Thanos", Set.InfinitySaga).MastermindId(132).Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos (Infinity Saga)").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Infinity Stones", Set.InfinitySaga).MastermindId(133).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Thanos (Infinity Saga)").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Infinity Stones", Set.InfinitySaga).MastermindId(134).Build(),

            new MastermindInfoBuilder().SetMastermindName("Lilith, Mother of Demons").SetMastermindSet(Set.MidnightSons).LeadsVillain("Lilin", Set.MidnightSons).MastermindId(135).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Lilith, Mother of Demons").SetMastermindSet(Set.MidnightSons).LeadsVillain("Lilin", Set.MidnightSons).MastermindId(136).Build(),
            new MastermindInfoBuilder().SetMastermindName("Zarathos").SetMastermindSet(Set.MidnightSons).LeadsVillain("The Fallen", Set.MidnightSons).MastermindId(137).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Zarathos").SetMastermindSet(Set.MidnightSons).LeadsVillain("The Fallen", Set.MidnightSons).MastermindId(138).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hank Pym, Yellowjacket").SetMastermindSet(Set.WhatIf).MastermindId(139).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hank Pym, Yellowjacket").SetMastermindSet(Set.WhatIf).MastermindId(140).Build(),
            new MastermindInfoBuilder().SetMastermindName("Killmonger, The Betrayer").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Vibranium Liberator Drones", Set.WhatIf).MastermindId(141).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Killmonger, The Betrayer").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Vibranium Liberator Drones", Set.WhatIf).MastermindId(142).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron Infinity").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Ultron Sentries", Set.WhatIf).AlwaysLeadsOnSolo().MastermindId(143).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron Infinity").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Ultron Sentries", Set.WhatIf).AlwaysLeadsOnSolo().MastermindId(144).Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Scarlet Witch").SetMastermindSet(Set.WhatIf).LeadsVillain("Zombie Avengers", Set.WhatIf).SetZombieSoloVillains().MastermindId(145).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Zombie Scarlet Witch").SetMastermindSet(Set.WhatIf).LeadsVillain("Zombie Avengers", Set.WhatIf).SetZombieSoloVillains().MastermindId(146).Build(),

            new MastermindInfoBuilder().SetMastermindName("Darrin Cross").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Cross Technologies", Set.AntmanWasp).MastermindId(147).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ghost, Master Thief").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Ghost Chasers", Set.AntmanWasp).MastermindId(148).Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang, Quantum Conqueror").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Armada of Kang", Set.AntmanWasp).MastermindId(149).Build(),

            new MastermindInfoBuilder().SetMastermindName("Alchemax Executives").SetMastermindSet(Set.TwentyNintyNine).LeadsVillain("Alchemax Enforcers", Set.TwentyNintyNine).IncludeExtraHero().MastermindId(150).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Alchemax Executives").SetMastermindSet(Set.TwentyNintyNine).LeadsVillain("Alchemax Enforcers", Set.TwentyNintyNine).IncludeExtraHero().MastermindId(151).Build(),
            new MastermindInfoBuilder().SetMastermindName("Sinister Six 2099").SetMastermindSet(Set.TwentyNintyNine).LeadsVillainsByKind(new List<string> {"Alchemax", "Sinister" }).MastermindId(152).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Sinister Six 2099").SetMastermindSet(Set.TwentyNintyNine).LeadsVillainsByKind(new List<string> {"Alchemax", "Sinister" }).MastermindId(153).Build(),

            new MastermindInfoBuilder().SetMastermindName("Omega Red").SetMastermindSet(Set.WeaponX).MastermindId(154).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Omega Red").SetMastermindSet(Set.WeaponX).MastermindId(155).Build(),
            new MastermindInfoBuilder().SetMastermindName("Romulus").SetMastermindSet(Set.WeaponX).LeadsVillain("Weapon Plus", Set.WeaponX).MastermindId(156).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Romulus").SetMastermindSet(Set.WeaponX).LeadsVillain("Weapon Plus", Set.WeaponX).MastermindId(157).Build(),

            new MastermindInfoBuilder().SetMastermindName("Doctor Doom").SetMastermindSet(Set.Core2E).LeadsHenchmen("Doombot Legion", Set.Core2E).Duplicates(new List<int>{1, 158}).MastermindId(158).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Doctor Doom").SetMastermindSet(Set.Core2E).LeadsHenchmen("Doombot Legion", Set.Core2E).MastermindId(159).Build(),
            new MastermindInfoBuilder().SetMastermindName("Doctor Octopus").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> { "Sinister" }).MastermindId(160).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Doctor Octopus").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> { "Sinister" }).MastermindId(161).Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").SetMastermindSet(Set.Core2E).LeadsVillain("Enemies of Asgard", Set.Core2E).Duplicates(new List<int>{2, 67, 162}).MastermindId(162).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Loki").SetMastermindSet(Set.Core2E).LeadsVillain("Enemies of Asgard", Set.Core2E).MastermindId(163).Build(),
            new MastermindInfoBuilder().SetMastermindName("Magneto").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Brotherhood", "X-Men" }).MastermindId(164).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Magneto").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Brotherhood", "X-Men" }).MastermindId(165).Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull, HYDRA Overlord").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Hydra" }).Duplicates(new List<int>{4, 68, 166}).MastermindId(166).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Red Skull, HYDRA Overlord").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Hydra" }).MastermindId(167).Build(),

        };

        public static IReadOnlyList<MastermindInfo> All => _masterminds;
    }

    public class Mastermind
    {
        public int Id { get; set; }
        public string MastermindName { get; set; }
        public Set SetName { get; set; }
        public Henchmen LeadsHenchmen { get; set; }
        public Villain LeadsVillain { get; set; }
        public bool DoesLeadHenchmen { get; set; }
        public bool DoesLeadVillain { get; set; }
        public MastermindInfo MastermindInfo { get; set; }
        public bool IncludeHorrors { get; set; }

        public static Mastermind ConvertToMastermind(MastermindInfo mastermindInfo)
        {
            return new Mastermind
            {
                Id = mastermindInfo.Id,
                MastermindName = mastermindInfo.MastermindName,
                SetName = mastermindInfo.SetName,
                LeadsHenchmen = mastermindInfo.LeadsHenchmen,
                LeadsVillain = mastermindInfo.LeadsVillain,
                DoesLeadHenchmen = mastermindInfo.DoesLeadHenchmen,
                DoesLeadVillain = mastermindInfo.DoesLeadVillain,
                MastermindInfo = mastermindInfo
            };

        }

        public static Mastermind GetNewMastermind(string mastermindName, Set ?mastermindSet)
        {
            var mastermindInfo = new MastermindInfo();

            if(!string.IsNullOrEmpty(mastermindName) && mastermindSet != null)
            {
                mastermindInfo = MastermindRepository.All.FirstOrDefault(m => m.MastermindName == mastermindName && m.SetName == mastermindSet);
            }
            else
            {
                mastermindInfo = MastermindRepository.All[RandomHelper.Instance.Next(MastermindRepository.All.Count)];
            }

            return ConvertToMastermind(mastermindInfo);
        }

        public static string ToString(Mastermind mastermind)
        {
            return $"\r\n{mastermind.MastermindName}, {mastermind.SetName.GetDescription()}";
        }

        public static string ToString(List<Mastermind> mastermindList)
        {
            var returnString = "\r\n";
            var counter = 1;

            var orderedMastermindList = mastermindList.OrderBy(x => (int)x.SetName).ToList();

            foreach (var mastermind in orderedMastermindList)
            {
                returnString = $"{returnString}{counter}) {mastermind.MastermindName.Split('_').First()}, {mastermind.SetName.GetDescription()}\r\n";
                counter++;
            }

            return $"{returnString.Remove(returnString.Length - 2)}\r\n";
        }

        public static List<string> GetListOfMasterminds()
        {
            var allMasterminds = MastermindRepository.All.Select(m => m.MastermindName).ToList();
            return allMasterminds;
        }

        public static List<Mastermind> ConvertToMastermindList(List<MastermindInfo> mastermindInfoList)
        {
            return (from mastermindInfo in mastermindInfoList
                    select ConvertToMastermind(mastermindInfo)).ToList();
        }

        private static List<Mastermind> ConvertToMastermindList(List<Card> mastermindCards)
        {
            var returnList = new List<Mastermind>();
            foreach (var mastermindCard in mastermindCards)
            {
                var mastermindInfo = MastermindRepository.All.FirstOrDefault(m => m.MastermindName == mastermindCard.CardName && (int)m.SetName == mastermindCard.SetId);
                returnList.Add(ConvertToMastermind(mastermindInfo));
            }

            return returnList;
        }

        public static Mastermind GetExtraMastermind(Scheme scheme, List<Mastermind> mastermindsInGame)
        {
            //Get Masterminds
            var mastermindList = ConvertToMastermindList(MastermindRepository.All.ToList());

            // Remove the masterminds that are currently in the game
            var idsInGame = new HashSet<int>(mastermindsInGame.Select(h => h.Id));
            var remainingMasterminds = mastermindList.Where(h => !idsInGame.Contains(h.Id)).ToList();

            //If a henchmen with duplicates is in the game then this will remove all duplicates from the pool to choose from
            foreach (var mastermindInGame in mastermindsInGame)
            {
                if (mastermindInGame.MastermindInfo.IsDuplicate)
                {
                    var duplicateMastermindList = MastermindRepository.All.Where(h => mastermindInGame.MastermindInfo.DuplicateMastermindIds.Contains(h.Id)).ToList();
                    idsInGame = new HashSet<int>(duplicateMastermindList.Select(h => h.Id));
                    remainingMasterminds = remainingMasterminds.Where(h => !idsInGame.Contains(h.Id)).ToList();
                }
            }

            var schemeCard = new Card
            {
                CardName = scheme.SchemeName,
                CardType = (int)CardType.Scheme,
                SetId = (int)scheme.SetName
            };

            //Get Masterminds that have played with the scheme
            var mastermindCardsByScheme = SqlHelper.GetCardRelationships(CardType.Mastermind, schemeCard);
            var mastermindsByScheme = ConvertToMastermindList(mastermindCardsByScheme);

            //Remove all Masterminds that have played with the scheme from the list
            idsInGame = new HashSet<int>(mastermindsByScheme.Select(m => m.Id));
            remainingMasterminds = remainingMasterminds.Where(m => !idsInGame.Contains(m.Id)).ToList();

            foreach (var mastermindInGame in mastermindsInGame)
            {
                var mastermindCard = new Card
                {
                    CardName = mastermindInGame.MastermindName,
                    CardType = (int)CardType.Mastermind,
                    SetId = (int)mastermindInGame.SetName
                };

                //Get all the masterminds that have played with the mastermind
                var mastermindCardsByMastermind = SqlHelper.GetCardRelationships(CardType.Mastermind, mastermindCard);
                var mastermindByMastermind = ConvertToMastermindList(mastermindCardsByMastermind);

                //Remove all Masterminds that have played with the Masterminds
                idsInGame = new HashSet<int>(mastermindByMastermind.Select(m => m.Id));
                remainingMasterminds = remainingMasterminds.Where(m => !idsInGame.Contains(m.Id)).ToList();
            }

            return remainingMasterminds[RandomHelper.Instance.Next(remainingMasterminds.Count)];
        }
    }
}
