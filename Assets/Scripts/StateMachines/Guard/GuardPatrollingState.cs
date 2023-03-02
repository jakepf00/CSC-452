using UnityEngine;

public class GuardPatrollingState : GuardBaseState {
    public GuardPatrollingState(GuardStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.NavMeshAgent.destination = _stateMachine.Patrolpoints[_stateMachine.CurrentPatrolpoint].position;
        _stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        if (HasReachedPatrolpoint()) {
            _stateMachine.NavMeshAgent.velocity = Vector3.zero;
            _stateMachine.CurrentPatrolpoint = (_stateMachine.CurrentPatrolpoint + 1) % _stateMachine.Patrolpoints.Count;
            _stateMachine.SwitchState(new GuardIdleState(_stateMachine));
            return;
        }
        if (IsInChaseRange()) {
            _stateMachine.SwitchState(new GuardChasingState(_stateMachine));
            return;
        }
        MoveTowardsPatrolpoint(deltaTime);
        FacePatrolpoint();
        _stateMachine.Animator.SetFloat(MovementSpeedHash, 1.0f, AnimationDamping, deltaTime);
    }
    public override void Exit() {}

    void MoveTowardsPatrolpoint(float deltaTime) {
        if (_stateMachine.Patrolpoints.Count == 0) { return; }
        if (_stateMachine.NavMeshAgent.isOnNavMesh) {
            _stateMachine.NavMeshAgent.destination = _stateMachine.Patrolpoints[_stateMachine.CurrentPatrolpoint].position;
            Move(_stateMachine.NavMeshAgent.desiredVelocity.normalized * _stateMachine.MovementSpeed, deltaTime);
            _stateMachine.NavMeshAgent.velocity = _stateMachine.CharacterController.velocity;
        }
    }
    protected void FacePatrolpoint() {
        if (_stateMachine.Patrolpoints.Count == 0) { return; }
        Vector3 lookPosition = _stateMachine.Patrolpoints[_stateMachine.CurrentPatrolpoint].position - _stateMachine.transform.position;
        lookPosition.y = 0.0f;
        _stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
    protected bool HasReachedPatrolpoint() {
        if (_stateMachine.Patrolpoints.Count == 0) { return false; }
        float distanceToPatrolpoint = (_stateMachine.Patrolpoints[_stateMachine.CurrentPatrolpoint].position - _stateMachine.transform.position).sqrMagnitude;
        if (distanceToPatrolpoint <= _stateMachine.PatrolpointRange * _stateMachine.PatrolpointRange) {
            return true;
        } else {
            return false;
        }
    }
}