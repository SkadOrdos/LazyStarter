using LazyStarter.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    internal class ProgramItem : CheckedListItem, IProgram
    {
        public readonly List<String> Argumets = new List<String>();

        private Period startupDelay = Period.SEC3;
        /// <summary>
        /// Startup delay in seconds
        /// </summary>
        public Period StartupDelay
        {
            get { return startupDelay; }
            set { startupDelay = value; }
        }

        public string Arguments { get { return null; } }

        public string Name { get { return Text; } }

        public string Path { get { return Description; } }

        public bool Exist { get { return File.Exists(Path); } }

        public ProgramItem(string caption) : base(caption)
        {
            Checked = true;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
