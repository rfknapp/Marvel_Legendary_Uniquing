using MarvelLegendary.Exclusions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarvelLegendary
{
    public class Scheme
    {
        public string SchemeName { get; set; }
        public string SetName { get; set; }
        public int Twists { get; set; }
        public int NumberOfSchemeTwists { get; set; }
        public SchemeInfo SchemeInfo { get; set; }
        public bool IsSchemeTwistsNextToScheme { get; set; }
        public int NumberTwistsNextToScheme { get; set; }
        public int NumberOfPlayers { get; set; }

        public int NumberOfHeroes { get; set; }
        public List<string> RequiredHeroes { get; set; }
        public List<string> HeroesInVillainDeck { get; set; }
        public int RandomHeroesInVillainDeck { get; set; }

        public int NumberOfVillains { get; set; }
        public List<string> RequiredVillains { get; set; }

        public int NumberOfMasterminds { get; set; }

        public int NumberOfHenchmen { get; set; }
        public List<string> RequiredHenchmen { get; set; }

        public bool WoundsPerPlayer { get; set; }
        public bool CustomWoundNumber { get; set; }
        public List<int> Wounds { get; set; }

        public int BystandersInVillainDeck { get; set; }
        public int BystandersInHeroDeck { get; set; }
        public bool IsBystandersInHeroDeck { get; set; }

        private readonly List<SchemeInfo> _schemes = new List<SchemeInfo>()
        {
            new SchemeInfoBuilder().SetSchemeName("The Legacy Virus").SetWoundCount(true, 6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Midtown Bank Robbery").SetBystanderCount(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Negative Zone Prison Breakout").AddAdditionalHenchmen(1).CannotBeSolo().Build(),
            new SchemeInfoBuilder().SetSchemeName("Portals to The Dark Dimension").SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders With Killbots").SetSchemeTwists(5).SetSchemesNextToTwist(3).SetBystanderCount(18).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Invasion of the Skrull Shapeshifters").SetHeroCount(6).SetRequiredVillains("Skrulls").Build(),
            new SchemeInfoBuilder().SetSchemeName("Super Hero Civil War").CannotBeSolo().SetSchemeTwists(new List<int> { 0,8,8,5,5}).SetHeroCount(new List<int> {0,4,5,5,6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube").Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Capture Baby Hope").SetSchemeSet(GameInfo.Set.Dc).Build(),
            new SchemeInfoBuilder().SetSchemeName("Detonate the Helicarrier").SetSchemeSet(GameInfo.Set.Dc).SetHeroCount(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Massive Earthquake Generator").SetSchemeSet(GameInfo.Set.Dc).Build(),
            new SchemeInfoBuilder().SetSchemeName("Organized Crimewave").SetSchemeSet(GameInfo.Set.Dc).SetRequiredHenchmen("Maggia Goons").Build(),
            new SchemeInfoBuilder().SetSchemeName("Save Humanity").SetSchemeSet(GameInfo.Set.Dc).SetHeroBystanderCount(new List<int> { 12, 24, 24, 24, 24}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Steal the Weaponized Plutonium").SetSchemeSet(GameInfo.Set.Dc).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Transform Citizens into Demons").SetSchemeSet(GameInfo.Set.Dc).HeroesInVillainDeck("Jean Grey").SetBystanderCount(0).Build(),
            new SchemeInfoBuilder().SetSchemeName("X-Cutioner's Song").SetSchemeSet(GameInfo.Set.Dc).HeroesInVillainDeck(1).SetBystanderCount(0).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Bathe Earth in Cosmic Rays").SetSchemeSet(GameInfo.Set.Ff).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Flood the Planet with Melted Glaciers").SetSchemeSet(GameInfo.Set.Ff).Build(),
            new SchemeInfoBuilder().SetSchemeName("Invincible Force Field").SetSchemeSet(GameInfo.Set.Ff).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Reality into the Negative Zone").SetSchemeSet(GameInfo.Set.Ff).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Invade the Daily Bugle News HQ").SetSchemeSet(GameInfo.Set.PttR).IncludeHenchmenInHeroDeck(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Splice Humans with Spider DNA").SetSchemeSet(GameInfo.Set.PttR).SetRequiredVillains("Sinister Six").Build(),
            new SchemeInfoBuilder().SetSchemeName("The Clone Saga").SetSchemeSet(GameInfo.Set.PttR).Build(),
            new SchemeInfoBuilder().SetSchemeName("Weave a Web of Lies").SetSchemeSet(GameInfo.Set.PttR).SetSchemeTwists(7).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Build an Underground MegaVault Prison").SetSchemeSet(GameInfo.Set.Villains).SetBindingCount(true,5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cage Villains in Power-Suppressing Cells").SetSchemeSet(GameInfo.Set.Villains).NumberHenchmenNextToScheme(2, "Cops").Build(),
            new SchemeInfoBuilder().SetSchemeName("Crown Thor King of Asgard").SetSchemeSet(GameInfo.Set.Villains).SetVillainCardNextToScheme("Thor").Build(),
            new SchemeInfoBuilder().SetSchemeName("Crush HYDRA").SetSchemeSet(GameInfo.Set.Villains).IncludeNewRecruits().IncludeMadameHydra().Build(),
            new SchemeInfoBuilder().SetSchemeName("Graduation at Xavier's X-Academy").SetSchemeSet(GameInfo.Set.Villains).SetBystandersNextToScheme(8).Build(),
            new SchemeInfoBuilder().SetSchemeName("Infiltrate the Lair with Spies").SetSchemeSet(GameInfo.Set.Villains).SetBystandersNextToScheme(21).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mass Produce War Machine Armor").SetSchemeSet(GameInfo.Set.Villains).SetRequiredHenchmen("S.H.I.E.L.D. Assault Squad").Build(),
            new SchemeInfoBuilder().SetSchemeName("Resurrect Heroes with Norn Stones").SetSchemeSet(GameInfo.Set.Villains).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Forge the Infinity Gauntlet").SetSchemeSet(GameInfo.Set.GotG).SetRequiredVillains("Infinity Gems").Build(),
            new SchemeInfoBuilder().SetSchemeName("Intergalactic Kree Nega-Bomb").SetSchemeSet(GameInfo.Set.GotG).SetBystandersNextToScheme(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Kree-Skrull War").SetSchemeSet(GameInfo.Set.GotG).SetRequiredVillains(new List<string> { "Kree Starforce", "Skrulls" }).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unite the Shards").SetSchemeSet(GameInfo.Set.GotG).SetShardNumber(30).SetSchemeTwists(new List<int> { 6, 7, 8, 9, 10 }).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Fear Itself").SetSchemeSet(GameInfo.Set.Fi).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Last Stand at Avengers Tower").SetSchemeSet(GameInfo.Set.Fi).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Traitor").SetSchemeSet(GameInfo.Set.Fi).CannotBeSolo().SetBindingCount(true,3).HasBetrayalDeck().Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Build an Army of Annihilation").SetSchemeSet(GameInfo.Set.Sw1).SetSchemeTwists(9).SetAnnihilationHenchmen().Build(),
            new SchemeInfoBuilder().SetSchemeName("Corrupt the Next Generation of Heroes").SetSchemeSet(GameInfo.Set.Sw1).SidekicksInVillainDeck(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Crush Them with My Bare Hands").SetSchemeSet(GameInfo.Set.Sw1).SetVillainCount(new List<int> { 2, 2, 3, 3, 4 }).Build(),
            new SchemeInfoBuilder().SetSchemeName("Dark Alliance").SetSchemeSet(GameInfo.Set.Sw1).AddDarkAllianceMastermind(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Fragmented Realities").SetSchemeSet(GameInfo.Set.Sw1).AddAdditionalVillain(1).SetSchemeTwists(new List<int> { 2, 4, 6, 8, 10}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Master of Tyrants").SetSchemeSet(GameInfo.Set.Sw1).SetTyrantVillains().Build(),
            new SchemeInfoBuilder().SetSchemeName("Pan-Dimensional Plague").SetSchemeSet(GameInfo.Set.Sw1).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Smash Two Dimensions Together").SetSchemeSet(GameInfo.Set.Sw1).AddAdditionalVillain(1).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Deadlands Hordes Charge the Wall").SetSchemeSet(GameInfo.Set.Sw2).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enthrone the Barons of Battleworld").SetSchemeSet(GameInfo.Set.Sw2).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Fountain of Eternal Life").SetSchemeSet(GameInfo.Set.Sw2).SetSchemeTwists(new List<int> { 4, 8, 8, 8, 8 }).Build(),
            new SchemeInfoBuilder().SetSchemeName("The God-Emperor of Battleworld").SetSchemeSet(GameInfo.Set.Sw2).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Mark of Khonshu").SetSchemeSet(GameInfo.Set.Sw2).SetSchemeTwists(10).SetRequiredHenchmen("Khonshu Guardians").HeroesInVillainDeck(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Master the Mysteries of Kung-Fu").SetSchemeSet(GameInfo.Set.Sw2).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Wars").SetSchemeSet(GameInfo.Set.Sw2).SetSecretWarsMasterminds().Build(),
            new SchemeInfoBuilder().SetSchemeName("Sinister Ambitions").SetSchemeSet(GameInfo.Set.Sw2).SetSchemeTwists(6).SetAmbitions().Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Brainwash the Military").SetSchemeSet(GameInfo.Set.Ca).SetSchemeTwists(7).SetVillainOfficers(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Change the Outcome of WWII").SetSchemeSet(GameInfo.Set.Ca).SetSchemeTwists(7).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Go Back in Time to Slay Heroes' Ancestors").SetSchemeSet(GameInfo.Set.Ca).SetSchemeTwists(9).SetHeroCount(8).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Unbreakable Enigma Code").SetSchemeSet(GameInfo.Set.Ca).SetSchemeTwists(6).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Avengers vs. X-Men").SetSchemeSet(GameInfo.Set.Cw).SetSchemeTwists(9).AvengersVsXmen().Build(),
            new SchemeInfoBuilder().SetSchemeName("Dark Reign of H.A.M.M.E.R. Officers").SetSchemeSet(GameInfo.Set.Cw).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Epic Super Hero Civil War").SetSchemeSet(GameInfo.Set.Cw).SetHeroCount(new List<int> {4,5,5,5,6}).SetSchemeTwists(new List<int> { 9, 9, 9, 6, 6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Imprison Unregistered Superhumans").SetSchemeSet(GameInfo.Set.Cw).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Nitro the Supervillain Threatens Crowds").SetSchemeSet(GameInfo.Set.Cw).Build(),
            new SchemeInfoBuilder().SetSchemeName("Predict Future Crime").SetSchemeSet(GameInfo.Set.Cw).SetSchemeTwists(6).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Reveal Heroes' Secret Identities").SetSchemeSet(GameInfo.Set.Cw).SetSchemeTwists(6).SetHeroCount(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("United States Split by Civil War").SetSchemeSet(GameInfo.Set.Cw).SetSchemeTwists(10).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Deadpool Kills the Marvel Universe").SetSchemeSet(GameInfo.Set.Deadpool).SetHeroCount(new List<int> {4,5,5,5,6}).SetSchemeTwists(new List<int> { 6, 6, 6, 5, 5}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Deadpool Wants a Chimichanga").SetSchemeSet(GameInfo.Set.Deadpool).SetSchemeTwists(6).SetBystanderCount(12).SetVillainCount(new List<int> { 1, 2, 4, 4, 5}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Deadpool Writes a Scheme").SetSchemeSet(GameInfo.Set.Deadpool).SetNumberOfHeroWithNameLike(1, "Deadpool").SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Everybody Hates Deadpool").SetSchemeSet(GameInfo.Set.Deadpool).SetSchemeTwists(6).IncludeHeroTeams(1, HeroTeam.MercsForMoney).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Find the Split Personality Killer").SetSchemeSet(GameInfo.Set.Noir).Build(),
            new SchemeInfoBuilder().SetSchemeName("Silence the Witnesses").SetSchemeSet(GameInfo.Set.Noir).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Five Families of Crime").SetSchemeSet(GameInfo.Set.Noir).AddAdditionalVillain(2).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hidden Heart of Darkness").SetSchemeSet(GameInfo.Set.Noir).MastermindTacticsInVillainDeck().Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Alien Brood Encounters").SetSchemeSet(GameInfo.Set.XMen).AddAdditionalHenchmen(1).SetRequiredHenchmen("The Brood").SetBystanderCount(0).Build(),
            new SchemeInfoBuilder().SetSchemeName("Anti-Mutant Hatred ").SetSchemeSet(GameInfo.Set.XMen).SetSchemeTwists(11).SetWoundCount(false, 30).Build(),
            new SchemeInfoBuilder().SetSchemeName("Horror of Horrors").SetSchemeSet(GameInfo.Set.XMen).SetSchemeTwists(6).IncludeHorrors().Build(),
            new SchemeInfoBuilder().SetSchemeName("Mutant-Hunting Super Sentinels").SetSchemeSet(GameInfo.Set.XMen).SetSchemeTwists(9).AddAdditionalHenchmen(1).SetRequiredHenchmen("Sentinels").Build(),
            new SchemeInfoBuilder().SetSchemeName("Nuclear Armageddon").SetSchemeSet(GameInfo.Set.XMen).SetSchemeTwists(5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Televised Deathtraps of Mojo World").SetSchemeSet(GameInfo.Set.XMen).SetSchemeTwists(11).SetWoundCount(true, 6).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Dark Phoenix Saga").SetSchemeSet(GameInfo.Set.XMen).SetSchemeTwists(10).SetRequiredVillains("Hellfire Club").HeroesInVillainDeck("Jean Grey").Build(),
            new SchemeInfoBuilder().SetSchemeName("X-Men Danger Room goes Berserk").SetSchemeSet(GameInfo.Set.XMen).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Distract the Hero").SetSchemeSet(GameInfo.Set.Sm).IncludeHeroTeams(1, HeroTeam.SpiderFriends).Build(),
            new SchemeInfoBuilder().SetSchemeName("Explosion at the Washington Monument").SetSchemeSet(GameInfo.Set.Sm).MonumentDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Ferry Disaster").SetSchemeTwists(9).SetSchemeSet(GameInfo.Set.Sm).Build(),
            new SchemeInfoBuilder().SetSchemeName("Scavenge Alien Weaponry").SetSchemeSet(GameInfo.Set.Sm).IncludeSmugglerHenchmen().Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Clash of the Monsters Unleashed").SetSchemeSet(GameInfo.Set.Champions).IncludeMonsterPitDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Divide and Conquer").SetSchemeSet(GameInfo.Set.Champions).SetHeroCount(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hypnotize Every Human").SetSchemeSet(GameInfo.Set.Champions).AddAdditionalHenchmen(1).SetBystanderCount(0).Build(),
            new SchemeInfoBuilder().SetSchemeName("Steal All the Oxygen on Earth").SetSchemeSet(GameInfo.Set.Champions).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Break the Planet Asunder").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(9).SetHeroCount(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cytoplasm Spike Invasion").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(10).IncludeInfectedDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Fall of the Hulks").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(10).SetWoundCount(true, 6).SetNumberOfHeroWithNameLike(2,"Hulk").Build(),
            new SchemeInfoBuilder().SetSchemeName("Gladiator Pits of Sakaar").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mutating Gamma Rays").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(7).IncludeMutationDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Shoot Hulk into Space").SetSchemeSet(GameInfo.Set.Wwh).IncludeHulkDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Subjugate with Obedience Disks").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("World War Hulk").SetSchemeSet(GameInfo.Set.Wwh).SetSchemeTwists(9).SetWorldWarHulkMasterminds().Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Asgard Under Siege (Negative Zone Prison Breakout)").SetSchemeSet(GameInfo.Set.P1).AddAdditionalHenchmen(1).CannotBeSolo().Build(),
            new SchemeInfoBuilder().SetSchemeName("Destroy the Cities of Earth! (Midtown Bank Robbery)").SetSchemeSet(GameInfo.Set.P1).SetBystanderCount(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enslave Minds with the Chitauri Scepter (Secret Invasion of the Skrull Shapeshifters)").SetSchemeSet(GameInfo.Set.P1).SetHeroCount(6).SetRequiredVillains("Chitauri").Build(),
            new SchemeInfoBuilder().SetSchemeName("Invade Asgard (Portals to The Dark Dimension)").SetSchemeSet(GameInfo.Set.P1).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Radioactive Palladium Poisoning (The Legacy Virus)").SetSchemeSet(GameInfo.Set.P1).SetWoundCount(true, 6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders with HYDRA (Replace Earth's Leaders With Killbots)").SetSchemeSet(GameInfo.Set.P1).SetSchemeTwists(5).SetSchemesNextToTwist(3).SetBystanderCount(18).Build(),
            new SchemeInfoBuilder().SetSchemeName("Super Hero Civil War (Super Hero Civil War)").SetSchemeSet(GameInfo.Set.P1).CannotBeSolo().SetSchemeTwists(new List<int> { 0,8,8,5,5}).SetHeroCount(new List<int> {0,4,5,5,6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube (Unleash The Power Of The Cosmic Cube)").SetSchemeSet(GameInfo.Set.P1).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Age of Ultron").SetSchemeSet(GameInfo.Set.Antman).SetSchemeTwists(7).SetHeroCount(new List<int> { 3, 5, 5, 6, 7}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Earth Into Midieval Times").SetSchemeSet(GameInfo.Set.Antman).SetSchemeTwists(9).Build(),
            new SchemeInfoBuilder().SetSchemeName("Transform Commuters Into Giant Ants").SetSchemeSet(GameInfo.Set.Antman).SetSchemeTwists(new List<int> { 7, 8, 9, 10, 11}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trap Heroes In The Microverse").SetSchemeSet(GameInfo.Set.Antman).SetSchemeTwists(11).HeroesInVillainDeck(1).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Invasion of the Venom Symbiotes").SetSchemeSet(GameInfo.Set.Venom).AddAdditionalHenchmen(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Maximum Carnage").SetSchemeSet(GameInfo.Set.Venom).SetSchemeTwists(10).SetWoundCount(true,6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Paralyzing Venom").SetSchemeSet(GameInfo.Set.Venom).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Symbiotic Absorption").SetSchemeSet(GameInfo.Set.Venom).SetSchemeTwists(11).SetDrainedMastermind().Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Earthquake Drains the Ocean").SetSchemeSet(GameInfo.Set.Revelations).SetSchemeTwists(11).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("House of M").SetSchemeSet(GameInfo.Set.Revelations).HeroesInVillainDeck("Scarlet Witch").Is4v2().Build(),
            new SchemeInfoBuilder().SetSchemeName("The Korvac Saga").SetSchemeSet(GameInfo.Set.Revelations).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret HYDRA Corruption").SetSchemeSet(GameInfo.Set.Revelations).SetSchemeTwists(new List<int>{7,9,9,11,11}).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Hail Hydra").SetSchemeSet(GameInfo.Set.Shield).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hydra Helicarriers Hunt Heroes").SetSchemeSet(GameInfo.Set.Shield).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Empire of Betrayal").SetSchemeSet(GameInfo.Set.Shield).SetSchemeTwists(11).SetDarkLoyalty().Build(),
            new SchemeInfoBuilder().SetSchemeName("S.H.I.E.L.D. vs. Hydra War").SetSchemeSet(GameInfo.Set.Shield).SetSchemeTwists(7).SetRequiredVillains(new List<string>{"Hydra Elite", "A.I.M., Hydra Offshoot" }, 1).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Asgardian Test of Worth").SetSchemeSet(GameInfo.Set.Asgard).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Dark World of Svartalfheim").SetSchemeSet(GameInfo.Set.Asgard).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Ragnarok, Twilight of the Gods").SetSchemeSet(GameInfo.Set.Asgard).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("War of the Frost Giants").SetSchemeSet(GameInfo.Set.Asgard).SetSchemeTwists(9).Build(),
            
            new SchemeInfoBuilder().SetSchemeName("Crash the Moon into the Sun").SetSchemeSet(GameInfo.Set.NewMutants).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Demon Bear Saga").SetSchemeSet(GameInfo.Set.NewMutants).SetRequiredVillains("Demons of Limbo").Build(),
            new SchemeInfoBuilder().SetSchemeName("Superhuman Baseball Game").SetSchemeSet(GameInfo.Set.NewMutants).SetSchemeTwists(9).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trapped in the Insane Asylum").SetSchemeSet(GameInfo.Set.NewMutants).SetSchemeTwists(new List<int>{3,5,7,9,11}).Build(),

            new SchemeInfoBuilder().SetSchemeName("Annihilation Conquest").SetSchemeSet(GameInfo.Set.Cosmos).SetSchemeTwists(11).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Contest of Champions").SetSchemeSet(GameInfo.Set.Cosmos).SetSchemeTwists(11).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Destroy the Nova Corps").SetSchemeSet(GameInfo.Set.Cosmos).SetSchemeTwists(9).SetHeroCount(new List<int>(){ 5, 5, 5, 5, 6 }).SetNumberOfHeroWithNameLike(1, "Nova").Build(),
            new SchemeInfoBuilder().SetSchemeName("Turn the Soul of Adam Warlock").SetSchemeSet(GameInfo.Set.Cosmos).SetSchemeTwists(14).SetSoulsDeck("Adam Warlock").Build()
        };

        public Scheme(int playerCount, string schemeName)
        {
            var schemeInfo = _schemes.First(x => x.SchemeName == schemeName);

            if (schemeInfo.SchemeName == "The Kree-Skrull War" && playerCount < 3)
            {
                playerCount = 3;
            }

            if (schemeInfo.RequiredHenchmen.Count > 0)
            {
                playerCount = 4;
            }

            while (schemeInfo.CannotBeSolo && playerCount == 1)
            {
                schemeInfo = _schemes[new Random().Next(_schemes.Count)];
            }

            SchemeName = schemeInfo.SchemeName;
            SetName = schemeInfo.SetName;
            Twists = schemeInfo.SchemeTwists[playerCount - 1];
            NumberOfSchemeTwists = schemeInfo.SchemeTwists[playerCount - 1];
            SchemeInfo = schemeInfo;
            IsSchemeTwistsNextToScheme = schemeInfo.IsSchemeTwistsNextToScheme;
            NumberTwistsNextToScheme = schemeInfo.NumberTwistsNextToScheme;
            NumberOfPlayers = playerCount;

            NumberOfMasterminds = schemeInfo.NumberOfMasterminds;

            NumberOfVillains = schemeInfo.Villains[playerCount - 1];
            RequiredVillains = schemeInfo.RequiredVillains;

            NumberOfHenchmen = schemeInfo.Henchmen[playerCount - 1];
            RequiredHenchmen = schemeInfo.RequiredHenchmen;

            NumberOfHeroes = schemeInfo.Heroes[playerCount - 1];
            RequiredHeroes = schemeInfo.RequiredHeroes;
            HeroesInVillainDeck = schemeInfo.HeroesInVillainDeck;
            RandomHeroesInVillainDeck = schemeInfo.NumberOfHeroesInVillainDeck;

            BystandersInVillainDeck = schemeInfo.Bystanders[playerCount - 1];
            BystandersInHeroDeck = schemeInfo.BystandersInHeroDeck[playerCount - 1];
            IsBystandersInHeroDeck = schemeInfo.IsBystandersInHeroDeck;

            WoundsPerPlayer = schemeInfo.WoundsPerPlayer;
            CustomWoundNumber = schemeInfo.CustomWoundCount;
            Wounds = schemeInfo.WoundPerPlayer;
        }

        public Scheme(int playerCount, Mastermind mastermind)
        {
            var getExclusions = new GetExclusions();
            var exclusions = getExclusions.GetMastermindExclusion(mastermind.MastermindName);
            var schemeExclusions = exclusions.SchemeList;

            var schemeInfo = _schemes[new Random().Next(_schemes.Count)];
            if(schemeExclusions.Count < _schemes.Count)
            {
                var schemeNameList = _schemes.ToList().Select(x => x.SchemeName).ToList();
                var remainingSchemes = schemeNameList.Except(schemeExclusions).ToList();
                var schemeName = remainingSchemes[new Random().Next(remainingSchemes.Count)];
                schemeInfo = _schemes.First(x => x.SchemeName == schemeName);
            }

            if (RequiredVillains != null && RequiredVillains.Count > 1 && playerCount < 3)
            {
                playerCount = 3;
            }

            if (mastermind.DoesLeadHenchmen)
            {
                playerCount = 4;
            }

            while (schemeInfo.CannotBeSolo && playerCount == 1)
            {
                schemeInfo = _schemes[new Random().Next(_schemes.Count)];
            }

            SchemeName = schemeInfo.SchemeName;
            SetName = schemeInfo.SetName;
            Twists = schemeInfo.SchemeTwists[playerCount - 1];
            NumberOfSchemeTwists = schemeInfo.SchemeTwists[playerCount - 1];
            SchemeInfo = schemeInfo;
            IsSchemeTwistsNextToScheme = schemeInfo.IsSchemeTwistsNextToScheme;
            NumberTwistsNextToScheme = schemeInfo.NumberTwistsNextToScheme;
            NumberOfPlayers = playerCount;

            NumberOfMasterminds = schemeInfo.NumberOfMasterminds;

            NumberOfVillains = schemeInfo.Villains[playerCount - 1];
            RequiredVillains = schemeInfo.RequiredVillains;

            NumberOfHenchmen = schemeInfo.Henchmen[playerCount - 1];
            RequiredHenchmen = schemeInfo.RequiredHenchmen;

            NumberOfHeroes = schemeInfo.Heroes[playerCount - 1];
            RequiredHeroes = schemeInfo.RequiredHeroes;
            HeroesInVillainDeck = schemeInfo.HeroesInVillainDeck;
            RandomHeroesInVillainDeck = schemeInfo.NumberOfHeroesInVillainDeck;

            BystandersInVillainDeck = schemeInfo.Bystanders[playerCount - 1];
            BystandersInHeroDeck = schemeInfo.BystandersInHeroDeck[playerCount-1];
            IsBystandersInHeroDeck = schemeInfo.IsBystandersInHeroDeck;

            WoundsPerPlayer = schemeInfo.WoundsPerPlayer;
            CustomWoundNumber = schemeInfo.CustomWoundCount;
            Wounds = schemeInfo.WoundPerPlayer;
        }
    }
}