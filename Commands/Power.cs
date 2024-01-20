using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Commands {
    internal class Power {
    }

    public class Shutdown : Command {
        public Shutdown(String name, String desc) : base(name, desc) { }

        public override string execute(string[] args) {
            Cosmos.System.Power.Shutdown();
            return "";
        }
    }

    public class Reboot : Command {
        public Reboot(String name, String desc) : base(name, desc) { }

        public override string execute(string[] args) {
            Cosmos.System.Power.Reboot();
            return "";
        }
    }
}
