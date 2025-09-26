namespace Interface.Strategy;

public interface IPhilosopherStrategy : IPhilosopherWithCreate
{
    IForkStrategy? LeftFork { get; protected internal set; }
    IForkStrategy? RightFork { get; protected internal set; }
    bool FirstTakeLeftFork { get; protected internal set; }
}
