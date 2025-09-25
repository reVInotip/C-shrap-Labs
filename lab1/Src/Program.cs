global using Interface.Strategy;

using System;
using Src;
using Src.Strategy;


try
{
    ParseArgs(out ProgramMode progMode, out string pathToConf, out bool helpOnly);

    if (helpOnly) return;

    switch (progMode)
    {
        case ProgramMode.StrategyMode:
            {
                Loader<Philosopher, Fork>.LoadPhilosophersFromFile(pathToConf, new Random());
                break;
            }
        case ProgramMode.StrategyDeadlockMode:
            {
                Loader<Philosopher, Fork>.LoadPhilosophersFromFile(pathToConf, new Random(), true);
                break;
            }
        default: throw new ApplicationException("This mode not supported yet");
    }

    MainLoop();
}
catch (ApplicationException e)
{
    Console.WriteLine(e.Message);
}
catch (NotImplementedException e)
{
    Console.WriteLine(e.Message);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.Write(e.StackTrace);
}

void MainLoop()
{
    var philosophers = Loader<Philosopher, Fork>.philosophers;
    var forks = Loader<Philosopher, Fork>.forks;

    for (int i = 0; i < 1000000; ++i)
    {
        Console.WriteLine("======== STEP {0} ========", i);
        Console.WriteLine("Philosophers:");

        foreach (var philosopher in philosophers)
        {
            philosopher.PrintInfo();
        }

        Console.WriteLine("\nForks:");

        foreach (var fork in forks)
        {
            fork.PrintInfo();
        }

        foreach (var philosopher in philosophers)
        {
            philosopher.Step();
        }

        if (DeadlockAnalyzer<Philosopher, Fork>.IsDeadlock(philosophers, forks))
        {
            Console.WriteLine("\nDEADLOCK DETECTED");
            return;
        }

        Thread.Sleep(1000);
        Console.Clear();
    }
}

void ParseArgs(out ProgramMode progMode, out string pathToConf, out bool helpOnly)
{
    ProgramMode? mode = null;
    string? confPath = null;

    bool modeFlag = false;
    bool pathFlag = false;

    helpOnly = false;
    foreach (var arg in args)
    {
        if (modeFlag)
        {
            if (mode is not null)
                throw new ArgumentException("Double set mode");

            mode = ProgramModeExtension.ToMode(arg);
            modeFlag = false;
        }
        else if (pathFlag)
        {
            if (confPath is not null)
                throw new ArgumentException("Double set path");

            confPath = arg;
            pathFlag = false;
        }

        if (arg == "-m" || arg == "--mode")
            modeFlag = true;
        else if (arg == "-c" || arg == "--config_path")
            pathFlag = true;
        else if (arg == "-h" || arg == "--help")
        {
            Console.Write
            (
                """
                This is lab1 of NSU C# course.

                *DESCRIPTION*
                In this lab, I solved the Dining Philosophers problem in a single thread. Several solution modes
                are presented: no controller, no controller with deadlock, and with controller. The default mode
                is no controller.

                *ARGUMENTS*
                -m or --mode - set execution mode. Correct values: strategy, strategy_deadlock, controller.
                    Default value: strategy.
                -c or --config_path - relative or full path to config file. Current directory used by default.
                -h or --help - see this page
            """
            );

            helpOnly = true;
        }
    }

    progMode = mode ?? ProgramMode.StrategyMode;
    pathToConf = confPath ?? "./philosophers.conf";
}