using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }
    bool _isPaused;// Temporary, remove after implementing real pause screen

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame() {
        if (_isPaused) { // (UIController.Instance.pauseScreen.activeInHierarchy)
            _isPaused = false; // UIController.Instance.pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            _isPaused = true; // UIController.Instance.pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
