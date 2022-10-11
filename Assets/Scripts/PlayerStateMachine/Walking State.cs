/// <summary>
/// walking state of the movement state machine
/// </summary>
public class WalkingState : BaseState
{
    public WalkingState(PlayerController context, StateFactory factory) : base(context, factory) { }
    public override void CheckSwitchState()
    {
        //if not walking, idle
        if (!Context.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        // if running, run
        else if (Context.IsMovementPressed && Context.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
    }

    public override void EnterState()
    {
        Context.Animator.SetBool(Context.IsWalkingHash, true);
        Context.Animator.SetBool(Context.IsRunningHash, false);
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        Context.AppliedMovementX = Context.CurrentMovementInput.x;
        Context.AppliedMovementY = Context.CurrentMovementInput.y;
        Context.Animator.SetInteger(Context.DirectionHash, Context.DirectionValue);
    }


}
