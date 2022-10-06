using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    private PlayerController _context;
    private StateFactory _factory;

    protected PlayerController Context { get { return _context; } }
    protected StateFactory Factory { get { return _factory; } }

    public BaseState (PlayerController context, StateFactory factory)
    {
        this._context = context;
        this._factory = factory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public abstract void CheckSwitchState();

    private protected void SwitchState(BaseState newState)
    {
        //current state exit
        ExitState();

        //new state enters state
        newState.EnterState();
        Context.CurrentState = newState;

    }
}
