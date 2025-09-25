namespace Interface.Strategy;

public interface IForkStrategy : IFork
{
    abstract static IFork Create(int number);
    /// <summary>
    /// 
    /// </summary>
}
