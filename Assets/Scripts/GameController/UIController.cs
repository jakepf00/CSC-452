using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {
    public static UIController Instance { get; private set; }
    public TextMeshProUGUI _healthText;

    void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    void Start() {}
    void Update() {}
}