using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateMachine : StateMachine {
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GuardForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; } = 5.0f;
    [field: SerializeField] public float PlayerChaseRange { get; private set; } = 50.0f;
    [field: SerializeField] public float PlayerAttackRange { get; private set; } = 35.0f;
    public GameObject Player { get; private set; }
    public List<Transform> Patrolpoints = new List<Transform>();
    public int CurrentPatrolpoint { get; set; } = 0;
    public float PatrolpointRange { get; private set; } = 1.0f;

    void Awake() {
        foreach (GameObject patrolpoint in GameObject.FindGameObjectsWithTag("Patrolpoint")) {
            Patrolpoints.Add(patrolpoint.GetComponent<Transform>());
        }
    }
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        NavMeshAgent.updatePosition = false;
        NavMeshAgent.updateRotation = false;
        SwitchState(new GuardIdleState(this));
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
        SwitchState(new GuardImpactState(this));
    }
    void OnDeath() {
        SwitchState(new GuardDeathState(this));
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChaseRange);
    }
}