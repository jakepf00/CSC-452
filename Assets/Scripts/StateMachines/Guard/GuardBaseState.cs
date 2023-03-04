using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardBaseState : State {
    protected readonly int MovementBlendTreeHash = Animator.StringToHash("MovementBlendTree");
    protected readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    protected const float AnimationDamping = 0.1f;
    protected const float RotationDamping = 10.0f;
    protected const float CrossFadeDuration = 1.0f;
    private float _viewOffsetY = 1.0f;

    protected GuardStateMachine _stateMachine;

    public GuardBaseState(GuardStateMachine stateMachine) {
        _stateMachine = stateMachine;
    }

    protected bool IsInAttackRange() { return IsInRange(_stateMachine.PlayerAttackRange); }
    protected bool IsInChaseRange() { return IsInRange(_stateMachine.PlayerChaseRange); }
    private bool IsInRange(float range) {
        if (_stateMachine.Player == null) { return false; }
        if (_stateMachine.Player.GetComponent<Health>().IsDead()) { return false; }
        float distanceToPlayer = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;
        return distanceToPlayer <= range * range;
    }
    protected bool CanSeePlayer() {
        if (_stateMachine.Player == null) { return false; }
        if (_stateMachine.Player.GetComponent<Health>().IsDead()) { return false; }
        float playerViewRange = _stateMachine.PlayerChaseRange - 0.2f;
        Vector3 origin = _stateMachine.transform.position;
        origin.y += _viewOffsetY;
        Vector3 direction = (_stateMachine.Player.transform.position - _stateMachine.transform.position).normalized;
        RaycastHit vision;
        if (Physics.Raycast(origin, direction, out vision, playerViewRange)) {
            //Debug.DrawRay(origin, direction * playerViewRange, Color.green);
            //Debug.DrawLine(origin, vision.point, Color.green);
            //Debug.Log(vision.transform.tag);
            if(vision.transform.tag == "Player") return true;
        }
        return false;
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