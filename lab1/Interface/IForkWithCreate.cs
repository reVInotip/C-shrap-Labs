using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

public interface IForkWithCreate : IFork
{
    virtual static IFork Create(int number)
    {
        throw new NotImplementedException("Create function not implemented here");
    }
}
