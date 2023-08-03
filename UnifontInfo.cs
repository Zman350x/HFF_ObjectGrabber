using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectGrabber
{
    using HarmonyLib;
    using UnityEngine;
    using TMPro;
    class UnifontInfo
    {
        public static TMP_FontAsset unifont = ScriptableObject.CreateInstance<TMP_FontAsset>();

        public static FaceInfo fontInfo = new FaceInfo
        {      
            Ascender = 62,
            AtlasHeight = 512,
		    AtlasWidth = 512,
		    Baseline = 0,
		    CapHeight = 87,
		    CenterLine = 0,
		    CharacterCount = 99,
		    Descender = -9,
		    LineHeight = 70,
		    Name = "Unifont",
		    Padding = 9,
		    PointSize = 70,
		    Scale = 1,
		    strikethrough = 22,
		    strikethroughThickness = 1.09375f,
		    SubscriptOffset = 0,
		    SubSize = 0.5f,
		    SuperscriptOffset = 62,
		    TabWidth = 350,
		    Underline = -4,
		    UnderlineThickness = 1.09375f
        };

        public static TMP_Glyph[] characters = new TMP_Glyph[]
        {
            new TMP_Glyph
            {
                id = 32,
                width = 35,
                height = 71,
                scale = 1,
                x = 9,
                y = 503,
                xOffset = 0,
                yOffset = 62,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 33,
                width = 4,
                height = 44,
                scale = 1,
                x = 434,
                y = 397,
                xOffset = 17,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 34,
                width = 22,
                height = 18,
                scale = 1,
                x = 466,
                y = 43,
                xOffset = 8,
                yOffset = 52,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 35,
                width = 27,
                height = 44,
                scale = 1,
                x = 134,
                y = 247,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 36,
                width = 31,
                height = 44,
                scale = 1,
                x = 111,
                y = 371,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 37,
                width = 31,
                height = 44,
                scale = 1,
                x = 177,
                y = 459,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 38,
                width = 31,
                height = 44,
                scale = 1,
                x = 117,
                y = 164,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 39,
                width = 4,
                height = 18,
                scale = 1,
                x = 439,
                y = 121,
                xOffset = 17,
                yOffset = 52,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 40,
                width = 13,
                height = 52,
                scale = 1,
                x = 9,
                y = 63,
                xOffset = 13,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 41,
                width = 13,
                height = 52,
                scale = 1,
                x = 40,
                y = 63,
                xOffset = 8,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 42,
                width = 31,
                height = 31,
                scale = 1,
                x = 166,
                y = 136,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 43,
                width = 31,
                height = 31,
                scale = 1,
                x = 215,
                y = 136,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 44,
                width = 9,
                height = 18,
                scale = 1,
                x = 448,
                y = 184,
                xOffset = 13,
                yOffset = 8,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 45,
                width = 17,
                height = 4,
                scale = 1,
                x = 146,
                y = 433,
                xOffset = 8,
                yOffset = 21,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 46,
                width = 9,
                height = 9,
                scale = 1,
                x = 448,
                y = 157,
                xOffset = 13,
                yOffset = 8,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 47,
                width = 27,
                height = 44,
                scale = 1,
                x = 166,
                y = 185,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 48,
                width = 27,
                height = 44,
                scale = 1,
                x = 179,
                y = 247,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 49,
                width = 22,
                height = 44,
                scale = 1,
                x = 391,
                y = 211,
                xOffset = 8,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 50,
                width = 27,
                height = 44,
                scale = 1,
                x = 183,
                y = 309,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 51,
                width = 27,
                height = 44,
                scale = 1,
                x = 211,
                y = 185,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 52,
                width = 27,
                height = 44,
                scale = 1,
                x = 224,
                y = 247,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 53,
                width = 27,
                height = 44,
                scale = 1,
                x = 169,
                y = 74,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 54,
                width = 27,
                height = 44,
                scale = 1,
                x = 196,
                y = 12,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 55,
                width = 27,
                height = 44,
                scale = 1,
                x = 214,
                y = 74,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 56,
                width = 27,
                height = 44,
                scale = 1,
                x = 256,
                y = 185,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 57,
                width = 27,
                height = 44,
                scale = 1,
                x = 241,
                y = 12,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 58,
                width = 9,
                height = 31,
                scale = 1,
                x = 448,
                y = 335,
                xOffset = 13,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 59,
                width = 9,
                height = 39,
                scale = 1,
                x = 491,
                y = 464,
                xOffset = 13,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 60,
                width = 22,
                height = 39,
                scale = 1,
                x = 107,
                y = 10,
                xOffset = 8,
                yOffset = 39,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 61,
                width = 27,
                height = 22,
                scale = 1,
                x = 466,
                y = 79,
                xOffset = 4,
                yOffset = 30,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 62,
                width = 22,
                height = 39,
                scale = 1,
                x = 451,
                y = 464,
                xOffset = 4,
                yOffset = 39,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 63,
                width = 27,
                height = 44,
                scale = 1,
                x = 259,
                y = 74,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 64,
                width = 27,
                height = 44,
                scale = 1,
                x = 286,
                y = 12,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 65,
                width = 27,
                height = 44,
                scale = 1,
                x = 304,
                y = 74,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 66,
                width = 27,
                height = 44,
                scale = 1,
                x = 331,
                y = 12,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 67,
                width = 27,
                height = 44,
                scale = 1,
                x = 209,
                y = 397,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 68,
                width = 27,
                height = 44,
                scale = 1,
                x = 226,
                y = 459,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 69,
                width = 27,
                height = 44,
                scale = 1,
                x = 228,
                y = 335,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 70,
                width = 27,
                height = 44,
                scale = 1,
                x = 254,
                y = 397,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 71,
                width = 27,
                height = 44,
                scale = 1,
                x = 271,
                y = 459,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 72,
                width = 27,
                height = 44,
                scale = 1,
                x = 269,
                y = 273,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 73,
                width = 22,
                height = 44,
                scale = 1,
                x = 404,
                y = 273,
                xOffset = 8,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 74,
                width = 31,
                height = 44,
                scale = 1,
                x = 120,
                y = 71,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 75,
                width = 27,
                height = 44,
                scale = 1,
                x = 273,
                y = 335,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 76,
                width = 27,
                height = 44,
                scale = 1,
                x = 299,
                y = 397,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 77,
                width = 27,
                height = 44,
                scale = 1,
                x = 316,
                y = 459,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 78,
                width = 27,
                height = 44,
                scale = 1,
                x = 301,
                y = 211,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 79,
                width = 27,
                height = 44,
                scale = 1,
                x = 314,
                y = 273,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 80,
                width = 27,
                height = 44,
                scale = 1,
                x = 318,
                y = 335,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 81,
                width = 31,
                height = 48,
                scale = 1,
                x = 71,
                y = 67,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 82,
                width = 27,
                height = 44,
                scale = 1,
                x = 344,
                y = 397,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 83,
                width = 27,
                height = 44,
                scale = 1,
                x = 361,
                y = 459,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 84,
                width = 31,
                height = 44,
                scale = 1,
                x = 147,
                y = 9,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 85,
                width = 27,
                height = 44,
                scale = 1,
                x = 313,
                y = 149,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 86,
                width = 31,
                height = 44,
                scale = 1,
                x = 134,
                y = 309,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 87,
                width = 27,
                height = 44,
                scale = 1,
                x = 346,
                y = 211,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 88,
                width = 27,
                height = 44,
                scale = 1,
                x = 359,
                y = 273,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 89,
                width = 31,
                height = 44,
                scale = 1,
                x = 160,
                y = 371,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 90,
                width = 27,
                height = 44,
                scale = 1,
                x = 363,
                y = 335,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 91,
                width = 13,
                height = 52,
                scale = 1,
                x = 41,
                y = 133,
                xOffset = 17,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 92,
                width = 27,
                height = 44,
                scale = 1,
                x = 389,
                y = 397,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 93,
                width = 14,
                height = 52,
                scale = 1,
                x = 9,
                y = 133,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 94,
                width = 27,
                height = 14,
                scale = 1,
                x = 421,
                y = 11,
                xOffset = 4,
                yOffset = 52,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 95,
                width = 31,
                height = 4,
                scale = 1,
                x = 97,
                y = 433,
                xOffset = 4,
                yOffset = 0,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 96,
                width = 13,
                height = 13,
                scale = 1,
                x = 444,
                y = 304,
                xOffset = 8,
                yOffset = 56,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 97,
                width = 27,
                height = 35,
                scale = 1,
                x = 456,
                y = 384,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 98,
                width = 27,
                height = 48,
                scale = 1,
                x = 44,
                y = 212,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 99,
                width = 27,
                height = 35,
                scale = 1,
                x = 376,
                y = 34,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 100,
                width = 27,
                height = 48,
                scale = 1,
                x = 72,
                y = 146,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 101,
                width = 27,
                height = 35,
                scale = 1,
                x = 394,
                y = 96,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 102,
                width = 22,
                height = 48,
                scale = 1,
                x = 94,
                y = 301,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 103,
                width = 27,
                height = 48,
                scale = 1,
                x = 49,
                y = 292,
                xOffset = 4,
                yOffset = 39,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 104,
                width = 27,
                height = 48,
                scale = 1,
                x = 66,
                y = 367,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 105,
                width = 22,
                height = 48,
                scale = 1,
                x = 97,
                y = 455,
                xOffset = 8,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 106,
                width = 22,
                height = 57,
                scale = 1,
                x = 9,
                y = 278,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 107,
                width = 27,
                height = 48,
                scale = 1,
                x = 89,
                y = 226,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 108,
                width = 22,
                height = 48,
                scale = 1,
                x = 137,
                y = 455,
                xOffset = 8,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 109,
                width = 31,
                height = 35,
                scale = 1,
                x = 9,
                y = 10,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 110,
                width = 27,
                height = 35,
                scale = 1,
                x = 403,
                y = 158,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 111,
                width = 27,
                height = 35,
                scale = 1,
                x = 431,
                y = 220,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 112,
                width = 27,
                height = 44,
                scale = 1,
                x = 406,
                y = 459,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 113,
                width = 27,
                height = 44,
                scale = 1,
                x = 349,
                y = 87,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 114,
                width = 27,
                height = 35,
                scale = 1,
                x = 476,
                y = 331,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 115,
                width = 27,
                height = 35,
                scale = 1,
                x = 476,
                y = 278,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 116,
                width = 22,
                height = 44,
                scale = 1,
                x = 408,
                y = 335,
                xOffset = 4,
                yOffset = 43,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 117,
                width = 27,
                height = 35,
                scale = 1,
                x = 476,
                y = 225,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 118,
                width = 27,
                height = 35,
                scale = 1,
                x = 476,
                y = 172,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 119,
                width = 31,
                height = 35,
                scale = 1,
                x = 58,
                y = 10,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 120,
                width = 27,
                height = 35,
                scale = 1,
                x = 475,
                y = 119,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 121,
                width = 27,
                height = 44,
                scale = 1,
                x = 358,
                y = 149,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 122,
                width = 27,
                height = 35,
                scale = 1,
                x = 421,
                y = 43,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 123,
                width = 17,
                height = 57,
                scale = 1,
                x = 31,
                y = 358,
                xOffset = 8,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 124,
                width = 4,
                height = 62,
                scale = 1,
                x = 9,
                y = 353,
                xOffset = 17,
                yOffset = 52,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 125,
                width = 17,
                height = 57,
                scale = 1,
                x = 9,
                y = 203,
                xOffset = 8,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 126,
                width = 31,
                height = 13,
                scale = 1,
                x = 117,
                y = 133,
                xOffset = 4,
                yOffset = 48,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 160,
                width = 0,
                height = 0,
                scale = 1,
                x = 9,
                y = 503,
                xOffset = 0,
                yOffset = 0,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 8203,
                width = 70,
                height = 70,
                scale = 1,
                x = 9,
                y = 433,
                xOffset = 0,
                yOffset = 61,
                xAdvance = 70
            },
            new TMP_Glyph
            {
                id = 8230,
                width = 31,
                height = 9,
                scale = 1,
                x = 456,
                y = 437,
                xOffset = 4,
                yOffset = 8,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 9633,
                width = 31,
                height = 31,
                scale = 1,
                x = 264,
                y = 136,
                xOffset = 4,
                yOffset = 35,
                xAdvance = 35
            },
            new TMP_Glyph
            {
                id = 32,
                width = 0,
                height = 71,
                scale = 1,
                x = 9,
                y = 503,
                xOffset = 0,
                yOffset = 62,
                xAdvance = 0
            },
            new TMP_Glyph
            {
                id = 10,
                width = 10,
                height = 71,
                scale = 1,
                x = 0,
                y = 0,
                xOffset = 0,
                yOffset = 62,
                xAdvance = 0
            },
            new TMP_Glyph
            {
                id = 10,
                width = 10,
                height = 71,
                scale = 1,
                x = 0,
                y = 0,
                xOffset = 0,
                yOffset = 62,
                xAdvance = 0
            },
            new TMP_Glyph
            {
                id = 9,
                width = 350,
                height = 71,
                scale = 1,
                x = 9,
                y = 503,
                xOffset = 0,
                yOffset = 62,
                xAdvance = 350
            }
        };

        public static void unifontInit(Texture2D atlas, Material material)
        {
            unifont.name = "Unifont";

            unifont.boldSpacing = 7;
            unifont.boldStyle = 0.75f;
            unifont.italicStyle = 35;
            unifont.fontAssetType = TMP_FontAsset.FontAssetTypes.Bitmap;
            unifont.normalSpacingOffset = 0;
            unifont.normalStyle = 0;
            unifont.tabSize = 1;

            unifont.atlas = atlas;
            unifont.material = material;

            unifont.AddFaceInfo(fontInfo);
            unifont.AddGlyphInfo(characters);
            unifont.AddKerningInfo(new KerningTable());
        }
    }
}
