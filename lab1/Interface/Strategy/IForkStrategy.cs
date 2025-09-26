namespace Interface.Strategy;

public interface IForkStrategy : IForkWithCreate
{
    bool TryTake(IPhilosopher philosopher);
}
