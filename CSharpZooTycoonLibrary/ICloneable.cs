using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpZooTycoonLibrary
{
    public interface ICloneable<T>
    {
        T Clone();
    }
}

