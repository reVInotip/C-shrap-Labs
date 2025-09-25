using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

public interface IPhilosopher
{
    string Name { get; protected internal set; }
    void Step();
    void PrintInfo();
    bool IsEating();
}
