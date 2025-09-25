using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Src.Strategy;

public class Fork: IForkStrategy
{
    private bool _isTaken;
    private readonly int _number;
    public IPhilosopher? Owner { get; set; }

    public static IFork Create(int number)
    {
        return new Fork(number);
    }

    public Fork(int number)
    {
        _isTaken = false;
        _number = number;
    }

    public bool TryTake(IPhilosopher philosopher)
    {
        if (_isTaken)
            return false;

        Owner = philosopher;
        _isTaken = true;
        return true;
    }

    public void Put()
    {
        if (!_isTaken)
            throw new ApplicationException("Try to put not taken fork");

        Owner = null;
        _isTaken = false;
    }

    public void PrintInfo()
    {
        var builder = new StringBuilder();
        builder.AppendFormat("Fork-{0}: ", _number);

        if (Owner is null)
            builder.Append("Available");
        else
            builder.AppendFormat("In Use (used by {0})", Owner.Name);

        Console.WriteLine(builder.ToString());
    }
}
