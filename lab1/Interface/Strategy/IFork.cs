namespace Interface.Strategy;

public interface IFork
{
    IPhilosopher? Owner { get; protected internal set; }
    abstract static IFork Create(int number);
    /// <summary>
    /// 
    /// </summary>
    bool TryTake(IPhilosopher philosopher);
    void Put();
    void PrintInfo();
}
