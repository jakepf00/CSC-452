using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour {
    List<Target> _targets = new List<Target>();
    public Target CurrentTarget { get; private set; }
    void OnTriggerEnter(Collider other) {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        _targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }
    void OnTriggerExit(Collider other) {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        RemoveTarget(target);
    }
    public bool SelectTarget() {
        if (_targets.Count == 0) { return false; }
        CurrentTarget = _targets[0];
        return true;
    }
    public void CancelTarget() {
        if (CurrentTarget == null) { return; }
        CurrentTarget = null;
    }
    void RemoveTarget(Target target) {
        if (CurrentTarget == target) {
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        _targets.Remove(target);
    }
}