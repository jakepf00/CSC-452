

using System;
using UnityEngine;

public class Health : MonoBehaviour {
    public event Action DamageEvent;
    public event Action DeathEvent;
    [SerializeField] int _healthMaximum = 100;
    int _healthCurrent = 0;

    public bool IsDead() => _healthCurrent == 0;
    void Start() {
        HealthReset();
        if (gameObject.CompareTag("Player")) {
            //UIController.Instance._healthSlider.maxValue = _healthMaximum;
            //UIUpdate();
        }
    }
    public void HealthUpdate(int update) {
        if (_healthCurrent == 0) {
            return;
        }
        if (update > 0) {
            _healthCurrent = Mathf.Min(_healthCurrent + update, _healthMaximum);
        }
        else if (update < 0) {
            _healthCurrent = Mathf.Max(_healthCurrent + update, 0);
            DamageEvent?.Invoke();
            if (IsDead()) DeathEvent?.Invoke();
        }
        if (gameObject.CompareTag("Player")) {
            //UIUpdate();
        }
    }
    public void TakeDamage(int damage) {
        if (damage < 0 || _healthCurrent == 0) return;
        _healthCurrent = Mathf.Max(_healthCurrent - damage, 0);
        DamageEvent?.Invoke();
        if (IsDead()) DeathEvent?.Invoke();
        if (gameObject.CompareTag("Player")) {
            //UIUpdate();
        }
    }
    //public void UIUpdate() {
    //    UIController.Instance._healthText.text = _healthCurrent.ToString();
    //    UIController.Instance._healthSlider.value = _healthCurrent;
    //}
    public void HealthReset() {
        _healthCurrent = _healthMaximum;
    }
}