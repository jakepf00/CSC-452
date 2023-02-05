using UnityEngine;

public class PlayerMovementState : PlayerBaseState {
    public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine) {

    }

    public override void Enter() {

    }
    public override void Exit() {
        
    }
    public Vector2 lookRotationaa;
    public override void Tick(float deltaTime) {
        lookRotationaa = _stateMachine.InputReader.LookValue;
        if(_stateMachine.InputReader.MovementValue == Vector2.zero) {
            Move(deltaTime);
            _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
            _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);
            _stateMachine.Animator.SetFloat("MovementSpeed", 0.0f, AnimationDamping, deltaTime);
            return;
        }

        Vector3 movement = MoveWithCamera();
        Move(movement * _stateMachine.MovementSpeed, deltaTime);
        _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
        _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);
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

