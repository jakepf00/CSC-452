using UnityEngine;

public class ForceReceiver : MonoBehaviour {
    [SerializeField] protected CharacterController _controller;
    [SerializeField] protected float _verticalVelocity = 10.0f;
    [SerializeField] protected float _gravityModifier = 5.0f;
    [SerializeField] protected float _drag = 0.1f;
    protected Vector3 _impact;
    protected Vector3 _dampingVelocity;
    public Vector3 Movement => Vector3.up * _verticalVelocity;

    void Update() {
        if (_verticalVelocity < 0.0f && _controller.isGrounded) {
            _verticalVelocity = Physics.gravity.y * _gravityModifier * Time.deltaTime;
        }
        else {
            _verticalVelocity += Physics.gravity.y * _gravityModifier * Time.deltaTime;
        }
        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    }
    public virtual void AddForce(Vector3 force) {
        _impact += force;
    }
}
