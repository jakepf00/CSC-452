using System;
using UnityEngine;

public class PlayerShootingState : PlayerBaseState {
    Weapon _weapon;
    int _weaponIndex;
    bool _started = false;
    DateTime stateEnterTime;

    public PlayerShootingState(PlayerStateMachine stateMachine, int weaponIndex) : base(stateMachine) {
        _weapon = _stateMachine.Weapons[weaponIndex];
        _weaponIndex = weaponIndex;
    }
    public override void Enter() {
        stateEnterTime = System.DateTime.Now;
        _stateMachine.InputReader.AttackEvent += OnAttack;
        _stateMachine.Animator.CrossFadeInFixedTime(_weapon.ShootAnimationName, _weapon.TransitionTime);
        Shoot();
    }
    public override void Tick(float deltaTime) {
        float normalizedTime = _stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        normalizedTime -= Mathf.Floor(normalizedTime);
        if (normalizedTime > 0.1f && normalizedTime < 0.2f) _started = true;
        if (_started) {
            TryFullAuto();
            if (normalizedTime > 0.95f) {
                _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
            }
        }

        if(_stateMachine.InputReader.MovementValue == Vector2.zero) {
            Move(deltaTime);
            _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
            if ((_stateMachine.MainCameraTransform.localRotation.x < 0.45f && _stateMachine.InputReader.LookValue.y > 0) || 
                (_stateMachine.MainCameraTransform.localRotation.x > -0.45f && _stateMachine.InputReader.LookValue.y < 0)) {
                _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);

            }
            return;
        }

        Vector3 movement = MoveWithCamera();
        Move(movement * _stateMachine.MovementSpeed, deltaTime);
        _stateMachine.transform.Rotate(new Vector3(0, _stateMachine.InputReader.LookValue.x, 0) * RotationDamping * deltaTime);
        if ((_stateMachine.MainCameraTransform.localRotation.x < 0.45f && _stateMachine.InputReader.LookValue.y > 0) || 
            (_stateMachine.MainCameraTransform.localRotation.x > -0.45f && _stateMachine.InputReader.LookValue.y < 0)) {
            _stateMachine.MainCameraTransform.Rotate(new Vector3(-_stateMachine.InputReader.LookValue.y, 0, 0) * RotationDamping * deltaTime);

        }
    }
    public override void Exit() {
        _stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    void TryApplyForce() {

    }
    void TryFullAuto() {
        if (_weapon.FullAutoTime < 0 || System.DateTime.Now.Ticks < stateEnterTime.AddSeconds(_weapon.FullAutoTime).Ticks || !_stateMachine.InputReader.IsAttacking) { return; }
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
    void OnAttack() {
        if (_weapon.SemiAutoTime < 0 || System.DateTime.Now.Ticks < stateEnterTime.AddSeconds(_weapon.SemiAutoTime).Ticks) { return; }
        _stateMachine.SwitchState(new PlayerShootingState(_stateMachine, _weaponIndex));
    }
    void Shoot() {
        _weapon.MuzzleFlash.Play(); 
        RaycastHit hit;
        if (Physics.Raycast(_stateMachine.MainCameraTransform.position, _stateMachine.MainCameraTransform.forward, out hit)) {
            Health health = hit.transform.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(_weapon.Damage);
            }
            GameObject.Instantiate(_weapon.ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}