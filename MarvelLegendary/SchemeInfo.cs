using System.Collections.Generic;

namespace MarvelLegendary
{
    public class SchemeInfo
    {
        public string SchemeName { get; set; }
        public List<int> SchemeTwists { get; set; }
        public string SetName { get; set; }
        public bool CannotBeSolo { get; set; }
        public int ShardCount { get; set; }
        public bool IsShardCount { get; set; }
        public bool HasBetryalDeck { get; set; }
        public bool IsSchemeTwistsNextToScheme { get; set; }
        public int NumberTwistsNextToScheme { get; set; }
        public bool HasAmbitions { get; set; }
        public int VillainOfficerCount { get; set; }
        public bool IsVillainOfficer { get; set; }
        public bool IsMonumentDeck { get; set; }
        public bool IsInfectedDeck { get; set; }
        public bool IncludeNewRecruits { get; set; }
        public bool IncludeMadameHydra { get; set; }
        public bool IncludeHorrors { get; set; }
        public bool IsRoyalWedding { get; set; }

        //Wounds/Bindings
        public int WoundCount { get; set; }
        public int BindingCount { get; set; }
        public bool CustomWoundCount { get; set; }
        public bool CustomBindingCount { get; set; }
        public List<int> WoundPerPlayer { get; set; }
        public List<int> BindingPerPlayer { get; set; }
        public bool WoundsPerPlayer { get; set; }
        public bool BindingsPerPlayer { get; set; }

        //Bystanders
        public List<int> BystandersInHeroDeck { get; set; }
        public List<int> Bystanders { get; set; }
        public int BystandersNextToScheme { get; set; }
        public bool IsBystandersNextToScheme { get; set; }
        public bool IsBystandersInHeroDeck { get; set; }
        public int AdditionalBystanders { get; set; }

        //Henchmen
        public List<int> Henchmen { get; set; }
        public List<string> RequiredHenchmen { get; set; }
        public int NumberHenchmenInHeroDeck { get; set; }
        public List<int> HenchmenNextToSchemePerPlayer { get; set; }
        public string HenchmenNextToScheme { get; set; }
        public bool IsHenchmenNextToScheme { get; set; }
        public bool HasAnnihilationHenchmen { get; set; }
        public bool IsHenchmenInHeroDeck { get; set; }
        public bool IsSmugglerHenchmen { get; set; }
        public bool IsXerogenHenchmen { get; set; }

        //Villains
        public List<int> Villains { get; set; }
        public List<string> RequiredVillains { get; set; }
        public List<string> VillainsNotAllowed { get; set; }
        public string VillainCardNextToScheme { get; set; }
        public bool IsVillainCardNextToScheme { get; set; }
        public bool IsMonsterPitDeck { get; set; }

        //Masterminds
        public int NumberOfMasterminds { get; set; }
        public int NumberExtraMasterminds { get; set; }
        public bool IsDarkAllianceMastermind { get; set; }
        public bool IsTyrantVillain { get; set; }
        public bool IsSecretWarsMasterminds { get; set; }
        public bool IsTacticsInVillainDeck { get; set; }
        public bool IsExtraMasterminds { get; set; }
        public bool IsWorldWarHulkMasterminds { get; set; }
        public bool IsDrainedMastermind { get; set; }
        public Mastermind DrainedMastermind { get; set; }
        public bool IncludeExtraAlwaysLeadsVillains { get; set; }

        //Heroes
        public List<int> Heroes { get; set; }
        public List<string> RequiredHeroes { get; set; }
        public List<string> HeroesInVillainDeck { get; set; }
        public string DarkLoyaltyHero { get; set; }
        public bool IsHeroesInVillainDeck { get; set; }
        public bool IsRandomHeroesInVillainDeck { get; set; }
        public int NumberOfHeroesInVillainDeck { get; set; }
        public bool Is3v3 { get; set; }
        public bool Is4v2 { get; set; }
        public HeroTeam IncludeHeroTeam { get; set; }
        public int NumberOfHeroesFromTeam { get; set; }
        public bool IsIncludeHeroTeam { get; set; }
        public bool IsHeroNameLimit { get; set; }
        public int NumberOfHeroesWithNameString { get; set; }
        public string CustomNameString { get; set; }
        public bool IsMutationDeck { get; set; }
        public bool IsHulkDeck { get; set; }
        public bool IsDarkLoyalty { get; set; }
        public bool IsSoulsHero { get; set; }
        public Hero SoulsHero { get; set; }
        public int RoyalWeddingHeroCount { get; set; }

        //Sidekicks
        public int SidekicksInVillainDeck { get; set; }
        public bool IsSidekickInVillainDeck { get; set; }
    }
}
