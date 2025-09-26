using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface.Controller;

public interface IForkController : IForkWithCreate
{
    void Take(IPhilosopher philosopher);
    void Lock(IPhilosopher philosopher);
    bool IsLocked();
    bool IsLockedBy(IPhilosopher philosopher);
}
