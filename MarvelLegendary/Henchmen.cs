using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelLegendary.Enums;

namespace MarvelLegendary
{
    public class HenchmenInfo
    {
        public string HenchmenName { get; set; }
        public Set HenchmenSetName { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeNewRecruits { get; set; }

        public HenchmenInfo(string name, Set set, bool includeNewRecruits = false)
        {
            HenchmenName = name;
            HenchmenSetName = set;
            IsDuplicate = false;
            DuplicateName = "";
            IncludeNewRecruits = includeNewRecruits;
        }

        public HenchmenInfo(string name, Set set, string duplicateMatch, bool includeNewRecruits = false)
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
        public Set HenchmenSet;
        public string HenchmenName;
        public HenchmenInfo HenchmenInfo { get; set; }

        private readonly List<HenchmenInfo> _henchmen = new List<HenchmenInfo>()
        {
            new HenchmenInfo("Doombot Legion", Set.Core, "Ten Ring Fantatics"),
            new HenchmenInfo("Hand Ninjas", Set.Core, "HYDRA Piots"),
            new HenchmenInfo("Savage Land Mutates", Set.Core, "HYDRA Spies"),
            new HenchmenInfo("Sentinels", Set.Core, "Hammer Drone Army"),
            
            new HenchmenInfo("Maggia Goons", Set.Dc),
            new HenchmenInfo("Phalanx", Set.Dc),
            
            new HenchmenInfo("Asgardian Warriors", Set.Villains),
            new HenchmenInfo("Cops", Set.Villains, true),
            new HenchmenInfo("Multiple Man", Set.Villains),
            new HenchmenInfo("S.H.I.E.L.D. Assault Squad", Set.Villains),
             
            new HenchmenInfo("Ghost Racers", Set.Sw1),
            new HenchmenInfo("M.O.D.O.K.s", Set.Sw1),
            new HenchmenInfo("Thor Corps", Set.Sw1),
             
            new HenchmenInfo("Khonshu Guardians", Set.Sw2),
            new HenchmenInfo("Magma Men", Set.Sw2),
            new HenchmenInfo("Spider-Infected", Set.Sw2),
             
            new HenchmenInfo("Cape-killers", Set.Cw),
            new HenchmenInfo("Mandroids", Set.Cw),
             
            new HenchmenInfo("Circus of Crime", Set.ThreeD),
            new HenchmenInfo("Spider-Slayer", Set.ThreeD),
            
            new HenchmenInfo("The Brood", Set.XMen),
            new HenchmenInfo("Hellfire Cult", Set.XMen),
            new HenchmenInfo("Sapien League", Set.XMen),
            new HenchmenInfo("Shi'ar Death Commandos", Set.XMen),
            new HenchmenInfo("Shi'ar Patrol Craft", Set.XMen),
             
            new HenchmenInfo("Cytoplasm Spikes", Set.Wwh),
            new HenchmenInfo("Death's Heads", Set.Wwh),
            new HenchmenInfo("Sakaaran Hivelings", Set.Wwh),
            
            new HenchmenInfo("Hammer Drone Army (Sentinels)", Set.P1, "Sentinels"),
            new HenchmenInfo("HYDRA Pilots (Hand Ninjas)", Set.P1, "Hand Ninjas"),
            new HenchmenInfo("HYDRA Spies (Savage Land Mutates)", Set.P1, "Savage Land Mutates"),
            new HenchmenInfo("Ten Rings Fanatics (Doombot Legion)", Set.P1, "Doombot Legion"),
            
            new HenchmenInfo("HYDRA Base", Set.Revelations),
            new HenchmenInfo("Mandarin's Rings", Set.Revelations),

            new HenchmenInfo("Sidera Maris, Bridge Builders", Set.Cosmos),
            new HenchmenInfo("Universal Church of Truth", Set.Cosmos),

            new HenchmenInfo("Mr. Sinister Clones", Set.Messiah),
            new HenchmenInfo("Sentinel Squad O*N*E*", Set.Messiah),

            new HenchmenInfo("Giants of Jotunheim", Set.WhatIf),
            new HenchmenInfo("Ultron Sentries", Set.WhatIf),
            new HenchmenInfo("Vibranium Liberator Drones", Set.WhatIf),

            new HenchmenInfo("Quantonauts", Set.AntmanWasp),
            new HenchmenInfo("Quantum Hounds", Set.AntmanWasp),
            new HenchmenInfo("Tardigrade", Set.AntmanWasp)
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
