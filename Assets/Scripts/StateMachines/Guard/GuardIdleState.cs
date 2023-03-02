using UnityEngine;

public class GuardIdleState : GuardBaseState {
    [SerializeField] float _idleWaitTime = 2.0f;
    float _idleTimer;

    public GuardIdleState(GuardStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter() {
        _idleTimer = _idleWaitTime;
        _stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        _idleTimer -= deltaTime;
        if (_idleTimer <= 0.0f) {
            if (IsInChaseRange()) {
                _stateMachine.SwitchState(new GuardChasingState(_stateMachine));
                return;
            }
            else {
                _stateMachine.SwitchState(new GuardPatrollingState(_stateMachine));
                return;
            }
        }
        else {
            Move(deltaTime);
            _stateMachine.Animator.SetFloat(MovementSpeedHash, 0.0f, AnimationDamping, deltaTime);
        }
    }
    public override void Exit() { }
}