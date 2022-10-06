
public class StateFactory
{
    private PlayerController _context;

    public StateFactory(PlayerController context)
    {
        _context = context;
    }

    public BaseState Idle()
    {
        return new IdleState(_context, this);
    }

    public BaseState Walk()
    {
        return new WalkingState(_context, this);
    }

    public BaseState Run()
    {
        return new RunningState(_context, this);
    }

}
