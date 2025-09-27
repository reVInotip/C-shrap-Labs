using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

public interface IFork
{
    int UsedTime { get; }
    int BlockTime { get; }
    int AvailableTime { get; }
    virtual static IFork Create(int number)
    {
        throw new NotImplementedException("Create function not implemented here");
    }
    IPhilosopher? Owner { get; protected internal set; }
    void Put();
    void PrintInfo();
    void PrintScore(double simulationTime);
    void Step();
}
