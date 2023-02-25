using UnityEngine;

public class PlayerMovementState : PlayerBaseState {
    public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine) {

    }

    public override void Enter() {
        _stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);
    }
    public override void Exit() {}
    public override void Tick(float deltaTime) {
        if (_stateMachine.InputReader.IsAiming) {
            _stateMachine.SwitchState(new PlayerAimingState(_stateMachine, 0)); // TODO: replace 0 with current weapon
        }

        if(_stateMachine.InputReader.MovementValue == Vector2.zero) {
            Move(deltaTime);
            _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
            if ((_stateMachine.MainCameraTransform.localRotation.x < 0.45f && _stateMachine.InputReader.LookValue.y > 0) || 
                (_stateMachine.MainCameraTransform.localRotation.x > -0.45f && _stateMachine.InputReader.LookValue.y < 0)) {
                _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);

            }
            _stateMachine.Animator.SetFloat("MovementSpeed", 0.0f, AnimationDamping, deltaTime);
            return;
        }

        Vector3 movement = MoveWithCamera();
        Move(movement * _stateMachine.MovementSpeed, deltaTime);
        _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
        if ((_stateMachine.MainCameraTransform.localRotation.x < 0.45f && _stateMachine.InputReader.LookValue.y > 0) || 
            (_stateMachine.MainCameraTransform.localRotation.x > -0.45f && _stateMachine.InputReader.LookValue.y < 0)) {
            _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);

        }
        _stateMachine.Animator.SetFloat("MovementSpeed", 1.0f, AnimationDamping, deltaTime);
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