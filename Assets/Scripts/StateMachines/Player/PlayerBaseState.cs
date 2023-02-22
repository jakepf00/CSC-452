using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State {
    protected readonly int MovementBlendTreeHash = Animator.StringToHash("MovementBlendTree");
    protected readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    protected const float AnimationDamping = 0.1f;
    protected const float RotationDamping = 10.0f;
    protected const float CrossFadeDuration = 1.0f;
    protected PlayerStateMachine _stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine) {
        _stateMachine = stateMachine;
    }

    protected void Move(float deltaTime) {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 movement, float deltaTime) {
        _stateMachine.CharacterController.Move((movement + _stateMachine.ForceReceiver.Movement) * deltaTime);
    }
}
