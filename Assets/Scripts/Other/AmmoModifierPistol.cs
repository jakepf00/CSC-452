using UnityEngine;

public class AmmoModifierPistol : MonoBehaviour {
    [SerializeField] int _AmmoValue = 0;
    [SerializeField] float _destroyTime = 0.0f;

    void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) { return; }
        if (other.TryGetComponent<Ammo>(out Ammo Ammo)) {
            Ammo.AmmoUpdatePistol(_AmmoValue);
        }
        if (!gameObject.CompareTag("Pickup")) { return; }

        Destroy(gameObject, _destroyTime);
    }
}