using BepInEx.Configuration;

namespace MageArenaItalianVoice.Config
{
    public static class VoiceCommandConfig
    {
        public static ConfigEntry<string> FireballCommand;
        public static ConfigEntry<string> FrostBoltCommand;
        public static ConfigEntry<string> WormCommand;
        public static ConfigEntry<string> HoleCommand;
        public static ConfigEntry<string> MagicMissileCommand;
        public static ConfigEntry<string> MirrorCommand;
        
        public static ConfigEntry<string> RockCommand;
        public static ConfigEntry<string> WispCommand;
        public static ConfigEntry<string> BlastCommand;
        public static ConfigEntry<string> DivineCommand;
        public static ConfigEntry<string> BlinkCommand;
        public static ConfigEntry<string> ThunderboltCommand;

        public static void Init(ConfigFile config)
        {
            FireballCommand = config.Bind("Commands", "Fireball", "palla infuocata",
                "Italian command for Fireball spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            FrostBoltCommand = config.Bind("Commands", "FrostBolt", "congelati",
                "Italian command for Frost Bolt spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            WormCommand = config.Bind("Commands", "Worm", "buco",
                "Italian command for Worm spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            HoleCommand = config.Bind("Commands", "Hole", "nero",
                "Italian command for Hole spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            MagicMissileCommand = config.Bind("Commands", "MagicMissile", "missile magico",
                "Italian command for Magic Missile spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            MirrorCommand = config.Bind("Commands", "Mirror", "specchio",
                "Italian command for Mirror spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            RockCommand = config.Bind("AdditionalCommands", "Rock", "roccia",
                "Italian command for Rock spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            WispCommand = config.Bind("AdditionalCommands", "Wisp", "spirito",
                "Italian command for Wisp spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            BlastCommand = config.Bind("AdditionalCommands", "Blast", "raggio oscuro",
                "Italian command for Blast spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            DivineCommand = config.Bind("AdditionalCommands", "Divine", "luce divina",
                "Italian command for Divine spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            BlinkCommand = config.Bind("AdditionalCommands", "Blink", "salto",
                "Italian command for Blink spell (tutte le varianti di una parola possono essere separate da uno spazio)");
                
            ThunderboltCommand = config.Bind("AdditionalCommands", "Thunderbolt", "fulmine",
                "Italian command for Thunderbolt spell (tutte le varianti di una parola possono essere separate da uno spazio)");
        }
    }
}