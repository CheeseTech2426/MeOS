using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeOS.Commands;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem.FAT;
using s = System;
using Cosmos.System.FileSystem.Listing;
using MeOS.Graphics;

namespace MeOS.Core.Other
{


    public class FileSearcher
    {
        public void SearchFile(string startDirectory, string fileName)
        {
            try
            {
                ListFiles(startDirectory, fileName);
            }
            catch (Exception ex)
            {
                CLI.WriteLine($"An error occurred: {ex.Message}", CLI.foreground, CLI.background);
            }
        }

        private void ListFiles(string directory, string fileName)
        {
            foreach (var entry in Kernel.vfs.GetDirectoryListing(directory))
            {
                if (entry.mEntryType == DirectoryEntryTypeEnum.Directory)
                {
                    ListFiles(directory + entry.mName + Path.DirectorySeparatorChar, fileName);
                }
                else if (entry.mEntryType == DirectoryEntryTypeEnum.File && entry.mName == fileName)
                {
                    CLI.WriteLine($"Found '{fileName}' at: {directory}{fileName}", CLI.foreground, CLI.background);
                }
            }
        }
    }



    public class RunSearchCommand : Command
    {
        public RunSearchCommand(string name, String desc) : base(name, desc) { }
        public override string execute(string[] args)
        {
            FileSearcher s = new FileSearcher();
            s.SearchFile(@"0:\", args[0]);
            return "";
        }
    }
}

