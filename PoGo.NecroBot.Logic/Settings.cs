#region using directives

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoGo.NecroBot.Logic;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;

#endregion

namespace PoGo.NecroBot.CLI
{
    internal class AuthSettings
    {
        public AuthType AuthType;


        [JsonIgnore]
        private string _filePath;

        public string GoogleRefreshToken;
        public string PtcUsername;
        public string PtcPassword;
        public string GoogleUsername;
        public string GooglePassword;

        public void Load(string path)
        {
            _filePath = path;

            if (File.Exists(_filePath))
            {
                //if the file exists, load the settings
                var input = File.ReadAllText(_filePath);

                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });

                JsonConvert.PopulateObject(input, this, settings);
            }
            else
            {
                Save(_filePath);
            }
        }

        public void Save(string path)
        {
            var output = JsonConvert.SerializeObject(this, Formatting.Indented,
                new StringEnumConverter { CamelCaseText = true });

            var folder = Path.GetDirectoryName(path);
            if (folder != null && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllText(path, output);
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_filePath))
            {
                Save(_filePath);
            }
        }
    }

    public class GlobalSettings
    {
        public int AmountOfPokemonToDisplayOnStart = 10;

        [JsonIgnore]
        internal AuthSettings Auth = new AuthSettings();
        [JsonIgnore]
        public string ProfilePath;
        [JsonIgnore]
        public string ProfileConfigPath;
        [JsonIgnore]
        public string GeneralConfigPath;

        public bool AutoUpdate = false;
        public double DefaultAltitude = 10;
        public double DefaultLatitude = 35.6894875;  // Tokyo
        public double DefaultLongitude = 139.691706;
        public int DelayBetweenPokemonCatch = 200;
        public int DelayBetweenPlayerActions = 200;
        public float EvolveAboveIvValue = 90;
        public bool EvolveAllPokemonAboveIv = false;
        public bool EvolveAllPokemonWithEnoughCandy = true;
        public int UseLuckyEggsMinPokemonAmount = 30;
        public bool UseLuckyEggsWhileEvolving = false;
        public bool UseEggIncubators = true;
        public bool DumpPokemonStats = true;
        public string GpxFile = "GPXPath.GPX";
        public bool UseGpxPathing = false;
        public double WalkingSpeedInKilometerPerHour = 1000.0;
        public int MaxTravelDistanceInMeters = 3500;
        public int KeepMinCp = 2000;
        public int KeepMinDuplicatePokemon = 1;
        public float KeepMinIvPercentage = 96;
        public bool KeepPokemonsThatCanEvolve = true;
        public bool PrioritizeIvOverCp = false;
        public bool RenameAboveIv = true;
        public string RenameTemplate = "{0} - {1}";
        public bool TransferDuplicatePokemon = true;
        public string TranslationLanguageCode = "en";
        public bool UsePokemonToNotCatchFilter = false;
        public int WebSocketPort = 14251;
        public bool StartupWelcomeDelay = false;
        public bool SnipeAtPokestops = true;
        public string SnipeLocationServer = "localhost";
        public int SnipeLocationServerPort = 16969;
        public bool UseSnipeLocationServer = false;
        public bool UseTransferIVForSnipe = false;
        public int MinDelayBetweenSnipes = 20000;


        public List<KeyValuePair<ItemId, int>> ItemRecycleFilter = new List<KeyValuePair<ItemId, int>>
        {
            new KeyValuePair<ItemId, int>(ItemId.ItemUnknown, 0),
            new KeyValuePair<ItemId, int>(ItemId.ItemPokeBall, 40),
            new KeyValuePair<ItemId, int>(ItemId.ItemGreatBall, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemUltraBall, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemMasterBall, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemPotion, 0),
            new KeyValuePair<ItemId, int>(ItemId.ItemSuperPotion, 0),
            new KeyValuePair<ItemId, int>(ItemId.ItemHyperPotion, 0),
            new KeyValuePair<ItemId, int>(ItemId.ItemMaxPotion, 25),
            new KeyValuePair<ItemId, int>(ItemId.ItemRevive, 0),
            new KeyValuePair<ItemId, int>(ItemId.ItemMaxRevive, 25),
            new KeyValuePair<ItemId, int>(ItemId.ItemLuckyEgg, 200),
            new KeyValuePair<ItemId, int>(ItemId.ItemIncenseOrdinary, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemIncenseSpicy, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemIncenseCool, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemIncenseFloral, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemTroyDisk, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemXAttack, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemXDefense, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemXMiracle, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemRazzBerry, 60),
            new KeyValuePair<ItemId, int>(ItemId.ItemBlukBerry, 10),
            new KeyValuePair<ItemId, int>(ItemId.ItemNanabBerry, 10),
            new KeyValuePair<ItemId, int>(ItemId.ItemWeparBerry, 30),
            new KeyValuePair<ItemId, int>(ItemId.ItemPinapBerry, 30),
            new KeyValuePair<ItemId, int>(ItemId.ItemSpecialCamera, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemIncubatorBasicUnlimited, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemIncubatorBasic, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemPokemonStorageUpgrade, 100),
            new KeyValuePair<ItemId, int>(ItemId.ItemItemStorageUpgrade, 100)
        };

        public List<PokemonId> PokemonsNotToTransfer = new List<PokemonId>
        {
            PokemonId.Lapras,
            PokemonId.Ditto,
            PokemonId.Articuno,
            PokemonId.Zapdos,
            PokemonId.Moltres,
            PokemonId.Dragonite,
            PokemonId.Mewtwo,
            PokemonId.Mew
        };

        public List<PokemonId> PokemonsToEvolve = new List<PokemonId>
        {
            PokemonId.Bulbasaur,
            PokemonId.Charmander,
            PokemonId.Squirtle,
            PokemonId.Caterpie,
            PokemonId.Dratini,
            PokemonId.Metapod,
            PokemonId.Weedle,
            PokemonId.Kakuna,
            PokemonId.Pidgey,
            PokemonId.Pidgeotto,
            PokemonId.Rattata,
            PokemonId.Spearow,
            PokemonId.Ekans,
            PokemonId.Pikachu,
            PokemonId.Sandshrew,
            PokemonId.NidoranFemale,
            PokemonId.Nidorina,
            PokemonId.NidoranMale,
            PokemonId.Nidorino,
            PokemonId.Clefairy,
            PokemonId.Vulpix,
            PokemonId.Jigglypuff,
            PokemonId.Zubat,
            PokemonId.Oddish,
            PokemonId.Gloom,
            PokemonId.Paras,
            PokemonId.Venonat,
            PokemonId.Diglett,
            PokemonId.Meowth,
            PokemonId.Psyduck,
            PokemonId.Mankey,
            PokemonId.Growlithe,
            PokemonId.Poliwag,
            PokemonId.Poliwhirl,
            PokemonId.Kadabra,
            PokemonId.Machop,
            PokemonId.Machoke,
            PokemonId.Bellsprout,
            PokemonId.Weepinbell,
            PokemonId.Tentacool,
            PokemonId.Geodude,
            PokemonId.Graveler,
            PokemonId.Ponyta,
            PokemonId.Slowpoke,
            PokemonId.Magnemite,
            PokemonId.Doduo,
            PokemonId.Seel,
            PokemonId.Grimer,
            PokemonId.Shellder,
            PokemonId.Gastly,
            PokemonId.Haunter,
            PokemonId.Drowzee,
            PokemonId.Krabby,
            PokemonId.Voltorb,
            PokemonId.Exeggcute,
            PokemonId.Hitmonlee,
            PokemonId.Koffing,
            PokemonId.Rhyhorn,
            PokemonId.Horsea,
            PokemonId.Goldeen,
            PokemonId.Staryu,
            PokemonId.Eevee,
            PokemonId.Porygon,
            PokemonId.Omanyte,
            PokemonId.Kabuto
        };

        public List<PokemonId> PokemonsToIgnore = new List<PokemonId>
        {
            PokemonId.Doduo
        };

        public Dictionary<PokemonId, TransferFilter> PokemonsTransferFilter = new Dictionary<PokemonId, TransferFilter>
        {
            {PokemonId.Eevee, new TransferFilter(750, 92, 2)},
            {PokemonId.Mew, new TransferFilter(0, 0, 10)}
        };

        public SnipeSettings PokemonToSnipe = new SnipeSettings
        {
            Locations = new List<Location>
            {
                new Location(38.55680748646112, -121.2383794784546), //Dratini Spot
                new Location(51.5025343,-0.2055027) //Charmender Spot

            },
            Pokemon = new List<PokemonId>()
            {
                PokemonId.Charmander,
                PokemonId.Snorlax,
                PokemonId.Dratini,
                PokemonId.Dragonair,
                PokemonId.Dragonite
            }
        };

        public static GlobalSettings Default => new GlobalSettings();

        public static GlobalSettings Load(string path)
        {
            GlobalSettings settings;
            var profilePath = Path.Combine(Directory.GetCurrentDirectory(), path);
            var profileConfigPath = Path.Combine(profilePath, "config");
            var configFile = Path.Combine(profileConfigPath, "config.json");

            if (File.Exists(configFile))
            {
                //if the file exists, load the settings
                var input = File.ReadAllText(configFile);

                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                jsonSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                jsonSettings.DefaultValueHandling = DefaultValueHandling.Populate;

                settings = JsonConvert.DeserializeObject<GlobalSettings>(input, jsonSettings);
            }
            else
            {
                settings = new GlobalSettings();
            }

            if (settings.WebSocketPort == 0)
            {
                settings.WebSocketPort = 14251;
            }

            if (settings.PokemonToSnipe == null)
            {
                settings.PokemonToSnipe = Default.PokemonToSnipe;
            }

            if(settings.RenameTemplate == null)
            {
                settings.RenameTemplate = Default.RenameTemplate;
            }

            if(settings.SnipeLocationServer == null)
            {
                settings.SnipeLocationServer = Default.SnipeLocationServer;
            }

            settings.ProfilePath = profilePath;
            settings.ProfileConfigPath = profileConfigPath;
            settings.GeneralConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "config");

            var firstRun = !File.Exists(configFile);

            settings.Save(configFile);
            settings.Auth.Load(Path.Combine(profileConfigPath, "auth.json"));

            if (firstRun)
            {
                return null;
            }

            return settings;
        }

        public void Save(string fullPath)
        {
            var output = JsonConvert.SerializeObject(this, Formatting.Indented,
                new StringEnumConverter { CamelCaseText = true });

            var folder = Path.GetDirectoryName(fullPath);
            if (folder != null && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllText(fullPath, output);
        }
    }

    public class ClientSettings : ISettings
    {
        private readonly GlobalSettings _settings;

        public ClientSettings(GlobalSettings settings)
        {
            _settings = settings;
        }


        public string GoogleUsername => _settings.Auth.GoogleUsername;
        public string GooglePassword => _settings.Auth.GooglePassword;

        public string GoogleRefreshToken
        {
            get { return _settings.Auth.GoogleRefreshToken; }
            set
            {
                _settings.Auth.GoogleRefreshToken = value;
                _settings.Auth.Save();
            }
        }

        AuthType ISettings.AuthType
        {
            get
            {
                return _settings.Auth.AuthType;
            }

            set
            {
                _settings.Auth.AuthType = value;
            }
        }

        double ISettings.DefaultLatitude
        {
            get
            {
                return _settings.DefaultLatitude;
            }

            set
            {
                _settings.DefaultLatitude = value;
            }
        }

        double ISettings.DefaultLongitude
        {
            get
            {
                return _settings.DefaultLongitude;
            }

            set
            {
                _settings.DefaultLongitude = value;
            }
        }

        double ISettings.DefaultAltitude
        {
            get
            {
                return _settings.DefaultAltitude;
            }

            set
            {
                _settings.DefaultAltitude = value;
            }
        }

        string ISettings.PtcPassword
        {
            get
            {
                return _settings.Auth.PtcPassword;
            }

            set
            {
                _settings.Auth.PtcPassword = value;
            }
        }

        string ISettings.PtcUsername
        {
            get
            {
                return _settings.Auth.PtcUsername;
            }

            set
            {
                _settings.Auth.PtcUsername = value;
            }
        }

        string ISettings.GoogleUsername
        {
            get
            {
                return _settings.Auth.GoogleUsername;
            }

            set
            {
                _settings.Auth.GoogleUsername = value;
            }
        }
        string ISettings.GooglePassword
        {
            get
            {
                return _settings.Auth.GooglePassword;
            }

            set
            {
                _settings.Auth.GooglePassword = value;
            }
        }
    }

    public class LogicSettings : ILogicSettings
    {
        private readonly GlobalSettings _settings;

        public LogicSettings(GlobalSettings settings)
        {
            _settings = settings;
        }

        public string ProfilePath => _settings.ProfilePath;
        public string ProfileConfigPath => _settings.ProfileConfigPath;
        public string GeneralConfigPath => _settings.GeneralConfigPath;
        public bool AutoUpdate => _settings.AutoUpdate;
        public float KeepMinIvPercentage => _settings.KeepMinIvPercentage;
        public int KeepMinCp => _settings.KeepMinCp;
        public double WalkingSpeedInKilometerPerHour => _settings.WalkingSpeedInKilometerPerHour;
        public bool EvolveAllPokemonWithEnoughCandy => _settings.EvolveAllPokemonWithEnoughCandy;
        public bool KeepPokemonsThatCanEvolve => _settings.KeepPokemonsThatCanEvolve;
        public bool TransferDuplicatePokemon => _settings.TransferDuplicatePokemon;
        public bool UseEggIncubators => _settings.UseEggIncubators;
        public int DelayBetweenPokemonCatch => _settings.DelayBetweenPokemonCatch;
        public int DelayBetweenPlayerActions => _settings.DelayBetweenPlayerActions;
        public bool UsePokemonToNotCatchFilter => _settings.UsePokemonToNotCatchFilter;
        public int KeepMinDuplicatePokemon => _settings.KeepMinDuplicatePokemon;
        public bool PrioritizeIvOverCp => _settings.PrioritizeIvOverCp;
        public int MaxTravelDistanceInMeters => _settings.MaxTravelDistanceInMeters;
        public string GpxFile => _settings.GpxFile;
        public bool UseGpxPathing => _settings.UseGpxPathing;
        public bool UseLuckyEggsWhileEvolving => _settings.UseLuckyEggsWhileEvolving;
        public int UseLuckyEggsMinPokemonAmount => _settings.UseLuckyEggsMinPokemonAmount;
        public bool EvolveAllPokemonAboveIv => _settings.EvolveAllPokemonAboveIv;
        public float EvolveAboveIvValue => _settings.EvolveAboveIvValue;
        public bool RenameAboveIv => _settings.RenameAboveIv;
        public string RenameTemplate => _settings.RenameTemplate;
        public int AmountOfPokemonToDisplayOnStart => _settings.AmountOfPokemonToDisplayOnStart;
        public bool DumpPokemonStats => _settings.DumpPokemonStats;
        public string TranslationLanguageCode => _settings.TranslationLanguageCode;
        public ICollection<KeyValuePair<ItemId, int>> ItemRecycleFilter => _settings.ItemRecycleFilter;
        public ICollection<PokemonId> PokemonsToEvolve => _settings.PokemonsToEvolve;
        public ICollection<PokemonId> PokemonsNotToTransfer => _settings.PokemonsNotToTransfer;
        public ICollection<PokemonId> PokemonsNotToCatch => _settings.PokemonsToIgnore;
        public Dictionary<PokemonId, TransferFilter> PokemonsTransferFilter => _settings.PokemonsTransferFilter;
        public bool StartupWelcomeDelay => _settings.StartupWelcomeDelay;
        public bool SnipeAtPokestops => _settings.SnipeAtPokestops;
        public SnipeSettings PokemonToSnipe => _settings.PokemonToSnipe;
        public string SnipeLocationServer => _settings.SnipeLocationServer;
        public int SnipeLocationServerPort => _settings.SnipeLocationServerPort;
        public bool UseSnipeLocationServer=> _settings.UseSnipeLocationServer;
        public bool UseTransferIVForSnipe => _settings.UseTransferIVForSnipe;
        public int MinDelayBetweenSnipes => _settings.MinDelayBetweenSnipes;
    }
}
