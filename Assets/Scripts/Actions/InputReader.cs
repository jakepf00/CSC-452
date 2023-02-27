using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions {
    public bool IsAiming { get; private set; }
    public bool IsAttacking { get; private set; }
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public event Action AimEvent;
    public event Action AimEventStop;
    public event Action AttackEvent;
    public event Action JumpEvent;
    public event Action PauseEvent;
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
    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            PauseEvent?.Invoke();
        }
    }
    public void OnLook(InputAction.CallbackContext context) {
        LookValue = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context) {
        MovementValue = context.ReadValue<Vector2>();
    }
    public void OnAim(InputAction.CallbackContext context) {
        if (context.performed) {
            AimEvent?.Invoke();
            IsAiming = true;
        }
        else if (context.canceled) {
            AimEventStop?.Invoke();
            IsAiming = false;
        }
    }
    public void OnAttack(InputAction.CallbackContext context) {
        if (context.performed) {
            AttackEvent?.Invoke();
            IsAttacking = true;
        }
        else if (context.canceled) {
            IsAttacking = false;
        }
    }
}
