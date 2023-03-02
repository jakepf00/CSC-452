using UnityEngine;

public class GuardAttackingState : GuardBaseState {
    readonly int AttackHash = Animator.StringToHash("Shoot");
    public GuardAttackingState(GuardStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        if (GetNormalizedTime(_stateMachine.Animator) >= 1.0f) {
            _stateMachine.SwitchState(new GuardChasingState(_stateMachine));
            return;
        }
        FacePlayer();
    }
    public override void Exit() {}
}