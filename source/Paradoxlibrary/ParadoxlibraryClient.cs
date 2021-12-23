using Playnite.Common;
using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Original code (c) JosefNemec
// Source: https://github.com/JosefNemec/PlayniteExtensions/blob/master/source/Libraries/GogLibrary/GogClient.cs

namespace Paradoxlibrary
{
    public class ParadoxClient : LibraryClient
    {
        public override string Icon => Paradox.Icon;

        public override bool IsInstalled => Paradox.IsInstalled;

        public override void Open()
        {
            ProcessStarter.StartProcess(Paradox.ClientExecPath, string.Empty);
        }
    }
}