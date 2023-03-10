using System;
using UnityEngine;

public class Ammo : MonoBehaviour {
    [SerializeField] int _AmmoMaximumPistol = 100;
    [SerializeField] int _AmmoMaximumRifle = 100;
    [SerializeField] int _AmmoMaximumShotgun = 100;
    int _AmmoCurrentPistol = 0;
    int _AmmoCurrentRifle = 0;
    int _AmmoCurrentShotgun = 0;

    void Start() {
        AmmoReset();
        UIUpdate();
    }
    public void AmmoUpdatePistol(int update) {
        if (update > 0) {
            _AmmoCurrentPistol = Mathf.Min(_AmmoCurrentPistol + update, _AmmoMaximumPistol);
        }
        else if (update < 0) {
            _AmmoCurrentPistol = Mathf.Max(_AmmoCurrentPistol + update, 0);
        }
        UIUpdate();
    }
    public void AmmoUpdateRifle(int update) {
        if (update > 0) {
            _AmmoCurrentRifle = Mathf.Min(_AmmoCurrentRifle + update, _AmmoMaximumRifle);
        }
        else if (update < 0) {
            _AmmoCurrentRifle = Mathf.Max(_AmmoCurrentRifle + update, 0);
        }
        UIUpdate();
    }
    public void AmmoUpdateShotgun(int update) {
        if (update > 0) {
            _AmmoCurrentShotgun = Mathf.Min(_AmmoCurrentShotgun + update, _AmmoMaximumShotgun);
        }
        else if (update < 0) {
            _AmmoCurrentShotgun = Mathf.Max(_AmmoCurrentShotgun + update, 0);
        }
        UIUpdate();
    }
    public void AmmoReset() {
        _AmmoCurrentPistol = 0;
        _AmmoCurrentRifle = 0;
        _AmmoCurrentShotgun = 0;
    }
    public void UIUpdate() {
        //UIController.Instance._AmmoText.text = _AmmoCurrent.ToString();
    }
}