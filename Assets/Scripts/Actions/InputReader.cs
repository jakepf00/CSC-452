using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions {
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public event Action JumpEvent;
    Controls _controls;

    void Start() {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }
    void OnDestroy() {
        _controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            JumpEvent?.Invoke();
        }
    }
    public void OnLook(InputAction.CallbackContext context) {
        LookValue = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context) {
        MovementValue = context.ReadValue<Vector2>();
    }
}
