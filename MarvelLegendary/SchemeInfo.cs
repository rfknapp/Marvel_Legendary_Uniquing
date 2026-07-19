using System.Collections.Generic;
using MarvelLegendary.Enums;


namespace MarvelLegendary
{
    public class SchemeInfo
    {
        public string SchemeName { get; set; }
        public List<int> SchemeTwists { get; set; }
        public Set SetName { get; set; }
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
        public bool isVeiled { get; set; }
        public int Id { get; set; }
        public List<int> DuplicateSchemeIds { get; set; }
        public bool IsDuplicate { get; set; }

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
        public List<Henchmen> RequiredHenchmen { get; set; }
        public int NumberHenchmenInHeroDeck { get; set; }
        public List<int> HenchmenNextToSchemePerPlayer { get; set; }
        public Henchmen HenchmenNextToScheme { get; set; }
        public bool IsHenchmenNextToScheme { get; set; }
        public bool HasAnnihilationHenchmen { get; set; }
        public bool IsHenchmenInHeroDeck { get; set; }
        public bool IsSmugglerHenchmen { get; set; }
        public bool IsXerogenHenchmen { get; set; }
        public bool IsVampireNeonaniteHenchmen { get; set; }
        public int NumberExtraHenchmenGroups { get; set; }

        //Villains
        public List<int> Villains { get; set; }
        public List<Villain> RequiredVillains { get; set; }
        public List<Villain> VillainsNotAllowed { get; set; }
        public string VillainCardNextToScheme { get; set; }
        public bool IsVillainCardNextToScheme { get; set; }
        public bool IsMonsterPitDeck { get; set; }
        public bool IsQuantumRealmDeck { get; set; }
        public bool IsMarvelZombies { get; set; }
        public List<string> MarvelZombiesGroup { get; set; }
        public Villain SchemeVillain { get; set; }
        public Keywords ZombieKeyword { get; set; }
        public int NumberOfSchemeVillains { get; set; }

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
        public bool IsEnshroudedMastermind { get; set; }

        //Heroes
        public List<int> Heroes { get; set; }
        public List<string> RequiredHeroes { get; set; }
        public List<Hero> HeroesInVillainDeck { get; set; }
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
        public bool IsShrinkTechHero { get; set; }
        public Hero ShrinkTechHero { get; set; }
        public int RoyalWeddingHeroCount { get; set; }
        public bool IsRandomHeroCardsInVillainDeck { get; set; }
        public int NumberRandomHeroCardsInVillainDeck { get; set; }
        public bool NoDuplicates { get; internal set; }

        //Sidekicks
        public int SidekicksInVillainDeck { get; set; }
        public bool IsSidekickInVillainDeck { get; set; }
        public bool IsLovedOne { get; internal set; }

        //Officers
        public int OfficersNextToMastermind { get; set; }
        public bool IsOfficersNextToMastermind { get; set; }
    }
}
