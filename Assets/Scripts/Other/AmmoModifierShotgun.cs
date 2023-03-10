using UnityEngine;

public class AmmoModifierShotgun : MonoBehaviour {
    [SerializeField] int _AmmoValue = 0;
    [SerializeField] float _destroyTime = 0.0f;

    void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) { return; }
        if (other.TryGetComponent<Ammo>(out Ammo Ammo)) {
            Ammo.AmmoUpdateShotgun(_AmmoValue);
        }
        if (!gameObject.CompareTag("Pickup")) { return; }

        Destroy(gameObject, _destroyTime);
    }
}