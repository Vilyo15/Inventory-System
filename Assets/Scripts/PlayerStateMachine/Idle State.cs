using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(PlayerController context, StateFactory factory) : base(context, factory) { }
    public override void CheckSwitchState()
    {
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
    }

    
}
