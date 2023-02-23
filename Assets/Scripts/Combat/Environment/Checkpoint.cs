using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public bool checkpointActive;
    [SerializeField] public GameObject checkpointObject;
    void Start() {
        checkpointObject.SetActive(true);
        checkpointActive = false;
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
            foreach (var checkpoint in checkpoints) {
                if (checkpoint.checkpointActive) {
                    // Deactivate previous checkpoint completely
                    checkpoint.checkpointObject.SetActive(false);
                    checkpoint.checkpointActive = false;
                }
            }
            checkpointActive = true;

            //GameController.Instance.RespawnPosition = gameObject.transform.position;
        }
    }
}