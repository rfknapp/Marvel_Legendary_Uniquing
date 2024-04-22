using MarvelLegendary.Exclusions;
using System;
using System.Collections.Generic;
using System.Linq;
using MarvelLegendary.Enums;

namespace MarvelLegendary
{
    public class VillainInfo
    {
        public string VillainName { get; set; }
        public Set VillainSetName { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeBindings { get; set; }
        public List<Keywords> KeywordsList { get; set; }

        public VillainInfo(string name, Set set, bool includeBindings = false)
        {
            VillainName = name;
            VillainSetName = set;
            IsDuplicate = false;
            DuplicateName = "";
            IncludeBindings = includeBindings;
            KeywordsList = new List<Keywords>();
        }

        public VillainInfo SetKeywords(List<Keywords> keywords)
        {
            KeywordsList = keywords;
            return this;
        }
    }

    public class Villain
    {
        public string VillainName { get; set; }
        public Set SetName { get; set; }
        public VillainInfo VillainInfo { get; set; }
        private Random random;

        private readonly List<VillainInfo> _villains = new List<VillainInfo>()
        {
            new VillainInfo("Brotherhood", Set.Core),
            new VillainInfo("Enemies of Asgard", Set.Core),
            new VillainInfo("HYDRA", Set.Core),
            new VillainInfo("Masters of Evil", Set.Core),
            new VillainInfo("Radiation", Set.Core),
            new VillainInfo("Skrulls", Set.Core),
            new VillainInfo("Spider-Foes", Set.Core),
            
            new VillainInfo("Emissaries of Evil", Set.Dc),
            new VillainInfo("Four Horsemen", Set.Dc),
            new VillainInfo("Marauders", Set.Dc),
            new VillainInfo("MLF", Set.Dc),
            new VillainInfo("Streets of New York", Set.Dc),
            new VillainInfo("Underworld", Set.Dc),
            
            new VillainInfo("Heralds of Galactus", Set.Ff),
            new VillainInfo("Subterranea", Set.Ff),
            
            new VillainInfo("Maximum Carnage", Set.PttR),
            new VillainInfo("Sinister Six", Set.PttR),
            
            new VillainInfo("Avengers", Set.Villains, true),
            new VillainInfo("Defenders", Set.Villains, true),
            new VillainInfo("Marvel Knights", Set.Villains, true),
            new VillainInfo("Spider Friends", Set.Villains, true),
            new VillainInfo("Uncanny Avengers", Set.Villains),
            new VillainInfo("Uncanny X-Men", Set.Villains, true),
            new VillainInfo("X-Men First Class", Set.Villains),
            
            new VillainInfo("Infinity Gems", Set.GotG),
            new VillainInfo("Kree Starforce", Set.GotG),
            
            new VillainInfo("The Mighty", Set.Fi, true),
            
            new VillainInfo("The Deadlands", Set.Sw1).SetKeywords(new List<Keywords>{ Keywords.LivingDead}),
            new VillainInfo("Domain of Apocalypse", Set.Sw1),
            new VillainInfo("Limbo", Set.Sw1),
            new VillainInfo("Manhattan (Earth-1610)", Set.Sw1),
            new VillainInfo("Sentinel Territories", Set.Sw1),
            new VillainInfo("Wasteland", Set.Sw1),
            
            new VillainInfo("Deadpool's Secret Secret Wars", Set.Sw2),
            new VillainInfo("Guardians of Knowhere", Set.Sw2),
            new VillainInfo("K'un-Lun", Set.Sw2),
            new VillainInfo("Monster Metropolis", Set.Sw2),
            new VillainInfo("Utopolis", Set.Sw2),
            new VillainInfo("X-Men '92", Set.Sw2),
            
            new VillainInfo("Masters of Evil (WWII)", Set.Ca),
            new VillainInfo("Zola's Creations", Set.Ca),
            
            new VillainInfo("CSA Special Marshals", Set.Cw),
            new VillainInfo("Great Lake Avengers", Set.Cw),
            new VillainInfo("Heroes for Hire", Set.Cw),
            new VillainInfo("Registration Enforcers", Set.Cw),
            new VillainInfo("S.H.I.E.L.D. Elite", Set.Cw),
            new VillainInfo("Superhuman Registration Act", Set.Cw),
            new VillainInfo("Thunderbolts", Set.Cw),
            
            new VillainInfo("Deadpool's \"Friends\"", Set.Deadpool),
            new VillainInfo("Evil Deadpool Corpse", Set.Deadpool),
            
            new VillainInfo("Goblin's Freak Show", Set.Noir),
            new VillainInfo("X-Men Noir", Set.Noir),
            
            new VillainInfo("Dark Descendants", Set.XMen),
            new VillainInfo("Hellfire Club", Set.XMen),
            new VillainInfo("Mojoverse", Set.XMen),
            new VillainInfo("Murderworld", Set.XMen),
            new VillainInfo("Shadow-X", Set.XMen),
            new VillainInfo("Shi'ar Imperial Guard", Set.XMen),
            new VillainInfo("Sisterhood of Mutants", Set.XMen),
            
            new VillainInfo("Salvagers", Set.Sm),
            new VillainInfo("Vulture Tech", Set.Sm),
            
            new VillainInfo("Monsters Unleashed", Set.Champions),
            new VillainInfo("Wrecking Crew", Set.Champions),
            
            new VillainInfo("Aspects of the Void", Set.Wwh),
            new VillainInfo("Code Red", Set.Wwh),
            new VillainInfo("Illuminati", Set.Wwh),
            new VillainInfo("Intelligencia", Set.Wwh),
            new VillainInfo("Sakaar Imperial Guard", Set.Wwh),
            new VillainInfo("U-Foes", Set.Wwh),
            new VillainInfo("Warbound", Set.Wwh),
            
            new VillainInfo("Chitauri", Set.P1),
            new VillainInfo("Gamma Hunters", Set.P1),
            new VillainInfo("Iron Foes", Set.P1),
            
            new VillainInfo("Queen's Vengeance", Set.Antman),
            new VillainInfo("Ultron's Legacy", Set.Antman),
            
            new VillainInfo("Life Foundation", Set.Venom),
            new VillainInfo("Poisons", Set.Venom),
            
            new VillainInfo("Army of Evil", Set.Revelations),
            new VillainInfo("Dark Avengers", Set.Revelations),
            new VillainInfo("Hood's Gang", Set.Revelations),
            new VillainInfo("Lethal Legion", Set.Revelations),
            
            new VillainInfo("A.I.M., Hydra Offshoot", Set.Shield),
            new VillainInfo("Hydra Elite", Set.Shield),
            
            new VillainInfo("Dark Council", Set.Asgard),
            new VillainInfo("Omens of Ragnarok", Set.Asgard),
            
            new VillainInfo("Demons of Limbo", Set.NewMutants),
            new VillainInfo("Hellions", Set.NewMutants),

            new VillainInfo("Black Order of Thanos", Set.Cosmos),
            new VillainInfo("Celestials", Set.Cosmos),
            new VillainInfo("From Beyond", Set.Cosmos),
            new VillainInfo("Elders of the Universe", Set.Cosmos),

            new VillainInfo("Shi'ar Imperial Elite", Set.Inhumans),
            new VillainInfo("Inhuman Rebellion", Set.Inhumans),

            new VillainInfo("Annihilation Wave", Set.Annihilation),
            new VillainInfo("Timelines of Kang", Set.Annihilation),

            new VillainInfo("Acolytes", Set.Messiah),
            new VillainInfo("Clan Yashida", Set.Messiah),
            new VillainInfo("Purifiers", Set.Messiah),
            new VillainInfo("Reavers", Set.Messiah),

            new VillainInfo("Fear Lords", Set.Strange),
            new VillainInfo("Lords of the Nether Realm", Set.Strange),

            new VillainInfo("Followers of Ronan", Set.Guardians),
            new VillainInfo("Ravagers", Set.Guardians),

            new VillainInfo("Enemies of Wakanda", Set.BlackPanther),
            new VillainInfo("Killmonger's League", Set.BlackPanther),

            new VillainInfo("Elite Assassins", Set.BlackWidow),
            new VillainInfo("Taskmaster's Thunderbolts", Set.BlackWidow),

            new VillainInfo("Children of Thanos", Set.InfinitySaga),
            new VillainInfo("Infinity Stones", Set.InfinitySaga),

            new VillainInfo("The Fallen", Set.MidnightSons),
            new VillainInfo("Lilin", Set.MidnightSons),

            new VillainInfo("Black Order Guards", Set.WhatIf),
            new VillainInfo("Intergalactic Party Animals", Set.WhatIf),
            new VillainInfo("Rival Overlords", Set.WhatIf),
            new VillainInfo("Strange's Demons", Set.WhatIf),
            new VillainInfo("Zombie Avengers", Set.WhatIf).SetKeywords(new List<Keywords>{ Keywords.LivingDead, Keywords.Rampage}),

            new VillainInfo("Armada of Kang", Set.AntmanWasp),
            new VillainInfo("Cross Technologies", Set.AntmanWasp),
            new VillainInfo("Ghost Chasers", Set.AntmanWasp),
            new VillainInfo("Quantum Realm", Set.AntmanWasp),

            new VillainInfo("Alchemax Enforcers", Set.TwentyNintyNine),
            new VillainInfo("False Aesir of Alchemax", Set.TwentyNintyNine),
        };

        public Villain()
        {
            random = new Random();
        }

        public Villain GetNewVillain(string villainName = "")
        {
            var newVillain = new Villain();
            var villain = villainName;
            if (string.IsNullOrEmpty(villain))
            {
                var allVillains = GetListOfVillains();
                villain = allVillains[random.Next(allVillains.Count)];
            }

            var villainInfo = _villains.FirstOrDefault(v => v.VillainName == villain);

            newVillain.VillainName = villainName;
            newVillain.SetName = villainInfo.VillainSetName;
            newVillain.VillainInfo = villainInfo;

            return newVillain;
        }

        public Villain GetNewVillain(List<Mastermind> allMastermindsInGame, Scheme scheme, List<string> villainsInGame)
        {
            var newVillain = new Villain();
            //Get Villains
            var villainList = GetListOfVillains();

            //Remove all Villains currently in the game from the list
            var remainingVillains = villainList.Except(villainsInGame).ToList();

            //Get Villains that have played with the Scheme
            var villainsByScheme = GetListOfVillainsByX("Scheme", scheme.SchemeName);

            //Remove all Villains that have played with the scheme from the list
            remainingVillains = remainingVillains.Except(villainsByScheme).ToList();

            //Get Villains that have played with each of the Masterminds with
            foreach (var mastermind in allMastermindsInGame)
            {
                var villainsByMastermind = GetListOfVillainsByX("Mastermind", mastermind.MastermindName);
                //Remove all Villains that have played with the Mastermind(s)
                remainingVillains = remainingVillains.Except(villainsByMastermind).ToList();
            }

            //Select Villain from remaining list
            var villainName = remainingVillains[random.Next(remainingVillains.Count)];
            var villainInfo = _villains.First(v => v.VillainName == villainName);

            //Set VillainName
            newVillain.VillainName = villainName;

            //Set SetName
            newVillain.SetName = villainInfo.VillainSetName;

            //Set VillainInfo
            newVillain.VillainInfo = villainInfo;

            return newVillain;
        }

        public string ToString(List<Villain> villainList)
        {
            var returnString = "\r\n";
            var counter = 1;

            var orderedVillainList = villainList.OrderBy(x => (int)x.SetName).ToList();

            foreach (var villain in orderedVillainList)
            {
                returnString = $"{returnString}{counter}) {villain.VillainName}, {villain.SetName.GetDescription()}\r\n";
                counter++;
            }

            return $"{returnString.Remove(returnString.Length-2)}\r\n";
        }

        public List<VillainInfo> ModifyVillainList(List<string> villainExclusions)
        {
            var returnList = _villains;

            foreach (var villainExclusion in villainExclusions)
            {
                while (returnList.Any(x => x.VillainName == villainExclusion))
                {
                    var itemToRemove = returnList.Single(x => x.VillainName == villainExclusion);
                    returnList.Remove(itemToRemove);
                }
            }

            return returnList;
        }

        public List<string> GetListOfVillains()
        {
            var allVillainsQuery = "SELECT [VillainName] FROM [Villains]";
            var allVillains = new SqlHelper().GetList(allVillainsQuery);
            return allVillains;
        }

        public List<string> GetListOfVillainsWithKeyword(Keywords keyword)
        {
            var returnList  = _villains
                .Where(villain => villain.KeywordsList.Contains(keyword))
                .Select(villain => villain.VillainName).ToList();

            return returnList;
        }

        public string GetRandomVillain()
        {
            var allVillains = GetListOfVillains();
            var villain = allVillains[random.Next(allVillains.Count)];
            return villain;
        }

        public List<string> GetListOfVillainsByX(string cardType, string name)
        {
            //cardType can be Henchmen, Scheme, Hero, Villain, or Mastermind
            var villainByTable = $"VillainBy{cardType}";
            var tableName = (cardType == "Henchmen") ? "Henchmen" : (cardType == "Hero" ? "Heroes" : $"{cardType}s");
            var updatedName = name.Contains("'") ? name.Replace("'", "''") : name;

            var allVillainsBy = $@"select v.VillainName from Villains v
                    inner join {villainByTable} vb ON v.Id = vb.VillainId
                    inner join {tableName} t ON t.Id = vb.{cardType}Id
                    where t.{cardType}Name = '{updatedName}'";

            var allVillainsByX = new SqlHelper().GetList(allVillainsBy);
            return allVillainsByX;
        }
    }
}
