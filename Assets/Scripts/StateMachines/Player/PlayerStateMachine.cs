using UnityEngine;

public class PlayerStateMachine : StateMachine {
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; } = 10.0f;
    [field: SerializeField] public float MovementSpeedAimMultiplier { get; private set; } = 0.5f;
    [field: SerializeField] public Weapon[] Weapons { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    void Start() {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerMovementState(this));
    }
    void OnEnable() {
        InputReader.PauseEvent += OnPause;
    }
    void OnDisable() {
        InputReader.PauseEvent -= OnPause;
    }

    void OnPause() {
        GameController.Instance.PauseGame();
    }
}
