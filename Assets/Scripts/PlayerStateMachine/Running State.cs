/// <summary>
/// running state of the movement state machine
/// </summary>
public class RunningState : BaseState
{
    public RunningState(PlayerController context, StateFactory factory) : base(context, factory) { }
    public override void CheckSwitchState()
    {
        //if not running and not walking go to idle
        if (!Context.IsMovementPressed && !Context.IsRunPressed)
        {
            SwitchState(Factory.Idle());
        }
        //if walking and not running go to walk
        else if (Context.IsMovementPressed && !Context.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void EnterState()
    {
        //set animator to walkking
        Context.Animator.SetBool(Context.IsWalkingHash, true);
        Context.Animator.SetBool(Context.IsRunningHash, true);
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }


    public override void UpdateState()
    {
        CheckSwitchState();
        Context.AppliedMovementX = Context.CurrentMovementInput.x * Context.RunMultiplier;
        Context.AppliedMovementY = Context.CurrentMovementInput.y * Context.RunMultiplier;
        Context.Animator.SetInteger(Context.DirectionHash, Context.DirectionValue);
    }
}
