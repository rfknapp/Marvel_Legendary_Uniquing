using System.Collections.Generic;
using System.Linq;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;

namespace MarvelLegendary
{
    public class VillainInfo
    {
        public int Id { get; set; }
        public string VillainName { get; set; }
        public Set VillainSetName { get; set; }
        public bool IsDuplicate { get; set; }
        public bool IncludeBindings { get; set; }
        public List<Keywords> KeywordsList { get; set; }
        public List<int> DuplicateVillainIds { get; set; } = new List<int>();
        public bool IsEnabled { get; set; } = true;

        public VillainInfo(int id, string name, Set set, bool includeBindings = false)
        {
            Id = id;
            VillainName = name;
            VillainSetName = set;
            IsDuplicate = false;
            IncludeBindings = includeBindings;
            KeywordsList = new List<Keywords>();
            IsEnabled = true;
        }

        public VillainInfo(int id, string name, Set set, List<int> duplicateIdList, bool includeBindings = false)
        {
            Id = id;
            VillainName = name;
            VillainSetName = set;
            IsDuplicate = true;
            IncludeBindings = includeBindings;
            KeywordsList = new List<Keywords>();
            DuplicateVillainIds = duplicateIdList;
            IsEnabled = true;
        }

        public VillainInfo SetKeywords(List<Keywords> keywords)
        {
            KeywordsList = keywords;
            return this;
        }

        public VillainInfo Duplicate(List<int> villainIds)
        {
            DuplicateVillainIds = villainIds;
            IsDuplicate = true;
            return this;
        }

        public VillainInfo Disable()
        {
            IsEnabled = false;
            return this;
        }
    }

    public static class VillainRepository
    {
        private static readonly List<VillainInfo> _villains = new List<VillainInfo>()
        {
            new VillainInfo(1, "Brotherhood", Set.Core, new List<int> { 1, 127}),
            new VillainInfo(2, "Enemies of Asgard", Set.Core, new List<int> { 2, 72, 128}),
            new VillainInfo(3, "HYDRA", Set.Core, new List<int> { 3, 74, 129}),
            new VillainInfo(4, "Masters of Evil", Set.Core, new List<int> { 4, 130}),
            new VillainInfo(5, "Radiation", Set.Core, new List<int> { 5, 131}),
            new VillainInfo(6, "Skrulls", Set.Core, new List<int> { 6, 134}),
            new VillainInfo(7, "Spider-Foes", Set.Core, new List<int> { 7, 132}),
            
            new VillainInfo(8, "Emissaries of Evil", Set.Dc),
            new VillainInfo(9, "Four Horsemen", Set.Dc),
            new VillainInfo(10, "Marauders", Set.Dc),
            new VillainInfo(11, "MLF", Set.Dc),
            new VillainInfo(12, "Streets of New York", Set.Dc),
            new VillainInfo(13, "Underworld", Set.Dc),

            new VillainInfo(14, "Heralds of Galactus", Set.Ff),
            new VillainInfo(15, "Subterranea", Set.Ff),

            new VillainInfo(16, "Maximum Carnage", Set.PttR),
            new VillainInfo(17, "Sinister Six", Set.PttR),

            new VillainInfo(18, "Avengers", Set.Villains, true),
            new VillainInfo(19, "Defenders", Set.Villains, true),
            new VillainInfo(20, "Marvel Knights", Set.Villains, true),
            new VillainInfo(21, "Spider Friends", Set.Villains, true),
            new VillainInfo(22, "Uncanny Avengers", Set.Villains),
            new VillainInfo(23, "Uncanny X-Men", Set.Villains, true),
            new VillainInfo(24, "X-Men First Class", Set.Villains),
            
            new VillainInfo(25, "Infinity Gems", Set.GotG),
            new VillainInfo(26, "Kree Starforce", Set.GotG),
            
            new VillainInfo(27, "The Mighty", Set.Fi, true),
            
            new VillainInfo(28, "The Deadlands", Set.Sw1).SetKeywords(new List<Keywords>{ Keywords.LivingDead}),
            new VillainInfo(29, "Domain of Apocalypse", Set.Sw1),
            new VillainInfo(30, "Limbo", Set.Sw1),
            new VillainInfo(31, "Manhattan (Earth-1610)", Set.Sw1),
            new VillainInfo(32, "Sentinel Territories", Set.Sw1),
            new VillainInfo(33, "Wasteland", Set.Sw1),
            
            new VillainInfo(34, "Deadpool's Secret Secret Wars", Set.Sw2),
            new VillainInfo(35, "Guardians of Knowhere", Set.Sw2),
            new VillainInfo(36, "K'un-Lun", Set.Sw2),
            new VillainInfo(37, "Monster Metropolis", Set.Sw2),
            new VillainInfo(38, "Utopolis", Set.Sw2),
            new VillainInfo(39, "X-Men '92", Set.Sw2),
            
            new VillainInfo(40, "Masters of Evil (WWII)", Set.Ca),
            new VillainInfo(41, "Zola's Creations", Set.Ca),
            
            new VillainInfo(42, "CSA Special Marshals", Set.Cw),
            new VillainInfo(43, "Great Lake Avengers", Set.Cw),
            new VillainInfo(44, "Heroes for Hire", Set.Cw),
            new VillainInfo(45, "Registration Enforcers", Set.Cw),
            new VillainInfo(46, "S.H.I.E.L.D. Elite", Set.Cw),
            new VillainInfo(47, "Superhuman Registration Act", Set.Cw),
            new VillainInfo(48, "Thunderbolts", Set.Cw),
            
            new VillainInfo(49, "Deadpool's \"Friends\"", Set.Deadpool),
            new VillainInfo(50, "Evil Deadpool Corpse", Set.Deadpool),
            
            new VillainInfo(51, "Goblin's Freak Show", Set.Noir),
            new VillainInfo(52, "X-Men Noir", Set.Noir),
            
            new VillainInfo(53, "Dark Descendants", Set.XMen),
            new VillainInfo(54, "Hellfire Club", Set.XMen),
            new VillainInfo(55, "Mojoverse", Set.XMen),
            new VillainInfo(56, "Murderworld", Set.XMen),
            new VillainInfo(57, "Shadow-X", Set.XMen),
            new VillainInfo(58, "Shi'ar Imperial Guard", Set.XMen),
            new VillainInfo(59, "Sisterhood of Mutants", Set.XMen),
            
            new VillainInfo(60, "Salvagers", Set.Sm),
            new VillainInfo(61, "Vulture Tech", Set.Sm),
            
            new VillainInfo(62, "Monsters Unleashed", Set.Champions),
            new VillainInfo(63, "Wrecking Crew", Set.Champions),
            
            new VillainInfo(64, "Aspects of the Void", Set.Wwh),
            new VillainInfo(65, "Code Red", Set.Wwh),
            new VillainInfo(66, "Illuminati", Set.Wwh),
            new VillainInfo(67, "Intelligencia", Set.Wwh),
            new VillainInfo(68, "Sakaar Imperial Guard", Set.Wwh),
            new VillainInfo(69, "U-Foes", Set.Wwh),
            new VillainInfo(70, "Warbound", Set.Wwh),
            
            new VillainInfo(71, "Chitauri", Set.P1),
            new VillainInfo(72, "Enemies of Asgard", Set.P1, new List<int> { 2, 72, 128}),
            new VillainInfo(73, "Gamma Hunters", Set.P1),
            new VillainInfo(74, "HYDRA", Set.P1, new List<int> { 3, 74, 129}),
            new VillainInfo(75, "Iron Foes", Set.P1),
            
            new VillainInfo(76, "Queen's Vengeance", Set.Antman),
            new VillainInfo(77, "Ultron's Legacy", Set.Antman),
            
            new VillainInfo(78, "Life Foundation", Set.Venom),
            new VillainInfo(79, "Poisons", Set.Venom),
            
            new VillainInfo(80, "Army of Evil", Set.Revelations),
            new VillainInfo(81, "Dark Avengers", Set.Revelations),
            new VillainInfo(82, "Hood's Gang", Set.Revelations),
            new VillainInfo(83, "Lethal Legion", Set.Revelations),
            
            new VillainInfo(84, "A.I.M., Hydra Offshoot", Set.Shield),
            new VillainInfo(85, "Hydra Elite", Set.Shield),
            
            new VillainInfo(86, "Dark Council", Set.Asgard),
            new VillainInfo(87, "Omens of Ragnarok", Set.Asgard),
            
            new VillainInfo(88, "Demons of Limbo", Set.NewMutants),
            new VillainInfo(89, "Hellions", Set.NewMutants),
            
            new VillainInfo(90, "Black Order of Thanos", Set.Cosmos),
            new VillainInfo(91, "Celestials", Set.Cosmos),
            new VillainInfo(92, "From Beyond", Set.Cosmos),
            new VillainInfo(93, "Elders of the Universe", Set.Cosmos),
            
            new VillainInfo(94, "Shi'ar Imperial Elite", Set.Inhumans),
            new VillainInfo(95, "Inhuman Rebellion", Set.Inhumans),
            
            new VillainInfo(96, "Annihilation Wave", Set.Annihilation),
            new VillainInfo(97, "Timelines of Kang", Set.Annihilation),
            
            new VillainInfo(98, "Acolytes", Set.Messiah),
            new VillainInfo(99, "Clan Yashida", Set.Messiah),
            new VillainInfo(100, "Purifiers", Set.Messiah),
            new VillainInfo(101, "Reavers", Set.Messiah),
            
            new VillainInfo(102, "Fear Lords", Set.Strange),
            new VillainInfo(103, "Lords of the Netherworld", Set.Strange),
            
            new VillainInfo(104, "Followers of Ronan", Set.Guardians),
            new VillainInfo(105, "Ravagers", Set.Guardians),
            
            new VillainInfo(106, "Enemies of Wakanda", Set.BlackPanther),
            new VillainInfo(107, "Killmonger's League", Set.BlackPanther),
            
            new VillainInfo(108, "Elite Assassins", Set.BlackWidow),
            new VillainInfo(109, "Taskmaster's Thunderbolts", Set.BlackWidow),
            
            new VillainInfo(110, "Children of Thanos", Set.InfinitySaga),
            new VillainInfo(111, "Infinity Stones", Set.InfinitySaga),
            
            new VillainInfo(112, "The Fallen", Set.MidnightSons),
            new VillainInfo(113, "Lilin", Set.MidnightSons),
            
            new VillainInfo(114, "Black Order Guards", Set.WhatIf),
            new VillainInfo(115, "Intergalactic Party Animals", Set.WhatIf),
            new VillainInfo(116, "Rival Overlords", Set.WhatIf),
            new VillainInfo(117, "Strange's Demons", Set.WhatIf),
            new VillainInfo(118, "Zombie Avengers", Set.WhatIf).SetKeywords(new List<Keywords>{ Keywords.LivingDead, Keywords.Rampage}),
            
            new VillainInfo(119, "Armada of Kang", Set.AntmanWasp),
            new VillainInfo(120, "Cross Technologies", Set.AntmanWasp),
            new VillainInfo(121, "Ghost Chasers", Set.AntmanWasp),
            new VillainInfo(122, "Quantum Realm", Set.AntmanWasp),
            
            new VillainInfo(123, "Alchemax Enforcers", Set.TwentyNintyNine),
            new VillainInfo(124, "False Aesir of Alchemax", Set.TwentyNintyNine),
            
            new VillainInfo(125, "Berserkers", Set.WeaponX),
            new VillainInfo(126, "Weapon Plus", Set.WeaponX),
            
            new VillainInfo(127, "Brotherhood", Set.Core2E, new List<int> { 1, 127}),
            new VillainInfo(128, "Enemies of Asgard", Set.Core2E, new List<int> { 2, 72, 128}),
            new VillainInfo(129, "HYDRA", Set.Core2E, new List<int> { 3, 74, 129}),
            new VillainInfo(130, "Masters of Evil", Set.Core2E, new List<int> { 4, 130}),
            new VillainInfo(131, "Radiation", Set.Core2E, new List<int> { 5, 131}),
            new VillainInfo(132, "Sinister Spider-Foes", Set.Core2E, new List<int> { 7, 132}),
            new VillainInfo(133, "Sinister Syndicate", Set.Core2E),
            new VillainInfo(134, "Skrulls", Set.Core2E, new List<int> { 6, 134}),
        };

        public static IReadOnlyList<VillainInfo> All => _villains;
    }

    public class Villain
    {
        public int Id { get; set; }
        public string VillainName { get; set; }
        public Set SetName { get; set; }
        public VillainInfo VillainInfo { get; set; }

        public static Villain GetNewVillain(string villainName, Set setName)
        {
            var villainInfo = VillainRepository.All.FirstOrDefault(v => v.VillainName == villainName && v.VillainSetName == setName);
            return GetNewVillain(villainInfo);
        }

        public static List<Villain> ConvertToVillainList(List<VillainInfo> villainInfoList)
        {
            return (from villainInfo in villainInfoList
                    select GetNewVillain(villainInfo)).ToList();
        }

        private static List<Villain> ConvertToVillainList(List<Card> villainCards)
        {
            var returnList = new List<Villain>();
            foreach (var villainCard in villainCards)
            {
                var villain = VillainRepository.All.FirstOrDefault(v => v.VillainName== villainCard.CardName && (int)v.VillainSetName == villainCard.SetId);
                returnList.Add(GetNewVillain(villain));
            }

            return returnList;
        }

        public static Villain GetNewVillain(VillainInfo villainInfo)
        {
            if (villainInfo.IsDuplicate)
            {
                villainInfo = GetDuplicateVillain(villainInfo);
            }

            return new Villain
            {
                Id = villainInfo.Id,
                VillainName = villainInfo.VillainName,
                SetName = villainInfo.VillainSetName,
                VillainInfo = villainInfo
            };
        }

        public static Villain GetNewVillain(List<Mastermind> allMastermindsInGame, Scheme scheme, List<Villain> villainsInGame)
        {
            var villainList = ConvertToVillainList(VillainRepository.All.ToList());

            //Remove all Villains currently in the game from the list
            var idsInGame = new HashSet<int>(villainsInGame.Select(v => v.Id));
            var remainingVillains = villainList.Where(h => !idsInGame.Contains(h.Id)).ToList();

            //If a villain with duplicates is in the game then this will remove all duplicates from the pool to choose from
            foreach (var villainInGame in villainsInGame)
            {
                if (villainInGame.VillainInfo.IsDuplicate)
                {
                    var duplicateVillainList = VillainRepository.All.Where(v => villainInGame.VillainInfo.DuplicateVillainIds.Contains(v.Id)).ToList();
                    idsInGame = new HashSet<int>(duplicateVillainList.Select(v => v.Id));
                    remainingVillains = remainingVillains.Where(h => !idsInGame.Contains(h.Id)).ToList();
                }
            }

            //Get Villains that have played with the Scheme
            var schemeCard = new Card
            {
                CardName = scheme.SchemeName,
                CardType = (int)CardType.Scheme,
                SetId = (int)scheme.SetName
            };

            //This is the new implementation of the VillainByScheme table lookup
            var villainCardssByScheme = SqlHelper.GetCardRelationships(CardType.Villain, schemeCard);
            var villainsByScheme = ConvertToVillainList(villainCardssByScheme);

            //Remove all Villains that have played with the scheme from the list
            remainingVillains = remainingVillains.Except(villainsByScheme).ToList();

            //Get Villains that have played with each of the Masterminds with
            foreach (var mastermind in allMastermindsInGame)
            {
                var mastermindCard = new Card
                {
                    CardName = mastermind.MastermindName,
                    CardType = (int)CardType.Mastermind,
                    SetId = (int)mastermind.SetName
                };

                //This is the new implementation of the VillainByMastermind table lookup
                var villainCardsByMastermind = SqlHelper.GetCardRelationships(CardType.Villain, mastermindCard);
                var villainsByMastermind = ConvertToVillainList(villainCardsByMastermind);

                remainingVillains = remainingVillains.Except(villainsByMastermind).ToList();
            }

            //Get Villains that have played with each of the Villains with
            foreach (var v in villainsInGame)
            {
                var villainCard = new Card
                {
                    CardName = v.VillainName,
                    CardType = (int)CardType.Villain,
                    SetId = (int)v.SetName
                };

                var villainCardsByVillain = SqlHelper.GetCardRelationships(CardType.Villain, villainCard);
                var villainsByVillain = ConvertToVillainList(villainCardsByVillain);

                //Remove all Villains that have played with the Villains(s)
                remainingVillains = remainingVillains.Except(villainsByVillain).ToList();
            }

            //Select Villain from remaining list
            var villain = remainingVillains[RandomHelper.Instance.Next(remainingVillains.Count)];
            var villainInfo = VillainRepository.All.First(v => v.VillainName == villain.VillainName && v.VillainSetName == villain.SetName);

            return GetNewVillain(villainInfo);
        }

        private static VillainInfo GetDuplicateVillain(VillainInfo villainInfo)
        {
            var listOfInts = villainInfo.DuplicateVillainIds;
            var matchingVillains = VillainRepository.All.Where(x => listOfInts.Contains(x.Id)).ToList();

            var enabledVillains = matchingVillains.Where(x => x.IsEnabled).ToList();

            var newestVillain = enabledVillains.OrderByDescending(x => (int)x.VillainSetName).FirstOrDefault();

            return newestVillain;
        }

        public static string ToString(List<Villain> villainList)
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
            var returnList = VillainRepository.All.ToList();

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

        public static List<string> GetListOfVillains()
        {
            var returnList = VillainRepository.All.Select(v => v.VillainName).ToList();
            return returnList;
        }

        public static List<Villain> GetListOfVillainsWithKeyword(Keywords keyword)
        {
            var returnList = VillainRepository.All
                .Where(villain => villain.KeywordsList.Contains(keyword)).ToList();

            return ConvertToVillainList(returnList);
        }

        public string GetRandomVillain()
        {
            var allVillains = GetListOfVillains();
            var villain = allVillains[RandomHelper.Instance.Next(allVillains.Count)];
            return villain;
        }
    }
}
