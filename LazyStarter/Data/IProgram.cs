using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter.Data
{
    interface IProgram
    {
        string Arguments { get; }

        string Name { get; }

        string Path { get; }

        bool Exist { get; }
    }
}
