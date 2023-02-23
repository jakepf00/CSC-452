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
        //_stateMachine.Weapon.SetAttackData(_attack.Damage, _attack.KnockBack);
        _stateMachine.Animator.CrossFadeInFixedTime(_weapon.ShootAnimationName, _weapon.TransitionTime);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        float normalizedTime = _stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        normalizedTime -= Mathf.Floor(normalizedTime);
        if (normalizedTime > 0.2f && normalizedTime < 0.5f) _started = true;
        if (_started) {
            if (_stateMachine.InputReader.IsAttacking && _weapon.FullAutoTime > 0.0f && normalizedTime >= _weapon.FullAutoTime) {
                TryFullAuto(normalizedTime);
            }
            else if (normalizedTime > 0.95f) {
                _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
            }
        }
    }
    public override void Exit() {}
    void TryApplyForce() {
        
    }
    void TryFullAuto(float normalizedTime) {
        // Check: is combo attack available AND ready to transition
        if (_weapon.FullAutoTime < 0 || normalizedTime < _weapon.FullAutoTime) { return; }
        _stateMachine.SwitchState(new PlayerShootingState(_stateMachine, _weaponIndex));
    }
}