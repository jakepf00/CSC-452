using UnityEngine;

public class PlayerStateMachine : StateMachine {
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float MovomentSpeed { get; private set; } = 10.0f;
    public Transform MainCameraTransform { get; private set; }

    void Start() {
        MainCameraTransform = Camera.main.transform;
        // SwitchState(new Player...State(this));
    }
}