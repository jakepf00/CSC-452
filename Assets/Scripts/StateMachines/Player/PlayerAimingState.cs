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
        
        if(_stateMachine.InputReader.MovementValue == Vector2.zero) {
            Move(deltaTime);
            _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
            _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);
            return;
        }

        Vector3 movement = MoveWithCamera();
        Move(movement * _stateMachine.MovementSpeed, deltaTime);
        _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
        _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);
    }
    public override void Exit() {}

    Vector3 MoveWithCamera() {
        Vector3 forward = _stateMachine.MainCameraTransform.forward;
        Vector3 right = _stateMachine.MainCameraTransform.right;
        forward.y = 0.0f;
        right.y = 0.0f;
        forward.Normalize();
        right.Normalize();
        return forward * _stateMachine.InputReader.MovementValue.y + right * _stateMachine.InputReader.MovementValue.x;
    }
}