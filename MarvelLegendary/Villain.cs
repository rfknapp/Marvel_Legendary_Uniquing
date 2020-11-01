using MarvelLegendary.Exclusions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarvelLegendary
{
    public class VillainInfo
    {
        public string VillainName { get; set; }
        public GameInfo.Set VillainSetName { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeBindings { get; set; }

        public VillainInfo(string name, GameInfo.Set set, bool includeBindings = false)
        {
            VillainName = name;
            VillainSetName = set;
            IsDuplicate = false;
            DuplicateName = "";
            IncludeBindings = includeBindings;
        }
    }

    public class Villain
    {
        public string VillainName { get; set; }
        public GameInfo.Set SetName { get; set; }
        public VillainInfo VillainInfo { get; set; }

        private readonly List<VillainInfo> _villains = new List<VillainInfo>()
        {
            new VillainInfo("Brotherhood", GameInfo.Set.Core),
            new VillainInfo("Enemies of Asgard", GameInfo.Set.Core),
            new VillainInfo("HYDRA", GameInfo.Set.Core),
            new VillainInfo("Masters of Evil", GameInfo.Set.Core),
            new VillainInfo("Radiation", GameInfo.Set.Core),
            new VillainInfo("Skrulls", GameInfo.Set.Core),
            new VillainInfo("Spider-Foes", GameInfo.Set.Core),
            
            new VillainInfo("Emissaries of Evil", GameInfo.Set.Dc),
            new VillainInfo("Four Horsemen", GameInfo.Set.Dc),
            new VillainInfo("Marauders", GameInfo.Set.Dc),
            new VillainInfo("MLF", GameInfo.Set.Dc),
            new VillainInfo("Streets of New York", GameInfo.Set.Dc),
            new VillainInfo("Underworld", GameInfo.Set.Dc),
            
            new VillainInfo("Heralds of Galactus", GameInfo.Set.Ff),
            new VillainInfo("Subterranea", GameInfo.Set.Ff),
            
            new VillainInfo("Maximum Carnage", GameInfo.Set.PttR),
            new VillainInfo("Sinister Six", GameInfo.Set.PttR),
            
            new VillainInfo("Avengers", GameInfo.Set.Villains, true),
            new VillainInfo("Defenders", GameInfo.Set.Villains, true),
            new VillainInfo("Marvel Knights", GameInfo.Set.Villains, true),
            new VillainInfo("Spider Friends", GameInfo.Set.Villains, true),
            new VillainInfo("Uncanny Avengers", GameInfo.Set.Villains),
            new VillainInfo("Uncanny X-Men", GameInfo.Set.Villains, true),
            new VillainInfo("X-Men First Class", GameInfo.Set.Villains),
            
            new VillainInfo("Infinity Gems", GameInfo.Set.GotG),
            new VillainInfo("Kree Starforce", GameInfo.Set.GotG),
            
            new VillainInfo("The Mighty", GameInfo.Set.Fi, true),
            
            new VillainInfo("The Deadlands", GameInfo.Set.Sw1),
            new VillainInfo("Domain of Apocalypse", GameInfo.Set.Sw1),
            new VillainInfo("Limbo", GameInfo.Set.Sw1),
            new VillainInfo("Manhattan (Earth-1610)", GameInfo.Set.Sw1),
            new VillainInfo("Sentinel Territories", GameInfo.Set.Sw1),
            new VillainInfo("Wasteland", GameInfo.Set.Sw1),
            
            new VillainInfo("Deadpool's Secret Secret Wars", GameInfo.Set.Sw2),
            new VillainInfo("Guardians of Knowhere", GameInfo.Set.Sw2),
            new VillainInfo("K'un-Lun", GameInfo.Set.Sw2),
            new VillainInfo("Monster Metropolis", GameInfo.Set.Sw2),
            new VillainInfo("Utopolis", GameInfo.Set.Sw2),
            new VillainInfo("X-Men '92", GameInfo.Set.Sw2),
            
            new VillainInfo("Masters of Evil (WWII)", GameInfo.Set.Ca),
            new VillainInfo("Zola's Creations", GameInfo.Set.Ca),
            
            new VillainInfo("CSA Special Marshals", GameInfo.Set.Cw),
            new VillainInfo("Great Lake Avengers", GameInfo.Set.Cw),
            new VillainInfo("Heroes for Hire", GameInfo.Set.Cw),
            new VillainInfo("Registration Enforcers", GameInfo.Set.Cw),
            new VillainInfo("S.H.I.E.L.D. Elite", GameInfo.Set.Cw),
            new VillainInfo("Superhuman Registration Act", GameInfo.Set.Cw),
            new VillainInfo("Thunderbolts", GameInfo.Set.Cw),
            
            new VillainInfo("Deadpool's \"Friends\"", GameInfo.Set.Deadpool),
            new VillainInfo("Evil Deadpool Corpse", GameInfo.Set.Deadpool),
            
            new VillainInfo("Goblin's Freak Show", GameInfo.Set.Noir),
            new VillainInfo("X-Men Noir", GameInfo.Set.Noir),
            
            new VillainInfo("Dark Descendants", GameInfo.Set.XMen),
            new VillainInfo("Hellfire Club", GameInfo.Set.XMen),
            new VillainInfo("Mojoverse", GameInfo.Set.XMen),
            new VillainInfo("Murderworld", GameInfo.Set.XMen),
            new VillainInfo("Shadow-X", GameInfo.Set.XMen),
            new VillainInfo("Shi'ar Imperial Guard", GameInfo.Set.XMen),
            new VillainInfo("Sisterhood of Mutants", GameInfo.Set.XMen),
            
            new VillainInfo("Salvagers", GameInfo.Set.Sm),
            new VillainInfo("Vulture Tech", GameInfo.Set.Sm),
            
            new VillainInfo("Monsters Unleashed", GameInfo.Set.Champions),
            new VillainInfo("Wrecking Crew", GameInfo.Set.Champions),
            
            new VillainInfo("Aspects of the Void", GameInfo.Set.Wwh),
            new VillainInfo("Code Red", GameInfo.Set.Wwh),
            new VillainInfo("Illuminati", GameInfo.Set.Wwh),
            new VillainInfo("Intelligencia", GameInfo.Set.Wwh),
            new VillainInfo("Sakaar Imperial Guard", GameInfo.Set.Wwh),
            new VillainInfo("U-Foes", GameInfo.Set.Wwh),
            new VillainInfo("Warbound", GameInfo.Set.Wwh),
            
            new VillainInfo("Chitauri", GameInfo.Set.P1),
            new VillainInfo("Gamma Hunters", GameInfo.Set.P1),
            new VillainInfo("Iron Foes", GameInfo.Set.P1),
            
            new VillainInfo("Queen's Vengeance", GameInfo.Set.Antman),
            new VillainInfo("Ultron's Legacy", GameInfo.Set.Antman),
            
            new VillainInfo("Life Foundation", GameInfo.Set.Venom),
            new VillainInfo("Poisons", GameInfo.Set.Venom),
            
            new VillainInfo("Army of Evil", GameInfo.Set.Revelations),
            new VillainInfo("Dark Avengers", GameInfo.Set.Revelations),
            new VillainInfo("Hood's Gang", GameInfo.Set.Revelations),
            new VillainInfo("Lethal Legion", GameInfo.Set.Revelations),
            
            new VillainInfo("A.I.M., Hydra Offshoot", GameInfo.Set.Shield),
            new VillainInfo("Hydra Elite", GameInfo.Set.Shield),
            
            new VillainInfo("Dark Council", GameInfo.Set.Asgard),
            new VillainInfo("Omens of Ragnarok", GameInfo.Set.Asgard),
            
            new VillainInfo("Demons of Limbo", GameInfo.Set.NewMutants),
            new VillainInfo("Hellions", GameInfo.Set.NewMutants),

            new VillainInfo("Black Order of Thanos", GameInfo.Set.Cosmos),
            new VillainInfo("Celestials", GameInfo.Set.Cosmos),
            new VillainInfo("From Beyond", GameInfo.Set.Cosmos),
            new VillainInfo("Elders of the Universe", GameInfo.Set.Cosmos),

            new VillainInfo("Shi'ar Imperial Elite", GameInfo.Set.Inhumans),
            new VillainInfo("Inhuman Rebellion", GameInfo.Set.Inhumans)
        };

        public Villain()
        {
            var villain = _villains[new Random().Next(_villains.Count)];

            VillainName = villain.VillainName;
            SetName = villain.VillainSetName;
            VillainInfo = villain;
        }

        public Villain(List<string> exclusionVillains)
        {
            VillainInfo villain;
            var villainsExcluded = _villains.Select(x => x.VillainName).Except(exclusionVillains).ToList();

            if (villainsExcluded.Count > 0)
            {
                var villainName = villainsExcluded[new Random().Next(villainsExcluded.Count)];
                villain = _villains.First(x => x.VillainName == villainName);
            }
            else
            {
                villain = _villains[new Random().Next(_villains.Count)];
            }

            VillainName = villain.VillainName;
            SetName = villain.VillainSetName;
            VillainInfo = villain;
        }

        public Villain(List<Mastermind> allMastermindsInGame)
        {
            var getExclusions = new GetExclusions();
            VillainInfo villain = null;
            var masterminds = allMastermindsInGame.Select(x => x.MastermindName).ToList();
            var villainList = new Villain().GetListOfVillains();

            for (int i = masterminds.Count - 1; i >= 0; i--)
            {
                var exclusions = getExclusions.GetMastermindExclusion(masterminds);

                var mastermindCompareList = villainList.Except(exclusions.VillainList).ToList();
                if (mastermindCompareList.Count > 0)
                {
                    var villainName = mastermindCompareList[new Random().Next(mastermindCompareList.Count)];
                    villain = _villains.First(x => x.VillainName == villainName);
                    break;
                }
                masterminds.RemoveAt(i);
            }

            if(villain == null)
            {
                villain = _villains[new Random().Next(_villains.Count)];
            }

            VillainName = villain.VillainName;
            SetName = villain.VillainSetName;
            VillainInfo = villain;
        }

        public Villain(string villainName)
        {
            var villain = _villains.First(x => x.VillainName == villainName);

            VillainName = villainName;
            SetName = villain.VillainSetName;
            VillainInfo = villain;
        }

        public Villain(List<VillainInfo> villains)
        {
            var villainInfo = villains[new Random().Next(villains.Count)];

            VillainName = villainInfo.VillainName;
            SetName = villainInfo.VillainSetName;
            VillainInfo = villainInfo;
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
            return _villains.ToList().Select(x => x.VillainName).ToList();
        }
    }
}
