using UnityEngine;

public class GuardDeathState : GuardBaseState {
    readonly int DeathHash = Animator.StringToHash("Death");
    const float DestroyTime = 5.0f;

    public GuardDeathState(GuardStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(DeathHash, CrossFadeDuration);
        GameObject.Destroy(_stateMachine.gameObject, DestroyTime);
    }
    public override void Tick(float deltaTime) {}
    public override void Exit() {}
}