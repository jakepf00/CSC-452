using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardBaseState : State {
    protected GuardStateMachine _stateMachine;
    
    public GuardBaseState(GuardStateMachine stateMachine) {
        _stateMachine = stateMachine;
    }
}
