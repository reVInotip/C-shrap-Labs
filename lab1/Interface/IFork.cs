using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

public interface IFork
{
    IPhilosopher? Owner { get; protected internal set; }
    bool TryTake(IPhilosopher philosopher);
    void Put();
    void PrintInfo();
}
