using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary.Enums
{
    public enum Set
    {
        [Description("Core")]
        Core = 1,
        [Description("Dark City")]
        Dc = 2,
        [Description("Fantastic Four")]
        Ff = 3,
        [Description("Paint The Town Red")]
        PttR = 4,
        [Description("Villains")]
        Villains = 5,
        [Description("Guardians Of The Galaxy")]
        GotG = 6,
        [Description("Fear Itself")]
        Fi = 7,
        [Description("Secret Wars Volume 1")]
        Sw1 = 8,
        [Description("Secret Wars Volume 2")]
        Sw2 = 9,
        [Description("Captain America 75th Anniversary")]
        Ca = 10,
        [Description("Civil War")]
        Cw = 11,
        [Description("3D")]
        ThreeD = 12,
        [Description("Deadpool")]
        Deadpool = 13,
        [Description("Noir")]
        Noir = 14,
        [Description("X-Men")]
        XMen = 15,
        [Description("Spider-Man Homecoming")]
        Sm = 16,
        [Description("Champions")]
        Champions = 17,
        [Description("World War Hulk")]
        Wwh = 18,
        [Description("Phase 1")]
        P1 = 19,
        [Description("Ant-Man")]
        Antman = 20,
        [Description("Venom")]
        Venom = 21,
        [Description("Dimensions")]
        Dimensions = 22,
        [Description("Revelations")]
        Revelations = 23,
        [Description("S.H.I.E.L.D.")]
        Shield = 24,
        [Description("Heroes of Asgard")]
        Asgard = 25,
        [Description("The New Mutants")]
        NewMutants = 26,
        [Description("Into the Cosmos")]
        Cosmos = 27,
        [Description("Realm of Kings")]
        Inhumans = 28,
        [Description("Annihilation")]
        Annihilation = 29,
        [Description("Messiah Complex")]
        Messiah = 30,
        [Description("Doctor Strange and the Shadows of Nightmare")]
        Strange = 31,
        [Description("Marvel Studios' Guardians of the Galaxy")]
        Guardians = 32,
        [Description("Black Panther")]
        BlackPanther = 33,
        [Description("Black Widow")]
        BlackWidow = 34,
        [Description("Marvel Studios' The Infinity Saga")]
        InfinitySaga = 35,
        [Description("Midnight Sons")]
        MidnightSons = 36,
        [Description("Marvel Studios' What If...?")]
        WhatIf = 37,
        [Description("Ant-Man and the Wasp")]
        AntmanWasp = 38,
        [Description("2099")]
        TwentyNintyNine = 39,
        [Description("Weapon X")]
        WeaponX = 40,
        [Description("Core Second Edition")]
        Core2E = 41
    }

    public enum Keywords
    {
        None,
        [Description("Rise of the Living Dead")]
        LivingDead,
        [Description("Cross-Dimensional Rampage")]
        Rampage,
        [Description("Size-Changing")]
        Size,
        [Description("Divided")]
        Divided,
        [Description("Phasing")]
        Phasing,
        [Description("Versatile")]
        Versatile,
        [Description("Cheering Crowds")]
        Cheering,
        [Description("Demolish")]
        Demolish,
        [Description("Empowered")]
        Empowered,
        [Description("Antics")]
        Antics,
        [Description("Heist")]
        Heist,
        [Description("Explore")]
        Explore,
    }

    public enum CardType
    {
        Mastermind = 1,
        Scheme = 2,
        Villain = 3,
        Henchmen = 4,
        Hero = 5
    }
}
