using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardBaseState : State {
    protected readonly int MovementBlendTreeHash = Animator.StringToHash("MovementBlendTree");
    protected readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    protected const float AnimationDamping = 0.1f;
    protected const float RotationDamping = 10.0f;
    protected const float CrossFadeDuration = 1.0f;

    protected GuardStateMachine _stateMachine;

    public GuardBaseState(GuardStateMachine stateMachine) {
        _stateMachine = stateMachine;
    }
}