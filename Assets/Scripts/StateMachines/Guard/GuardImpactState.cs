using UnityEngine;

public class GuardImpactState : GuardBaseState {
    readonly int ImpactHash = Animator.StringToHash("Impact");
    [SerializeField] float _impactDuration = 1.0f;
    public GuardImpactState(GuardStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.NavMeshAgent.ResetPath();
        _stateMachine.NavMeshAgent.velocity = Vector3.zero;
        _stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        _impactDuration -= deltaTime;
        if (_impactDuration <= 0.0f) {
            _stateMachine.SwitchState(new GuardIdleState(_stateMachine));
            return;
        }
        Move(deltaTime);
    }
    public override void Exit() {}
}