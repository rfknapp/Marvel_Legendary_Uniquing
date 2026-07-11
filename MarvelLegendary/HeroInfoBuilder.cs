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
                Id = 0,
                HeroName = "",
                SetName = Set.Core,
                HeroTeam = HeroTeam.Avengers,
                IsDuplicate = false,
                DuplicateName = "",
                IncludeNewRecruits = false,
                IncludeBindings = false,
                IncludeMadameHydra = false,
                KeywordsList = new List<Keywords>(),
                DuplicateHeroIds = new List<int>(),
                IsEnabled = true
            };
        }
        
        public HeroInfoBuilder HeroId(int id)
        {
            _heroInfo.Id = id;
            return this;
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

        public HeroInfoBuilder Duplicates(List<int> duplicateCardIds)
        {
            _heroInfo.DuplicateHeroIds = duplicateCardIds;
            _heroInfo.IsDuplicate = true;
            return this;
        }

        public HeroInfoBuilder Disable()
        {
            _heroInfo.IsEnabled = false;
            return this;
        }

        public HeroInfo Build()
        {
            return _heroInfo;
        }
    }
}
