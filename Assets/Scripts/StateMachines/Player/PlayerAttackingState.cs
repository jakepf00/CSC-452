using UnityEngine;

public class PlayerAttackingState : PlayerBaseState {
    Attack _attack;
    float _previousAnimationFrameTime = 0.0f;
    bool _forceApplied = false;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) {
        _attack = _stateMachine.Attacks[attackIndex];
    }
    public override void Enter() {
        //_stateMachine.Weapon.SetAttackData(_attack.Damage, _attack.KnockBack);
        _stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionTime);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        //FaceTarget();
        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);
        if (normalizedTime >= _previousAnimationFrameTime && normalizedTime < 1.0f) {
            // Try combo attack
            if (normalizedTime >= _attack.ForceTime) {
                TryApplyForce();
            }
            if (_stateMachine.InputReader.IsAttacking) {
                TryComboAttack(normalizedTime);
            }
        }
        else {
            // Transition to another state
            if (_stateMachine.Targeter.CurrentTarget == null) {
                _stateMachine.SwitchState(new PlayerMovementState(_stateMachine));
            }
        }
        _previousAnimationFrameTime = normalizedTime;
    }
    public override void Exit() {}
    void TryApplyForce() {
        if (_forceApplied) { return; }
        _stateMachine.ForceReceiver.AddForce(_stateMachine.transform.forward * _attack.Force);
        _forceApplied = true;
    }
    void TryComboAttack(float normalizedTime) {
        // Check: is combo attack available AND ready to transition
        if (_attack.ComboIndex == -1 || normalizedTime < _attack.ComboTime) { return; }
        _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, _attack.ComboIndex));
    }
}