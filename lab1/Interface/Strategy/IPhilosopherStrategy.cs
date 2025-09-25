namespace Interface.Strategy;

public interface IPhilosopherStrategy : IPhilosopher
{
    abstract static IPhilosopher Create(PhilosopherDTO philosopherDTO);
    IFork? LeftFork { get; protected internal set; }
    IFork? RightFork { get; protected internal set; }
    bool FirstTakeLeftFork { get; protected internal set; }
    /// <summary>
    /// Make next step
    /// </summary>
    /// <throws>ApplicationException</throws>
}
