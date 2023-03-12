using System;
using UnityEngine;

public class Ammo : MonoBehaviour {
    [SerializeField] public int AmmoMaximumPistol { get; private set; } = 100;
    [SerializeField] public int AmmoMaximumRifle { get; private set; } = 100;
    [SerializeField] public int AmmoMaximumShotgun {get; private set; } = 100;
    public int AmmoCurrentPistol { get; private set; } = 0;
    public int AmmoCurrentRifle { get; private set; } = 0;
    public int AmmoCurrentShotgun { get; private set; } = 0;

    void Start() {
        AmmoReset();
        UIUpdate();
    }
    public void AmmoUpdatePistol(int update) {
        if (update > 0) {
            AmmoCurrentPistol = Mathf.Min(AmmoCurrentPistol + update, AmmoMaximumPistol);
        }
        else if (update < 0) {
            AmmoCurrentPistol = Mathf.Max(AmmoCurrentPistol + update, 0);
        }
        UIUpdate();
    }
    public void AmmoUpdateRifle(int update) {
        if (update > 0) {
            AmmoCurrentRifle = Mathf.Min(AmmoCurrentRifle + update, AmmoMaximumRifle);
        }
        else if (update < 0) {
            AmmoCurrentRifle = Mathf.Max(AmmoCurrentRifle + update, 0);
        }
        UIUpdate();
    }
    public void AmmoUpdateShotgun(int update) {
        if (update > 0) {
            AmmoCurrentShotgun = Mathf.Min(AmmoCurrentShotgun + update, AmmoMaximumShotgun);
        }
        else if (update < 0) {
            AmmoCurrentShotgun = Mathf.Max(AmmoCurrentShotgun + update, 0);
        }
        UIUpdate();
    }
    public void AmmoReset() {
        AmmoCurrentPistol = 0;
        AmmoCurrentRifle = 0;
        AmmoCurrentShotgun = 0;
    }
    public void UIUpdate() {
        //UIController.Instance.AmmoText.text = AmmoCurrent.ToString();
    }
}