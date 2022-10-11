/// <summary>
/// base state for movement state machine
/// </summary>
public abstract class BaseState
{
    //references for controller and factory
    private PlayerController _context;
    private StateFactory _factory;

    //getters
    protected PlayerController Context { get { return _context; } }
    protected StateFactory Factory { get { return _factory; } }

    //builder sets references
    public BaseState(PlayerController context, StateFactory factory)
    {
        this._context = context;
        this._factory = factory;
    }

    //method that triggers when a new state enters state machine execution
    public abstract void EnterState();

    //method that triggers on controller update so that only active state is updated
    public abstract void UpdateState();

    //if something needs to happen on state exit (not used in this case)
    public abstract void ExitState();

    //check switch conditions for state change
    public abstract void CheckSwitchState();

    //switches to a different state
    private protected void SwitchState(BaseState newState)
    {
        //current state exit
        ExitState();

        //new state enters state
        newState.EnterState();
        Context.CurrentState = newState;

    }
}
