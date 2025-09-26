using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface.Controller;

public interface IPhilosopherController : IPhilosopherWithCreate
{
    IWaiter? Waiter { get; protected internal set; }
    event EventHandler IAmHungryNotify;
    event EventHandler INeedLeftForkNotify;
    event EventHandler INeedRightForkNotify;
    event EventHandler ICanTakeLeftForkNotify;
    event EventHandler ICanTakeRightForkNotify;
    event EventHandler IAmFullNotify;
    public void AddWaiterAndSubscribeOnHisEvents(IWaiter waiter);
}
