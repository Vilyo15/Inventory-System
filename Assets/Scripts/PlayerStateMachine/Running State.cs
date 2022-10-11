using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public RunningState(PlayerController context, StateFactory factory) : base(context, factory) { }
    public override void CheckSwitchState()
    {
        if (!Context.IsMovementPressed && !Context.IsRunPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.IsMovementPressed && !Context.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void EnterState()
    {
        Context.Animator.SetBool(Context.IsWalkingHash, true);
        Context.Animator.SetBool(Context.IsRunningHash, true);
    }

    public override void ExitState()
    {
        // throw new System.NotImplementedException();
    }

    
    public override void UpdateState()
    {
        Debug.Log("stillrunning" + Context.DirectionValue);
        CheckSwitchState();
        Context.AppliedMovementX = Context.CurrentMovementInput.x * Context.RunMultiplier;
        Context.AppliedMovementY = Context.CurrentMovementInput.y * Context.RunMultiplier;
        Context.Animator.SetInteger(Context.DirectionHash, Context.DirectionValue);
    }
}
