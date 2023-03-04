using System;
using UnityEngine;

public class GuardAttackingState : GuardBaseState {
    readonly int AttackHash = Animator.StringToHash("Shoot");
    bool _started = false;
    DateTime _stateEnterTime;
    public GuardAttackingState(GuardStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter() {
        _stateEnterTime = System.DateTime.Now;
        _stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
        Shoot();
    }
    public override void Tick(float deltaTime) {
        if (!IsInAttackRange()) {
            _stateMachine.NavMeshAgent.ResetPath();
            _stateMachine.NavMeshAgent.velocity = Vector3.zero;
            _stateMachine.SwitchState(new GuardChasingState(_stateMachine));
            return;
        }

        FacePlayer();
        float normalizedTime = _stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        normalizedTime -= Mathf.Floor(normalizedTime);
        if (normalizedTime > 0.1f && normalizedTime < 0.2f) _started = true;
        if (_started) {
            TryFullAuto();
            if (normalizedTime > 0.95f) {
                _stateMachine.SwitchState(new GuardAttackingState(_stateMachine));
            }
        }
    }
    public override void Exit() {}

    void TryFullAuto() {
        if (_stateMachine.Weapon.FullAutoTime < 0 || System.DateTime.Now.Ticks < _stateEnterTime.AddSeconds(_stateMachine.Weapon.FullAutoTime).Ticks) { return; }
        _stateMachine.SwitchState(new GuardAttackingState(_stateMachine));
    }
    void Shoot() {
        _stateMachine.Weapon.MuzzleFlash.Play(); 
        RaycastHit hit;
        float spread = _stateMachine.Weapon.BulletSpreadHip;
        Vector3 direction = new Vector3(
            _stateMachine.transform.forward.x + UnityEngine.Random.Range(-spread, spread)/100.0f,
            _stateMachine.transform.forward.y + UnityEngine.Random.Range(-spread, spread)/100.0f,
            _stateMachine.transform.forward.z + UnityEngine.Random.Range(-spread, spread)/100.0f
        );
        Vector3 origin = _stateMachine.Weapon.transform.position;
        Debug.DrawRay(origin, direction * 1000.0f, Color.yellow, 1);
        if (Physics.Raycast(origin, direction, out hit)) { // TODO: origin raised by offsetY
            Health health = hit.transform.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(_stateMachine.Weapon.Damage);
            }
            GameObject.Instantiate(_stateMachine.Weapon.ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}