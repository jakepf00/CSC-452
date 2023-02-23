using System;
using UnityEngine;
[Serializable]

public class Weapon {
    [field: SerializeField] public string AimAnimationName { get; private set; }
    [field: SerializeField] public string ShootAnimationName { get; private set; }
    [field: SerializeField] public string ReloadAnimationName { get; private set; }
    [field: SerializeField] public float TransitionTime { get; private set; } = 0.1f;
    [field: SerializeField] public float FullAutoTime { get; private set; } = -1.0f;
    [field: SerializeField] public float SemiAutoTime { get; private set; } = 0.0f;
    [field: SerializeField] public float ReloadTime { get; private set; } = 1.0f;
    [field: SerializeField] public int Damage { get; private set; } = -10;
}