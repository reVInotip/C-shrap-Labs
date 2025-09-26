using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

public interface IPhilosopherWithCreate : IPhilosopher
{
    virtual static IPhilosopher Create(PhilosopherDTO philosopherDTO)
    {
        throw new NotImplementedException("Create function not implemented here");
    }
}
