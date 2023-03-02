using UnityEngine;

public class GuardAttackingState : GuardBaseState {
    readonly int AttackHash = Animator.StringToHash("Shoot");
    public GuardAttackingState(GuardStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        if (!IsInAttackRange()) {
            _stateMachine.NavMeshAgent.ResetPath();
            _stateMachine.NavMeshAgent.velocity = Vector3.zero;
            _stateMachine.SwitchState(new GuardChasingState(_stateMachine));
            return;
        }
        FacePlayer();
    }
    public override void Exit() {}
}