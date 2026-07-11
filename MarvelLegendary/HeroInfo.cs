using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MarvelLegendary.Enums;

namespace MarvelLegendary
{
    public class HeroInfo
    {
        public int Id { get; set; }
        public string HeroName { get; set; }
        public Set SetName { get; set; }
        public HeroTeam HeroTeam { get; set; }
        public bool IsDuplicate { get; set; }
        public string DuplicateName { get; set; }
        public bool IncludeNewRecruits { get; set; }
        public bool IncludeBindings { get; set; }
        public bool IncludeMadameHydra { get; set; }
        public List<Keywords> KeywordsList { get; set; } = new List<Keywords>();
        public List<Set> DuplicateHeroSets { get; set; } = new List<Set>();
        public List<int> DuplicateHeroIds { get; set; } = new List<int>();
        public bool IsEnabled { get; set; } = true;
    }

    public enum HeroTeam
    {
        [Description("Avengers")]
        Avengers,
        [Description("X-Men")]
        XMen,
        [Description("S.H.I.E.L.D.")]
        SHIELD,
        [Description("Spider Friends")]
        SpiderFriends,
        [Description("Marvel Knights")]
        MarvelKnights,
        [Description("X-Force")]
        XForce,
        [Description("Fantastic Four")]
        FantasticFour,
        [Description("Crime Syndicate")]
        CrimeSyndicate,
        [Description("Sinister Six")]
        SinisterSix,
        [Description("Foes Of Asgard")]
        FoesOfAsgard,
        [Description("Brotherhood")]
        Brotherhood,
        [Description("Guardians Of The Galaxy")]
        GuardiansOfTheGalaxy,
        [Description("H.Y.D.R.A.")]
        HYDRA,
        [Description("Illuminati")]
        Illuminati,
        [Description("Cabal")]
        Cabal,
        [Description("New Warriors")]
        NewWarriors,
        [Description("Mercs For Money")]
        MercsForMoney,
        [Description("Champions")]
        Champions,
        [Description("Warbound")]
        Warbound,
        [Description("Venomverse")]
        Venomverse,
        [Description("Unaffiliated")]
        Unaffiliated,
        [Description("Heroes of Asgard")]
        HeroesOfAsgard,
        [Description("Inhumans")]
        Inhumans,
        [Description("X-Factor Investigations")]
        XFactor,
        [Description("Wakanda")]
        Wakanda,
        [Description("Guardians of the Multiverse")]
        Multiverse
    };
}
