using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Interface.Controller;

namespace Src.Controller;

public class Fork: IForkController
{
    private bool _isTaken;
    private bool _isLocked;
    private readonly int _number;
    public IPhilosopher? Owner { get; set; }
    public IPhilosopher? Locker { get; set; }

    public static IFork Create(int number)
    {
        return new Fork(number);
    }

    public Fork(int number)
    {
        _isTaken = false;
        _isLocked = false;
        Owner = null;
        Locker = null;
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
        if (!_isTaken && !_isLocked)
            throw new ApplicationException("Try to put not taken fork");

        Owner = null;
        Locker = null;
        _isTaken = false;
        _isLocked = false;
    }

    public void Take(IPhilosopher philosopher)
    {
        if (_isTaken || !_isLocked || (_isLocked && Locker != philosopher))
        {
            string message = string.Format("{0} try to take already taken fork {1} by {2}, locked by {3}",
                philosopher.Name, _number, Owner?.Name, Locker?.Name);
            throw new ApplicationException(message);
        }

        Locker = philosopher;
        Owner = philosopher;
        _isTaken = true;
        _isLocked = true;
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

    public void Lock(IPhilosopher philosopher)
    {
        if (_isLocked)
        {
            string message = string.Format("{0} try to locked already locked fork {1} by {2}",
                philosopher.Name, _number, Locker?.Name);
            throw new ApplicationException(message);
        }

        Locker = philosopher;
        _isLocked = true;
    }

    public bool IsLocked()
    {
        return _isLocked;
    }

    public bool IsLockedBy(IPhilosopher philosopher)
    {
        return _isLocked && Locker == philosopher;
    }
}
