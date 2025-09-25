using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Src.Strategy;

public class DeadlockAnalyzer<T, U> : IDeadlockAnalyzer<T, U>
    where T : class, IPhilosopher
    where U : class, IFork
{
    public static bool IsDeadlock(List<T> philosophers, List<U> forks)
    {
        int countUsingForks = 0;
        foreach (var fork in forks)
        {
            if (fork.Owner != null) ++countUsingForks;
        }

        if (countUsingForks == forks.Count)
        {
            foreach (var philosopher in philosophers)
            {
                if (philosopher.IsEating()) return false;
            }

            return true;
        }

        return false;
    }
}
