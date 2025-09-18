namespace Interface;

public interface IPhilosopher
{
    abstract static IPhilosopher Create();
    void loop();
}
