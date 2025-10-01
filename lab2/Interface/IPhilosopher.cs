using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

public interface IPhilosopher
{
    int CountEatingFood { get; }
    int HungryTime { get; }
    virtual static IPhilosopher Create(PhilosopherDTO philosopherDTO)
    {
        throw new NotImplementedException("Create function not implemented here");
    }
    string Name { get; protected internal set; }
    void Step();
    void PrintInfo();
    void PrintScore(double simulationTime);
    bool IsEating();
}
