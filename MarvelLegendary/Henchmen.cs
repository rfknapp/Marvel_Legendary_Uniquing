using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;

namespace MarvelLegendary
{
    public class HenchmenInfo
    {
        public int Id { get; set; }
        public string HenchmenName { get; set; }
        public Set HenchmenSetName { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeNewRecruits { get; set; }
        public List<int> DuplicateHenchmenIds { get; set; } = new List<int>();
        public bool IsEnabled { get; set; } = true;

        public HenchmenInfo(int id, string name, Set set, bool includeNewRecruits = false)
        {
            Id = id;
            HenchmenName = name;
            HenchmenSetName = set;
            IsDuplicate = false;
            DuplicateName = "";
            IncludeNewRecruits = includeNewRecruits;
        }

        public HenchmenInfo(int id, string name, Set set, List<int> duplicateIdList, bool includeNewRecruits = false)
        {
            Id = id;
            HenchmenName = name;
            HenchmenSetName = set;
            IsDuplicate = true;
            DuplicateHenchmenIds = duplicateIdList;
            IncludeNewRecruits = includeNewRecruits;
        }

        public HenchmenInfo Duplicate(List<int> henchmenIds)
        {
            DuplicateHenchmenIds = henchmenIds;
            IsDuplicate = true;
            return this;
        }

        public HenchmenInfo Disable()
        {
            IsEnabled = false;
            return this;
        }
    }

    public static class HenchmenRepository
    {
        private static readonly List<HenchmenInfo> _henchmen = new List<HenchmenInfo>()
        {
            new HenchmenInfo(1, "Doombot Legion", Set.Core, new List<int> { 1, 32, 47}),
            new HenchmenInfo(2, "Hand Ninjas", Set.Core, new List<int> { 2, 30, 48}),
            new HenchmenInfo(3, "Savage Land Mutates", Set.Core, new List<int> { 3, 31, 49}),
            new HenchmenInfo(4, "Sentinel", Set.Core, new List<int> { 4, 29, 50}),

            new HenchmenInfo(5, "Maggia Goons", Set.Dc),
            new HenchmenInfo(6, "Phalanx", Set.Dc),
            
            new HenchmenInfo(7, "Asgardian Warriors", Set.Villains),
            new HenchmenInfo(8, "Cops", Set.Villains, true),
            new HenchmenInfo(9, "Multiple Man", Set.Villains),
            new HenchmenInfo(10, "S.H.I.E.L.D. Assault Squad", Set.Villains),
            
            new HenchmenInfo(11, "Ghost Racers", Set.Sw1),
            new HenchmenInfo(12, "M.O.D.O.K.s", Set.Sw1),
            new HenchmenInfo(13, "Thor Corps", Set.Sw1),
            
            new HenchmenInfo(14, "Khonshu Guardians", Set.Sw2),
            new HenchmenInfo(15, "Magma Men", Set.Sw2),
            new HenchmenInfo(16, "Spider-Infected", Set.Sw2),
            
            new HenchmenInfo(17, "Cape-killers", Set.Cw),
            new HenchmenInfo(18, "Mandroids", Set.Cw),
            
            new HenchmenInfo(19, "Circus of Crime", Set.ThreeD),
            new HenchmenInfo(20, "Spider-Slayer", Set.ThreeD),
            
            new HenchmenInfo(21, "The Brood", Set.XMen),
            new HenchmenInfo(22, "Hellfire Cult", Set.XMen),
            new HenchmenInfo(23, "Sapien League", Set.XMen),
            new HenchmenInfo(24, "Shi'ar Death Commandos", Set.XMen),
            new HenchmenInfo(25, "Shi'ar Patrol Craft", Set.XMen),
            
            new HenchmenInfo(26, "Cytoplasm Spikes", Set.Wwh),
            new HenchmenInfo(27, "Death's Heads", Set.Wwh),
            new HenchmenInfo(28, "Sakaaran Hivelings", Set.Wwh),
            
            new HenchmenInfo(29, "Hammer Drone Army", Set.P1, new List<int> { 4, 29, 50}),
            new HenchmenInfo(30, "HYDRA Pilots", Set.P1, new List<int> { 2, 30, 48}),
            new HenchmenInfo(31, "HYDRA Spies", Set.P1, new List<int> { 3, 31, 49}),
            new HenchmenInfo(32, "Ten Rings Fanatics", Set.P1, new List<int> { 1, 32, 47}),

            new HenchmenInfo(33, "Circus of Crime", Set.Dimensions),
            new HenchmenInfo(34, "Spider-Slayer", Set.Dimensions),
            
            new HenchmenInfo(35, "HYDRA Base", Set.Revelations),
            new HenchmenInfo(36, "Mandarin's Rings", Set.Revelations),
            
            new HenchmenInfo(37, "Sidera Maris, Bridge Builders", Set.Cosmos),
            new HenchmenInfo(38, "Universal Church of Truth", Set.Cosmos),
            
            new HenchmenInfo(39, "Mr. Sinister Clones", Set.Messiah),
            new HenchmenInfo(40, "Sentinel Squad O*N*E*", Set.Messiah),
            
            new HenchmenInfo(41, "Giants of Jotunheim", Set.WhatIf),
            new HenchmenInfo(42, "Ultron Sentries", Set.WhatIf),
            new HenchmenInfo(43, "Vibranium Liberator Drones", Set.WhatIf),
            
            new HenchmenInfo(44, "Quantonauts", Set.AntmanWasp),
            new HenchmenInfo(45, "Quantum Hounds", Set.AntmanWasp),
            new HenchmenInfo(46, "Tardigrade", Set.AntmanWasp),
            
            new HenchmenInfo(47, "Doombot Legion", Set.Core2E, new List<int> { 1, 32, 47}),
            new HenchmenInfo(48, "Hand Ninjas", Set.Core2E, new List<int> { 2, 30, 48}),
            new HenchmenInfo(49, "Savage Land Mutates", Set.Core2E, new List<int> { 3, 31, 49}),
            new HenchmenInfo(50, "Sentinel", Set.Core2E, new List<int> { 4, 29, 50}),

        };

        public static IReadOnlyList<HenchmenInfo> All => _henchmen;
    }

    public class Henchmen
    {
        public int Id { get; set; }
        public Set HenchmenSet { get; set; }
        public string HenchmenName { get; set; }
        public HenchmenInfo HenchmenInfo { get; set; }

        public static Henchmen GetNewHenchmen(string henchmenName = "")
        {
            var henchmen = henchmenName;
            if (string.IsNullOrEmpty(henchmenName))
            {
                var allHenchmen = GetListOfHenchmen();
                henchmen = allHenchmen[RandomHelper.Instance.Next(allHenchmen.Count)];
            }

            var henchmenInfo = HenchmenRepository.All.FirstOrDefault(h => h.HenchmenName == henchmen);

            return new Henchmen
            {
                Id = henchmenInfo.Id,
                HenchmenName = henchmen,
                HenchmenSet = henchmenInfo.HenchmenSetName,
                HenchmenInfo = henchmenInfo
            };
        }

        public static Henchmen GetNewHenchmen(string henchmenName, Set setName)
        {
            var henchmenInfo = HenchmenRepository.All.FirstOrDefault(h => h.HenchmenName == henchmenName && h.HenchmenSetName == setName);

            if(henchmenInfo.IsDuplicate)
            {
                henchmenInfo = GetDuplicateHenchmen(henchmenInfo);
            }

            return new Henchmen
            {
                Id = henchmenInfo.Id,
                HenchmenName = henchmenInfo.HenchmenName,
                HenchmenSet = henchmenInfo.HenchmenSetName,
                HenchmenInfo = henchmenInfo
            };
        }

        public static Henchmen GetNewHenchmen(HenchmenInfo henchmenInfo)
        {
            if (henchmenInfo.IsDuplicate)
            {
                henchmenInfo = GetDuplicateHenchmen(henchmenInfo);
            }

            return new Henchmen
            {
                Id = henchmenInfo.Id,
                HenchmenName = henchmenInfo.HenchmenName,
                HenchmenSet = henchmenInfo.HenchmenSetName,
                HenchmenInfo = henchmenInfo
            };
        }

        private static HenchmenInfo GetDuplicateHenchmen(HenchmenInfo henchmenName)
        {
            var listOfInts = henchmenName.DuplicateHenchmenIds;
            var matchingHenchmen = HenchmenRepository.All.Where(x => listOfInts.Contains(x.Id)).ToList();

            var enabledHenchmen = matchingHenchmen.Where(x => x.IsEnabled).ToList();

            var newestHenchmen = enabledHenchmen.OrderByDescending(x => (int)x.HenchmenSetName).FirstOrDefault();

            return newestHenchmen;
        }

        private static List<Henchmen> ConvertToHenchmenList(List<HenchmenInfo> henchmenInfoList)
        {
            var returnList = new List<Henchmen>();
            foreach (var henchmenInfo in henchmenInfoList)
            {
                returnList.Add(
                    new Henchmen
                    {
                        Id = henchmenInfo.Id,
                        HenchmenName = henchmenInfo.HenchmenName,
                        HenchmenSet = henchmenInfo.HenchmenSetName,
                        HenchmenInfo = henchmenInfo
                    });
            }

            return returnList;
        }

        private static List<Henchmen> ConvertToHenchmenList(List<Card> henchmenCards)
        {
            var returnList = new List<Henchmen>();
            foreach (var henchmenCard in henchmenCards)
            {
                var henchmen = HenchmenRepository.All.FirstOrDefault(h => h.HenchmenName == henchmenCard.CardName && (int)h.HenchmenSetName == henchmenCard.SetId);
                returnList.Add(
                    new Henchmen
                    {
                        Id = henchmen.Id,
                        HenchmenName = henchmen.HenchmenName,
                        HenchmenSet = henchmen.HenchmenSetName,
                        HenchmenInfo = henchmen
                    });
            }

            return returnList;
        }

        public static Henchmen GetNewHenchmen(List<Mastermind> allMastermindsInGame, Scheme scheme, List<Villain> villains, List<Henchmen> henchmenInGame)
        {
            //Get Henchmen
            var henchmenList = ConvertToHenchmenList(HenchmenRepository.All.ToList());

            //Remove all Henchmen currently in the game from the list
            var idsInGame = new HashSet<int>(henchmenInGame.Select(h => h.Id));
            var remainingHenchmen = henchmenList.Where(h => !idsInGame.Contains(h.Id)).ToList();

			//If a henchmen with duplicates is in the game then this will remove all duplicates from the pool to choose from
            foreach (var henchmanInGame in henchmenInGame)
            {
                if(henchmanInGame.HenchmenInfo.IsDuplicate)
                {
                    var duplicateHenchmenList = HenchmenRepository.All.Where(h => henchmanInGame.HenchmenInfo.DuplicateHenchmenIds.Contains(h.Id)).ToList();
                    idsInGame = new HashSet<int>(duplicateHenchmenList.Select(h => h.Id));
                    remainingHenchmen = remainingHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();
                }
            }

            //Get Henchmen that have played with the Scheme
            var schemeCard = new Card
            {
                CardName = scheme.SchemeName,
                CardType = (int)CardType.Scheme,
                SetId = (int)scheme.SetName
            };

            var henchmenCardsByScheme = SqlHelper.GetCardRelationships(CardType.Henchmen, schemeCard);
            var henchmenByScheme = ConvertToHenchmenList(henchmenCardsByScheme);

            //Remove all Henchmen that have played with the scheme from the list
            idsInGame = new HashSet<int>(henchmenByScheme.Select(h => h.Id));
            remainingHenchmen = remainingHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();

            //Get Henchmen that have played with each of the Masterminds with
            foreach (var mastermind in allMastermindsInGame)
            {
                var mastermindCard = new Card
                {
                    CardName = mastermind.MastermindName,
                    CardType = (int)CardType.Mastermind,
                    SetId = (int)mastermind.SetName
                };

                var henchmenCardsByMastermind = SqlHelper.GetCardRelationships(CardType.Henchmen, mastermindCard);
                var henchmenByMastermind = ConvertToHenchmenList(henchmenCardsByMastermind);
                //Remove all Henchmen that have played with the Mastermind(s)

                idsInGame = new HashSet<int>(henchmenByMastermind.Select(h => h.Id));
                remainingHenchmen = remainingHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();
            }

            //Get Henchmen that have played with each of the Villains with
            foreach (var villain in villains)
            {
                var villainCard = new Card
                {
                    CardName = villain.VillainName,
                    CardType = (int)CardType.Villain,
                    SetId = (int)villain.SetName
                };

                var henchmenCardsByVillain = SqlHelper.GetCardRelationships(CardType.Henchmen, villainCard);
                var henchmenByVillain = ConvertToHenchmenList(henchmenCardsByVillain);

                //Remove all Henchmen that have played with the Villains
                idsInGame = new HashSet<int>(henchmenByVillain.Select(h => h.Id));
                remainingHenchmen = remainingHenchmen.Where(h => !idsInGame.Contains(h.Id)).ToList();
            }

            //Get Henchmen that have played with each of the Henchmen with
            foreach (var h in henchmenInGame)
            {
                var henchmenCard = new Card
                {
                    CardName = h.HenchmenName,
                    CardType = (int)CardType.Henchmen,
                    SetId = (int)h.HenchmenSet
                };

                var henchmenCardsByHenchmen = SqlHelper.GetCardRelationships(CardType.Henchmen, henchmenCard);
                var henchmenByHenchmen = ConvertToHenchmenList(henchmenCardsByHenchmen);

                //Remove all Henchmen that have played with the Henchmen
                idsInGame = new HashSet<int>(henchmenByHenchmen.Select(hm => h.Id));
                remainingHenchmen = remainingHenchmen.Where(hm => !idsInGame.Contains(h.Id)).ToList();
            }

            //Select Henchmen from remaining list
            var henchmen = remainingHenchmen[RandomHelper.Instance.Next(remainingHenchmen.Count)];
            var henchmenInfo = HenchmenRepository.All.First(h => h.HenchmenName == henchmen.HenchmenName);

            if(henchmenInfo.IsDuplicate)
            {
                henchmenInfo = GetDuplicateHenchmen(henchmenInfo);
            }

            return GetNewHenchmen(henchmenInfo);
        }

        public static string ToString(List<Henchmen> henchmenList)
        {
            var returnString = "\r\n";
            var counter = 1;
            var orderedHenchmenList = henchmenList.OrderBy(x => (int)x.HenchmenSet).ToList();

            foreach (var henchmen in orderedHenchmenList)
            {
                returnString = $"{returnString}{counter}) {henchmen.HenchmenName.Split('_').First()}, {henchmen.HenchmenSet.GetDescription()}\r\n";
                counter++;
            }

            return $"{returnString.Remove(returnString.Length - 2)}\r\n";
        }

        public static List<string> GetListOfHenchmen()
        {
            var returnList = HenchmenRepository.All.Select(h => h.HenchmenName).ToList();
            return returnList;
        }

        public static HenchmenInfo GetRandomHenchmen()
        {
            var henchmen = HenchmenRepository.All[RandomHelper.Instance.Next(HenchmenRepository.All.Count)];
            return henchmen;
        }
    }
}
