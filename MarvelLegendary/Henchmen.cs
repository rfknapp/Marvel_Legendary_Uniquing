using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary
{
    public class HenchmenInfo
    {
        public string HenchmenName { get; set; }
        public GameInfo.Set HenchmenSetName { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeNewRecruits { get; set; }

        public HenchmenInfo(string name, GameInfo.Set set, bool includeNewRecruits = false)
        {
            HenchmenName = name;
            HenchmenSetName = set;
            IsDuplicate = false;
            DuplicateName = "";
            IncludeNewRecruits = includeNewRecruits;
        }

        public HenchmenInfo(string name, GameInfo.Set set, string duplicateMatch, bool includeNewRecruits = false)
        {
            HenchmenName = name;
            HenchmenSetName = set;
            IsDuplicate = true;
            DuplicateName = duplicateMatch;
            IncludeNewRecruits = includeNewRecruits;
        }
    }

    public class Henchmen
    {
        public GameInfo.Set HenchmenSet;
        public string HenchmenName;
        public HenchmenInfo HenchmenInfo { get; set; }

        private readonly List<HenchmenInfo> _henchmen = new List<HenchmenInfo>()
        {
            new HenchmenInfo("Doombot Legion", GameInfo.Set.Core, "Ten Ring Fantatics"),
            new HenchmenInfo("Hand Ninjas", GameInfo.Set.Core, "HYDRA Piots"),
            new HenchmenInfo("Savage Land Mutates", GameInfo.Set.Core, "HYDRA Spies"),
            new HenchmenInfo("Sentinels", GameInfo.Set.Core, "Hammer Drone Army"),
            
            new HenchmenInfo("Maggia Goons", GameInfo.Set.Dc),
            new HenchmenInfo("Phalanx", GameInfo.Set.Dc),
            
            new HenchmenInfo("Asgardian Warriors", GameInfo.Set.Villains),
            new HenchmenInfo("Cops", GameInfo.Set.Villains, true),
            new HenchmenInfo("Multiple Man", GameInfo.Set.Villains),
            new HenchmenInfo("S.H.I.E.L.D. Assault Squad", GameInfo.Set.Villains),
             
            new HenchmenInfo("Ghost Racers", GameInfo.Set.Sw1),
            new HenchmenInfo("M.O.D.O.K.s", GameInfo.Set.Sw1),
            new HenchmenInfo("Thor Corps", GameInfo.Set.Sw1),
             
            new HenchmenInfo("Khonshu Guardians", GameInfo.Set.Sw2),
            new HenchmenInfo("Magma Men", GameInfo.Set.Sw2),
            new HenchmenInfo("Spider-Infected", GameInfo.Set.Sw2),
             
            new HenchmenInfo("Cape-killers", GameInfo.Set.Cw),
            new HenchmenInfo("Mandroids", GameInfo.Set.Cw),
             
            new HenchmenInfo("Circus of Crime", GameInfo.Set.ThreeD),
            new HenchmenInfo("Spider-Slayer", GameInfo.Set.ThreeD),
            
            new HenchmenInfo("The Brood", GameInfo.Set.XMen),
            new HenchmenInfo("Hellfire Cult", GameInfo.Set.XMen),
            new HenchmenInfo("Sapien League", GameInfo.Set.XMen),
            new HenchmenInfo("Shi'ar Death Commandos", GameInfo.Set.XMen),
            new HenchmenInfo("Shi'ar Patrol Craft", GameInfo.Set.XMen),
             
            new HenchmenInfo("Cytoplasm Spikes", GameInfo.Set.Wwh),
            new HenchmenInfo("Death's Heads", GameInfo.Set.Wwh),
            new HenchmenInfo("Sakaaran Hivelings", GameInfo.Set.Wwh),
            
            new HenchmenInfo("Hammer Drone Army (Sentinels)", GameInfo.Set.P1, "Sentinels"),
            new HenchmenInfo("HYDRA Pilots (Hand Ninjas)", GameInfo.Set.P1, "Hand Ninjas"),
            new HenchmenInfo("HYDRA Spies (Savage Land Mutates)", GameInfo.Set.P1, "Savage Land Mutates"),
            new HenchmenInfo("Ten Rings Fanatics (Doombot Legion)", GameInfo.Set.P1, "Doombot Legion"),
            
            new HenchmenInfo("HYDRA Base", GameInfo.Set.Revelations),
            new HenchmenInfo("Mandarin's Rings", GameInfo.Set.Revelations),

            new HenchmenInfo("Sidera Maris, Bridge Builders", GameInfo.Set.Cosmos),
            new HenchmenInfo("Universal Church of Truth", GameInfo.Set.Cosmos),

            new HenchmenInfo("Mr. Sinister Clones", GameInfo.Set.Messiah),
            new HenchmenInfo("Sentinel Squad O*N*E*", GameInfo.Set.Messiah),

            new HenchmenInfo("Giants of Jotunheim", GameInfo.Set.WhatIf),
            new HenchmenInfo("Ultron Sentries", GameInfo.Set.WhatIf),
            new HenchmenInfo("Vibranium Liberator Drones", GameInfo.Set.WhatIf),

            new HenchmenInfo("Quantonauts", GameInfo.Set.AntmanWasp),
            new HenchmenInfo("Quantum Hounds", GameInfo.Set.AntmanWasp),
            new HenchmenInfo("Tardigrade", GameInfo.Set.AntmanWasp)
        };

        public Henchmen()
        {
            var henchmen = _henchmen[new Random().Next(_henchmen.Count)];
            if(henchmen.HenchmenName.Contains('('))
            {
                henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
            }

            while (henchmen == null)
            {
                henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                if (henchmen.HenchmenName.Contains('('))
                {
                    henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
                }
            }

            HenchmenName = henchmen.HenchmenName;
            HenchmenSet = henchmen.HenchmenSetName;
            HenchmenInfo = henchmen;
        }

        /*public Henchmen(string henchmenName)
        {
            var henchmen = henchmenName != "" ? _henchmen.FirstOrDefault(x=>x.HenchmenName==henchmenName) : _henchmen[new Random().Next(_henchmen.Count)];

            if (henchmen != null && henchmen.HenchmenName.Contains('('))
            {
                henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
            }

            while (henchmen == null)
            {
                henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                if (henchmen.HenchmenName.Contains('('))
                {
                    henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
                }
            }

            HenchmenName = henchmen.HenchmenName;
            HenchmenSet = henchmen.HenchmenSetName;
            HenchmenInfo = henchmen;
        }*/

        public Henchmen GetNewHenchmen(string henchmenName = "")
        {
            var henchmen = henchmenName;
            if (string.IsNullOrEmpty(henchmen))
            {
                var allHenchmen = GetListOfHenchmen();
                henchmen = allHenchmen[new Random().Next(allHenchmen.Count)];
            }

            var henchmenInfo = _henchmen.FirstOrDefault(h => h.HenchmenName == henchmen);

            HenchmenName = henchmen;
            HenchmenSet = henchmenInfo.HenchmenSetName;
            HenchmenInfo = henchmenInfo;

            return this;
        }

        public Henchmen(List<string> exclusionHenchmen)
        {
            var henchmen = _henchmen[new Random().Next(_henchmen.Count)];
            if (henchmen.HenchmenName.Contains('('))
            {
                henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
            }

            while (henchmen == null)
            {
                henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                if (henchmen.HenchmenName.Contains('('))
                {
                    henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
                }
            }
            
            if (exclusionHenchmen.Count < _henchmen.Count)
            {
                while (exclusionHenchmen.Any(x => x == henchmen.HenchmenName.Split('_').First()))
                {
                    henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                    if (henchmen.HenchmenName.Contains('('))
                    {
                        var tempName = henchmen.HenchmenName.Split('(')[1].Split(')')[0];
                        henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == tempName);
                    }

                    while (henchmen == null)
                    {
                        henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                        if (henchmen.HenchmenName.Contains('('))
                        {
                            henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
                        }
                    }
                }
            }

            HenchmenName = henchmen.HenchmenName;
            HenchmenSet = henchmen.HenchmenSetName;
            HenchmenInfo = henchmen;
        }

        public Henchmen GetNewHenchmen(List<string> exclusionHenchmen)
        {
            var henchmenName = GetRandomHenchmen();
            var henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmenName);

            //In Phase 1 there were Henchmen that were clones of the Henchmen released in the base game.
            //This will choose the base game versions of those Henchmen
            if (henchmenName.Contains('('))
            {
                henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
            }

            if (exclusionHenchmen.Count < _henchmen.Count)
            {
                while (exclusionHenchmen.Any(x => x == henchmen.HenchmenName.Split('_').First()))
                {
                    henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                    if (henchmen.HenchmenName.Contains('('))
                    {
                        var tempName = henchmen.HenchmenName.Split('(')[1].Split(')')[0];
                        henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == tempName);
                    }

                    while (henchmen == null)
                    {
                        henchmen = _henchmen[new Random().Next(_henchmen.Count)];
                        if (henchmen.HenchmenName.Contains('('))
                        {
                            henchmen = _henchmen.FirstOrDefault(x => x.HenchmenName == henchmen.DuplicateName);
                        }
                    }
                }
            }

            HenchmenName = henchmen.HenchmenName;
            HenchmenSet = henchmen.HenchmenSetName;
            HenchmenInfo = henchmen;

            return this;
        }

        public Henchmen(List<HenchmenInfo> henchmenInfoList)
        {
            var henchmen = henchmenInfoList[new Random().Next(henchmenInfoList.Count)];

            HenchmenName = henchmen.HenchmenName;
            HenchmenSet = henchmen.HenchmenSetName;
            HenchmenInfo = henchmen;
        }

        public string ToString(List<Henchmen> henchmenList)
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

        public List<HenchmenInfo> ModifyHenchmenList(List<string> henchmenExclusions)
        {
            var returnList = new List<HenchmenInfo>(_henchmen);

            foreach (var henchmenExclusion in henchmenExclusions)
            {
                while (returnList.Any(x => x.HenchmenName == henchmenExclusion))
                {
                    var itemToRemove = returnList.Single(x => x.HenchmenName == henchmenExclusion);
                    returnList.Remove(itemToRemove);
                }
            }

            return returnList;
        }

        public List<string> GetListOfHenchmen()
        {
            var allHenchmenQuery = "SELECT [HenchmenName] FROM [Henchmen]";
            var allHenchmen = new SqlHelper().GetList(allHenchmenQuery);
            return allHenchmen;
        }

        public string GetRandomHenchmen()
        {
            var allHenchmen = GetListOfHenchmen();
            var henchmen = allHenchmen[new Random().Next(allHenchmen.Count)];
            return henchmen;
        }
    }
}
