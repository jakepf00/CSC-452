using UnityEngine;
using UnityEngine.AI;

public class GuardForceReceiver : ForceReceiver {
    [SerializeField] NavMeshAgent _agent;
    void Update() {
        if (_verticalVelocity < 0.0f && _controller.isGrounded) {
            _verticalVelocity = Physics.gravity.y * _gravityModifier * Time.deltaTime;
        }
        else {
            _verticalVelocity += Physics.gravity.y * _gravityModifier * Time.deltaTime;
        }
        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
        if (_agent != null && _impact.sqrMagnitude < 0.2f * 0.2f) {
            _impact = Vector3.zero;
            _agent.enabled = true;
        }
    }
    public override void AddForce(Vector3 force) {
        base.AddForce(force);
        if (_agent == null) { return; }
        _agent.enabled = false;
    }
}