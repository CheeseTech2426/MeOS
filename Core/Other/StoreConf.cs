using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using MeOS.Graphics;

namespace MeOS.Core
{
    internal static class StoreConf
    {
        public static readonly string meOSConfFile = Kernel.meOSConfigFile;

        public static string confFile = null;
        public static string tempFile = null;


        public static void Init() {
            if (!HeaderExist(meOSConfFile, "Config File")) Store(meOSConfFile, "Config File", @"0:\MeOS\conf.conf");
            if (!HeaderExist(meOSConfFile, "Temp File")) Store(meOSConfFile, "Temp File", @"0:\MeOS\mescript.tmp");
            confFile = Get(meOSConfFile, 0);
            confFile = Get(meOSConfFile, 1);
        }
        public static void Store(string file, string header, string data) {
            try {
                string[] f = File.ReadAllLines(file);

                File.AppendAllText(file, "[" + header + "] " + data + "\n");

            } catch (Exception e) {
                CLI.WriteLine("Failed to write to conf.conf with data: " + data + " " + e.Message, CLI.foreground, CLI.background);
                return;
            }
        }

        public static string Get(string file, int lineNumber) {
            try {
                string[] f = File.ReadAllLines(file);
                return f[lineNumber - 1];

            } catch (Exception e) {
                CLI.WriteLine("Failed to read conf.conf at line: " + lineNumber + " " + e.Message, CLI.foreground, CLI.background);
                return null;
            }

        }

        public static string GetData(string file, string header) {
            try {
                string[] f = File.ReadAllLines(file);
                for (int i = 0; i < f.Length; i++) {
                    string l = f[i];
                    string[] w = l.Split();
                    if (w[0] == $"[{header}]")
                        return l;
                }
                return null;

            } catch (Exception e) {
                CLI.WriteLine($"Header {header} doesn't exist! " + e.Message, CLI.foreground, CLI.background);
                return null;
            }
        }

        public static bool HeaderExist(string file, string header) {
            try {
                string[] f = File.ReadAllLines(file);
                for (int i = 0; i < f.Length; i++) {
                    string l = f[i];
                    string[] w = l.Split();
                    if (w[0] == $"[{header}]")
                        return true;
                }
                return false;

            } catch (Exception e) {
                CLI.WriteLine($"Header {header} doesn't exist! " + e.Message, CLI.foreground, CLI.background);
                return false;
            }
        }

        public static void StoreTemp(string file, string data) {
            try {
                File.WriteAllText(tempFile, data);
            } catch (Exception e) {
                CLI.WriteLine($"Failed to store temp {data} to file {file}! " + e.Message, CLI.foreground, CLI.background);
                return;
            }
        }


        public static string GetTemp()
        {
            try { return File.ReadAllText(tempFile); } catch (Exception e) { CLI.WriteLine("Failed to read temp! " + e.Message, CLI.foreground, CLI.background); return null; }
        }
    }
}
