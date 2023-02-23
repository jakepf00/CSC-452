using UnityEngine;

public class PlayerAimingState : PlayerBaseState {
    Weapon _weapon;

    public PlayerAimingState(PlayerStateMachine stateMachine, int weaponIndex) : base(stateMachine) { // int weaponIndex?
        _weapon = _stateMachine.Weapons[weaponIndex];
    }
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(_weapon.AimAnimationName, _weapon.TransitionTime);
    }
    public override void Tick(float deltaTime) {
        if (!_stateMachine.InputReader.IsAiming) {
            _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
        }
    }
    public override void Exit() {}
}