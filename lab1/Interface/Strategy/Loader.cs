namespace Interface.Strategy;

/// <summary>
/// Load all philosophers from file
/// </summary>
/// <typeparam name="T">Defines a concrete implementation of the IPhilosopher interface</typeparam>
/// <typeparam name="U">Defines a concrete implementation of the IFork interface</typeparam>
public static class Loader<T, U>
    where T : class, IPhilosopher
    where U : class, IFork
{
    public static readonly List<T> philosophers = [];
    public static readonly List<U> forks = [];

    /// <summary>
    ///     Load philosophers from file
    /// </summary>
    /// <param name="filePath">Path to file with philosophers</param>
    /// <param name="random">Some realization of random number generator</param>
    /// <param name="deadlockConfigure">Configure philosophers for deadlock</param>
    /// <throws>ArgumentException</throws>
    public static void LoadPhilosophersFromFile(string filePath, Random random, bool deadlockConfigure = false)
    {
        using var reader = new StreamReader(filePath);
        if (reader.Peek() < 0)
            throw new ArgumentException("Configuration file is empty");

        string? line = null;

        // move this ranges to config file
        var thinkingRange = new int[] { 3, 10 };
        var eatingRange = new int[] { 4, 5 };

        int takeForkTime = 2;
        int putForkTimeout = 3;
        int eatingTime;
        int thinkingTime;

        int num = 1;
        do
        {
            line = reader.ReadLine();

            // add some validation here

            eatingTime = random.Next(eatingRange[0], eatingRange[1]);
            thinkingTime = random.Next(thinkingRange[0], thinkingRange[1]);

            if (line is not null)
            {
                philosophers.Add((T)T.Create(
                    new PhilosopherDTO()
                    {
                        Name = line,
                        EatingTime = eatingTime,
                        TakeForkTime = takeForkTime,
                        ThinkingTime = thinkingTime,
                        PutForkTimeout = putForkTimeout
                    }
                ));
                forks.Add((U)U.Create(num));
            }

            ++num;
        }
        while (line is not null);

        for (int i = 0; i < forks.Count; ++i)
        {
            philosophers[i].RightFork = forks[i];
            philosophers[i].LeftFork = forks[(i + 1) % forks.Count];
            philosophers[i].FirstTakeLeftFork = false;
        }

        if (!deadlockConfigure) philosophers[^1].FirstTakeLeftFork = true;
    }
}
