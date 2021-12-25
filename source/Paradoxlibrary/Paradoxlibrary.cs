using Playnite.SDK;
using Playnite.SDK.Data;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Paradoxlibrary
{
    [LoadPlugin]
    public class Paradoxlibrary : LibraryPluginBase<ParadoxlibrarySettingsViewModel>
    {
        // Offload info about possible games here.
        // Do note that you'll have to add intel about game's executable later.
        // Remember that we looked for bootstrapper folder already!
        // Game folders will be in ..\games (so we essentially can replace "bootstrapper\" with "games\GameID" just fine.
        // Game counts as installed only if its folder exists. If it's not - do not add it at all.
        string StellarisInstallDirectory = Path.Combine(Paradox.InstallationPath, "stellaris").Replace("bootstrapper", "games");
        bool StellarisInstalled
        {
            get
            {
                if (string.IsNullOrEmpty(StellarisInstallDirectory) || !Directory.Exists(StellarisInstallDirectory))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        string BattletechInstallDirectory = Path.Combine(Paradox.InstallationPath, "battletech").Replace("bootstrapper", "games");
        bool BattletechInstalled
        {
            get
            {
                if (string.IsNullOrEmpty(BattletechInstallDirectory) || !Directory.Exists(BattletechInstallDirectory))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        string CK3InstallDirectory = Path.Combine(Paradox.InstallationPath, "ck3").Replace("bootstrapper", "games");
        bool CK3Installed
        {
            get
            {
                if (string.IsNullOrEmpty(CK3InstallDirectory) || !Directory.Exists(CK3InstallDirectory))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        // End of offloaded info about possible games.

        public Paradoxlibrary(IPlayniteAPI api) : base(
            "Paradox",
            Guid.Parse("5a25b200-7283-40a2-ad36-51abf4eb7ca0"),
            // PDX closes its own launcher v2 just fine, no need in auto-close.
            // No need in extra settings either.
            new LibraryPluginProperties { CanShutdownClient = false, HasSettings = false },
            new ParadoxClient(),
            Paradox.Icon,
            (_) => new ParadoxlibrarySettingsView(),
            api)
        {
            // No settings - no problem. Looks optional.
            //SettingsViewModel = new ParadoxlibrarySettingsViewModel(this, api);
        }

        public override IEnumerable<GameMetadata> GetGames(LibraryGetGamesArgs args)
        {
            return new List<GameMetadata>()
            {
                // Sober people do cycles - but I'm too unskilled currently.

                // Start of new game entry.
                new GameMetadata()
                {
                    Name = "Stellaris",
                    // As seen in ..\launcher\apps - because eventually we'll learn how to parse it properly.
                    // Note to future self: if there are only manifest file and nothing else - it's DLC.
                    GameId = "stellaris",
                    InstallDirectory = StellarisInstallDirectory,
                    GameActions = new List<GameAction>
                    {
                        // Do note that we care about executable to point at.
                        // We're obliged to run dowser.exe to trigger game-specific copy of Paradox launcher v2 launch, where applicable.
                        // Without that, there will be no mods nor DLC ownership verifications neither a chance to do auth for multiplayer games.
                        new GameAction()
                        {
                            Name = "Run PDX Launcher v2",
                            Type = GameActionType.File,
                            Path = "{InstallDir}\\dowser.exe",
                            IsPlayAction = true
                        }
                    },
                    IsInstalled = StellarisInstalled,
                    Source = new MetadataNameProperty("Paradox"),
                    Links = new List<Link>()
                    {
                        new Link("Store", @"https://www.paradoxplaza.com/stellaris-all/")
                    },
                    Platforms = new HashSet<MetadataProperty> { new MetadataSpecProperty("pc_windows") }
                },
                // End of new game entry. Do note that last entry in the list should not have comma as last symbol.

                new GameMetadata()
                {
                    Name = "Battletech",
                    GameId = "battletech",
                    InstallDirectory = BattletechInstallDirectory,
                    GameActions = new List<GameAction>
                    {
                        new GameAction()
                        {
                            Type = GameActionType.File,
                            Path = "{InstallDir}\\BattleTech.exe",
                            IsPlayAction = true
                        }
                    },
                    IsInstalled = BattletechInstalled,
                    Source = new MetadataNameProperty("Paradox"),
                    Links = new List<Link>()
                    {
                        new Link("Store", @"https://www.paradoxplaza.com/battletech-all/")
                    },
                    Platforms = new HashSet<MetadataProperty> { new MetadataSpecProperty("pc_windows") }
                },
                new GameMetadata()
                {
                    Name = "Crusader Kings III",
                    GameId = "ck3",
                    InstallDirectory = CK3InstallDirectory,
                    GameActions = new List<GameAction>
                    {
                        new GameAction()
                        {
                            Name = "Run PDX Launcher v2",
                            Type = GameActionType.File,
                            Path = "{InstallDir}\\dowser.exe",
                            IsPlayAction = true
                        }
                    },
                    IsInstalled = CK3Installed,
                    Source = new MetadataNameProperty("Paradox"),
                    Links = new List<Link>()
                    {
                        new Link("Store", @"https://www.paradoxplaza.com/crusader-kings-all/")
                    },
                    Platforms = new HashSet<MetadataProperty> { new MetadataSpecProperty("pc_windows") }
                }
            };
        }


        // Start of blatant install/uninstall links adding.
        // I'd really like to utilize something much more simple since we have only one entry point anyway,
        // in a form of stand-alone launcher, but we're having what we're having for now.
        public override IEnumerable<InstallController> GetInstallActions(GetInstallActionsArgs args)
        {
            if (args.Game.PluginId != Id)
            {
                yield break;
            }

            yield return new ParadoxlibraryInstallController(args.Game);
        }

        public override IEnumerable<UninstallController> GetUninstallActions(GetUninstallActionsArgs args)
        {
            if (args.Game.PluginId != Id)
            {
                yield break;
            }

            yield return new ParadoxlibraryUninstallController(args.Game);
        }
        // End of blatant install/uninstall links adding.

        // No settings - no problem. Looks optional.
        //public override ISettings GetSettings(bool firstRunSettings)
        //{
        //    return settings;
        //}

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            return new ParadoxlibrarySettingsView();
        }
    }
}