using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour {
    State _currentState;
    
    void Update()
    {
        _currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState) {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
