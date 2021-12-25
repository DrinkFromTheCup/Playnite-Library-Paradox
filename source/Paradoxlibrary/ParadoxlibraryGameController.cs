using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Playnite;
using Playnite.Common;
using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;

// Not entirely sure that it will work all the time for all the people...

namespace Paradoxlibrary
{
    public class ParadoxlibraryInstallController : InstallController
    {
        //private CancellationTokenSource watcherToken;

        public ParadoxlibraryInstallController(Game game) : base(game)
        {
            if (Paradox.IsInstalled)
            {
                Name = "Install using stand-alone launcher";
            }
            else
            // Failsafe for cases where we added games anyway but launcher is nowhere to be seen.
            {
                Name = "Download stand-alone launcher";
            }
        }

        public override void Dispose()
        {
            //watcherToken?.Cancel();
        }

        public override void Install(InstallActionArgs args)
        {
            if (Paradox.IsInstalled)
            {
                ProcessStarter.StartProcess(Paradox.ClientExecPath);
            }
            else
            // Failsafe for cases where we added games anyway but launcher is nowhere to be seen.
            {
                ProcessStarter.StartUrl(@"https://launcher.paradoxplaza.com/windows_launcher");
            }

            //StartInstallWatcher();
        }

        //public async void StartInstallWatcher()
        //{
            //watcherToken = new CancellationTokenSource();
            //await Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        if (watcherToken.IsCancellationRequested)
            //        {
            //            return;
            //        }

                    // Skipping temporarily to avoid going insane.
                    //var games = Paradox.GetInstalledGames();
                    //if (games.ContainsKey(Game.GameId))
                    //{
                    //    var game = games[Game.GameId];
                    //   var installInfo = new GameInstallationData()
                    //    {
                    //        InstallDirectory = game.InstallDirectory
                    //    };

                    //    InvokeOnInstalled(new GameInstalledEventArgs(installInfo));
                    //    return;
            //        }

            //        await Task.Delay(10000);
            //    }
            //});
        //}
    }

    public class ParadoxlibraryUninstallController : UninstallController
    {
        //private CancellationTokenSource watcherToken;

        public ParadoxlibraryUninstallController(Game game) : base(game)
        {
            Name = "Uninstall";
        }

        public override void Dispose()
        {
            //watcherToken?.Cancel();
        }

        public override void Uninstall(UninstallActionArgs args)
        {
            Dispose();
            if (!File.Exists(Paradox.ClientExecPath))
            // Launcher was removed but game persisted somehow.
            {
                throw new FileNotFoundException("Uninstaller not found.");
            }
            // NB: for some reason, Process.Start results in successful launcher launch - but all games
            // are listed as uninstalled. Resorting to ProcessStarter since we prefer it anyway...
            ProcessStarter.StartProcess(Paradox.ClientExecPath);
            //StartUninstallWatcher();
        }

        //public async void StartUninstallWatcher()
        //{
            //watcherToken = new CancellationTokenSource();
            //while (true)
            //{
            //    if (watcherToken.IsCancellationRequested)
            //    {
            //        return;
            //    }

                // Skipping temporarily to avoid going insane.
                //var games = Paradox.GetInstalledGames();
                //if (!games.ContainsKey(Game.GameId))
                //{
            //        InvokeOnUninstalled(new GameUninstalledEventArgs());
            //        return;
                //}

                //await Task.Delay(5000);
            //}
        //}
    }
}
