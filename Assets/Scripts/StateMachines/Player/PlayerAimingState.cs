using UnityEngine;

public class PlayerAimingState : PlayerBaseState {
    Weapon _weapon;
    int _weaponIndex;

    public PlayerAimingState(PlayerStateMachine stateMachine, int weaponIndex) : base(stateMachine) {
        _weapon = _stateMachine.Weapons[weaponIndex];
        _weaponIndex = weaponIndex;
    }
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(_weapon.AimAnimationName, _weapon.TransitionTime);
    }
    public override void Tick(float deltaTime) {
        if (_stateMachine.InputReader.IsAttacking) {
            _stateMachine.SwitchState(new PlayerShootingState(_stateMachine, _weaponIndex));
        }

        if (!_stateMachine.InputReader.IsAiming) {
            _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
        }
    }
    public override void Exit() {}
}