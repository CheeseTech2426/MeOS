using System;
using System.Collections.Generic;
using System.Text;

namespace MeOS.Commands
{
    public class Command
    {
        public readonly string name;
        public readonly string desc;

        public Command(string name, string desc) { this.name = name; this.desc = desc; }

        public virtual string execute(string[] args) { return ""; }

    }
}
