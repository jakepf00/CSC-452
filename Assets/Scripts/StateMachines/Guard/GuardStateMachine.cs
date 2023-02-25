using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateMachine : StateMachine {
    [field: SerializeField] public Animator Animator { get; private set; }
    //[field: SerializeField] public EnemyForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    public GameObject Player { get; private set; }

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        //SwitchState(new GuardIdleState(this));
    }
    void OnEnable() {
        Health.DamageEvent += OnDamage;
        Health.DeathEvent += OnDeath;
    }
    void OnDisable() {
        Health.DamageEvent -= OnDamage;
        Health.DeathEvent -= OnDeath;
    }
    void OnDamage() {
        //SwitchState(new GuardImpactState(this));
    }
    void OnDeath() {
        SwitchState(new GuardDeathState(this));
    }
}