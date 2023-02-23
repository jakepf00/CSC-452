using System;
using UnityEngine;
[Serializable]

public class Attack {
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionTime { get; private set; } = 0.1f;
    [field: SerializeField] public int ComboIndex { get; private set; } = -1;
    [field: SerializeField] public float ComboTime { get; private set; } = 0.0f;
    [field: SerializeField] public float Force { get; private set; } = 15.0f;
    [field: SerializeField] public float ForceTime { get; private set; } = 0.35f;
    [field: SerializeField] public float KnockBack { get; private set; } = 10.0f;
    [field: SerializeField] public int Damage { get; private set; } = -10;
}
