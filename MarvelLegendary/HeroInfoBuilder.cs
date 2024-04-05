using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MarvelLegendary.Enums;

namespace MarvelLegendary
{
    public class HeroInfoBuilder
    {
        private HeroInfo _heroInfo;

        public HeroInfoBuilder()
        {
            _heroInfo = new HeroInfo
            {
                HeroName = "",
                SetName = Set.Core,
                HeroTeam = HeroTeam.Avengers,
                IsDuplicate = false,
                DuplicateName = "",
                IncludeNewRecruits = false,
                IncludeBindings = false,
                IncludeMadameHydra = false,
                KeywordsList = new List<Keywords>()
            };
        }

        public HeroInfoBuilder SetHeroName(string name)
        {
            _heroInfo.HeroName = name;
            return this;
        }

        public HeroInfoBuilder SetHeroTeam(HeroTeam team)
        {
            _heroInfo.HeroTeam = team;
            return this;
        }

        public HeroInfoBuilder SetGameSet(Set gameSet)
        {
            _heroInfo.SetName = gameSet;
            return this;
        }

        public HeroInfoBuilder IncludeNewRecruits()
        {
            _heroInfo.IncludeNewRecruits = true;
            return this;
        }

        public HeroInfoBuilder IncludeBindings()
        {
            _heroInfo.IncludeBindings = true;
            return this;
        }

        public HeroInfoBuilder IncludeMadameHydra()
        {
            _heroInfo.IncludeMadameHydra = true;
            return this;
        }

        public HeroInfoBuilder SetKeywords(List<Keywords> keywords)
        {
            _heroInfo.KeywordsList = keywords;
            return this;
        }

        public HeroInfo Build()
        {
            return _heroInfo;
        }
    }
}
