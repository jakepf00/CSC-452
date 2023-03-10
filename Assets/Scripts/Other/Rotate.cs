using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField] float _rotateX = 0.0f;
    [SerializeField] float _rotateY = 0.0f;
    [SerializeField] float _rotateZ = 0.0f;
    void Update() {
        transform.Rotate(_rotateX, _rotateY, _rotateZ, Space.Self);
    }
}