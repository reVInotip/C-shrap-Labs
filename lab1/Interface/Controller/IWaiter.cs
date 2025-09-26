namespace Interface.Controller;

public interface IWaiter
{
    abstract static IWaiter Create(List<IPhilosopherController> philosophers, List<IForkController> forks, bool isDeadlockConfigure);
    event EventHandler<IPhilosopherController> YouHasRightForkNotify;
    event EventHandler<IPhilosopherController> YouHasLeftForkNotify;
    void Step();
}
