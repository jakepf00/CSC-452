using UnityEngine;

public class PlayerAttackingState : PlayerBaseState {
    Weapon _weapon;
    int _weaponIndex;
    float _previousAnimationFrameTime = 0.0f;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int weaponIndex) : base(stateMachine) {
        _weapon = _stateMachine.Weapons[weaponIndex];
        _weaponIndex = weaponIndex;
    }
    public override void Enter() {
        //_stateMachine.Weapon.SetAttackData(_attack.Damage, _attack.KnockBack);
        _stateMachine.Animator.CrossFadeInFixedTime(_weapon.ShootAnimationName, _weapon.TransitionTime);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        //FaceTarget();
        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);
        if (normalizedTime >= _previousAnimationFrameTime && normalizedTime < 1.0f) {
            
        }
        else {
            // Transition to another state
            if (_stateMachine.Targeter.CurrentTarget == null) {
                _stateMachine.SwitchState(new PlayerAimingState(_stateMachine, _weaponIndex));
            }
        }
        _previousAnimationFrameTime = normalizedTime;
    }
    public override void Exit() {}
    void TryApplyForce() {
        
    }
    void TryComboAttack(float normalizedTime) {
        // Check: is combo attack available AND ready to transition
        if (_weapon.FullAutoTime < 0 || normalizedTime < _weapon.FullAutoTime) { return; }
        _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, _weaponIndex));
    }
}