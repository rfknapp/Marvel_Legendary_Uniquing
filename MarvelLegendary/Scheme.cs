using System.Collections.Generic;
using System.Linq;
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
            var schemeInfo = schemeName == "" ? _unveiledSchemes[RandomHelper.Instance.Next(_unveiledSchemes.Count)] : _unveiledSchemes.First(x => x.SchemeName == schemeName);
            SchemeName = schemeInfo.SchemeName;
            SetName = schemeInfo.SetName;
        }
    }

    public static class SchemeRepository
    {
        private static readonly List<SchemeInfo> _schemes = new List<SchemeInfo>()
        {
            new SchemeInfoBuilder().SetSchemeName("The Legacy Virus").SetWoundCount(true, 6).SchemeId(1).Duplicates(new List<int>{1, 104, 190}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Midtown Bank Robbery").SetBystanderCount(12).SchemeId(2).Duplicates(new List<int>{2, 101, 188}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Negative Zone Prison Breakout").AddAdditionalHenchmen(1).CannotBeSolo().SchemeId(3).Duplicates(new List<int>{3, 100, 191}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Portals to The Dark Dimension").SetSchemeTwists(7).SchemeId(4).Duplicates(new List<int>{4, 103, 192}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders With Killbots").SetSchemeTwists(5).SetTwistsNextToScheme(3).SetBystanderCount(18).SchemeId(5).Duplicates(new List<int>{5, 105, 193}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Invasion of the Skrull Shapeshifters").SetHeroCount(6).SetRequiredVillains("Skrulls", Set.Core).SetRandomHeroCardsInVillainDeck(12).SchemeId(6).Duplicates(new List<int>{6, 102, 194}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Super Hero Civil War").CannotBeSolo().SetSchemeTwists(new List<int> { 0,8,8,5,5}).SetHeroCount(new List<int> {0,4,5,5,6}).SchemeId(7).Duplicates(new List<int>{7, 106, 195}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube").SchemeId(8).Duplicates(new List<int>{8, 107, 196}).Build(),

            new SchemeInfoBuilder().SetSchemeName("Capture Baby Hope").SetSchemeSet(Set.Dc).SchemeId(9).Build(),
            new SchemeInfoBuilder().SetSchemeName("Detonate the Helicarrier").SetSchemeSet(Set.Dc).SetHeroCount(6).SchemeId(10).Build(),
            new SchemeInfoBuilder().SetSchemeName("Massive Earthquake Generator").SetSchemeSet(Set.Dc).SchemeId(11).Build(),
            new SchemeInfoBuilder().SetSchemeName("Organized Crimewave").SetSchemeSet(Set.Dc).SetRequiredHenchmen(Henchmen.GetNewHenchmen("Maggia Goons", Set.Dc)).SchemeId(12).Build(),
            new SchemeInfoBuilder().SetSchemeName("Save Humanity").SetSchemeSet(Set.Dc).SetHeroBystanderCount(new List<int> { 12, 24, 24, 24, 24}).SchemeId(13).Build(),
            new SchemeInfoBuilder().SetSchemeName("Steal the Weaponized Plutonium").SetSchemeSet(Set.Dc).AddAdditionalVillain(1).SchemeId(14).Build(),
            new SchemeInfoBuilder().SetSchemeName("Transform Citizens into Demons").SetSchemeSet(Set.Dc).HeroesInVillainDeck("Jean Grey", Set.Dc).SetBystanderCount(0).SchemeId(15).Build(),
            new SchemeInfoBuilder().SetSchemeName("X-Cutioner's Song").SetSchemeSet(Set.Dc).HeroesInVillainDeck(1).SetBystanderCount(0).SchemeId(16).Build(),

            new SchemeInfoBuilder().SetSchemeName("Bathe Earth in Cosmic Rays").SetSchemeSet(Set.Ff).SetSchemeTwists(6).SchemeId(17).Build(),
            new SchemeInfoBuilder().SetSchemeName("Flood the Planet with Melted Glaciers").SetSchemeSet(Set.Ff).SchemeId(18).Build(),
            new SchemeInfoBuilder().SetSchemeName("Invincible Force Field").SetSchemeSet(Set.Ff).SetSchemeTwists(7).SchemeId(19).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Reality into the Negative Zone").SetSchemeSet(Set.Ff).SchemeId(20).Build(),

            new SchemeInfoBuilder().SetSchemeName("Invade the Daily Bugle News HQ").SetSchemeSet(Set.PttR).IncludeHenchmenInHeroDeck(1).SchemeId(21).Build(),
            new SchemeInfoBuilder().SetSchemeName("Splice Humans with Spider DNA").SetSchemeSet(Set.PttR).SetRequiredVillains("Sinister Six", Set.PttR).SchemeId(22).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Clone Saga").SetSchemeSet(Set.PttR).SchemeId(23).Build(),
            new SchemeInfoBuilder().SetSchemeName("Weave a Web of Lies").SetSchemeSet(Set.PttR).SetSchemeTwists(7).SchemeId(24).Build(),

            new SchemeInfoBuilder().SetSchemeName("Build an Underground MegaVault Prison").SetSchemeSet(Set.Villains).SetBindingCount(true,5).SchemeId(25).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cage Villains in Power-Suppressing Cells").SetSchemeSet(Set.Villains).NumberHenchmenNextToScheme(2, "Cops", Set.Villains).SchemeId(26).Build(),
            new SchemeInfoBuilder().SetSchemeName("Crown Thor King of Asgard").SetSchemeSet(Set.Villains).SetVillainCardNextToScheme("Thor").SchemeId(27).Build(),
            new SchemeInfoBuilder().SetSchemeName("Crush HYDRA").SetSchemeSet(Set.Villains).IncludeNewRecruits().IncludeMadameHydra().SchemeId(28).Build(),
            new SchemeInfoBuilder().SetSchemeName("Graduation at Xavier's X-Academy").SetSchemeSet(Set.Villains).SetBystandersNextToScheme(8).SchemeId(29).Build(),
            new SchemeInfoBuilder().SetSchemeName("Infiltrate the Lair with Spies").SetSchemeSet(Set.Villains).SetBystandersNextToScheme(21).SchemeId(30).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mass Produce War Machine Armor").SetSchemeSet(Set.Villains).SetRequiredHenchmen(Henchmen.GetNewHenchmen("S.H.I.E.L.D. Assault Squad", Set.Villains)).SchemeId(31).Build(),
            new SchemeInfoBuilder().SetSchemeName("Resurrect Heroes with Norn Stones").SetSchemeSet(Set.Villains).SchemeId(32).Build(),

            new SchemeInfoBuilder().SetSchemeName("Forge the Infinity Gauntlet").SetSchemeSet(Set.GotG).SetRequiredVillains("Infinity Gems", Set.GotG).SchemeId(33).Build(),
            new SchemeInfoBuilder().SetSchemeName("Intergalactic Kree Nega-Bomb").SetSchemeSet(Set.GotG).SetBystandersNextToScheme(6).SchemeId(34).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Kree-Skrull War").SetSchemeSet(Set.GotG).SetRequiredVillains(new List<Villain> { Villain.GetNewVillain("Kree Starforce", Set.GotG), Villain.GetNewVillain("Skrulls", Set.Core) }).SchemeId(35).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unite the Shards").SetSchemeSet(Set.GotG).SetShardNumber(30).SetSchemeTwists(new List<int> { 6, 7, 8, 9, 10 }).SchemeId(36).Build(),

            new SchemeInfoBuilder().SetSchemeName("Fear Itself").SetSchemeSet(Set.Fi).SetSchemeTwists(10).SchemeId(37).Build(),
            new SchemeInfoBuilder().SetSchemeName("Last Stand at Avengers Tower").SetSchemeSet(Set.Fi).SetSchemeTwists(6).SchemeId(38).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Traitor").SetSchemeSet(Set.Fi).CannotBeSolo().SetBindingCount(true,3).HasBetrayalDeck().SchemeId(39).Build(),

            new SchemeInfoBuilder().SetSchemeName("Build an Army of Annihilation").SetSchemeSet(Set.Sw1).SetSchemeTwists(9).SetAnnihilationHenchmen().SchemeId(40).Build(),
            new SchemeInfoBuilder().SetSchemeName("Corrupt the Next Generation of Heroes").SetSchemeSet(Set.Sw1).SidekicksInVillainDeck(10).SchemeId(41).Build(),
            new SchemeInfoBuilder().SetSchemeName("Crush Them with My Bare Hands").SetSchemeSet(Set.Sw1).SetVillainCount(new List<int> { 2, 2, 3, 3, 4 }).SchemeId(42).Build(),
            new SchemeInfoBuilder().SetSchemeName("Dark Alliance").SetSchemeSet(Set.Sw1).AddDarkAllianceMastermind(1).SchemeId(43).Build(),
            new SchemeInfoBuilder().SetSchemeName("Fragmented Realities").SetSchemeSet(Set.Sw1).AddAdditionalVillain(1).SetSchemeTwists(new List<int> { 2, 4, 6, 8, 10}).SchemeId(44).Build(),
            new SchemeInfoBuilder().SetSchemeName("Master of Tyrants").SetSchemeSet(Set.Sw1).SetTyrantVillains().SchemeId(45).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pan-Dimensional Plague").SetSchemeSet(Set.Sw1).SetSchemeTwists(10).SchemeId(46).Build(),
            new SchemeInfoBuilder().SetSchemeName("Smash Two Dimensions Together").SetSchemeSet(Set.Sw1).AddAdditionalVillain(1).SchemeId(47).Build(),

            new SchemeInfoBuilder().SetSchemeName("Deadlands Hordes Charge the Wall").SetSchemeSet(Set.Sw2).AddAdditionalVillain(1).SchemeId(48).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enthrone the Barons of Battleworld").SetSchemeSet(Set.Sw2).SchemeId(49).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Fountain of Eternal Life").SetSchemeSet(Set.Sw2).SetSchemeTwists(new List<int> { 4, 8, 8, 8, 8 }).SchemeId(50).Build(),
            new SchemeInfoBuilder().SetSchemeName("The God-Emperor of Battleworld").SetSchemeSet(Set.Sw2).SchemeId(51).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Mark of Khonshu").SetSchemeSet(Set.Sw2).SetSchemeTwists(10).SetRequiredHenchmen(Henchmen.GetNewHenchmen("Khonshu Guardians",Set.Sw2)).HeroesInVillainDeck(1).SchemeId(52).Build(),
            new SchemeInfoBuilder().SetSchemeName("Master the Mysteries of Kung-Fu").SetSchemeSet(Set.Sw2).SchemeId(53).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Wars").SetSchemeSet(Set.Sw2).SetSecretWarsMasterminds().SchemeId(54).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sinister Ambitions").SetSchemeSet(Set.Sw2).SetSchemeTwists(6).SetAmbitions().SchemeId(55).Build(),

            new SchemeInfoBuilder().SetSchemeName("Brainwash the Military").SetSchemeSet(Set.Ca).SetSchemeTwists(7).SetVillainOfficers(12).SchemeId(56).Build(),
            new SchemeInfoBuilder().SetSchemeName("Change the Outcome of WWII").SetSchemeSet(Set.Ca).SetSchemeTwists(7).AddAdditionalVillain(1).SchemeId(57).Build(),
            new SchemeInfoBuilder().SetSchemeName("Go Back in Time to Slay Heroes' Ancestors").SetSchemeSet(Set.Ca).SetSchemeTwists(9).SetHeroCount(8).SchemeId(58).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Unbreakable Enigma Code").SetSchemeSet(Set.Ca).SetSchemeTwists(6).SchemeId(59).Build(),

            new SchemeInfoBuilder().SetSchemeName("Avengers vs. X-Men").SetSchemeSet(Set.Cw).SetSchemeTwists(9).AvengersVsXmen().SchemeId(60).Build(),
            new SchemeInfoBuilder().SetSchemeName("Dark Reign of H.A.M.M.E.R. Officers").SetSchemeSet(Set.Cw).SetSchemeTwists(7).SchemeId(61).Build(),
            new SchemeInfoBuilder().SetSchemeName("Epic Super Hero Civil War").SetSchemeSet(Set.Cw).SetHeroCount(new List<int> {4,5,5,5,6}).SetSchemeTwists(new List<int> { 9, 9, 9, 6, 6}).SchemeId(62).Build(),
            new SchemeInfoBuilder().SetSchemeName("Imprison Unregistered Superhumans").SetSchemeSet(Set.Cw).SetSchemeTwists(11).SchemeId(63).Build(),
            new SchemeInfoBuilder().SetSchemeName("Nitro the Supervillain Threatens Crowds").SetSchemeSet(Set.Cw).SchemeId(64).Build(),
            new SchemeInfoBuilder().SetSchemeName("Predict Future Crime").SetSchemeSet(Set.Cw).SetSchemeTwists(6).AddAdditionalVillain(1).SchemeId(65).Build(),
            new SchemeInfoBuilder().SetSchemeName("Reveal Heroes' Secret Identities").SetSchemeSet(Set.Cw).SetSchemeTwists(6).SetHeroCount(7).SchemeId(66).Build(),
            new SchemeInfoBuilder().SetSchemeName("United States Split by Civil War").SetSchemeSet(Set.Cw).SetSchemeTwists(10).SchemeId(67).Build(),

            new SchemeInfoBuilder().SetSchemeName("Deadpool Kills the Marvel Universe").SetSchemeSet(Set.Deadpool).SetHeroCount(new List<int> {4,5,5,5,6}).SetSchemeTwists(new List<int> { 6, 6, 6, 5, 5}).SchemeId(68).Build(),
            new SchemeInfoBuilder().SetSchemeName("Deadpool Wants a Chimichanga").SetSchemeSet(Set.Deadpool).SetSchemeTwists(6).SetBystanderCount(12).SetVillainCount(new List<int> { 1, 2, 4, 4, 5}).SchemeId(69).Build(),
            new SchemeInfoBuilder().SetSchemeName("Deadpool Writes a Scheme").SetSchemeSet(Set.Deadpool).SetNumberOfHeroWithNameLike(1, "Deadpool").SetSchemeTwists(6).SchemeId(70).Build(),
            new SchemeInfoBuilder().SetSchemeName("Everybody Hates Deadpool").SetSchemeSet(Set.Deadpool).SetSchemeTwists(6).IncludeHeroTeams(1, HeroTeam.MercsForMoney).SchemeId(71).Build(),

            new SchemeInfoBuilder().SetSchemeName("Find the Split Personality Killer").SetSchemeSet(Set.Noir).SchemeId(72).Build(),
            new SchemeInfoBuilder().SetSchemeName("Silence the Witnesses").SetSchemeSet(Set.Noir).SetSchemeTwists(6).SchemeId(73).Build(),
            new SchemeInfoBuilder().SetSchemeName("Five Families of Crime").SetSchemeSet(Set.Noir).AddAdditionalVillain(2).SchemeId(74).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hidden Heart of Darkness").SetSchemeSet(Set.Noir).MastermindTacticsInVillainDeck().SchemeId(75).Build(),

            new SchemeInfoBuilder().SetSchemeName("Alien Brood Encounters").SetSchemeSet(Set.XMen).AddAdditionalHenchmen(1).SetRequiredHenchmen(Henchmen.GetNewHenchmen("The Brood",Set.XMen)).SetBystanderCount(0).SchemeId(76).Build(),
            new SchemeInfoBuilder().SetSchemeName("Anti-Mutant Hatred ").SetSchemeSet(Set.XMen).SetSchemeTwists(11).SetWoundCount(false, 30).SchemeId(77).Build(),
            new SchemeInfoBuilder().SetSchemeName("Horror of Horrors").SetSchemeSet(Set.XMen).SetSchemeTwists(6).IncludeHorrors().SchemeId(78).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mutant-Hunting Super Sentinels").SetSchemeSet(Set.XMen).SetSchemeTwists(9).AddAdditionalHenchmen(1).SchemeId(79).Build(),
            new SchemeInfoBuilder().SetSchemeName("Nuclear Armageddon").SetSchemeSet(Set.XMen).SetSchemeTwists(5).SchemeId(80).Build(),
            new SchemeInfoBuilder().SetSchemeName("Televised Deathtraps of Mojo World").SetSchemeSet(Set.XMen).SetSchemeTwists(11).SetWoundCount(true, 6).SchemeId(81).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Dark Phoenix Saga").SetSchemeSet(Set.XMen).SetSchemeTwists(10).SetRequiredVillains("Hellfire Club", Set.XMen).HeroesInVillainDeck("Jean Grey", Set.Dc).SchemeId(82).Build(),
            new SchemeInfoBuilder().SetSchemeName("X-Men Danger Room goes Berserk").SetSchemeSet(Set.XMen).SchemeId(83).Build(),

            new SchemeInfoBuilder().SetSchemeName("Distract the Hero").SetSchemeSet(Set.Sm).IncludeHeroTeams(1, HeroTeam.SpiderFriends).SchemeId(84).Build(),
            new SchemeInfoBuilder().SetSchemeName("Explosion at the Washington Monument").SetSchemeSet(Set.Sm).MonumentDeck().SchemeId(85).Build(),
            new SchemeInfoBuilder().SetSchemeName("Ferry Disaster").SetSchemeTwists(9).SetSchemeSet(Set.Sm).SchemeId(86).Build(),
            new SchemeInfoBuilder().SetSchemeName("Scavenge Alien Weaponry").SetSchemeSet(Set.Sm).IncludeSmugglerHenchmen().SchemeId(87).Build(),

            new SchemeInfoBuilder().SetSchemeName("Clash of the Monsters Unleashed").SetSchemeSet(Set.Champions).IncludeMonsterPitDeck().SchemeId(88).Build(),
            new SchemeInfoBuilder().SetSchemeName("Divide and Conquer").SetSchemeSet(Set.Champions).SetHeroCount(7).SchemeId(89).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hypnotize Every Human").SetSchemeSet(Set.Champions).AddAdditionalHenchmen(1).SetBystanderCount(0).SchemeId(90).Build(),
            new SchemeInfoBuilder().SetSchemeName("Steal All the Oxygen on Earth").SetSchemeSet(Set.Champions).SchemeId(91).Build(),

            new SchemeInfoBuilder().SetSchemeName("Break the Planet Asunder").SetSchemeSet(Set.Wwh).SetSchemeTwists(9).SetHeroCount(7).SchemeId(92).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cytoplasm Spike Invasion").SetSchemeSet(Set.Wwh).SetSchemeTwists(10).IncludeInfectedDeck().SchemeId(93).Build(),
            new SchemeInfoBuilder().SetSchemeName("Fall of the Hulks").SetSchemeSet(Set.Wwh).SetSchemeTwists(10).SetWoundCount(true, 6).SetNumberOfHeroWithNameLike(2,"Hulk").SchemeId(94).Build(),
            new SchemeInfoBuilder().SetSchemeName("Gladiator Pits of Sakaar").SetSchemeSet(Set.Wwh).SetSchemeTwists(6).SchemeId(95).Build(),
            new SchemeInfoBuilder().SetSchemeName("Mutating Gamma Rays").SetSchemeSet(Set.Wwh).SetSchemeTwists(7).IncludeMutationDeck().SchemeId(96).Build(),
            new SchemeInfoBuilder().SetSchemeName("Shoot Hulk into Space").SetSchemeSet(Set.Wwh).IncludeHulkDeck().SchemeId(97).Build(),
            new SchemeInfoBuilder().SetSchemeName("Subjugate with Obedience Disks").SetSchemeSet(Set.Wwh).SetSchemeTwists(11).SchemeId(98).Build(),
            new SchemeInfoBuilder().SetSchemeName("World War Hulk").SetSchemeSet(Set.Wwh).SetSchemeTwists(9).SetWorldWarHulkMasterminds().SchemeId(99).Build(),

            new SchemeInfoBuilder().SetSchemeName("Asgard Under Siege (Negative Zone Prison Breakout)").SetSchemeSet(Set.P1).AddAdditionalHenchmen(1).CannotBeSolo().SchemeId(100).Duplicates(new List<int>{3, 100, 191}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Destroy the Cities of Earth! (Midtown Bank Robbery)").SetSchemeSet(Set.P1).SetBystanderCount(12).SchemeId(101).Duplicates(new List<int>{2, 101, 188}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enslave Minds with the Chitauri Scepter (Secret Invasion of the Skrull Shapeshifters)").SetSchemeSet(Set.P1).SetHeroCount(6).SetRequiredVillains("Chitauri", Set.P1).SetRandomHeroCardsInVillainDeck(12).SchemeId(102).Duplicates(new List<int>{6, 102, 194}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Invade Asgard (Portals to The Dark Dimension)").SetSchemeSet(Set.P1).SetSchemeTwists(7).SchemeId(103).Duplicates(new List<int>{4, 103, 192}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Radioactive Palladium Poisoning (The Legacy Virus)").SetSchemeSet(Set.P1).SetWoundCount(true, 6).SchemeId(104).Duplicates(new List<int>{1, 104, 190}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders with HYDRA (Replace Earth's Leaders With Killbots)").SetSchemeSet(Set.P1).SetSchemeTwists(5).SetTwistsNextToScheme(3).SetBystanderCount(18).SchemeId(105).Duplicates(new List<int>{5, 105, 193}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Super Hero Civil War (Super Hero Civil War)").SetSchemeSet(Set.P1).CannotBeSolo().SetSchemeTwists(new List<int> { 0,8,8,5,5}).SetHeroCount(new List<int> {0,4,5,5,6}).SchemeId(106).Duplicates(new List<int>{7, 106, 195}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube (Unleash The Power Of The Cosmic Cube)").SetSchemeSet(Set.P1).SchemeId(107).Duplicates(new List<int>{8, 107, 196}).Build(),

            new SchemeInfoBuilder().SetSchemeName("Age of Ultron").SetSchemeSet(Set.Antman).SetSchemeTwists(11).SetHeroCount(new List<int> { 3, 5, 5, 6, 7}).SchemeId(108).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Earth Into Midieval Times").SetSchemeSet(Set.Antman).SetSchemeTwists(9).SchemeId(109).Build(),
            new SchemeInfoBuilder().SetSchemeName("Transform Commuters Into Giant Ants").SetSchemeSet(Set.Antman).SetSchemeTwists(new List<int> { 7, 8, 9, 10, 11}).SchemeId(110).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trap Heroes In The Microverse").SetSchemeSet(Set.Antman).SetSchemeTwists(11).HeroesInVillainDeck(1).SchemeId(111).Build(),

            new SchemeInfoBuilder().SetSchemeName("Invasion of the Venom Symbiotes").SetSchemeSet(Set.Venom).AddAdditionalHenchmen(1).SchemeId(112).Build(),
            new SchemeInfoBuilder().SetSchemeName("Maximum Carnage").SetSchemeSet(Set.Venom).SetSchemeTwists(10).SetWoundCount(true,6).SchemeId(113).Build(),
            new SchemeInfoBuilder().SetSchemeName("Paralyzing Venom").SetSchemeSet(Set.Venom).SetSchemeTwists(6).SchemeId(114).Build(),
            new SchemeInfoBuilder().SetSchemeName("Symbiotic Absorption").SetSchemeSet(Set.Venom).SetSchemeTwists(11).SetDrainedMastermind().SchemeId(115).Build(),

            new SchemeInfoBuilder().SetSchemeName("Earthquake Drains the Ocean").SetSchemeSet(Set.Revelations).SetSchemeTwists(11).AddAdditionalVillain(1).SchemeId(116).Build(),
            new SchemeInfoBuilder().SetSchemeName("House of M").SetSchemeSet(Set.Revelations).HeroesInVillainDeck("Scarlet Witch", Set.Revelations).Is4v2().SchemeId(117).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Korvac Saga").SetSchemeSet(Set.Revelations).SchemeId(118).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret HYDRA Corruption").SetSchemeSet(Set.Revelations).SetSchemeTwists(new List<int>{7,9,9,11,11}).SchemeId(119).Build(),

            new SchemeInfoBuilder().SetSchemeName("Hail Hydra").SetSchemeSet(Set.Shield).SetSchemeTwists(11).SchemeId(120).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hydra Helicarriers Hunt Heroes").SetSchemeSet(Set.Shield).AddAdditionalHero(1).SchemeId(121).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Empire of Betrayal").SetSchemeSet(Set.Shield).SetSchemeTwists(11).SetDarkLoyalty().SchemeId(122).Build(),
            new SchemeInfoBuilder().SetSchemeName("S.H.I.E.L.D. vs. Hydra War").SetSchemeSet(Set.Shield).SetSchemeTwists(7).SetOneButNotOther(new List<Villain>{Villain.GetNewVillain("Hydra Elite", Set.Shield), Villain.GetNewVillain("A.I.M., Hydra Offshoot",Set.Shield) }).SchemeId(123).Build(),

            new SchemeInfoBuilder().SetSchemeName("Asgardian Test of Worth").SetSchemeSet(Set.Asgard).SetSchemeTwists(11).SchemeId(124).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Dark World of Svartalfheim").SetSchemeSet(Set.Asgard).SetSchemeTwists(10).SchemeId(125).Build(),
            new SchemeInfoBuilder().SetSchemeName("Ragnarok, Twilight of the Gods").SetSchemeSet(Set.Asgard).SetSchemeTwists(11).SchemeId(126).Build(),
            new SchemeInfoBuilder().SetSchemeName("War of the Frost Giants").SetSchemeSet(Set.Asgard).SetSchemeTwists(9).SchemeId(127).Build(),

            new SchemeInfoBuilder().SetSchemeName("Crash the Moon into the Sun").SetSchemeSet(Set.NewMutants).SetSchemeTwists(11).SchemeId(128).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Demon Bear Saga").SetSchemeSet(Set.NewMutants).SetRequiredVillains("Demons of Limbo", Set.NewMutants).SchemeId(129).Build(),
            new SchemeInfoBuilder().SetSchemeName("Superhuman Baseball Game").SetSchemeSet(Set.NewMutants).SetSchemeTwists(9).AddAdditionalVillain(1).SchemeId(130).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trapped in the Insane Asylum").SetSchemeSet(Set.NewMutants).SetSchemeTwists(new List<int>{3,5,7,9,11}).SchemeId(131).Build(),

            new SchemeInfoBuilder().SetSchemeName("Annihilation Conquest").SetSchemeSet(Set.Cosmos).SetSchemeTwists(11).AddAdditionalHero(1).SchemeId(132).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Contest of Champions").SetSchemeSet(Set.Cosmos).SetSchemeTwists(11).AddAdditionalHero(1).SchemeId(133).Build(),
            new SchemeInfoBuilder().SetSchemeName("Destroy the Nova Corps").SetSchemeSet(Set.Cosmos).SetSchemeTwists(9).SetHeroCount(new List<int>(){ 5, 5, 5, 5, 6 }).SetNumberOfHeroWithNameLike(1, "Nova").SchemeId(134).Build(),
            new SchemeInfoBuilder().SetSchemeName("Turn the Soul of Adam Warlock").SetSchemeSet(Set.Cosmos).SetSchemeTwists(14).SetSoulsDeck("Adam Warlock", Set.Cosmos).SchemeId(135).Build(),

            new SchemeInfoBuilder().SetSchemeName("Devolve with Xerogen Crystals").SetSchemeSet(Set.Inhumans).SetSchemeTwists(new List<int>{4,5,6,7,8}).AddXerogenHenchmen().SchemeId(136).Build(),
            new SchemeInfoBuilder().SetSchemeName("Ruin the Perfect Wedding").SetSchemeSet(Set.Inhumans).SetRoyalWedding().SchemeId(137).Build(),
            new SchemeInfoBuilder().SetSchemeName("Tornado of Terrigen Mists").SetSchemeSet(Set.Inhumans).SetSchemeTwists(10).AddAdditionalVillain(1).SchemeId(138).Build(),
            new SchemeInfoBuilder().SetSchemeName("War of Kings").SetSchemeSet(Set.Inhumans).SetSchemeTwists(11).SchemeId(139).Build(),

            new SchemeInfoBuilder().SetSchemeName("Breach Parallel Dimensions").SetSchemeSet(Set.Annihilation).SetSchemeTwists(6).IncreaseBystanders(4).SchemeId(140).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pulse Waves from the Negative Zone").SetSchemeSet(Set.Annihilation).SetSchemeTwists(9).SchemeId(141).Build(),
            new SchemeInfoBuilder().SetSchemeName("Put Humanity on Trial").SetSchemeSet(Set.Annihilation).SetSchemeTwists(11).SchemeId(142).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sneak Attack the Heroes").SetSchemeSet(Set.Annihilation).SetSchemeTwists(6).SchemeId(143).Build(),

            new SchemeInfoBuilder().SetSchemeName("Drain Mutants' Powers To...").SetSchemeSet(Set.Messiah).SetSchemeTwists(11).SetVeiledScheme().SchemeId(144).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hack Cerebro Servers To...").SetSchemeSet(Set.Messiah).SetSchemeTwists(10).SetVeiledScheme().SchemeId(145).Build(),
            new SchemeInfoBuilder().SetSchemeName("Hire Singularity Investigations To...").SetSchemeSet(Set.Messiah).SetSchemeTwists(9).SetVeiledScheme().SchemeId(146).Build(),
            new SchemeInfoBuilder().SetSchemeName("Raid Gene Banks To...").SetSchemeSet(Set.Messiah).SetVeiledScheme().SchemeId(147).Build(),

            new SchemeInfoBuilder().SetSchemeName("Claim Souls for Demons").SetSchemeSet(Set.Strange).SchemeId(148).Build(),
            new SchemeInfoBuilder().SetSchemeName("Cursed Pages of the Darkhold Tome").SetSchemeSet(Set.Strange).SetSchemeTwists(11).AddAdditionalVillain(1).SchemeId(149).Build(),
            new SchemeInfoBuilder().SetSchemeName("Duels of Science and Magic").SetSchemeSet(Set.Strange).SetSchemeTwists(new List<int>{10,9,11,10,11}).SchemeId(150).Build(),
            new SchemeInfoBuilder().SetSchemeName("War for the Dream Dimension").SetSchemeSet(Set.Strange).SetSchemeTwists(7).AddAdditionalVillain(1).SchemeId(151).Build(),

            new SchemeInfoBuilder().SetSchemeName("Inescapable \"Kyln\" Space Prison").SetSchemeSet(Set.Guardians).AddAdditionalVillain(1).SchemeId(152).Build(),
            new SchemeInfoBuilder().SetSchemeName("Provoke the Sovereign War Fleet").SetSchemeSet(Set.Guardians).SetSchemeTwists(11).AddAdditionalVillain(1).SchemeId(153).Build(),
            new SchemeInfoBuilder().SetSchemeName("Star-Lord's Awesome Mix Tape").SetSchemeSet(Set.Guardians).SetSchemeTwists(7).SetHeroCount(7).DoubleHenchmen().DoubleVillains().IncludeHeroTeams(1, HeroTeam.GuardiansOfTheGalaxy).SchemeId(154).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Abilisk Space Monster").SetSchemeSet(Set.Guardians).SetSchemeTwists(9).SchemeId(155).Build(),

            new SchemeInfoBuilder().SetSchemeName("Plunder Wakanda's Vibranium").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(10).SchemeId(156).Build(),
            new SchemeInfoBuilder().SetSchemeName("Poison Lakes with Nanite Microbots").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(new List<int>{5,6,7,8,9}).SetWoundCount(false, 30).SchemeId(157).Build(),
            new SchemeInfoBuilder().SetSchemeName("Provoke a Clash of Nations").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(11).SchemeId(158).Build(),
            new SchemeInfoBuilder().SetSchemeName("Seize the Wakandan Throne").SetSchemeSet(Set.BlackPanther).SetSchemeTwists(6).SchemeId(159).Build(),

            new SchemeInfoBuilder().SetSchemeName("Corrupt the Spy Agencies").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(7).SchemeId(160).Build(),
            new SchemeInfoBuilder().SetSchemeName("Frame Heroes for Murder").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(7).SetHeroCount(6).SchemeId(161).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sniper Rifle Assassins").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(new List<int>{10,9,8,7,6}).SchemeId(162).Build(),
            new SchemeInfoBuilder().SetSchemeName("Train Black Widows in the Red Room").SetSchemeSet(Set.BlackWidow).SetSchemeTwists(new List<int>{7,6,5,4,3}).SetVillainOfficers(8).SchemeId(163).Build(),

            new SchemeInfoBuilder().SetSchemeName("Halve All Life in the Universe").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(5).SchemeId(164).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sacrifice for the Soul Stone").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(new List<int>{5,6,7,8,9}).SchemeId(165).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Time Heist").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(11).SetHeroCount(8).SchemeId(166).Build(),
            new SchemeInfoBuilder().SetSchemeName("Warp Reality Into a TV Show").SetSchemeSet(Set.InfinitySaga).SetSchemeTwists(11).SchemeId(167).Build(),

            new SchemeInfoBuilder().SetSchemeName("Midnight Massacre").SetSchemeSet(Set.MidnightSons).SetSchemeTwists(11).HeroesInVillainDeckWithNameLike(1, "Blade").SchemeId(168).Build(),
            new SchemeInfoBuilder().SetSchemeName("Ritual Sacrifice to Summon Chthon").SetSchemeSet(Set.MidnightSons).SchemeId(169).Build(),
            new SchemeInfoBuilder().SetSchemeName("Sire Vampires at the Blood Bank").SetSchemeSet(Set.MidnightSons).SetSchemeTwists(10).SetVampireNaniteHenchmen().SchemeId(170).Build(),
            new SchemeInfoBuilder().SetSchemeName("Wager at Blackjack for Heroes' Souls").SetSchemeSet(Set.MidnightSons).SetSchemeTwists(11).AddAdditionalHero(2).SchemeId(171).Build(),

            new SchemeInfoBuilder().SetSchemeName("Breach the Nexus of All Realities").SetSchemeSet(Set.WhatIf).SetVillainCount(new List<int>{ 3, 3, 3, 3, 4 }).SetSchemeTwists(new List<int>{6,6,6,6,8}).SchemeId(172).Build(),
            new SchemeInfoBuilder().SetSchemeName("Collect an Interstellar Zoo").SetSchemeSet(Set.WhatIf).SetSchemeTwists(11).SchemeId(173).Build(),
            new SchemeInfoBuilder().SetSchemeName("Marvel Zombies").SetSchemeSet(Set.WhatIf).SetSchemeTwists(4).HeroesInVillainDeck(1).MarvelZombieVillains(1, Keywords.LivingDead).SetBystanderCount(new List<int>{ 4, 5, 8, 8, 12 }).SchemeId(174).Build(),
            new SchemeInfoBuilder().SetSchemeName("Trash Earth with Hugest Party Ever").SetSchemeSet(Set.WhatIf).SetSchemeTwists(6).SetRequiredHeroes("Party Thor").SetRequiredVillains("Intergalactic Party Animals", Set.WhatIf).SchemeId(175).Build(),

            new SchemeInfoBuilder().SetSchemeName("Auction Shrink Tech to Highest Bidder").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(11).SetShrinkTechDeck().SchemeId(176).Build(),
            new SchemeInfoBuilder().SetSchemeName("Escape an Imprisoning Dimension").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(5).SchemeId(177).Build(),
            new SchemeInfoBuilder().SetSchemeName("Safeguard Dark Secrets").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(5).SchemeId(178).Build(),
            new SchemeInfoBuilder().SetSchemeName("Siphon Energy from the Quantum Realm").SetSchemeSet(Set.AntmanWasp).SetSchemeTwists(9).IncludeQuantumRealmDeck().SchemeId(179).Build(),

            new SchemeInfoBuilder().SetSchemeName("Become President of the United States").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(11).SchemeId(180).Build(),
            new SchemeInfoBuilder().SetSchemeName("Befoul Earth Into a Polluted Wasteland").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(11).AddAdditionalHero(1).SchemeId(181).Build(),
            new SchemeInfoBuilder().SetSchemeName("Pull Reality Into Cyberspace").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(7).SchemeId(182).Build(),
            new SchemeInfoBuilder().SetSchemeName("Subjugate Earth with Mega-Corporations").SetSchemeSet(Set.TwentyNintyNine).SetSchemeTwists(11).AddAdditionalHero(1).SchemeId(183).Build(),

            new SchemeInfoBuilder().SetSchemeName("Condition Logan Into Weapon X").SetSchemeSet(Set.WeaponX).SetNumberOfHeroWithNameLike(1, "Wolverine").SchemeId(184).Build(),
            //Need to code "Don't use multiple Heroes that have the same Hero Name.SchemeId(185)
            new SchemeInfoBuilder().SetSchemeName("Go After Heroes' Loved Ones").SetSchemeSet(Set.WeaponX).SetSchemeTwists(new List<int>{ 8, 10, 10, 10, 11 }).AddAdditionalHero(1).NoDuplicates().SetLovedOnesDeck().SchemeId(186).Build(),
            new SchemeInfoBuilder().SetSchemeName("Wipe Heroes' Memories").SetSchemeSet(Set.WeaponX).SetSchemeTwists(new List<int>{ 5, 6, 7, 8, 9 }).SchemeId(187).Build(),

            new SchemeInfoBuilder().SetSchemeName("Bank Robbery Hostage Crisis").SetSchemeTwists(9).AddAdditionalVillain(1).SetSchemeSet(Set.Core2E).SchemeId(188).Duplicates(new List<int>{2, 101, 188}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Enshrounded Identity").SetSchemeTwists(new List<int> { 4,5,6,7,8}).SetEnshroudedGame().SetSchemeSet(Set.Core2E).SchemeId(189).Build(),
            new SchemeInfoBuilder().SetSchemeName("The Legacy Virus").SetSchemeTwists(9).SetWoundCount(true, 6).SetSchemeSet(Set.Core2E).SchemeId(190).Duplicates(new List<int>{1, 104, 190}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Negative Zone Prison Breakout").SetSchemeTwists(new List<int> { 7,8,9,10,11}).AddAdditionalVillain(1).IncreaseBystanders(4).SetSchemeSet(Set.Core2E).CannotBeSolo().SchemeId(191).Duplicates(new List<int>{3, 100, 191}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Portals to The Dark Dimension").SetSchemeTwists(7).SetSchemeSet(Set.Core2E).SchemeId(192).Duplicates(new List<int>{4, 103, 192}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Replace Earth's Leaders With Killbots").SetSchemeTwists(10).SetTwistsNextToScheme(1).SetSchemeSet(Set.Core2E).SchemeId(193).Duplicates(new List<int>{5, 105, 193}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Secret Invasion of the Skrull Shapeshifters").SetSchemeTwists(6).AddAdditionalHero(1).SetRequiredVillains("Skrulls", Set.Core).SetRandomHeroCardsInVillainDeck(4).SetSchemeSet(Set.Core2E).SchemeId(194).Duplicates(new List<int>{6, 102, 194}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Superhero Civil War").SetSchemeTwists(new List<int> { 6,6,6,5,5}).SetHeroCount(new List<int> {3,4,5,5,6}).SetSchemeSet(Set.Core2E).SchemeId(195).Duplicates(new List<int>{7, 106, 195}).Build(),
            new SchemeInfoBuilder().SetSchemeName("Unleash the Power of the Cosmic Cube").SetSchemeSet(Set.Core2E).SchemeId(196).Duplicates(new List<int>{8, 107, 196}).Build()
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
        public List<Hero> HeroesInVillainDeck { get; set; }
        public int RandomHeroesInVillainDeck { get; set; }

        public int NumberOfVillains { get; set; }
        public List<Villain> RequiredVillains { get; set; }

        public int NumberOfMasterminds { get; set; }

        public int NumberOfHenchmen { get; set; }
        public List<Henchmen> RequiredHenchmen { get; set; }

        public bool WoundsPerPlayer { get; set; }
        public bool CustomWoundNumber { get; set; }
        public List<int> Wounds { get; set; }

        public int BystandersInVillainDeck { get; set; }
        public int BystandersInHeroDeck { get; set; }
        public bool IsBystandersInHeroDeck { get; set; }

        public Scheme() 
        {
        }

        //This function is only used in the ConverTrackedGames class
        public static Scheme GetNewScheme(Scheme schemeName = null)
        {
            if (schemeName != null) return schemeName;
            
            var allSchemes = SchemeRepository.All.ToList();
            var schemeInfo = allSchemes[RandomHelper.Instance.Next(allSchemes.Count)];

            return new Scheme
            {
                SchemeName = schemeInfo.SchemeName,
                SetName = schemeInfo.SetName
            };
        }

        public static Scheme GetNewScheme(string schemeName, Set set)
        {
            var schemeInfo = SchemeRepository.All.FirstOrDefault(s => s.SchemeName == schemeName && s.SetName == set);

            return new Scheme
            {
                SchemeName = schemeInfo.SchemeName,
                SetName = schemeInfo.SetName
            };
        }

        public static Scheme GetNewScheme(int playerCount, Mastermind mastermind, string schemeName=null, Set setName = Set.Unknown)
        {
            SchemeInfo schemeInfo;

            if (string.IsNullOrEmpty(schemeName) || setName == Set.Unknown)
            {
                schemeInfo = GetRandomScheme(mastermind);
            }
            else
            {
                schemeInfo = GetSchemeInfo(schemeName, setName);
            }

            var newScheme = ProcessSchemeInfo(playerCount, schemeInfo, mastermind);
            return newScheme;
        }

        private static SchemeInfo GetSchemeInfo(string schemeName, Set set)
        {
            return SchemeRepository.All.First(x => x.SchemeName == schemeName && x.SetName == set);
        }

        private static SchemeInfo GetRandomScheme(Mastermind mastermind)
        {
            var mastermindCard = new Card
            {
                CardName = mastermind.MastermindName,
                CardType = (int)CardType.Mastermind,
                SetId = (int)mastermind.SetName
            };

            var schemeCardsPlayedWithMastermind = SqlHelper.GetCardRelationships(CardType.Scheme, mastermindCard);
            var schemesPlayedWithMastermind = ConvertToSchemeList(schemeCardsPlayedWithMastermind);

            var schemeNameList = SchemeRepository.All.ToList();

            var idsInGame = new HashSet<int>(schemesPlayedWithMastermind.Select(s => s.Id));
            var remainingSchemes = schemeNameList.Where(s => !idsInGame.Contains(s.Id)).ToList();

            if(remainingSchemes.Count > 0)
            {
                return remainingSchemes[RandomHelper.Instance.Next(remainingSchemes.Count)];
            }
            else
            {
                return schemeNameList[RandomHelper.Instance.Next(schemeNameList.Count)];
            }
        }

        private static List<SchemeInfo> ConvertToSchemeList(List<Card> cardList)
        {
            List<SchemeInfo> returnList = new List<SchemeInfo>();
            foreach (var card in cardList)
            {
                returnList.Add(SchemeRepository.All.First(s => s.SchemeName == card.CardName && (int)s.SetName == card.SetId));
            }

            return returnList;
        }

        public static Scheme ProcessSchemeInfo(int playerCount, SchemeInfo schemeInfo, Mastermind mastermind)
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
            newScheme.NumberOfSchemeTwists = newScheme.SchemeName == "Ritual Sacrifice to Summon Chthon" && mastermind.MastermindName == "Lilith" ? 1 : schemeInfo.SchemeTwists[playerCount - 1];
            newScheme.SchemeInfo = schemeInfo;
            newScheme.IsSchemeTwistsNextToScheme = schemeInfo.IsSchemeTwistsNextToScheme;
            newScheme.NumberTwistsNextToScheme = schemeInfo.NumberTwistsNextToScheme;
            newScheme.NumberOfPlayers = playerCount;

            newScheme.NumberOfMasterminds = schemeInfo.NumberOfMasterminds;

            newScheme.NumberOfVillains = schemeInfo.Villains[playerCount - 1];
            
            //This covers the case in the Ritual Sacrifice to Summon Chthon where the mastermind is Lilith
            if (newScheme.SchemeName == "Ritual Sacrifice to Summon Chthon" && mastermind.MastermindName == "Lilith")
                newScheme.NumberOfVillains++;

            newScheme.RequiredVillains = schemeInfo.RequiredVillains;

            //This covers the case in the Ritual Sacrifice to Summon Chthon where the mastermind is not Lilith
            if (newScheme.SchemeName == "Ritual Sacrifice to Summon Chthon" && mastermind.MastermindName != "Lilith")
                newScheme.RequiredVillains.Add(Villain.GetNewVillain("Lilin", Set.MidnightSons));

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

        public static List<string> GetListOfSchemes()
        {
            var allSchemes = SchemeRepository.All.Select(s => s.SchemeName).ToList();
            return allSchemes;
        }

        public static string ToString(Scheme scheme)
        {
            return $"{scheme.SchemeName}, {scheme.SetName.GetDescription()}";
        }
    }
}