using UnityEngine;

public class PlayerShootingState : PlayerBaseState {
    Weapon _weapon;
    int _weaponIndex;
    bool _started = false;

    public PlayerShootingState(PlayerStateMachine stateMachine, int weaponIndex) : base(stateMachine) {
        _weapon = _stateMachine.Weapons[weaponIndex];
        _weaponIndex = weaponIndex;
    }
    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(_weapon.ShootAnimationName, _weapon.TransitionTime);
    }
    public override void Tick(float deltaTime) {
        float normalizedTime = _stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        normalizedTime -= Mathf.Floor(normalizedTime);
        if (normalizedTime > 0.2f && normalizedTime < 0.4f) _started = true;
        if (_started) {
            if (_stateMachine.InputReader.IsAttacking && _weapon.FullAutoTime > 0.0f && normalizedTime >= _weapon.FullAutoTime) {
                TryFullAuto(normalizedTime);
            }
            else if (normalizedTime > 0.95f) {
                _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
            }
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
    
    void TryApplyForce() {
        
    }
    void TryFullAuto(float normalizedTime) {
        // Check: is combo attack available AND ready to transition
        if (_weapon.FullAutoTime < 0 || normalizedTime < _weapon.FullAutoTime) { return; }
        _stateMachine.SwitchState(new PlayerShootingState(_stateMachine, _weaponIndex));
    }
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