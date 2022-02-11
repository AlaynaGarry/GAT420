using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : State
{
    public SeekState(StateAgent owner, string name) : base(owner, name) { }

    public override void OnEnter()
    {
        owner.movement.Stop();
        owner.animator.SetTrigger("seek");
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
       
    }
}
