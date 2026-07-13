using System;
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
            new MastermindInfoBuilder().SetMastermindName("Dr. Doom").LeadsHenchmen("Doombot Legion", Set.Core).Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").LeadsVillain("Enemies of Asgard", Set.Core).Build(),
            new MastermindInfoBuilder().SetMastermindName("Magneto").LeadsVillain("Brotherhood", Set.Core).Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull").LeadsVillain("HYDRA", Set.Core).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Apocalypse").SetMastermindSet(Set.Dc).LeadsVillain("Four Horsemen", Set.Dc).Build(),
            new MastermindInfoBuilder().SetMastermindName("Kingpin").SetMastermindSet(Set.Dc).LeadsVillain("Streets of New York", Set.Dc).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mephisto").SetMastermindSet(Set.Dc).LeadsVillain("Underworld", Set.Dc).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mr. Sinister").SetMastermindSet(Set.Dc).LeadsVillain("Marauders", Set.Dc).Build(),
            new MastermindInfoBuilder().SetMastermindName("Stryfe").SetMastermindSet(Set.Dc).LeadsVillain("MLF", Set.Dc).Build(),

            new MastermindInfoBuilder().SetMastermindName("Galactus").SetMastermindSet(Set.Ff).LeadsVillain("Heralds of Galactus", Set.Ff).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mole Man").SetMastermindSet(Set.Ff).LeadsVillain("Subterranea", Set.Ff).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Carnage").SetMastermindSet(Set.PttR).LeadsVillain("Maximum Carnage", Set.PttR).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mysterio").SetMastermindSet(Set.PttR).LeadsVillain("Sinister Six", Set.PttR).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Dr. Strange").SetMastermindSet(Set.Villains).LeadsVillain("Defenders", Set.Villains).IncludeBindings().Build(),
            new MastermindInfoBuilder().SetMastermindName("Nick Fury").SetMastermindSet(Set.Villains).LeadsVillain("Avengers", Set.Villains).IncludeMadameHydra().Build(),
            new MastermindInfoBuilder().SetMastermindName("Odin").SetMastermindSet(Set.Villains).LeadsHenchmen("Asgardian Warriors", Set.Villains).IncludeBindings().Build(),
            new MastermindInfoBuilder().SetMastermindName("Professor X").SetMastermindSet(Set.Villains).LeadsVillain("X-Men First Class", Set.Villains).IncludeBindings().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Supreme Intelligence Of The Kree").SetMastermindSet(Set.GotG).LeadsVillain("Kree Starforce", Set.GotG).Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos").SetMastermindSet(Set.GotG).LeadsVillain("Infinity Gems", Set.GotG).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Uru-Enchanted Iron Man").SetMastermindSet(Set.Fi).LeadsVillain("The Mighty", Set.Fi).IncludeBindings().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Madelyne Pryor, Goblin Queen").SetMastermindSet(Set.Sw1).LeadsVillain("Limbo", Set.Sw1).Build(),
            new MastermindInfoBuilder().SetMastermindName("Nimrod, Super Sentinel").SetMastermindSet(Set.Sw1).LeadsVillain("Sentinel Territories", Set.Sw1).Build(),
            new MastermindInfoBuilder().SetMastermindName("Wasteland Hulk").SetMastermindSet(Set.Sw1).LeadsVillain("Wasteland", Set.Sw1).Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Green Goblin").SetMastermindSet(Set.Sw1).LeadsVillain("The Deadlands", Set.Sw1).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Immortal Emperor Zheng-Zhu").SetMastermindSet(Set.Sw2).LeadsVillain("K'un-Lun", Set.Sw2).Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hyperion").SetMastermindSet(Set.Sw2).LeadsVillain("Utopolis", Set.Sw2).Build(),
            new MastermindInfoBuilder().SetMastermindName("Shiklah, the Demon Bride").SetMastermindSet(Set.Sw2).LeadsVillain("Monster Metropolis", Set.Sw2).Build(),
            new MastermindInfoBuilder().SetMastermindName("Spider-Queen").SetMastermindSet(Set.Sw2).LeadsHenchmen("Spider-Infected", Set.Sw2).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Armin Zola").SetMastermindSet(Set.Ca).LeadsVillain("Zola's Creations", Set.Ca).Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Heinrich Zemo").SetMastermindSet(Set.Ca).LeadsVillain("Masters of Evil (WWII)", Set.Ca).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Authoritarian Iron Man").SetMastermindSet(Set.Cw).LeadsVillain("Superhuman Registration Act", Set.Cw).Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Helmut Zemo").SetMastermindSet(Set.Cw).LeadsVillain("Thunderbolts", Set.Cw).Build(),
            new MastermindInfoBuilder().SetMastermindName("Maria Hill, Director Of S.H.I.E.L.D").SetMastermindSet(Set.Cw).LeadsVillain("S.H.I.E.L.D. Elite", Set.Cw).Build(),
            new MastermindInfoBuilder().SetMastermindName("Misty Knight").SetMastermindSet(Set.Cw).LeadsVillain("Heroes for Hire", Set.Cw).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ragnarok").SetMastermindSet(Set.Cw).LeadsVillain("Registration Enforcers", Set.Cw).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Evil Deadpool").SetMastermindSet(Set.Deadpool).LeadsVillain("Evil Deadpool Corpse", Set.Deadpool).Build(),
            new MastermindInfoBuilder().SetMastermindName("Macho Gomez").SetMastermindSet(Set.Deadpool).LeadsVillain("Deadpool's \"Friends\"", Set.Deadpool).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Charles Xavier").SetMastermindSet(Set.Noir).LeadsVillain("X-Men Noir", Set.Noir).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Goblin, Underworld Boss").SetMastermindSet(Set.Noir).LeadsVillain("Goblin's Freak Show", Set.Noir).Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Arcade").SetMastermindSet(Set.XMen).LeadsVillain("Murderworld", Set.XMen).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Arcade").SetMastermindSet(Set.XMen).LeadsVillain("Murderworld", Set.XMen).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Dark Phoenix").SetMastermindSet(Set.XMen).LeadsVillain("Hellfire Club", Set.XMen).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dark Phoenix").SetMastermindSet(Set.XMen).LeadsVillain("Hellfire Club", Set.XMen).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Deathbird").SetMastermindSet(Set.XMen).LeadsVillain("Shi'ar Imperial Guard", Set.XMen).LeadsHenchmen(new List<Henchmen> { Henchmen.GetNewHenchmen("Shi'ar Death Commandos", Set.XMen), Henchmen.GetNewHenchmen("Shi'ar Patrol Craft", Set.XMen)}).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Deathbird").SetMastermindSet(Set.XMen).LeadsVillain("Shi'ar Imperial Guard", Set.XMen).LeadsHenchmen(new List<Henchmen> { Henchmen.GetNewHenchmen("Shi'ar Death Commandos", Set.XMen), Henchmen.GetNewHenchmen("Shi'ar Patrol Craft", Set.XMen)}).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Mojo").SetMastermindSet(Set.XMen).LeadsVillain("Mojoverse", Set.XMen).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mojo").SetMastermindSet(Set.XMen).LeadsVillain("Mojoverse", Set.XMen).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Onslaught").SetMastermindSet(Set.XMen).LeadsVillain("Dark Descendants", Set.XMen).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Onslaught").SetMastermindSet(Set.XMen).LeadsVillain("Dark Descendants", Set.XMen).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Shadow King").SetMastermindSet(Set.XMen).LeadsVillain("Shadow-X", Set.XMen).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Shadow King").SetMastermindSet(Set.XMen).LeadsVillain("Shadow-X", Set.XMen).IncludeHorrors().Build(),

            new MastermindInfoBuilder().SetMastermindName("Adrian Toomes").SetMastermindSet(Set.Sm).LeadsVillain("Salvagers", Set.Sm).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Adrian Toomes").SetMastermindSet(Set.Sm).LeadsVillain("Salvagers", Set.Sm).Build(),
            new MastermindInfoBuilder().SetMastermindName("Vulture").SetMastermindSet(Set.Sm).LeadsVillain("Vulture Tech", Set.Sm).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Vulture").SetMastermindSet(Set.Sm).LeadsVillain("Vulture Tech", Set.Sm).Build(),

            new MastermindInfoBuilder().SetMastermindName("Fin Fang Foom").SetMastermindSet(Set.Champions).LeadsVillain("Monsters Unleashed", Set.Champions).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Fin Fang Foom").SetMastermindSet(Set.Champions).LeadsVillain("Monsters Unleashed", Set.Champions).Build(),
            new MastermindInfoBuilder().SetMastermindName("Pagliacci").SetMastermindSet(Set.Champions).LeadsVillain("Wrecking Crew", Set.Champions).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Pagliacci").SetMastermindSet(Set.Champions).LeadsVillain("Wrecking Crew", Set.Champions).Build(),

            new MastermindInfoBuilder().SetMastermindName("General Ross").SetMastermindSet(Set.Wwh).LeadsVillain("Code Red", Set.Wwh).Build(),
            new MastermindInfoBuilder().SetMastermindName("Illuminati, Secret Society").SetMastermindSet(Set.Wwh).LeadsVillain("Illuminati", Set.Wwh).Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hulk, Sakaarson").SetMastermindSet(Set.Wwh).LeadsVillain("Warbound", Set.Wwh).Build(),
            new MastermindInfoBuilder().SetMastermindName("M.O.D.O.K.").SetMastermindSet(Set.Wwh).LeadsVillain("Intelligencia", Set.Wwh).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Red King").SetMastermindSet(Set.Wwh).LeadsVillain("Sakaar Imperial Guard", Set.Wwh).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Sentry").SetMastermindSet(Set.Wwh).LeadsVillain("Aspects of the Void", Set.Wwh).Build(),

            new MastermindInfoBuilder().SetMastermindName("Iron Monger").SetMastermindSet(Set.P1).LeadsVillain("Iron Foes", Set.P1).Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").SetMastermindSet(Set.P1).LeadsVillain("Enemies of Asgard", Set.P1).Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull").SetMastermindSet(Set.P1).LeadsVillain("HYDRA", Set.P1).Build(),

            new MastermindInfoBuilder().SetMastermindName("Morgan Le Fay").SetMastermindSet(Set.Antman).LeadsVillain("Queen's Vengeance", Set.Antman).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Morgan Le Fay").SetMastermindSet(Set.Antman).LeadsVillain("Queen's Vengeance", Set.Antman).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron").SetMastermindSet(Set.Antman).LeadsVillain("Ultron's Legacy", Set.Antman).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron").SetMastermindSet(Set.Antman).LeadsVillain("Ultron's Legacy", Set.Antman).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hybrid").SetMastermindSet(Set.Venom).LeadsVillain("Life Foundation", Set.Venom).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hybrid").SetMastermindSet(Set.Venom).LeadsVillain("Life Foundation", Set.Venom).Build(),
            new MastermindInfoBuilder().SetMastermindName("Poison Thanos").SetMastermindSet(Set.Venom).LeadsVillain("Poisons", Set.Venom).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Poison Thanos").SetMastermindSet(Set.Venom).LeadsVillain("Poisons", Set.Venom).Build(),

            new MastermindInfoBuilder().SetMastermindName("J. Jonah Jameson").SetMastermindSet(Set.Dimensions).LeadsHenchmen("Spider-Slayer", Set.Dimensions).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic J. Jonah Jameson").SetMastermindSet(Set.Dimensions).LeadsHenchmen("Spider-Slayer", Set.Dimensions).Build(),

            new MastermindInfoBuilder().SetMastermindName("Grim Reaper").SetMastermindSet(Set.Revelations).LeadsVillain("Lethal Legion", Set.Revelations).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grim Reaper").SetMastermindSet(Set.Revelations).LeadsVillain("Lethal Legion", Set.Revelations).Build(),
            new MastermindInfoBuilder().SetMastermindName("The Hood").SetMastermindSet(Set.Revelations).LeadsVillain("Hood's Gang", Set.Revelations).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Hood").SetMastermindSet(Set.Revelations).LeadsVillain("Hood's Gang", Set.Revelations).Build(),
            new MastermindInfoBuilder().SetMastermindName("Mandarin").SetMastermindSet(Set.Revelations).LeadsHenchmen("Mandarin's Rings", Set.Revelations).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mandarin").SetMastermindSet(Set.Revelations).LeadsHenchmen("Mandarin's Rings", Set.Revelations).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hydra High Council").SetMastermindSet(Set.Shield).LeadsVillain("Hydra Elite", Set.Shield).Build(),
            new MastermindInfoBuilder().SetMastermindName("Hydra Super-Adaptoid").SetMastermindSet(Set.Shield).LeadsVillain("A.I.M., Hydra Offshoot", Set.Shield).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hela").SetMastermindSet(Set.Asgard).LeadsVillain("Omens of Ragnarok", Set.Asgard).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hela").SetMastermindSet(Set.Asgard).LeadsVillain("Omens of Ragnarok", Set.Asgard).Build(),
            new MastermindInfoBuilder().SetMastermindName("Malekith").SetMastermindSet(Set.Asgard).LeadsVillain("Dark Council", Set.Asgard).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Malekith").SetMastermindSet(Set.Asgard).LeadsVillain("Dark Council", Set.Asgard).Build(),

            new MastermindInfoBuilder().SetMastermindName("Belasco, Demon Lord of Limbo").SetMastermindSet(Set.NewMutants).LeadsVillain("Demons of Limbo", Set.NewMutants).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Belasco, Demon Lord of Limbo").SetMastermindSet(Set.NewMutants).LeadsVillain("Demons of Limbo", Set.NewMutants).Build(),
            new MastermindInfoBuilder().SetMastermindName("Emma Frost, The White Queen").SetMastermindSet(Set.NewMutants).LeadsVillain("Hellions", Set.NewMutants).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emma Frost, The White Queen").SetMastermindSet(Set.NewMutants).LeadsVillain("Hellions", Set.NewMutants).Build(),

            new MastermindInfoBuilder().SetMastermindName("The Beyonder").SetMastermindSet(Set.Cosmos).LeadsVillain("From Beyond", Set.Cosmos).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Beyonder").SetMastermindSet(Set.Cosmos).LeadsVillain("From Beyond", Set.Cosmos).Build(),
            new MastermindInfoBuilder().SetMastermindName("Grandmaster").SetMastermindSet(Set.Cosmos).LeadsVillain("Elders of the Universe", Set.Cosmos).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grandmaster").SetMastermindSet(Set.Cosmos).LeadsVillain("Elders of the Universe", Set.Cosmos).Build(),
            new MastermindInfoBuilder().SetMastermindName("Magus").SetMastermindSet(Set.Cosmos).LeadsHenchmen("Universal Church of Truth", Set.Cosmos).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Magus").SetMastermindSet(Set.Cosmos).LeadsHenchmen("Universal Church of Truth", Set.Cosmos).Build(),

            new MastermindInfoBuilder().SetMastermindName("Emperor Vulcan of the Shi'ar").SetMastermindSet(Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite", Set.Inhumans).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emperor Vulcan").SetMastermindSet(Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite", Set.Inhumans).Build(),
            new MastermindInfoBuilder().SetMastermindName("Maximus the Mad").SetMastermindSet(Set.Inhumans).LeadsVillain("Inhuman Rebellion", Set.Inhumans).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Maximus the Mad").SetMastermindSet(Set.Inhumans).LeadsVillain("Inhuman Rebellion", Set.Inhumans).Build(),

            new MastermindInfoBuilder().SetMastermindName("Annihilus").SetMastermindSet(Set.Annihilation).LeadsVillain("Annihilation Wave", Set.Annihilation).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Annihilus").SetMastermindSet(Set.Annihilation).LeadsVillain("Annihilation Wave", Set.Annihilation).Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang the Conqueror").SetMastermindSet(Set.Annihilation).LeadsVillain("Timelines of Kang", Set.Annihilation).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Kang the Conqueror").SetMastermindSet(Set.Annihilation).LeadsVillain("Timelines of Kang", Set.Annihilation).Build(),

            new MastermindInfoBuilder().SetMastermindName("Bastion, Fused Sentinel").SetMastermindSet(Set.Messiah).LeadsVillain("Purifiers", Set.Messiah).LeadsHenchmenByKind(new List<string> {"Sentinel" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Bastion, Fused Sentinel").SetMastermindSet(Set.Messiah).LeadsVillain("Purifiers", Set.Messiah).LeadsHenchmenByKind(new List<string> {"Sentinel" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Exodus").SetMastermindSet(Set.Messiah).LeadsVillain("Acolytes", Set.Messiah).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Exodus").SetMastermindSet(Set.Messiah).LeadsVillain("Acolytes", Set.Messiah).Build(),
            new MastermindInfoBuilder().SetMastermindName("Lady Deathstrike").SetMastermindSet(Set.Messiah).LeadsVillain("Reavers", Set.Messiah).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Lady Deathstrike").SetMastermindSet(Set.Messiah).LeadsVillain("Reavers", Set.Messiah).Build(),

            new MastermindInfoBuilder().SetMastermindName("Dormammu").SetMastermindSet(Set.Strange).LeadsVillain("Lords of the Netherworld", Set.Strange).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dormammu").SetMastermindSet(Set.Strange).LeadsVillain("Lords of the Netherworld", Set.Strange).Build(),
            new MastermindInfoBuilder().SetMastermindName("Nightmare").SetMastermindSet(Set.Strange).LeadsVillain("Fear Lords", Set.Strange).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Nightmare").SetMastermindSet(Set.Strange).LeadsVillain("Fear Lords", Set.Strange).Build(),

            new MastermindInfoBuilder().SetMastermindName("Ego, the Living Planet").SetMastermindSet(Set.Guardians).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ego, the Living Planet").SetMastermindSet(Set.Guardians).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ronan the Accuser").SetMastermindSet(Set.Guardians).LeadsVillain("Followers of Ronan", Set.Guardians).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ronan the Accuser").SetMastermindSet(Set.Guardians).LeadsVillain("Followers of Ronan", Set.Guardians).Build(),

            new MastermindInfoBuilder().SetMastermindName("Killmonger").SetMastermindSet(Set.BlackPanther).LeadsVillain("Killmonger's League", Set.BlackPanther).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Killmonger").SetMastermindSet(Set.BlackPanther).LeadsVillain("Killmonger's League", Set.BlackPanther).Build(),
            new MastermindInfoBuilder().SetMastermindName("Klaw").SetMastermindSet(Set.BlackPanther).LeadsVillain("Enemies of Wakanda", Set.BlackPanther).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Klaw").SetMastermindSet(Set.BlackPanther).LeadsVillain("Enemies of Wakanda", Set.BlackPanther).Build(),

            new MastermindInfoBuilder().SetMastermindName("Indestructible Man").SetMastermindSet(Set.BlackWidow).LeadsVillain("Elite Assassins", Set.BlackWidow).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Indestructible Man").SetMastermindSet(Set.BlackWidow).LeadsVillain("Elite Assassins", Set.BlackWidow).Build(),
            new MastermindInfoBuilder().SetMastermindName("Taskmaster").SetMastermindSet(Set.BlackWidow).LeadsVillain("Taskmaster's Thunderbolts", Set.BlackWidow).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Taskmaster").SetMastermindSet(Set.BlackWidow).LeadsVillain("Taskmaster's Thunderbolts", Set.BlackWidow).Build(),

            new MastermindInfoBuilder().SetMastermindName("Ebony Maw").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Children of Thanos", Set.InfinitySaga).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ebony Maw").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Children of Thanos", Set.InfinitySaga).Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos (Infinity Saga)").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Infinity Stones", Set.InfinitySaga).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Thanos (Infinity Saga)").SetMastermindSet(Set.InfinitySaga).LeadsVillain("Infinity Stones", Set.InfinitySaga).Build(),

            new MastermindInfoBuilder().SetMastermindName("Lilith, Mother of Demons").SetMastermindSet(Set.MidnightSons).LeadsVillain("Lilin", Set.MidnightSons).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Lilith, Mother of Demons").SetMastermindSet(Set.MidnightSons).LeadsVillain("Lilin", Set.MidnightSons).Build(),
            new MastermindInfoBuilder().SetMastermindName("Zarathos").SetMastermindSet(Set.MidnightSons).LeadsVillain("The Fallen", Set.MidnightSons).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Zarathos").SetMastermindSet(Set.MidnightSons).LeadsVillain("The Fallen", Set.MidnightSons).Build(),

            new MastermindInfoBuilder().SetMastermindName("Hank Pym, Yellowjacket").SetMastermindSet(Set.WhatIf).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hank Pym, Yellowjacket").SetMastermindSet(Set.WhatIf).Build(),
            new MastermindInfoBuilder().SetMastermindName("Killmonger, The Betrayer").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Vibranium Liberator Drones", Set.WhatIf).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Killmonger, The Betrayer").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Vibranium Liberator Drones", Set.WhatIf).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron Infinity").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Ultron Sentries", Set.WhatIf).AlwaysLeadsOnSolo().Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron Infinity").SetMastermindSet(Set.WhatIf).LeadsHenchmen("Ultron Sentries", Set.WhatIf).AlwaysLeadsOnSolo().Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Scarlet Witch").SetMastermindSet(Set.WhatIf).LeadsVillain("Zombie Avengers", Set.WhatIf).SetZombieSoloVillains().Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Zombie Scarlet Witch").SetMastermindSet(Set.WhatIf).LeadsVillain("Zombie Avengers", Set.WhatIf).SetZombieSoloVillains().Build(),

            new MastermindInfoBuilder().SetMastermindName("Darrin Cross").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Cross Technologies", Set.AntmanWasp).Build(),
            new MastermindInfoBuilder().SetMastermindName("Ghost, Master Thief").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Ghost Chasers", Set.AntmanWasp).Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang, Quantum Conqueror").SetMastermindSet(Set.AntmanWasp).LeadsVillain("Armada of Kang", Set.AntmanWasp).Build(),

            new MastermindInfoBuilder().SetMastermindName("Alchemax Executives").SetMastermindSet(Set.TwentyNintyNine).LeadsVillain("Alchemax Enforcers", Set.TwentyNintyNine).IncludeExtraHero().Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Alchemax Executives").SetMastermindSet(Set.TwentyNintyNine).LeadsVillain("Alchemax Enforcers", Set.TwentyNintyNine).IncludeExtraHero().Build(),
            new MastermindInfoBuilder().SetMastermindName("Sinister Six 2099").SetMastermindSet(Set.TwentyNintyNine).LeadsVillainsByKind(new List<string> {"Alchemax", "Sinister" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Sinister Six 2099").SetMastermindSet(Set.TwentyNintyNine).LeadsVillainsByKind(new List<string> {"Alchemax", "Sinister" }).Build(),

            new MastermindInfoBuilder().SetMastermindName("Omega Red").SetMastermindSet(Set.WeaponX).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Omega Red").SetMastermindSet(Set.WeaponX).Build(),
            new MastermindInfoBuilder().SetMastermindName("Romulus").SetMastermindSet(Set.WeaponX).LeadsVillain("Weapon Plus", Set.WeaponX).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Romulus").SetMastermindSet(Set.WeaponX).LeadsVillain("Weapon Plus", Set.WeaponX).Build(),

            new MastermindInfoBuilder().SetMastermindName("Doctor Doom").SetMastermindSet(Set.Core2E).LeadsHenchmen("Doombot Legion", Set.Core2E).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Doctor Doom").SetMastermindSet(Set.Core2E).LeadsHenchmen("Doombot Legion", Set.Core2E).Build(),
            new MastermindInfoBuilder().SetMastermindName("Doctor Octopus").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> { "Sinister" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Doctor Octopus").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> { "Sinister" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").SetMastermindSet(Set.Core2E).LeadsVillain("Enemies of Asgard", Set.Core2E).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Loki").SetMastermindSet(Set.Core2E).LeadsVillain("Enemies of Asgard", Set.Core2E).Build(),
            new MastermindInfoBuilder().SetMastermindName("Magneto").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Brotherhood", "X-Men" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Magneto").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Brotherhood", "X-Men" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull, HYDRA Overlord").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Hydra" }).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Red Skull, HYDRA Overlord").SetMastermindSet(Set.Core2E).LeadsVillainsByKind(new List<string> {"Hydra" }).Build(),
        };

        public static IReadOnlyList<MastermindInfo> All => _masterminds;
    }

    public class Mastermind
    {
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
                MastermindName = mastermindInfo.MastermindName,
                SetName = mastermindInfo.SetName,
                LeadsHenchmen = mastermindInfo.LeadsHenchmen,
                LeadsVillain = mastermindInfo.LeadsVillain,
                DoesLeadHenchmen = mastermindInfo.DoesLeadHenchmen,
                DoesLeadVillain = mastermindInfo.DoesLeadVillain,
                MastermindInfo = mastermindInfo
            };

        }

        public static Mastermind GetNewMastermind(string mastermindName = "", Set ?mastermindSet = null)
        {
            var mastermindInfo = new MastermindInfo();

            if(!string.IsNullOrEmpty(mastermindName) && mastermindSet != null)
            {
                mastermindInfo = MastermindRepository.All.FirstOrDefault(m => m.MastermindName == mastermindName && m.SetName == mastermindSet);
            }
            if(string.IsNullOrEmpty(mastermindName) && mastermindSet == null)
            {
                mastermindInfo = MastermindRepository.All[RandomHelper.Instance.Next(MastermindRepository.All.Count)];
            }

            return ConvertToMastermind(mastermindInfo);
        }

        public List<MastermindInfo> GetMasterminds(List<string> masterminds)
        {
            var returnList = new List<MastermindInfo>();
            foreach (var mastermind in masterminds)
            {
                if (MastermindRepository.All.Any(x => x.MastermindName == mastermind))
                {
                    returnList.Add(MastermindRepository.All.First(x => x.MastermindName == mastermind));
                }
            }

            return returnList;
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

        public List<MastermindInfo> ModifyMastermindList(List<string> mastermindExclusions)
        {
            var returnList = MastermindRepository.All.ToList();

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

        public static List<string> GetListOfMasterminds()
        {
            var allMasterminds = MastermindRepository.All.Select(m => m.MastermindName).ToList();
            return allMasterminds;
        }

        public static string GetRandomMastermind()
        {
            var allMasterminds = GetListOfMasterminds();
            var mastermind = allMasterminds[RandomHelper.Instance.Next(allMasterminds.Count)];
            return mastermind;
        }

        private static List<Mastermind> ConvertToMastermindList(List<MastermindInfo> mastermindInfoList)
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
            var remainingMasterminds = mastermindList.Except(mastermindsInGame).ToList();

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
            remainingMasterminds = remainingMasterminds.Except(mastermindsByScheme).ToList();

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
                remainingMasterminds = remainingMasterminds.Except(mastermindByMastermind).ToList();
            }

            return remainingMasterminds[RandomHelper.Instance.Next(remainingMasterminds.Count)];
        }
    }
}
