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

    protected bool IsInAttackRange() {
        if (_stateMachine.Player == null) { return false; }
        if (_stateMachine.Player.GetComponent<Health>().IsDead()) { return false; }
        float distanceToPlayer = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;
        return distanceToPlayer <= _stateMachine.PlayerAttackRange * _stateMachine.PlayerAttackRange;
    }
    protected bool IsInChaseRange() {
        if (_stateMachine.Player == null) { return false; }
        if (_stateMachine.Player.GetComponent<Health>().IsDead()) { return false; }
        float distanceToPlayer = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;
        return distanceToPlayer <= _stateMachine.PlayerChaseRange * _stateMachine.PlayerChaseRange;
    }
    protected void Move(float deltaTime) {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 movement, float deltaTime) {
        _stateMachine.CharacterController.Move((movement + _stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FacePlayer() {
        if (_stateMachine.Player == null) { return; }
        Vector3 lookPosition = _stateMachine.Player.transform.position - _stateMachine.transform.position;
        lookPosition.y = 0.0f;
        _stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
}