using System;
using System.Collections.Generic;
using System.Linq;
using MarvelLegendary.Enums;

namespace MarvelLegendary
{
    public class Mastermind
    {
        public string MastermindName { get; set; }
        public Set SetName { get; set; }
        public string LeadsHenchmen { get; set; }
        public string LeadsVillain { get; set; }
        public bool DoesLeadHenchmen { get; set; }
        public bool DoesLeadVillain { get; set; }
        public MastermindInfo MastermindInfo { get; set; }
        public bool IncludeHorrors { get; set; }
        private Random random;

        private readonly List<MastermindInfo> _masterminds = new List<MastermindInfo>()
        {
            new MastermindInfoBuilder().SetMastermindName("Dr. Doom").LeadsHenchmen("Doombot Legion").Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").LeadsVillain("Enemies of Asgard").Build(),
            new MastermindInfoBuilder().SetMastermindName("Magneto").LeadsVillain("Brotherhood").Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull").LeadsVillain("HYDRA").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Apocalypse").SetMastermindSet(Set.Dc).LeadsVillain("Four Horsemen").Build(),
            new MastermindInfoBuilder().SetMastermindName("Kingpin").SetMastermindSet(Set.Dc).LeadsVillain("Streets of New York").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mephisto").SetMastermindSet(Set.Dc).LeadsVillain("Underworld").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mr. Sinister").SetMastermindSet(Set.Dc).LeadsVillain("Marauders").Build(),
            new MastermindInfoBuilder().SetMastermindName("Stryfe").SetMastermindSet(Set.Dc).LeadsVillain("MLF").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Galactus").SetMastermindSet(Set.Ff).LeadsVillain("Heralds of Galactus").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mole Man").SetMastermindSet(Set.Ff).LeadsVillain("Subterranea").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Carnage").SetMastermindSet(Set.PttR).LeadsVillain("Maximum Carnage").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mysterio").SetMastermindSet(Set.PttR).LeadsVillain("Sinister Six").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Dr. Strange").SetMastermindSet(Set.Villains).LeadsVillain("Defenders").IncludeBindings().Build(),
            new MastermindInfoBuilder().SetMastermindName("Nick Fury").SetMastermindSet(Set.Villains).LeadsVillain("Avengers").IncludeMadameHydra().Build(),
            new MastermindInfoBuilder().SetMastermindName("Odin").SetMastermindSet(Set.Villains).LeadsHenchmen("Asgardian Warriors").IncludeBindings().Build(),
            new MastermindInfoBuilder().SetMastermindName("Professor X").SetMastermindSet(Set.Villains).LeadsVillain("X-Men First Class").IncludeBindings().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Supreme Intelligence Of The Kree").SetMastermindSet(Set.GotG).LeadsVillain("Kree Starforce").Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos").SetMastermindSet(Set.GotG).LeadsVillain("Infinity Gems").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Uru-Enchanted Iron Man").SetMastermindSet(Set.Fi).LeadsVillain("The Mighty").IncludeBindings().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Madelyne Pryor, Goblin Queen").SetMastermindSet(Set.Sw1).LeadsVillain("Limbo").Build(),
            new MastermindInfoBuilder().SetMastermindName("Nimrod, Super Sentinel").SetMastermindSet(Set.Sw1).LeadsVillain("Sentinel Territories").Build(),
            new MastermindInfoBuilder().SetMastermindName("Wasteland Hulk").SetMastermindSet(Set.Sw1).LeadsVillain("Wasteland").Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Green Goblin").SetMastermindSet(Set.Sw1).LeadsVillain("The Deadlands").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Immortal Emperor Zheng-Zhu").SetMastermindSet(Set.Sw2).LeadsVillain("K'un-Lun").Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hyperion").SetMastermindSet(Set.Sw2).LeadsVillain("Utopolis").Build(),
            new MastermindInfoBuilder().SetMastermindName("Shiklah, the Demon Bride").SetMastermindSet(Set.Sw2).LeadsVillain("Monster Metropolis").Build(),
            new MastermindInfoBuilder().SetMastermindName("Spider-Queen").SetMastermindSet(Set.Sw2).LeadsHenchmen("Spider-Infected").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Armin Zola").SetMastermindSet(Set.Ca).LeadsVillain("Zola's Creations").Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Heinrich Zemo").SetMastermindSet(Set.Ca).LeadsVillain("Masters of Evil (WWII)").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Authoritarian Iron Man").SetMastermindSet(Set.Cw).LeadsVillain("Superhuman Registration Act").Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Helmut Zemo").SetMastermindSet(Set.Cw).LeadsVillain("Thunderbolts").Build(),
            new MastermindInfoBuilder().SetMastermindName("Maria Hill, Director Of S.H.I.E.L.D").SetMastermindSet(Set.Cw).LeadsVillain("S.H.I.E.L.D. Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Misty Knight").SetMastermindSet(Set.Cw).LeadsVillain("Heroes for Hire").Build(),
            new MastermindInfoBuilder().SetMastermindName("Ragnarok").SetMastermindSet(Set.Cw).LeadsVillain("Registration Enforcers").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Evil Deadpool").SetMastermindSet(Set.Deadpool).LeadsVillain("Evil Deadpool Corpse").Build(),
            new MastermindInfoBuilder().SetMastermindName("Macho Gomez").SetMastermindSet(Set.Deadpool).LeadsVillain("Deadpool's \"Friends\"").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Charles Xavier").SetMastermindSet(Set.Noir).LeadsVillain("X-Men Noir").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Goblin, Underworld Boss").SetMastermindSet(Set.Noir).LeadsVillain("Goblin's Freak Show").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Arcade").SetMastermindSet(Set.XMen).LeadsVillain("Murderworld").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Arcade").SetMastermindSet(Set.XMen).LeadsVillain("Murderworld").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Dark Phoenix").SetMastermindSet(Set.XMen).LeadsVillain("Hellfire Club").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dark Phoenix").SetMastermindSet(Set.XMen).LeadsVillain("Hellfire Club").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Deathbird").SetMastermindSet(Set.XMen).LeadsVillain("Shi'ar Imperial Guard").LeadsHenchmen(new List<string> { "Shi'ar Death Commandos", "Shi'ar Patrol Craft"}).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Deathbird").SetMastermindSet(Set.XMen).LeadsVillain("Shi'ar Imperial Guard").LeadsHenchmen(new List<string> { "Shi'ar Death Commandos", "Shi'ar Patrol Craft"}).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Mojo").SetMastermindSet(Set.XMen).LeadsVillain("Mojoverse").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mojo").SetMastermindSet(Set.XMen).LeadsVillain("Mojoverse").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Onslaught").SetMastermindSet(Set.XMen).LeadsVillain("Dark Descendants").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Onslaught").SetMastermindSet(Set.XMen).LeadsVillain("Dark Descendants").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Shadow King").SetMastermindSet(Set.XMen).LeadsVillain("Shadow-X").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Shadow King").SetMastermindSet(Set.XMen).LeadsVillain("Shadow-X").IncludeHorrors().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Adrian Toomes").SetMastermindSet(Set.Sm).LeadsVillain("Salvagers").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Adrian Toomes").SetMastermindSet(Set.Sm).LeadsVillain("Salvagers").Build(),
            new MastermindInfoBuilder().SetMastermindName("Vulture").SetMastermindSet(Set.Sm).LeadsVillain("Vulture Tech").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Vulture").SetMastermindSet(Set.Sm).LeadsVillain("Vulture Tech").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Fin Fang Foom").SetMastermindSet(Set.Champions).LeadsVillain("Monsters Unleashed").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Fin Fang Foom").SetMastermindSet(Set.Champions).LeadsVillain("Monsters Unleashed").Build(),
            new MastermindInfoBuilder().SetMastermindName("Pagliacci").SetMastermindSet(Set.Champions).LeadsVillain("Wrecking Crew").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Pagliacci").SetMastermindSet(Set.Champions).LeadsVillain("Wrecking Crew").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("General Ross").SetMastermindSet(Set.Wwh).LeadsVillain("Code Red").Build(),
            new MastermindInfoBuilder().SetMastermindName("Illuminati, Secret Society").SetMastermindSet(Set.Wwh).LeadsVillain("Illuminati").Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hulk, Sakaarson").SetMastermindSet(Set.Wwh).LeadsVillain("Warbound").Build(),
            new MastermindInfoBuilder().SetMastermindName("M.O.D.O.K.").SetMastermindSet(Set.Wwh).LeadsVillain("Intelligencia").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Red King").SetMastermindSet(Set.Wwh).LeadsVillain("Sakaar Imperial Guard").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Sentry").SetMastermindSet(Set.Wwh).LeadsVillain("Aspects of the Void").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Iron Monger").SetMastermindSet(Set.P1).LeadsVillain("Iron Foes").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Morgan Le Fay").SetMastermindSet(Set.Antman).LeadsVillain("Queen's Vengeance").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Morgan Le Fay").SetMastermindSet(Set.Antman).LeadsVillain("Queen's Vengeance").Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron").SetMastermindSet(Set.Antman).LeadsVillain("Ultron's Legacy").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron").SetMastermindSet(Set.Antman).LeadsVillain("Ultron's Legacy").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Hybrid").SetMastermindSet(Set.Venom).LeadsVillain("Life Foundation").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hybrid").SetMastermindSet(Set.Venom).LeadsVillain("Life Foundation").Build(),
            new MastermindInfoBuilder().SetMastermindName("Poison Thanos").SetMastermindSet(Set.Venom).LeadsVillain("Poisons").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Poison Thanos").SetMastermindSet(Set.Venom).LeadsVillain("Poisons").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("J. Jonah Jameson").SetMastermindSet(Set.Dimensions).LeadsHenchmen("Spider-Slayer").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic J. Jonah Jameson").SetMastermindSet(Set.Dimensions).LeadsHenchmen("Spider-Slayer").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Grim Reaper").SetMastermindSet(Set.Revelations).LeadsVillain("Lethal Legion").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grim Reaper").SetMastermindSet(Set.Revelations).LeadsVillain("Lethal Legion").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Hood").SetMastermindSet(Set.Revelations).LeadsVillain("Hood's Gang").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Hood").SetMastermindSet(Set.Revelations).LeadsVillain("Hood's Gang").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mandarin").SetMastermindSet(Set.Revelations).LeadsHenchmen("Mandarin's Rings").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mandarin").SetMastermindSet(Set.Revelations).LeadsHenchmen("Mandarin's Rings").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Hydra High Council").SetMastermindSet(Set.Shield).LeadsVillain("Hydra Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Hydra Super-Adaptoid").SetMastermindSet(Set.Shield).LeadsHenchmen("A.I.M., Hydra Offshoot").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Hela").SetMastermindSet(Set.Asgard).LeadsVillain("Omens of Ragnarok").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hela").SetMastermindSet(Set.Asgard).LeadsVillain("Omens of Ragnarok").Build(),
            new MastermindInfoBuilder().SetMastermindName("Malekith").SetMastermindSet(Set.Asgard).LeadsVillain("Dark Council").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Malekith").SetMastermindSet(Set.Asgard).LeadsVillain("Dark Council").Build(),

            new MastermindInfoBuilder().SetMastermindName("Belasco, Demon Lord of Limbo").SetMastermindSet(Set.NewMutants).LeadsVillain("Demons of Limbo").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Belasco, Demon Lord of Limbo").SetMastermindSet(Set.NewMutants).LeadsVillain("Demons of Limbo").Build(),
            new MastermindInfoBuilder().SetMastermindName("Emma Frost, The White Queen").SetMastermindSet(Set.NewMutants).LeadsVillain("Hellions").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emma Frost, The White Queen").SetMastermindSet(Set.NewMutants).LeadsVillain("Hellions").Build(),

            new MastermindInfoBuilder().SetMastermindName("The Beyonder").SetMastermindSet(Set.Cosmos).LeadsVillain("From Beyond").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Beyonder").SetMastermindSet(Set.Cosmos).LeadsVillain("From Beyond").Build(),
            new MastermindInfoBuilder().SetMastermindName("Grandmaster").SetMastermindSet(Set.Cosmos).LeadsVillain("Elders of the Universe").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grandmaster").SetMastermindSet(Set.Cosmos).LeadsVillain("Elders of the Universe").Build(),
            new MastermindInfoBuilder().SetMastermindName("Magus").SetMastermindSet(Set.Cosmos).LeadsHenchmen("Universal Church of Truth").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Magus").SetMastermindSet(Set.Cosmos).LeadsHenchmen("Universal Church of Truth").Build(),

            new MastermindInfoBuilder().SetMastermindName("Emperor Vulcan of the Shi'ar").SetMastermindSet(Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emperor Vulcan").SetMastermindSet(Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Maximus the Mad").SetMastermindSet(Set.Inhumans).LeadsVillain("Inhuman Rebellion").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Maximus the Mad").SetMastermindSet(Set.Inhumans).LeadsVillain("Inhuman Rebellion").Build(),

            new MastermindInfoBuilder().SetMastermindName("Annihilus").SetMastermindSet(Set.Annihilation).LeadsVillain("Annihilation Wave").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Annihilus").SetMastermindSet(Set.Annihilation).LeadsVillain("Annihilation Wave").Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang the Conqueror").SetMastermindSet(Set.Annihilation).LeadsVillain("Timelines of Kang").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Kang the Conqueror").SetMastermindSet(Set.Annihilation).LeadsVillain("Timelines of Kang").Build(),

            new MastermindInfoBuilder().SetMastermindName("Bastion, Fused Sentinel").SetMastermindSet(Set.Messiah).LeadsVillain("Purifiers").LeadsHenchmenByKind("Sentinel").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Bastion, Fused Sentinel").SetMastermindSet(Set.Messiah).LeadsVillain("Purifiers").LeadsHenchmenByKind("Sentinel").Build(),
            new MastermindInfoBuilder().SetMastermindName("Exodus").SetMastermindSet(Set.Messiah).LeadsVillain("Acolytes").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Exodus").SetMastermindSet(Set.Messiah).LeadsVillain("Acolytes").Build(),
            new MastermindInfoBuilder().SetMastermindName("Lady Deathstrike").SetMastermindSet(Set.Messiah).LeadsVillain("Reavers").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Lady Deathstrike").SetMastermindSet(Set.Messiah).LeadsVillain("Reavers").Build(),

            new MastermindInfoBuilder().SetMastermindName("Dormammu").SetMastermindSet(Set.Strange).LeadsVillain("Lords of the Netherworld").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dormammu").SetMastermindSet(Set.Strange).LeadsVillain("Lords of the Netherworld").Build(),
            new MastermindInfoBuilder().SetMastermindName("Nightmare").SetMastermindSet(Set.Strange).LeadsVillain("Fear Lords").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Nightmare").SetMastermindSet(Set.Strange).LeadsVillain("Fear Lords").Build(),

            new MastermindInfoBuilder().SetMastermindName("Ego, the Living Planet").SetMastermindSet(Set.Guardians).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ego, the Living Planet").SetMastermindSet(Set.Guardians).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ronan the Accuser").SetMastermindSet(Set.Guardians).LeadsVillain("Followers of Ronan").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ronan the Accuser").SetMastermindSet(Set.Guardians).LeadsVillain("Followers of Ronan").Build(),

            new MastermindInfoBuilder().SetMastermindName("Killmonger").SetMastermindSet(Set.BlackPanther).LeadsVillain("Killmonger's League").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Killmonger").SetMastermindSet(Set.BlackPanther).LeadsVillain("Killmonger's League").Build(),
            new MastermindInfoBuilder().SetMastermindName("Klaw").SetMastermindSet(Set.BlackPanther).LeadsVillain("Enemies of Wakanda").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Klaw").SetMastermindSet(Set.BlackPanther).LeadsVillain("Enemies of Wakanda").Build(),

            new MastermindInfoBuilder().SetMastermindName("Indestructible Man").SetMastermindSet(Set.BlackWidow).LeadsVillain("Elite Assassins").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Indestructible Man").SetMastermindSet(Set.BlackWidow).LeadsVillain("Elite Assassins").Build(),
            new MastermindInfoBuilder().SetMastermindName("Taskmaster").SetMastermindSet(Set.BlackWidow).LeadsVillain("Taskmaster's Thunderbolts").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Taskmaster").SetMastermindSet(Set.BlackWidow).LeadsVillain("Taskmaster's Thunderbolts").Build(),

            new MastermindInfoBuilder().SetMastermindName("Ebony Maw").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Children of Thanos").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ebony Maw").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Children of Thanos").Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos (Infinity Saga)").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Infinity Stones").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Thanos (Infinity Saga)").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Infinity Stones").Build(),

            new MastermindInfoBuilder().SetMastermindName("Lilith, Mother of Demons").SetMastermindSet(Set.MidnightSons).LeadsVillain("Lilin").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Lilith, Mother of Demons").SetMastermindSet(Set.MidnightSons).LeadsVillain("Lilin").Build(),
            new MastermindInfoBuilder().SetMastermindName("Zarathos").SetMastermindSet(Set.MidnightSons).LeadsVillain("The Fallen").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Zarathos").SetMastermindSet(Set.MidnightSons).LeadsVillain("The Fallen").Build(),

            new MastermindInfoBuilder().SetMastermindName("Hank Pym, Yellowjacket").SetMastermindSet(Set.WhatIf).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hank Pym, Yellowjacket").SetMastermindSet(Set.WhatIf).Build(),
            new MastermindInfoBuilder().SetMastermindName("Killmonger, The Betrayer").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Vibranium Liberator Drones").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Killmonger, The Betrayer").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Vibranium Liberator Drones").Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron Infinity").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Ultron Sentries").AlwaysLeadsOnSolo().Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron Infinity").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Ultron Sentries").AlwaysLeadsOnSolo().Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Scarlet Witch").SetMastermindSet(Set.WhatIf).LeadsVillain("Zombie Avengers").SetZombieSoloVillains().Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Zombie Scarlet Witch").SetMastermindSet(Set.WhatIf).LeadsVillain("Zombie Avengers").SetZombieSoloVillains().Build(),

            new MastermindInfoBuilder().SetMastermindName("Darrin Cross").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Cross Technologies").Build(),
            new MastermindInfoBuilder().SetMastermindName("Ghost, Master Thief").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Ghost Chasers").Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang, Quantum Conqueror").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Armada of Kang").Build(),

            new MastermindInfoBuilder().SetMastermindName("Alchemax Executives").SetMastermindSet(Set.TwentyNintyNine).LeadsVillain("Alchemax Executives").IncludeExtraHero().Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Alchemax Executives").SetMastermindSet(Set.TwentyNintyNine).LeadsVillain("Alchemax Executives").IncludeExtraHero().Build(),
            new MastermindInfoBuilder().SetMastermindName("Sinister Six 2099").SetMastermindSet(Set.TwentyNintyNine).LeadsVillainsByKind(new List<string> {"Alchemax", "Sinister" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Sinister Six 2099").SetMastermindSet(Set.TwentyNintyNine).LeadsVillainsByKind(new List<string> {"Alchemax", "Sinister" }).Build(),
        };

        public Mastermind()
        {
            random = new Random();
        }

        public Mastermind GetNewMastermind(string mastermindName = "")
        {
            var newMastermind = new Mastermind();
            var mastermind = mastermindName;
            if(string.IsNullOrEmpty(mastermind))
            {
                var allMasterminds = GetListOfMasterminds();
                mastermind = allMasterminds[random.Next(allMasterminds.Count)];
            }
            
            var mastermindInfo = _masterminds.FirstOrDefault(m => m.MastermindName == mastermind);

            newMastermind.MastermindName = mastermindInfo.MastermindName;
            newMastermind.SetName = mastermindInfo.SetName;
            newMastermind.LeadsHenchmen = mastermindInfo.LeadsHenchmen;
            newMastermind.LeadsVillain = mastermindInfo.LeadsVillain;
            newMastermind.DoesLeadHenchmen = mastermindInfo.DoesLeadHenchmen;
            newMastermind.DoesLeadVillain = mastermindInfo.DoesLeadVillain;
            newMastermind.MastermindInfo = mastermindInfo;

            return newMastermind;
        }
        
        public Mastermind(string mastermindName)
        {
            var mastermindInfo = _masterminds.First(x => x.MastermindName == mastermindName);

            MastermindName = mastermindName;
            SetName = mastermindInfo.SetName;
            LeadsHenchmen = mastermindInfo.LeadsHenchmen;
            LeadsVillain = mastermindInfo.LeadsVillain;
            DoesLeadHenchmen = mastermindInfo.DoesLeadHenchmen;
            DoesLeadVillain = mastermindInfo.DoesLeadVillain;
            MastermindInfo = mastermindInfo;
        }

        public List<MastermindInfo> GetMasterminds(List<string> masterminds)
        {
            var returnList = new List<MastermindInfo>();
            foreach (var mastermind in masterminds)
            {
                if (_masterminds.Any(x => x.MastermindName == mastermind))
                {
                    returnList.Add(_masterminds.First(x => x.MastermindName == mastermind));
                }
            }

            return returnList;
        }

        public string ToString(Mastermind mastermind)
        {
            return $"\r\n{mastermind.MastermindName}, {mastermind.SetName.GetDescription()}";
        }

        public string ToString(List<Mastermind> mastermindList)
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

        public List<MastermindInfo> ModifyMastermindList(List<string> mastermindExclusions)
        {
            var returnList = _masterminds;

            foreach (var mastermindExclusion in mastermindExclusions)
            {
                while (returnList.Any(x => x.MastermindName == mastermindExclusion))
                {
                    var itemToRemove = returnList.Single(x => x.MastermindName == mastermindExclusion);
                    returnList.Remove(itemToRemove);
                }
            }

            return returnList;
        }

        public List<string> GetListOfMasterminds()
        {
            var allMastermindsQuery = "SELECT [MastermindName] FROM [Masterminds]";
            var allMasterminds = new SqlHelper().GetList(allMastermindsQuery);
            return allMasterminds;
        }

        public string GetRandomMastermind()
        {
            var allMasterminds = GetListOfMasterminds();
            var mastermind = allMasterminds[random.Next(allMasterminds.Count)];
            return mastermind;
        }

        public List<string> GetListOfMastermindByX(string cardType, string name)
        {
            //cardType can be Henchmen, Scheme, Hero, Villain, or Mastermind
            var mastermindByTable = $"MastermindBy{cardType}";
            var tableName = (cardType == "Henchmen") ? "Henchmen" : (cardType == "Hero" ? "Heroes" : $"{cardType}s");

            //need to handle if this is MastermindxMastermind
            //{tableSuffix}Id has to become {tableSuffix}2Id
            var cardTypeId = cardType == "Mastermind" ? $"{cardType}2Id" : $"{cardType}Id";

            var allMastermindsBy = $@"select m.MastermindName from Masterminds m
                    inner join {mastermindByTable} mb ON m.Id = mb.MastermindId
                    inner join {tableName} t ON t.Id = mb.{cardTypeId}
                    where t.{cardType}Name = '{name}'";

            var allMastermindsByX = new SqlHelper().GetList(allMastermindsBy);
            return allMastermindsByX;
        }

        public List<Mastermind> GetExtraMasterminds(Scheme scheme, Mastermind mainMastermind)
        {
            var sqlHelper = new SqlHelper();
            var returnList = new List<Mastermind>();
            var mastermindsInGame = new List<string> { mainMastermind.MastermindName };
            var extraMasterminds = new List<string>();

            //Get Masterminds
            var mastermindList = GetListOfMasterminds();

            //Get Masterminds that have played with the scheme
            //var mastermindsByScheme = GetListOfMastermindByX("Scheme", scheme.SchemeName);
            var mastermindsByScheme = sqlHelper.GetListFromByTable("Mastermind", "Scheme", scheme.SchemeName);

            //Remove all Masterminds that have played with the scheme from the list
            var remainingMasterminds = mastermindList.Except(mastermindsByScheme).ToList();

            //Remove the current Mastermind from the list
            remainingMasterminds = remainingMasterminds.Except(mastermindsInGame).ToList();

            //Get all the masterminds that have played with the main mastermind
            //var mastermindByMastermind = GetListOfMastermindByX("Mastermind", mainMastermind.MastermindName);
            var mastermindByMastermind = sqlHelper.GetListFromByTable("Mastermind", "Mastermind", mainMastermind.MastermindName);

            for (int i = 0; i < scheme.SchemeInfo.NumberExtraMasterminds; i++)
            {
                //Get random mastermind from remaining list
                var newMastermind = remainingMasterminds[random.Next(remainingMasterminds.Count)];

                //Get MastermindxMastermind
                //mastermindByMastermind = GetListOfMastermindByX("Mastermind", newMastermind);
                mastermindByMastermind = sqlHelper.GetListFromByTable("Mastermind", "Mastermind", mainMastermind.MastermindName);

                //Get SchemexMastermind
                //var schemeByMastermind = new Scheme().GetListOfSchemesByX("Mastermind", newMastermind);
                var schemeByMastermind = sqlHelper.GetListFromByTable("Scheme", "Mastermind", newMastermind);

                //Remove all from main list
                remainingMasterminds = remainingMasterminds.Except(mastermindByMastermind).ToList();
                remainingMasterminds = remainingMasterminds.Except(schemeByMastermind).ToList();

                //Add mastermind to extraMasterminds
                extraMasterminds.Add(newMastermind);

                //Add mastermind to mastermndsInGame
                mastermindsInGame.Add(newMastermind);
            }

            returnList.AddRange(from item in extraMasterminds
                                select new Mastermind().GetNewMastermind(item));

            if (scheme.SchemeInfo.IsDrainedMastermind)
            {
                scheme.SchemeInfo.DrainedMastermind = new Mastermind().GetNewMastermind(extraMasterminds.First());
            }

            return returnList;
        }
    }
}
