using System;
using System.Collections.Generic;
using System.Linq;

namespace MarvelLegendary
{
    public class Mastermind
    {
        public string MastermindName { get; set; }
        public GameInfo.Set SetName { get; set; }
        public string LeadsHenchmen { get; set; }
        public string LeadsVillain { get; set; }
        public bool DoesLeadHenchmen { get; set; }
        public bool DoesLeadVillain { get; set; }
        public MastermindInfo MastermindInfo { get; set; }
        public bool IncludeHorrors { get; set; }

        private readonly List<MastermindInfo> _masterminds = new List<MastermindInfo>()
        {
            new MastermindInfoBuilder().SetMastermindName("Dr. Doom").LeadsHenchmen("Doombot Legion").Build(),
            new MastermindInfoBuilder().SetMastermindName("Loki").LeadsVillain("Enemies of Asgard").Build(),
            new MastermindInfoBuilder().SetMastermindName("Magneto").LeadsVillain("Brotherhood").Build(),
            new MastermindInfoBuilder().SetMastermindName("Red Skull").LeadsVillain("HYDRA").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Apocalypse").SetMastermindSet(GameInfo.Set.Dc).LeadsVillain("Four Horsemen").Build(),
            new MastermindInfoBuilder().SetMastermindName("Kingpin").SetMastermindSet(GameInfo.Set.Dc).LeadsVillain("Streets of New York").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mephisto").SetMastermindSet(GameInfo.Set.Dc).LeadsVillain("Underworld").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mr. Sinister").SetMastermindSet(GameInfo.Set.Dc).LeadsVillain("Marauders").Build(),
            new MastermindInfoBuilder().SetMastermindName("Stryfe").SetMastermindSet(GameInfo.Set.Dc).LeadsVillain("MLF").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Galactus").SetMastermindSet(GameInfo.Set.Ff).LeadsVillain("Heralds of Galactus").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mole Man").SetMastermindSet(GameInfo.Set.Ff).LeadsVillain("Subterranea").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Carnage").SetMastermindSet(GameInfo.Set.PttR).LeadsVillain("Maximum Carnage").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mysterio").SetMastermindSet(GameInfo.Set.PttR).LeadsVillain("Sinister Six").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Dr. Strange").SetMastermindSet(GameInfo.Set.Villains).LeadsVillain("Defenders").IncludeBindings().Build(),
            new MastermindInfoBuilder().SetMastermindName("Nick Fury").SetMastermindSet(GameInfo.Set.Villains).LeadsVillain("Avengers").IncludeMadameHydra().Build(),
            new MastermindInfoBuilder().SetMastermindName("Odin").SetMastermindSet(GameInfo.Set.Villains).LeadsHenchmen("Asgardian Warriors").IncludeBindings().Build(),
            new MastermindInfoBuilder().SetMastermindName("Professor X").SetMastermindSet(GameInfo.Set.Villains).LeadsVillain("X-Men First Class").IncludeBindings().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Supreme Intelligence Of The Kree").SetMastermindSet(GameInfo.Set.GotG).LeadsVillain("Kree Starforce").Build(),
            new MastermindInfoBuilder().SetMastermindName("Thanos").SetMastermindSet(GameInfo.Set.GotG).LeadsVillain("Infinity Gems").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Uru-Enchanted Iron Man").SetMastermindSet(GameInfo.Set.Fi).LeadsVillain("The Mighty").IncludeBindings().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Madelyne Pryor, Goblin Queen").SetMastermindSet(GameInfo.Set.Sw1).LeadsVillain("Limbo").Build(),
            new MastermindInfoBuilder().SetMastermindName("Nimrod, Super Sentinel").SetMastermindSet(GameInfo.Set.Sw1).LeadsVillain("Sentinel Territories").Build(),
            new MastermindInfoBuilder().SetMastermindName("Wasteland Hulk").SetMastermindSet(GameInfo.Set.Sw1).LeadsVillain("Wasteland").Build(),
            new MastermindInfoBuilder().SetMastermindName("Zombie Green Goblin").SetMastermindSet(GameInfo.Set.Sw1).LeadsVillain("The Deadlands").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Immortal Emperor Zheng-Zhu").SetMastermindSet(GameInfo.Set.Sw2).LeadsVillain("K'un-Lun").Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hyperion").SetMastermindSet(GameInfo.Set.Sw2).LeadsVillain("Utopolis").Build(),
            new MastermindInfoBuilder().SetMastermindName("Shiklah, the Demon Bride").SetMastermindSet(GameInfo.Set.Sw2).LeadsVillain("Monster Metropolis").Build(),
            new MastermindInfoBuilder().SetMastermindName("Spider-Queen").SetMastermindSet(GameInfo.Set.Sw2).LeadsHenchmen("Spider-Infected").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Armin Zola").SetMastermindSet(GameInfo.Set.Ca).LeadsVillain("Zola's Creations").Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Heinrich Zemo").SetMastermindSet(GameInfo.Set.Ca).LeadsVillain("Masters of Evil (WWII)").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Authoritarian Iron Man").SetMastermindSet(GameInfo.Set.Cw).LeadsVillain("Superhuman Registration Act").Build(),
            new MastermindInfoBuilder().SetMastermindName("Baron Helmut Zemo").SetMastermindSet(GameInfo.Set.Cw).LeadsVillain("Thunderbolts").Build(),
            new MastermindInfoBuilder().SetMastermindName("Maria Hill, Director Of S.H.I.E.L.D").SetMastermindSet(GameInfo.Set.Cw).LeadsVillain("S.H.I.E.L.D. Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Misty Knight").SetMastermindSet(GameInfo.Set.Cw).LeadsVillain("Heroes for Hire").Build(),
            new MastermindInfoBuilder().SetMastermindName("Ragnarok").SetMastermindSet(GameInfo.Set.Cw).LeadsVillain("Registration Enforcers").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Evil Deadpool").SetMastermindSet(GameInfo.Set.Deadpool).LeadsVillain("Evil Deadpool Corpse").Build(),
            new MastermindInfoBuilder().SetMastermindName("Macho Gomez").SetMastermindSet(GameInfo.Set.Deadpool).LeadsVillain("Deadpool's \"Friends\"").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Charles Xavier").SetMastermindSet(GameInfo.Set.Noir).LeadsVillain("X-Men Noir").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Goblin, Underworld Boss").SetMastermindSet(GameInfo.Set.Noir).LeadsVillain("Goblin's Freak Show").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Arcade").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Murderworld").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Arcade").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Murderworld").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Dark Phoenix").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Hellfire Club").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Dark Phoenix").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Hellfire Club").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Deathbird").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Shi'ar Imperial Guard").LeadsHenchmen(new List<string> { "Shi'ar Death Commandos", "Shi'ar Patrol Craft"}).Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Deathbird").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Shi'ar Imperial Guard").LeadsHenchmen(new List<string> { "Shi'ar Death Commandos", "Shi'ar Patrol Craft"}).IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Mojo").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Mojoverse").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mojo").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Mojoverse").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Onslaught").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Dark Descendants").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Onslaught").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Dark Descendants").IncludeHorrors().Build(),
            new MastermindInfoBuilder().SetMastermindName("Shadow King").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Shadow-X").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Shadow King").SetMastermindSet(GameInfo.Set.XMen).LeadsVillain("Shadow-X").IncludeHorrors().Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Adrian Toomes").SetMastermindSet(GameInfo.Set.Sm).LeadsVillain("Salvagers").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Adrian Toomes").SetMastermindSet(GameInfo.Set.Sm).LeadsVillain("Salvagers").Build(),
            new MastermindInfoBuilder().SetMastermindName("Vulture").SetMastermindSet(GameInfo.Set.Sm).LeadsVillain("Vulture Tech").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Vulture").SetMastermindSet(GameInfo.Set.Sm).LeadsVillain("Vulture Tech").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Fin Fang Foom").SetMastermindSet(GameInfo.Set.Champions).LeadsVillain("Monsters Unleashed").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Fin Fang Foom").SetMastermindSet(GameInfo.Set.Champions).LeadsVillain("Monsters Unleashed").Build(),
            new MastermindInfoBuilder().SetMastermindName("Pagliacci").SetMastermindSet(GameInfo.Set.Champions).LeadsVillain("Wrecking Crew").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Pagliacci").SetMastermindSet(GameInfo.Set.Champions).LeadsVillain("Wrecking Crew").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("General Ross").SetMastermindSet(GameInfo.Set.Wwh).LeadsVillain("Code Red").Build(),
            new MastermindInfoBuilder().SetMastermindName("Illuminati, Secret Society").SetMastermindSet(GameInfo.Set.Wwh).LeadsVillain("Illuminati").Build(),
            new MastermindInfoBuilder().SetMastermindName("King Hulk, Sakaarson").SetMastermindSet(GameInfo.Set.Wwh).LeadsVillain("Warbound").Build(),
            new MastermindInfoBuilder().SetMastermindName("M.O.D.O.K.").SetMastermindSet(GameInfo.Set.Wwh).LeadsVillain("Intelligencia").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Red King").SetMastermindSet(GameInfo.Set.Wwh).LeadsVillain("Sakaar Imperial Guard").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Sentry").SetMastermindSet(GameInfo.Set.Wwh).LeadsVillain("Aspects of the Void").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Iron Monger").SetMastermindSet(GameInfo.Set.P1).LeadsVillain("Iron Foes").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Morgan Le Fay").SetMastermindSet(GameInfo.Set.Antman).LeadsVillain("Queen's Vengeance").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Morgan Le Fay").SetMastermindSet(GameInfo.Set.Antman).LeadsVillain("Queen's Vengeance").Build(),
            new MastermindInfoBuilder().SetMastermindName("Ultron").SetMastermindSet(GameInfo.Set.Antman).LeadsVillain("Ultron's Legacy").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Ultron").SetMastermindSet(GameInfo.Set.Antman).LeadsVillain("Ultron's Legacy").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Hybrid").SetMastermindSet(GameInfo.Set.Venom).LeadsVillain("Life Foundation").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hybrid").SetMastermindSet(GameInfo.Set.Venom).LeadsVillain("Life Foundation").Build(),
            new MastermindInfoBuilder().SetMastermindName("Poison Thanos").SetMastermindSet(GameInfo.Set.Venom).LeadsVillain("Poisons").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Poison Thanos").SetMastermindSet(GameInfo.Set.Venom).LeadsVillain("Poisons").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("J. Jonah Jameson").SetMastermindSet(GameInfo.Set.Dimensions).LeadsHenchmen("Spider-Slayer").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic J. Jonah Jameson").SetMastermindSet(GameInfo.Set.Dimensions).LeadsHenchmen("Spider-Slayer").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Grim Reaper").SetMastermindSet(GameInfo.Set.Revelations).LeadsVillain("Lethal Legion").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grim Reaper").SetMastermindSet(GameInfo.Set.Revelations).LeadsVillain("Lethal Legion").Build(),
            new MastermindInfoBuilder().SetMastermindName("The Hood").SetMastermindSet(GameInfo.Set.Revelations).LeadsVillain("Hood's Gang").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Hood").SetMastermindSet(GameInfo.Set.Revelations).LeadsVillain("Hood's Gang").Build(),
            new MastermindInfoBuilder().SetMastermindName("Mandarin").SetMastermindSet(GameInfo.Set.Revelations).LeadsHenchmen("Mandarin's Rings").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Mandarin").SetMastermindSet(GameInfo.Set.Revelations).LeadsHenchmen("Mandarin's Rings").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Hydra High Council").SetMastermindSet(GameInfo.Set.Shield).LeadsVillain("Hydra Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Hydra Sper-Adaptoid").SetMastermindSet(GameInfo.Set.Shield).LeadsHenchmen("A.I.M., Hydra Offshoot").Build(),
            
            new MastermindInfoBuilder().SetMastermindName("Hela").SetMastermindSet(GameInfo.Set.Asgard).LeadsVillain("Omens of Ragnarok").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Hela").SetMastermindSet(GameInfo.Set.Asgard).LeadsVillain("Omens of Ragnarok").Build(),
            new MastermindInfoBuilder().SetMastermindName("Malekith").SetMastermindSet(GameInfo.Set.Asgard).LeadsVillain("Dark Council").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Malekith").SetMastermindSet(GameInfo.Set.Asgard).LeadsVillain("Dark Council").Build(),

            new MastermindInfoBuilder().SetMastermindName("Belasco, Demon Lord of Limbo").SetMastermindSet(GameInfo.Set.NewMutants).LeadsVillain("Demons of Limbo").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Belasco, Demon Lord of Limbo").SetMastermindSet(GameInfo.Set.NewMutants).LeadsVillain("Demons of Limbo").Build(),
            new MastermindInfoBuilder().SetMastermindName("Emma Frost, The White Queen").SetMastermindSet(GameInfo.Set.NewMutants).LeadsVillain("Hellions").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emma Frost, The White Queen").SetMastermindSet(GameInfo.Set.NewMutants).LeadsVillain("Hellions").Build(),

            new MastermindInfoBuilder().SetMastermindName("The Beyonder").SetMastermindSet(GameInfo.Set.Cosmos).LeadsVillain("From Beyond").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic The Beyonder").SetMastermindSet(GameInfo.Set.Cosmos).LeadsVillain("From Beyond").Build(),
            new MastermindInfoBuilder().SetMastermindName("Grandmaster").SetMastermindSet(GameInfo.Set.Cosmos).LeadsVillain("Elders of the Universe").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Grandmaster").SetMastermindSet(GameInfo.Set.Cosmos).LeadsVillain("Elders of the Universe").Build(),
            new MastermindInfoBuilder().SetMastermindName("Magus").SetMastermindSet(GameInfo.Set.Cosmos).LeadsHenchmen("Universal Church of Truth").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Magus").SetMastermindSet(GameInfo.Set.Cosmos).LeadsHenchmen("Universal Church of Truth").Build(),

            new MastermindInfoBuilder().SetMastermindName("Emperor Vulcan of the Shi'ar").SetMastermindSet(GameInfo.Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Emperor Vulcan").SetMastermindSet(GameInfo.Set.Inhumans).LeadsVillain("Shi'ar Imperial Elite").Build(),
            new MastermindInfoBuilder().SetMastermindName("Maximus the Mad").SetMastermindSet(GameInfo.Set.Inhumans).LeadsVillain("Inhuman Rebellion").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Maximus the Mad").SetMastermindSet(GameInfo.Set.Inhumans).LeadsVillain("Inhuman Rebellion").Build(),

            new MastermindInfoBuilder().SetMastermindName("Annihilus").SetMastermindSet(GameInfo.Set.Annihilation).LeadsVillain("Annihilation Wave").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Annihilus").SetMastermindSet(GameInfo.Set.Annihilation).LeadsVillain("Annihilation Wave").Build(),
            new MastermindInfoBuilder().SetMastermindName("Kang the Conqueror").SetMastermindSet(GameInfo.Set.Annihilation).LeadsVillain("Timelines of Kang").Build(),
            new MastermindInfoBuilder().SetMastermindName("Epic Kang the Conqueror").SetMastermindSet(GameInfo.Set.Annihilation).LeadsVillain("Timelines of Kang").Build()
        };

        public Mastermind()
        {
            var mastermindInfo = _masterminds[new Random().Next(_masterminds.Count)];
            
            MastermindName = mastermindInfo.MastermindName;
            SetName = mastermindInfo.SetName;
            LeadsHenchmen = mastermindInfo.LeadsHenchmen;
            LeadsVillain = mastermindInfo.LeadsVillain;
            DoesLeadHenchmen = mastermindInfo.DoesLeadHenchmen;
            DoesLeadVillain = mastermindInfo.DoesLeadVillain;
            MastermindInfo = mastermindInfo;
        }

        public Mastermind(List<MastermindInfo> masterminds)
        {
            var mastermindInfo = masterminds[new Random().Next(masterminds.Count)];
            //mastermindInfo = masterminds.First(x => x.MastermindName == "Loki");

            MastermindName = mastermindInfo.MastermindName;
            SetName = mastermindInfo.SetName;
            LeadsHenchmen = mastermindInfo.LeadsHenchmen;
            LeadsVillain = mastermindInfo.LeadsVillain;
            DoesLeadHenchmen = mastermindInfo.DoesLeadHenchmen;
            DoesLeadVillain = mastermindInfo.DoesLeadVillain;
            MastermindInfo = mastermindInfo;
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
            return _masterminds.ToList().Select(x => x.MastermindName).ToList();
        }
    }
}
