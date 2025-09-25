using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Strategy;

namespace Src.Strategy;

public class Philosopher: IPhilosopher
{
    private PhilosopherStates _state;
    private Actions _action;
    private int _countEatingFood;
    private readonly int _eatingTime;
    private readonly int _takeForkTime;
    private readonly int _thinkingTime;
    private readonly int _putForkTimeout;
    private int _thinkingCounter;
    private int _eatingCounter;
    private int _takeForkCounter;
    private readonly int _putForkCounter;

    public string Name { get; set; }
    public IFork? LeftFork { get; set; }
    public IFork? RightFork { get; set; }
    public bool FirstTakeLeftFork { get; set; }

    public static IPhilosopher Create(PhilosopherDTO philosopherDto)
    {
        return new Philosopher
            (
                philosopherDto.Name,
                philosopherDto.EatingTime,
                philosopherDto.TakeForkTime,
                philosopherDto.ThinkingTime,
                philosopherDto.PutForkTimeout
            );
    }

    public Philosopher(string name, int eatingTime, int takeForkTime, int thinkingTime, int putForkTimeout)
    {
        Name = name;

        _state = PhilosopherStates.Thinking;
        _eatingTime = eatingTime;
        _takeForkTime = takeForkTime;
        _thinkingTime = thinkingTime;
        _putForkTimeout = putForkTimeout; // maybe it useless


        _thinkingCounter = 0;
        _eatingCounter = 0;
        _takeForkCounter = 0;
        _putForkCounter = 0;
    }

    public void Step()
    {
        switch (_state)
        {
            case PhilosopherStates.Thinking:
                {
                    ProcessThinkingState();
                    break;
                }
            case PhilosopherStates.Hungry:
                {
                    ProcessHungryState();
                    break;
                }
            case PhilosopherStates.TakeLeftFork:
                {
                    ProcessTakeLeftForkState();
                    break;
                }
            case PhilosopherStates.TakeRightFork:
                {
                    ProcessTakeRightForkState();
                    break;
                }
            case PhilosopherStates.Eating:
                {
                    ProcessEatingState();
                    break;
                }
            default: throw new ApplicationException("Unknown philosopher state");
        }
    }

    private void ProcessThinkingState()
    {
        if (_thinkingCounter < _thinkingTime)
        {
            ++_thinkingCounter;
            _action = Actions.None;
            return;
        }

        _thinkingCounter = 0;
        _state = PhilosopherStates.Hungry;
        if (FirstTakeLeftFork)
            _action = Actions.TryTakeLeftFork;
        else
            _action = Actions.TryTakeRightFork;
    }

    private void ProcessHungryState()
    {
        if (_takeForkCounter < _takeForkTime)
        {
            ++_takeForkCounter;
            if (FirstTakeLeftFork)
                _action = Actions.TryTakeLeftFork;
            else
                _action = Actions.TryTakeLeftFork;

            return;
        }

        _takeForkCounter = 0;

        if (FirstTakeLeftFork && LeftFork!.TryTake(this))
        {
            _state = PhilosopherStates.TakeLeftFork;
            _action = Actions.TakeLeftFork;
        }
        else if (RightFork!.TryTake(this))
        {
            _state = PhilosopherStates.TakeRightFork;
            _action = Actions.TakeRightFork;
        }
    }

    private void ProcessTakeLeftForkState()
    {
        if (_takeForkCounter < _takeForkTime)
        {
            ++_takeForkCounter;
            _action = Actions.TryTakeLeftFork;
            return;
        }

        _takeForkCounter = 0;

        if (RightFork!.TryTake(this))
        {
            _state = PhilosopherStates.Eating;
            _action = Actions.None;
        }
    }

    private void ProcessTakeRightForkState()
    {
        if (_takeForkCounter < _takeForkTime)
        {
            ++_takeForkCounter;
            _action = Actions.TryTakeRightFork;
        }

        _takeForkCounter = 0;

        if (LeftFork!.TryTake(this))
        {
            _state = PhilosopherStates.Eating;
            _action = Actions.None;
        }
    }

    private void ProcessEatingState()
    {
        if (_eatingCounter < _eatingTime)
        {
            ++_eatingCounter;
            return;
        }

        _eatingCounter = 0;
        ++_countEatingFood;

        if (FirstTakeLeftFork)
        {
            LeftFork!.Put();
            RightFork!.Put();
        }
        else
        {
            RightFork!.Put();
            LeftFork!.Put();
        }

        _state = PhilosopherStates.Thinking;
        _action = Actions.ReleaseForks;
    }

    public void PrintInfo()
    {
        var builder = new StringBuilder(Name);
        _ = builder.AppendFormat(": {0} (Action = {1}), eating: {2}", _state, _action, _countEatingFood);
        Console.WriteLine(builder.ToString());
    }

    public bool IsEating()
    {
        return _state == PhilosopherStates.Eating;
    }
}
