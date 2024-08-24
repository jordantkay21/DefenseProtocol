using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponBase initialWeapon;

    private WeaponBase equippedWeapon;

    private void OnEnable()
    {
        InputManager.Instance.OnFire += HandleFire;
        InputManager.Instance.OnReload += HandleReload;
    }

    private void Start()
    {
        equippedWeapon = initialWeapon;   
    }

    private void HandleFire()
    {
        equippedWeapon?.Fire();
    }

    private void HandleReload()
    {
        equippedWeapon?.Reload();
    }
}
