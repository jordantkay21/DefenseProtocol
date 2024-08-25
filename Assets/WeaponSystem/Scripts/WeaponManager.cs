using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [SerializeField] WeaponBase initialWeapon;

    private WeaponBase equippedWeapon;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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

    public void PickupWeapon(WeaponBase newWeapon)
    {
        if(equippedWeapon != null)
        {
            DropWeapon();
        }

        equippedWeapon = newWeapon;
        //Call Equip Method

        //Call an event once eventManager is configured to notify other systems that a weapon has been picked up
    }

    private void DropWeapon()
    {
        //Logic to drop the equipped weapon
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}
