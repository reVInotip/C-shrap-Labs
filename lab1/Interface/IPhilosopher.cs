namespace Interface;

public interface IPhilosopher
{
    abstract static IPhilosopher Create(string name);
    void loop();
}
