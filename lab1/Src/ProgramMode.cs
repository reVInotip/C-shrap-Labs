namespace Src;

public enum ProgramMode
{
    StrategyMode,
    StrategyDeadlockMode,
    ControllerMode
}

public static class ProgramModeExtension
{
    public static ProgramMode ToMode(string stringMode)
    {
        return stringMode switch
        {
            "strategy" => ProgramMode.StrategyMode,
            "strategy_deadlock" => ProgramMode.StrategyDeadlockMode,
            "controller" => ProgramMode.ControllerMode,
            _ => throw new NotImplementedException("Invalid program mode"),
        };
    }
}