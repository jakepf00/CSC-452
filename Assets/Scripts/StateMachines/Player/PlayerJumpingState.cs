using UnityEngine;

public class PlayerJumpingState : PlayerBaseState {
    Vector3 _momentum;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {
        _stateMachine.ForceReceiver.AddJumpForce(_stateMachine.JumpForce);
        _momentum = _stateMachine.CharacterController.velocity;
        _momentum.y = 0.0f;
    }

    public override void Exit() {}

    public override void Tick(float deltaTime) {
        Move(_momentum, deltaTime);
        _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
        if ((_stateMachine.MainCameraTransform.localRotation.x < 0.45f && _stateMachine.InputReader.LookValue.y > 0) || 
            (_stateMachine.MainCameraTransform.localRotation.x > -0.45f && _stateMachine.InputReader.LookValue.y < 0)) {
            _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);

        }
        
        if (_stateMachine.CharacterController.isGrounded) {
            _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
            return;
        }
    }
}