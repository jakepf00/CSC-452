using UnityEngine;

public class PlayerMovementState : PlayerBaseState {
    public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine) {

    }

    public override void Enter() {

    }
    public override void Exit() {
        
    }
    public override void Tick(float deltaTime) {
        if(_stateMachine.InputReader.MovementValue == Vector2.zero) return;

        Vector3 movement = Vector3.zero;
        movement.x = _stateMachine.InputReader.MovementValue.x;
        movement.y = 0.0f;
        movement.z = _stateMachine.InputReader.MovementValue.y;
        _stateMachine.CharacterController.Move(movement * _stateMachine.MovementSpeed * deltaTime);
    }
}
