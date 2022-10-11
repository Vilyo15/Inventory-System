using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : BaseState
{
    public WalkingState(PlayerController context, StateFactory factory) : base(context, factory) { }
    public override void CheckSwitchState()
    {
        if (!Context.IsMovementPressed)
        {
            Debug.Log("is not movement pressed");
            SwitchState(Factory.Idle());
        }
        else if (Context.IsMovementPressed && Context.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
    }

    public override void EnterState()
    {
        Debug.Log("walking");
        Context.Animator.SetBool(Context.IsWalkingHash, true);
        Context.Animator.SetBool(Context.IsRunningHash, false);
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        Debug.Log("stillwalking");
        CheckSwitchState();
        Context.AppliedMovementX = Context.CurrentMovementInput.x;
        Context.AppliedMovementY = Context.CurrentMovementInput.y;
        Context.Animator.SetInteger(Context.DirectionHash, Context.DirectionValue);
    }

    
}
