/// <summary>
/// movement idle state of state machine
/// </summary>
public class IdleState : BaseState
{
    public IdleState(PlayerController context, StateFactory factory) : base(context, factory) { }
    public override void CheckSwitchState()
    {
        //check if player starts walking or running and change state
        if (Context.IsMovementPressed && Context.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        else if (Context.IsMovementPressed)
        {

            SwitchState(Factory.Walk());
        }
    }

    public override void EnterState()
    {
        //sets animations and movements to 0 on enter
        Context.Animator.SetBool(Context.IsWalkingHash, false);
        Context.Animator.SetBool(Context.IsRunningHash, false);
        Context.AppliedMovementX = 0;
        Context.AppliedMovementY = 0;
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        Context.Animator.SetInteger(Context.DirectionHash, 0);
    }


}
