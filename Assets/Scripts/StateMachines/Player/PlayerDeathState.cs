using UnityEngine;

public class PlayerDeathState : PlayerBaseState {
    readonly int DeathHash = Animator.StringToHash("Death");
    const float DestroyTime = 10.0f;

    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(DeathHash, CrossFadeDuration);
        GameObject.Destroy(_stateMachine.gameObject, DestroyTime);
    }
    public override void Tick(float deltaTime) {}
    public override void Exit() {}
}