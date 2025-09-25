using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface.Strategy;

public interface IDeadlockAnalyzer<T, U>
    where T : class, IPhilosopher
    where U : class, IFork
{
    abstract static bool IsDeadlock(List<T> philosophers, List<U> forks);
}
