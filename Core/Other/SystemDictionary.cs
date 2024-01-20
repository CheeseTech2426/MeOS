using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Core.Other
{
    public static class SystemDictionary
    {

        public static string PickRandomWord()
        {
            Random random = new Random();
            return words[random.Next(words.Length)];
        }

        public static string GetWordFromIndex(int index)
        {
            return words[index];
        }

        public static bool CheckIfExists(string word)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == word)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetWordIndex(string word)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == word)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string[] words = {
            "Elephant",
            "Computer",
            "Butterfly",
            "Adventure",
            "Sunshine",
            "Happiness",
            "Chocolate",
            "Universe",
            "Journey",
            "Rainbow",
            "Symphony",
            "Pancakes",
            "Zephyr",
            "Wanderlust",
            "Serendipity",
            "Galaxy",
            "Mysterious",
            "Dragonfly",
            "Lighthouse",
            "Thunderstorm",
            "Echo",
            "Whirlwind",
            "Enchantment",
            "Treasure",
            "Wonderland",
            "Squirrel",
            "Mountain",
            "Keyboard",
            "Blueprint",
            "Reflection",
            "Elegance",
            "Bouquet",
            "Melody",
            "Serenity",
            "Ponder",
            "Quasar",
            "Umbrella",
            "Tornado",
            "Enigma",
            "Symphony",
            "Celebration",
            "Freedom",
            "Ocean",
            "Firefly",
            "Adventure",
            "Bamboo",
            "Chameleon",
            "Radiance",
            "Serenade",
            "Mystery",
            "Harvest",
            "Oasis",
            "Imagination",
            "Horizon",
            "Zephyr",
            "Reverie",
            "Harmony",
            "Symphony",
            "Kaleidoscope",
            "Bliss",
            "Gazebo",
            "Cascade",
            "Whimsical",
            "Majestic",
            "Velvety",
            "Spontaneous",
            "Pinnacle",
            "Vibrant",
            "Utopia",
            "Whistle",
            "Gondola",
            "Velvet",
            "Carousel",
            "Liberty",
            "Elixir",
            "Puzzlement",
            "Infinity",
            "Phenomenal",
            "Eccentric",
            "Liberty",
            "Labyrinth",
            "Venture",
            "Quintessence",
            "Tranquil",
            "Quicksilver",
            "Cascade",
            "Illusion",
            "Nebula",
            "Luminous",
            "Spectacle",
            "Symphony",
            "Ethereal",
            "Oasis",
            "Infinitesimal",
            "Opulent",
            "Elusive",
            "Solitude",
            "Whisper",
            "Ephemeral",
            "Enchanted",
            "Cascade",
            "Essence",
            "Nebulous",
            "Resonance",
            "Rhapsody",
            "Utopia",
            "Ponder",
            "Serenity",
            "Whimsical",
            "Kaleidoscope",
            "Lagoon",
            "Mesmerize",
            "Enigma",
            "Panorama",
            "Jubilant",
            "Ineffable",
            "Gossamer",
            "Cascade",
            "Harmony",
            "Zenith",
            "Epiphany",
            "Twilight",
            "Luminous",
            "Illusion",
            "Elysium",
            "Ethereal",
            "Pinnacle",
            "Nebula",
            "Enchanted",
            "Jubilant",
            "Breeze",
            "Solitude",
            "Nimbus",
            "Ephemeral",
            "Captivate",
            "Ponder",
            "Lagoon",
            "Labyrinth",
            "Ethereal",
            "Cascade",
            "Zephyr",
            "Panorama",
            "Nebulous",
            "Whimsical",
            "Quintessence",
            "Ineffable",
            "Resonance",
            "Reverie",
            "Breathtaking",
            "Harmony",
            "Nebula",
            "Luminous",
            "Illusion",
            "Whimsical",
            "Utopia",
            "Zenith",
            "Cascade",
            "Elysium",
            "Opulent",
            "Rhapsody",
            "Essence",
            "Serenity",
            "Infinitesimal",
            "Gossamer",
            "Panorama",
            "Mesmerize",
            "Lagoon",
            "Pinnacle",
            "Illusion",
            "Nebulous",
            "Resonance",
            "Nebula",
            "Whimsical",
            "Ephemeral",
            "Harmony",
            "Zephyr",
            "Tranquil",
            "Jubilant",
            "Ethereal",
            "Nebula",
            "Serenade",
            "Quintessence",
            "Utopia",
            "Breeze",
            "Luminous",
            "Illusion",
            "Solitude",
            "Panorama",
            "Nebulous",
            "Resonance",
            "Nebula",
            "Whimsical",
            "Elysium"
        };
    }
}
