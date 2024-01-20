using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using MeOS;
using MeOS.Commands;
using MeOS.Core;
using MeOS.Core.Other;
using MeOS.Graphics;
using MeOS.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security;
using System.Text;
using Sys = Cosmos.System;

namespace MeOS
{
    public class Kernel : Sys.Kernel
    {

        public static CosmosVFS vfs;
        public static string path;
        public static CommandManager cm;
        public static FileSearcher s;
        TaskScheduler ts;
        public static int left = Console.GetCursorPosition().Left;
        public static int top = Console.GetCursorPosition().Top;
        public static readonly string meOSConfigFile = @"0:\MeOS\meos.conf";
        public static string currentlyLoggedUser;
        public static bool showBar = true;

        /* MIV - by Arawn Davies | GitHub -> https://github.com/bartashevich/MIV/tree/master */
        public static string file; // required by MIV

        protected override void BeforeRun()
        {
            vfs = new CosmosVFS();
            VFSManager.RegisterVFS(vfs);
            cm = new CommandManager();
            s = new FileSearcher();
            CLI.init(); // loads the command line interface and all color settings
            StoreConf.Init();
            SystemStartUp.KernelInit();
            Directory.CreateDirectory(@"0:\MeOS\");
            showBar = true;
            currentlyLoggedUser = "";
            UserManager.CreateUser("root", "root");
            UserManager.LoginPage();
        }

        protected override void Run()
        {
            CLI.PrintCommandLine(path);

            ts.ScheduleAndRun(Console.ReadLine());
        }
    }
}
