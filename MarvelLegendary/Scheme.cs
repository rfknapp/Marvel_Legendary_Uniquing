using MarvelLegendary.Exclusions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;

namespace MarvelLegendary
{
    public class UnveiledScheme
    {
        public string SchemeName { get; set; }
        public Set SetName { get; set; }

        private readonly List<SchemeInfo> _unveiledSchemes = new List<SchemeInfo>()
        {
            new SchemeInfoBuilder().SetSchemeName("...Control The Mutant Messiah").SetSchemeSet(Set.Messiah).Build(),
            new SchemeInfoBuilder().SetSchemeName("...Open Rifts To Future Timelines").SetSchemeSet(Set.Messiah).Build(),
            new SchemeInfoBuilder().SetSchemeName("...Reveal The Heroes' Evil Clones").SetSchemeSet(Set.Messiah).Build(),
            new SchemeInfoBuilder().SetSchemeName("...Unleash An Anti-Mutant Bioweapon").SetSchemeSet(Set.Messiah).Build()
        };

        public UnveiledScheme(string schemeName = "")
        {
            var schemeInfo = schemeName == "" ? _unveiledSchemes[new Random().Next(_unveiledSchemes.Count)] : _unveiledSchemes.First(x => x.SchemeName == schemeName);
            SchemeName = schemeInfo.SchemeName;
            SetName = schemeInfo.SetName;
        }
    }

    public static class SchemeRepository
    {
        private static readonly List<SchemeInfo> _schemes = new List<SchemeInfo>()
        {
            new SchemeInfoBuilder().SetSchemeName("The Legacy Virus").SetWoundCount(true, 6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Midtown Bank Robbery").SetBystanderCount(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Negative Zone Prison Breakout").AddAdditionalHenchmen(1).CannotBeSolo().Build(),
            new SchemeInfoBuilder().SetSchemeName("Portals to The Dark Dimension").SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders With Killbots").SetSchemeTwists(5).SetSchemesNextToTwist(3).SetBystanderCount(18).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Invasion of the Skrull Shapeshifters").SetHeroCount(6).SetRequiredVillains("Skrulls").Build(),
            new SchemeInfoBuilder().SetSchemeName("Super Hero Civil War").CannotBeSolo().SetSchemeTwists(new List<int> { 0,8,8,5,5}).SetHeroCount(new List<int> {0,4,5,5,6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube").Build(),

            new SchemeInfoBuilder().SetSchemeName("Capture Baby Hope").SetSchemeSet(Set.Dc).Build(),
            new SchemeInfoBuilder().SetSchemeName("Detonate the Helicarrier").SetSchemeSet(Set.Dc).SetHeroCount(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Massive Earthquake Generator").SetSchemeSet(Set.Dc).Build(),
            new SchemeInfoBuilder().SetSchemeName("Organized Crimewave").SetSchemeSet(Set.Dc).SetRequiredHenchmen("Maggia Goons").Build(),
            new SchemeInfoBuilder().SetSchemeName("Save Humanity").SetSchemeSet(Set.Dc).SetHeroBystanderCount(new List<int> { 12, 24, 24, 24, 24}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Steal the Weaponized Plutonium").SetSchemeSet(Set.Dc).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Transform Citizens into Demons").SetSchemeSet(Set.Dc).HeroesInVillainDeck("Jean Grey").SetBystanderCount(0).Build(),
            new SchemeInfoBuilder().SetSchemeName("X-Cutioner's Song").SetSchemeSet(Set.Dc).HeroesInVillainDeck(1).SetBystanderCount(0).Build(),

            new SchemeInfoBuilder().SetSchemeName("Bathe Earth in Cosmic Rays").SetSchemeSet(Set.Ff).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Flood the Planet with Melted Glaciers").SetSchemeSet(Set.Ff).Build(),
            new SchemeInfoBuilder().SetSchemeName("Invincible Force Field").SetSchemeSet(Set.Ff).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Reality into the Negative Zone").SetSchemeSet(Set.Ff).Build(),

            new SchemeInfoBuilder().SetSchemeName("Invade the Daily Bugle News HQ").SetSchemeSet(Set.PttR).IncludeHenchmenInHeroDeck(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Splice Humans with Spider DNA").SetSchemeSet(Set.PttR).SetRequiredVillains("Sinister Six").Build(),
            new SchemeInfoBuilder().SetSchemeName("The Clone Saga").SetSchemeSet(Set.PttR).Build(),
            new SchemeInfoBuilder().SetSchemeName("Weave a Web of Lies").SetSchemeSet(Set.PttR).SetSchemeTwists(7).Build(),

            new SchemeInfoBuilder().SetSchemeName("Build an Underground MegaVault Prison").SetSchemeSet(Set.Villains).SetBindingCount(true,5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cage Villains in Power-Suppressing Cells").SetSchemeSet(Set.Villains).NumberHenchmenNextToScheme(2, "Cops").Build(),
            new SchemeInfoBuilder().SetSchemeName("Crown Thor King of Asgard").SetSchemeSet(Set.Villains).SetVillainCardNextToScheme("Thor").Build(),
            new SchemeInfoBuilder().SetSchemeName("Crush HYDRA").SetSchemeSet(Set.Villains).IncludeNewRecruits().IncludeMadameHydra().Build(),
            new SchemeInfoBuilder().SetSchemeName("Graduation at Xavier's X-Academy").SetSchemeSet(Set.Villains).SetBystandersNextToScheme(8).Build(),
            new SchemeInfoBuilder().SetSchemeName("Infiltrate the Lair with Spies").SetSchemeSet(Set.Villains).SetBystandersNextToScheme(21).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mass Produce War Machine Armor").SetSchemeSet(Set.Villains).SetRequiredHenchmen("S.H.I.E.L.D. Assault Squad").Build(),
            new SchemeInfoBuilder().SetSchemeName("Resurrect Heroes with Norn Stones").SetSchemeSet(Set.Villains).Build(),

            new SchemeInfoBuilder().SetSchemeName("Forge the Infinity Gauntlet").SetSchemeSet(Set.GotG).SetRequiredVillains("Infinity Gems").Build(),
            new SchemeInfoBuilder().SetSchemeName("Intergalactic Kree Nega-Bomb").SetSchemeSet(Set.GotG).SetBystandersNextToScheme(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Kree-Skrull War").SetSchemeSet(Set.GotG).SetRequiredVillains(new List<string> { "Kree Starforce", "Skrulls" }).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unite the Shards").SetSchemeSet(Set.GotG).SetShardNumber(30).SetSchemeTwists(new List<int> { 6, 7, 8, 9, 10 }).Build(),

            new SchemeInfoBuilder().SetSchemeName("Fear Itself").SetSchemeSet(Set.Fi).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Last Stand at Avengers Tower").SetSchemeSet(Set.Fi).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Traitor").SetSchemeSet(Set.Fi).CannotBeSolo().SetBindingCount(true,3).HasBetrayalDeck().Build(),

            new SchemeInfoBuilder().SetSchemeName("Build an Army of Annihilation").SetSchemeSet(Set.Sw1).SetSchemeTwists(9).SetAnnihilationHenchmen().Build(),
            new SchemeInfoBuilder().SetSchemeName("Corrupt the Next Generation of Heroes").SetSchemeSet(Set.Sw1).SidekicksInVillainDeck(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Crush Them with My Bare Hands").SetSchemeSet(Set.Sw1).SetVillainCount(new List<int> { 2, 2, 3, 3, 4 }).Build(),
            new SchemeInfoBuilder().SetSchemeName("Dark Alliance").SetSchemeSet(Set.Sw1).AddDarkAllianceMastermind(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Fragmented Realities").SetSchemeSet(Set.Sw1).AddAdditionalVillain(1).SetSchemeTwists(new List<int> { 2, 4, 6, 8, 10}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Master of Tyrants").SetSchemeSet(Set.Sw1).SetTyrantVillains().Build(),
            new SchemeInfoBuilder().SetSchemeName("Pan-Dimensional Plague").SetSchemeSet(Set.Sw1).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Smash Two Dimensions Together").SetSchemeSet(Set.Sw1).AddAdditionalVillain(1).Build(),

            new SchemeInfoBuilder().SetSchemeName("Deadlands Hordes Charge the Wall").SetSchemeSet(Set.Sw2).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enthrone the Barons of Battleworld").SetSchemeSet(Set.Sw2).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Fountain of Eternal Life").SetSchemeSet(Set.Sw2).SetSchemeTwists(new List<int> { 4, 8, 8, 8, 8 }).Build(),
            new SchemeInfoBuilder().SetSchemeName("The God-Emperor of Battleworld").SetSchemeSet(Set.Sw2).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Mark of Khonshu").SetSchemeSet(Set.Sw2).SetSchemeTwists(10).SetRequiredHenchmen("Khonshu Guardians").HeroesInVillainDeck(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Master the Mysteries of Kung-Fu").SetSchemeSet(Set.Sw2).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Wars").SetSchemeSet(Set.Sw2).SetSecretWarsMasterminds().Build(),
            new SchemeInfoBuilder().SetSchemeName("Sinister Ambitions").SetSchemeSet(Set.Sw2).SetSchemeTwists(6).SetAmbitions().Build(),

            new SchemeInfoBuilder().SetSchemeName("Brainwash the Military").SetSchemeSet(Set.Ca).SetSchemeTwists(7).SetVillainOfficers(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Change the Outcome of WWII").SetSchemeSet(Set.Ca).SetSchemeTwists(7).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Go Back in Time to Slay Heroes' Ancestors").SetSchemeSet(Set.Ca).SetSchemeTwists(9).SetHeroCount(8).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Unbreakable Enigma Code").SetSchemeSet(Set.Ca).SetSchemeTwists(6).Build(),

            new SchemeInfoBuilder().SetSchemeName("Avengers vs. X-Men").SetSchemeSet(Set.Cw).SetSchemeTwists(9).AvengersVsXmen().Build(),
            new SchemeInfoBuilder().SetSchemeName("Dark Reign of H.A.M.M.E.R. Officers").SetSchemeSet(Set.Cw).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Epic Super Hero Civil War").SetSchemeSet(Set.Cw).SetHeroCount(new List<int> {4,5,5,5,6}).SetSchemeTwists(new List<int> { 9, 9, 9, 6, 6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Imprison Unregistered Superhumans").SetSchemeSet(Set.Cw).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Nitro the Supervillain Threatens Crowds").SetSchemeSet(Set.Cw).Build(),
            new SchemeInfoBuilder().SetSchemeName("Predict Future Crime").SetSchemeSet(Set.Cw).SetSchemeTwists(6).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Reveal Heroes' Secret Identities").SetSchemeSet(Set.Cw).SetSchemeTwists(6).SetHeroCount(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("United States Split by Civil War").SetSchemeSet(Set.Cw).SetSchemeTwists(10).Build(),

            new SchemeInfoBuilder().SetSchemeName("Deadpool Kills the Marvel Universe").SetSchemeSet(Set.Deadpool).SetHeroCount(new List<int> {4,5,5,5,6}).SetSchemeTwists(new List<int> { 6, 6, 6, 5, 5}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Deadpool Wants a Chimichanga").SetSchemeSet(Set.Deadpool).SetSchemeTwists(6).SetBystanderCount(12).SetVillainCount(new List<int> { 1, 2, 4, 4, 5}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Deadpool Writes a Scheme").SetSchemeSet(Set.Deadpool).SetNumberOfHeroWithNameLike(1, "Deadpool").SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Everybody Hates Deadpool").SetSchemeSet(Set.Deadpool).SetSchemeTwists(6).IncludeHeroTeams(1, HeroTeam.MercsForMoney).Build(),

            new SchemeInfoBuilder().SetSchemeName("Find the Split Personality Killer").SetSchemeSet(Set.Noir).Build(),
            new SchemeInfoBuilder().SetSchemeName("Silence the Witnesses").SetSchemeSet(Set.Noir).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Five Families of Crime").SetSchemeSet(Set.Noir).AddAdditionalVillain(2).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hidden Heart of Darkness").SetSchemeSet(Set.Noir).MastermindTacticsInVillainDeck().Build(),

            new SchemeInfoBuilder().SetSchemeName("Alien Brood Encounters").SetSchemeSet(Set.XMen).AddAdditionalHenchmen(1).SetRequiredHenchmen("The Brood").SetBystanderCount(0).Build(),
            new SchemeInfoBuilder().SetSchemeName("Anti-Mutant Hatred ").SetSchemeSet(Set.XMen).SetSchemeTwists(11).SetWoundCount(false, 30).Build(),
            new SchemeInfoBuilder().SetSchemeName("Horror of Horrors").SetSchemeSet(Set.XMen).SetSchemeTwists(6).IncludeHorrors().Build(),
            new SchemeInfoBuilder().SetSchemeName("Mutant-Hunting Super Sentinels").SetSchemeSet(Set.XMen).SetSchemeTwists(9).AddAdditionalHenchmen(1).SetRequiredHenchmen("Sentinels").Build(),
            new SchemeInfoBuilder().SetSchemeName("Nuclear Armageddon").SetSchemeSet(Set.XMen).SetSchemeTwists(5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Televised Deathtraps of Mojo World").SetSchemeSet(Set.XMen).SetSchemeTwists(11).SetWoundCount(true, 6).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Dark Phoenix Saga").SetSchemeSet(Set.XMen).SetSchemeTwists(10).SetRequiredVillains("Hellfire Club").HeroesInVillainDeck("Jean Grey").Build(),
            new SchemeInfoBuilder().SetSchemeName("X-Men Danger Room goes Berserk").SetSchemeSet(Set.XMen).Build(),

            new SchemeInfoBuilder().SetSchemeName("Distract the Hero").SetSchemeSet(Set.Sm).IncludeHeroTeams(1, HeroTeam.SpiderFriends).Build(),
            new SchemeInfoBuilder().SetSchemeName("Explosion at the Washington Monument").SetSchemeSet(Set.Sm).MonumentDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Ferry Disaster").SetSchemeTwists(9).SetSchemeSet(Set.Sm).Build(),
            new SchemeInfoBuilder().SetSchemeName("Scavenge Alien Weaponry").SetSchemeSet(Set.Sm).IncludeSmugglerHenchmen().Build(),

            new SchemeInfoBuilder().SetSchemeName("Clash of the Monsters Unleashed").SetSchemeSet(Set.Champions).IncludeMonsterPitDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Divide and Conquer").SetSchemeSet(Set.Champions).SetHeroCount(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hypnotize Every Human").SetSchemeSet(Set.Champions).AddAdditionalHenchmen(1).SetBystanderCount(0).Build(),
            new SchemeInfoBuilder().SetSchemeName("Steal All the Oxygen on Earth").SetSchemeSet(Set.Champions).Build(),

            new SchemeInfoBuilder().SetSchemeName("Break the Planet Asunder").SetSchemeSet(Set.Wwh).SetSchemeTwists(9).SetHeroCount(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cytoplasm Spike Invasion").SetSchemeSet(Set.Wwh).SetSchemeTwists(10).IncludeInfectedDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Fall of the Hulks").SetSchemeSet(Set.Wwh).SetSchemeTwists(10).SetWoundCount(true, 6).SetNumberOfHeroWithNameLike(2,"Hulk").Build(),
            new SchemeInfoBuilder().SetSchemeName("Gladiator Pits of Sakaar").SetSchemeSet(Set.Wwh).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mutating Gamma Rays").SetSchemeSet(Set.Wwh).SetSchemeTwists(7).IncludeMutationDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Shoot Hulk into Space").SetSchemeSet(Set.Wwh).IncludeHulkDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Subjugate with Obedience Disks").SetSchemeSet(Set.Wwh).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("World War Hulk").SetSchemeSet(Set.Wwh).SetSchemeTwists(9).SetWorldWarHulkMasterminds().Build(),

            new SchemeInfoBuilder().SetSchemeName("Asgard Under Siege (Negative Zone Prison Breakout)").SetSchemeSet(Set.P1).AddAdditionalHenchmen(1).CannotBeSolo().Build(),
            new SchemeInfoBuilder().SetSchemeName("Destroy the Cities of Earth! (Midtown Bank Robbery)").SetSchemeSet(Set.P1).SetBystanderCount(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enslave Minds with the Chitauri Scepter (Secret Invasion of the Skrull Shapeshifters)").SetSchemeSet(Set.P1).SetHeroCount(6).SetRequiredVillains("Chitauri").Build(),
            new SchemeInfoBuilder().SetSchemeName("Invade Asgard (Portals to The Dark Dimension)").SetSchemeSet(Set.P1).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Radioactive Palladium Poisoning (The Legacy Virus)").SetSchemeSet(Set.P1).SetWoundCount(true, 6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders with HYDRA (Replace Earth's Leaders With Killbots)").SetSchemeSet(Set.P1).SetSchemeTwists(5).SetSchemesNextToTwist(3).SetBystanderCount(18).Build(),
            new SchemeInfoBuilder().SetSchemeName("Super Hero Civil War (Super Hero Civil War)").SetSchemeSet(Set.P1).CannotBeSolo().SetSchemeTwists(new List<int> { 0,8,8,5,5}).SetHeroCount(new List<int> {0,4,5,5,6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube (Unleash The Power Of The Cosmic Cube)").SetSchemeSet(Set.P1).Build(),

            new SchemeInfoBuilder().SetSchemeName("Age of Ultron").SetSchemeSet(Set.Antman).SetSchemeTwists(11).SetHeroCount(new List<int> { 3, 5, 5, 6, 7}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Earth Into Midieval Times").SetSchemeSet(Set.Antman).SetSchemeTwists(9).Build(),
            new SchemeInfoBuilder().SetSchemeName("Transform Commuters Into Giant Ants").SetSchemeSet(Set.Antman).SetSchemeTwists(new List<int> { 7, 8, 9, 10, 11}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trap Heroes In The Microverse").SetSchemeSet(Set.Antman).SetSchemeTwists(11).HeroesInVillainDeck(1).Build(),

            new SchemeInfoBuilder().SetSchemeName("Invasion of the Venom Symbiotes").SetSchemeSet(Set.Venom).AddAdditionalHenchmen(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Maximum Carnage").SetSchemeSet(Set.Venom).SetSchemeTwists(10).SetWoundCount(true,6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Paralyzing Venom").SetSchemeSet(Set.Venom).SetSchemeTwists(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Symbiotic Absorption").SetSchemeSet(Set.Venom).SetSchemeTwists(11).SetDrainedMastermind().Build(),

            new SchemeInfoBuilder().SetSchemeName("Earthquake Drains the Ocean").SetSchemeSet(Set.Revelations).SetSchemeTwists(11).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("House of M").SetSchemeSet(Set.Revelations).HeroesInVillainDeck("Scarlet Witch").Is4v2().Build(),
            new SchemeInfoBuilder().SetSchemeName("The Korvac Saga").SetSchemeSet(Set.Revelations).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret HYDRA Corruption").SetSchemeSet(Set.Revelations).SetSchemeTwists(new List<int>{7,9,9,11,11}).Build(),

            new SchemeInfoBuilder().SetSchemeName("Hail Hydra").SetSchemeSet(Set.Shield).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hydra Helicarriers Hunt Heroes").SetSchemeSet(Set.Shield).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Empire of Betrayal").SetSchemeSet(Set.Shield).SetSchemeTwists(11).SetDarkLoyalty().Build(),
            new SchemeInfoBuilder().SetSchemeName("S.H.I.E.L.D. vs. Hydra War").SetSchemeSet(Set.Shield).SetSchemeTwists(7).SetRequiredVillains(new List<string>{"Hydra Elite", "A.I.M., Hydra Offshoot" }, 1).Build(),

            new SchemeInfoBuilder().SetSchemeName("Asgardian Test of Worth").SetSchemeSet(Set.Asgard).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Dark World of Svartalfheim").SetSchemeSet(Set.Asgard).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Ragnarok, Twilight of the Gods").SetSchemeSet(Set.Asgard).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("War of the Frost Giants").SetSchemeSet(Set.Asgard).SetSchemeTwists(9).Build(),

            new SchemeInfoBuilder().SetSchemeName("Crash the Moon into the Sun").SetSchemeSet(Set.NewMutants).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Demon Bear Saga").SetSchemeSet(Set.NewMutants).SetRequiredVillains("Demons of Limbo").Build(),
            new SchemeInfoBuilder().SetSchemeName("Superhuman Baseball Game").SetSchemeSet(Set.NewMutants).SetSchemeTwists(9).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trapped in the Insane Asylum").SetSchemeSet(Set.NewMutants).SetSchemeTwists(new List<int>{3,5,7,9,11}).Build(),

            new SchemeInfoBuilder().SetSchemeName("Annihilation Conquest").SetSchemeSet(Set.Cosmos).SetSchemeTwists(11).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Contest of Champions").SetSchemeSet(Set.Cosmos).SetSchemeTwists(11).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Destroy the Nova Corps").SetSchemeSet(Set.Cosmos).SetSchemeTwists(9).SetHeroCount(new List<int>(){ 5, 5, 5, 5, 6 }).SetNumberOfHeroWithNameLike(1, "Nova").Build(),
            new SchemeInfoBuilder().SetSchemeName("Turn the Soul of Adam Warlock").SetSchemeSet(Set.Cosmos).SetSchemeTwists(14).SetSoulsDeck("Adam Warlock").Build(),

            new SchemeInfoBuilder().SetSchemeName("Devolve with Xerogen Crystals").SetSchemeSet(Set.Inhumans).SetSchemeTwists(new List<int>{4,5,6,7,8}).AddXerogenHenchmen().Build(),
            new SchemeInfoBuilder().SetSchemeName("Ruin the Perfect Wedding").SetSchemeSet(Set.Inhumans).SetRoyalWedding().Build(),
            new SchemeInfoBuilder().SetSchemeName("Tornado of Terrigen Mists").SetSchemeSet(Set.Inhumans).SetSchemeTwists(10).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("War of Kings").SetSchemeSet(Set.Inhumans).SetSchemeTwists(11).Build(),

            new SchemeInfoBuilder().SetSchemeName("Breach Parallel Dimensions").SetSchemeSet(Set.Annihilation).SetSchemeTwists(6).IncreaseBystanders(4).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pulse Waves from the Negative Zone").SetSchemeSet(Set.Annihilation).SetSchemeTwists(9).Build(),
            new SchemeInfoBuilder().SetSchemeName("Put Humanity on Trial").SetSchemeSet(Set.Annihilation).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sneak Attack the Heroes").SetSchemeSet(Set.Annihilation).SetSchemeTwists(6).Build(),

            new SchemeInfoBuilder().SetSchemeName("Drain Mutants' Powers To...").SetSchemeSet(Set.Messiah).SetSchemeTwists(11).SetVeiledScheme().Build(),
            new SchemeInfoBuilder().SetSchemeName("Hack Cerebro Servers To...").SetSchemeSet(Set.Messiah).SetSchemeTwists(10).SetVeiledScheme().Build(),
            new SchemeInfoBuilder().SetSchemeName("Hire Singularity Investigations To...").SetSchemeSet(Set.Messiah).SetSchemeTwists(9).SetVeiledScheme().Build(),
            new SchemeInfoBuilder().SetSchemeName("Raid Gene Banks To...").SetSchemeSet(Set.Messiah).SetVeiledScheme().Build(),

            new SchemeInfoBuilder().SetSchemeName("Claim Souls for Demons").SetSchemeSet(Set.Strange).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cursed Pages of the Darkhold Tome").SetSchemeSet(Set.Strange).SetSchemeTwists(11).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Duels of Science and Magic").SetSchemeSet(Set.Strange).SetSchemeTwists(new List<int>{10,9,11,10,11}).Build(),
            new SchemeInfoBuilder().SetSchemeName("War for the Dream Dimension").SetSchemeSet(Set.Strange).SetSchemeTwists(7).AddAdditionalVillain(1).Build(),

            new SchemeInfoBuilder().SetSchemeName("Inescapable \"Kyln\" Space Prison").SetSchemeSet(Set.Guardians).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Provoke the Sovereign War Fleet").SetSchemeSet(Set.Guardians).SetSchemeTwists(11).AddAdditionalVillain(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Star-Lord's Awesome Mix Tape").SetSchemeSet(Set.Guardians).SetSchemeTwists(7).SetHeroCount(7).DoubleHenchmen().DoubleVillains().IncludeHeroTeams(1, HeroTeam.GuardiansOfTheGalaxy).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Abilisk Space Monster").SetSchemeSet(Set.Guardians).SetSchemeTwists(9).Build(),

            new SchemeInfoBuilder().SetSchemeName("Plunder Wakanda's Vibranium").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Poison Lakes with Nanite Microbots").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(new List<int>{5,6,7,8,9}).SetWoundCount(false, 30).Build(),
            new SchemeInfoBuilder().SetSchemeName("Provoke a Clash of Nations").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Seize the Wakandan Throne").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(6).Build(),

            new SchemeInfoBuilder().SetSchemeName("Corrupt the Spy Agencies").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Frame Heroes for Murder").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(7).SetHeroCount(6).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sniper Rifle Assassins").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(new List<int>{10,9,8,7,6}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Train Black Widows in the Red Room").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(new List<int>{7,6,5,4,3}).SetVillainOfficers(8).Build(),

            new SchemeInfoBuilder().SetSchemeName("Halve All Life in the Universe").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sacrifice for the Soul Stone").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(new List<int>{5,6,7,8,9}).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Time Heist").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(11).SetHeroCount(8).Build(),
            new SchemeInfoBuilder().SetSchemeName("Warp Reality Into a TV Show").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(11).Build(),

            new SchemeInfoBuilder().SetSchemeName("Midnight Massacre").SetSchemeSet(Set.MidnightSons).SetSchemeTwists(11).HeroesInVillainDeckWithNameLike(1, "Blade").Build(),
            new SchemeInfoBuilder().SetSchemeName("Ritual Sacrifice to Summon Chthon").SetSchemeSet(Set.MidnightSons).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sire Vampires at the Blood Bank").SetSchemeSet(Set.MidnightSons).SetSchemeTwists(10).SetVampireNaniteHenchmen().Build(),
            new SchemeInfoBuilder().SetSchemeName("Wager at Blackjack for Heroes' Souls").SetSchemeSet(Set.MidnightSons).SetSchemeTwists(11).AddAdditionalHero(2).Build(),

            new SchemeInfoBuilder().SetSchemeName("Breach the Nexus of All Realities").SetSchemeSet(Set.WhatIf).SetVillainCount(new List<int>{ 3, 3, 3, 3, 4 }).SetSchemeTwists(new List<int>{6,6,6,6,8}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Collect an Interstellar Zoo").SetSchemeSet(Set.WhatIf).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Marvel Zombies").SetSchemeSet(Set.WhatIf).SetSchemeTwists(4).HeroesInVillainDeck(1).MarvelZombieVillains(1, Keywords.LivingDead).SetBystanderCount(new List<int>{ 4, 5, 8, 8, 12 }).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trash Earth with Hugest Party Ever").SetSchemeSet(Set.WhatIf).SetSchemeTwists(6).SetRequiredHeroes("Party Thor").SetRequiredVillains("Intergalactic Party Animals").Build(),

            new SchemeInfoBuilder().SetSchemeName("Auction Shrink Tech to Highest Bidder").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(11).SetShrinkTechDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Escape an Imprisoning Dimension").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Safeguard Dark Secrets").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(5).Build(),
            new SchemeInfoBuilder().SetSchemeName("Siphon Energy from the Quantum Realm").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(9).IncludeQuantumRealmDeck().Build(),

            new SchemeInfoBuilder().SetSchemeName("Become President of the United States").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Befoul Earth Into a Polluted Wasteland").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(11).AddAdditionalHero(1).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Reality Into Cyberspace").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(7).Build(),
            new SchemeInfoBuilder().SetSchemeName("Subjugate Earth with Mega-Corporations").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(11).AddAdditionalHero(1).Build(),

            new SchemeInfoBuilder().SetSchemeName("Condition Logan Into Weapon X").SetSchemeSet(Set.WeaponX).SetNumberOfHeroWithNameLike(1, "Wolverine").Build(),
            //Need to code "Don't use multiple Heroes that have the same Hero Name
            new SchemeInfoBuilder().SetSchemeName("Go After Heroes' Loved Ones").SetSchemeSet(Set.WeaponX).SetSchemeTwists(new List<int>{ 8, 10, 10, 10, 11 }).AddAdditionalHero(1).NoDuplicates().SetLovedOnesDeck().Build(),
            new SchemeInfoBuilder().SetSchemeName("Wipe Heroes' Memories").SetSchemeSet(Set.WeaponX).SetSchemeTwists(new List<int>{ 5, 6, 7, 8, 9 }).Build(),

        };

        public static IReadOnlyList<SchemeInfo> All => _schemes;
    }
    
    public class Scheme
    {
        public string SchemeName { get; set; }
        public Set SetName { get; set; }
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
        private Random random;

        public Scheme() 
        {
            random = new Random();
        }

        //This function is only used in the ConverTrackedGames class
        public Scheme GetNewScheme(string schemeName = "")
        {
            var newScheme = new Scheme();
            var scheme = schemeName;
            if (string.IsNullOrEmpty(scheme))
            {
                var allSchemes = GetListOfSchemes();
                scheme = allSchemes[RandomHelper.Instance.Next(allSchemes.Count)];
            }

            var schemeInfo = SchemeRepository.All.FirstOrDefault(s => s.SchemeName == scheme);

            newScheme.SchemeName = schemeInfo.SchemeName;
            newScheme.SetName = newScheme.SetName;

            return newScheme;
        }

        public Scheme GetNewScheme(int playerCount, Mastermind mastermind, string schemeName="")
        {
            SchemeInfo schemeInfo;

            if (string.IsNullOrEmpty(schemeName))
            {
                schemeInfo = GetRandomScheme(mastermind);
            }
            else
            {
                schemeInfo = GetSchemeInfo(schemeName);
            }

            var newScheme = ProcessSchemeInfo(playerCount, schemeInfo, mastermind);
            return newScheme;
        }

        private SchemeInfo GetSchemeInfo(string schemeName)
        {
            return SchemeRepository.All.First(x => x.SchemeName == schemeName);
        }

        private SchemeInfo GetRandomScheme(Mastermind mastermind)
        {
            var schemesPlayedWithMastermind = new List<SchemeInfo>();

            var command = SqlHelper.GetConnection().CreateCommand();
            command.CommandText =
                @"SELECT CardId
                  FROM Card
                  WHERE CardName = $name
                    AND SetId = $setId";

            command.Parameters.AddWithValue("$name", mastermind.MastermindName);
            command.Parameters.AddWithValue("$setId", (int)mastermind.SetName);

            var mastermindId = SqlHelper.RunCommandScalar<int>(command);

            command.Parameters.Clear();
            command.CommandText =
                @"SELECT Card2Id
                  FROM CardRelationship cr
                  INNER JOIN Card c
                    ON cr.Card2Id = c.CardId
                  WHERE cr.Card1Id = $card1Id
                    AND c.CardType = $cardTypeId";

            command.Parameters.AddWithValue("$card1Id", mastermindId);
            command.Parameters.AddWithValue("$cardTypeId", (int)CardType.Scheme);

            schemesPlayedWithMastermind = SqlHelper.RunCommand(command);

            var schemeName = "";
            var allSchemeList = SchemeRepository.All.ToList();
            var schemeNameList = SchemeRepository.All.Select(s => s.SchemeName).ToList();
            
            if (schemesPlayedWithMastermind.Count < schemeNameList.Count)
            {
                var remainingSchemes = allSchemeList.
                    Where(s => !schemesPlayedWithMastermind.Any(s2 =>
                      s2.SchemeName == s.SchemeName &&
                      s2.SetName == s2.SetName)).ToList();
                schemeName = remainingSchemes[random.Next(remainingSchemes.Count)].SchemeName;
            }
            else
            {
                schemeName = schemeNameList[RandomHelper.Instance.Next(schemeNameList.Count)];
            }

            return GetSchemeInfo(schemeName);
        }

        private Scheme ProcessSchemeInfo(int playerCount, SchemeInfo schemeInfo, Mastermind mastermind)
        {
            var newScheme = new Scheme();
            if (schemeInfo.RequiredVillains != null && schemeInfo.RequiredVillains.Count > 0 && playerCount < 3)
            {
                playerCount = 3;
            }

            if (schemeInfo.RequiredHenchmen.Count > 0 || mastermind.DoesLeadHenchmen)
            {
                playerCount = 4;
            }

            newScheme.SchemeName = schemeInfo.SchemeName;
            newScheme.SetName = schemeInfo.SetName;
            newScheme.Twists = schemeInfo.SchemeTwists[playerCount - 1];
            newScheme.NumberOfSchemeTwists = SchemeName == "Ritual Sacrifice to Summon Chthon" && mastermind.MastermindName == "Lilith" ? 1 : schemeInfo.SchemeTwists[playerCount - 1];
            newScheme.SchemeInfo = schemeInfo;
            newScheme.IsSchemeTwistsNextToScheme = schemeInfo.IsSchemeTwistsNextToScheme;
            newScheme.NumberTwistsNextToScheme = schemeInfo.NumberTwistsNextToScheme;
            newScheme.NumberOfPlayers = playerCount;

            newScheme.NumberOfMasterminds = schemeInfo.NumberOfMasterminds;

            newScheme.NumberOfVillains = schemeInfo.Villains[playerCount - 1];
            
            //This covers the case in the Ritual Sacrifice to Summon Chthon where the mastermind is Lilith
            if (SchemeName == "Ritual Sacrifice to Summon Chthon" && mastermind.MastermindName == "Lilith")
                newScheme.NumberOfVillains++;

            newScheme.RequiredVillains = schemeInfo.RequiredVillains;

            //This covers the case in the Ritual Sacrifice to Summon Chthon where the mastermind is not Lilith
            if (SchemeName == "Ritual Sacrifice to Summon Chthon" && mastermind.MastermindName != "Lilith")
                newScheme.RequiredVillains.Add("Lilin");

            newScheme.NumberOfHenchmen = schemeInfo.Henchmen[playerCount - 1];
            newScheme.RequiredHenchmen = schemeInfo.RequiredHenchmen;

            newScheme.NumberOfHeroes = schemeInfo.Heroes[playerCount - 1];
            newScheme.RequiredHeroes = schemeInfo.RequiredHeroes;
            newScheme.HeroesInVillainDeck = schemeInfo.HeroesInVillainDeck;
            newScheme.RandomHeroesInVillainDeck = schemeInfo.NumberOfHeroesInVillainDeck;

            newScheme.BystandersInVillainDeck = schemeInfo.Bystanders[playerCount - 1] + schemeInfo.AdditionalBystanders;
            newScheme.BystandersInHeroDeck = schemeInfo.BystandersInHeroDeck[playerCount - 1];
            newScheme.IsBystandersInHeroDeck = schemeInfo.IsBystandersInHeroDeck;

            newScheme.WoundsPerPlayer = schemeInfo.WoundsPerPlayer;
            newScheme.CustomWoundNumber = schemeInfo.CustomWoundCount;
            newScheme.Wounds = schemeInfo.WoundPerPlayer;

            return newScheme;
        }

        public List<string> GetListOfSchemes()
        {
            var allSchemes = SchemeRepository.All.Select(s => s.SchemeName).ToList();
            return allSchemes;
        }

        public List<string> GetListOfSchemesByX(string cardType, string name)
        {
            //cardType can be Henchmen, Scheme, Hero, Villain, or Mastermind
            var schemeByTable = $"SchemeBy{cardType}";
            var tableName = (cardType == "Henchmen") ? "Henchmen" : (cardType == "Hero" ? "Heroes" : $"{cardType}s");
            var updatedName = name.Replace("'", "''");

            var allSchemesBy = $@"select s.SchemeName from Schemes s
                    inner join {schemeByTable} sb ON s.Id = sb.SchemeId
                    inner join {tableName} t ON t.Id = sb.{cardType}Id
                    where t.{cardType}Name = '{updatedName}'";

            var allSchemesByX = new DatabaseHelper().GetList(allSchemesBy);
            return allSchemesByX;
        }
    }
}