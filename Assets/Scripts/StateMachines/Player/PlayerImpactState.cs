using UnityEngine;

public class PlayerImpactState : PlayerBaseState {
    readonly int ImpactHash = Animator.StringToHash("Impact");
    [SerializeField] float _impactDuration = 1.0f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        _impactDuration -= deltaTime;
        if (_impactDuration <= 0.0f) {
            _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
            return;
        }
        Move(deltaTime);
    }
    public override void Exit() {}
}