using System;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    MachineGun
}


public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [SerializeField] Transform crosshairTarget;

    private WeaponBase equippedWeapon;
    private WeaponHolder weaponHolder;
    private EventManager weaponEvents;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            weaponHolder = GetComponent<WeaponHolder>();
            weaponEvents = GameManager.Instance.WeaponEvents;
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
        InputManager.Instance.OnFireStarted += StartFiring;
        InputManager.Instance.OnFireStopped += StopFiring;
    }

    private void StopFiring()
    {
        if (equippedWeapon.weaponType == WeaponType.MachineGun)
            equippedWeapon?.StopFiring();
    }

    private void StartFiring()
    {
        if (equippedWeapon.weaponType == WeaponType.MachineGun)
            equippedWeapon?.StartFiring();
    }

    private void OnDisable()
    {
        InputManager.Instance.OnFire -= HandleFire;
        InputManager.Instance.OnReload -= HandleReload;
    }

    private void Update()
    {
        if (equippedWeapon != null && equippedWeapon.showRaycast)
        {
            equippedWeapon.UpdateWeaponRaycast();
        }
    
    }

    private void HandleFire()
    {
        if(equippedWeapon.weaponType != WeaponType.MachineGun)
            equippedWeapon?.Fire();
    }

    private void HandleReload()
    {
        equippedWeapon?.Reload();
    }

    public void PickupWeapon(WeaponBase newWeapon)
    {
        //Unequip current weapon, if applicable
        if(equippedWeapon != null)
        {
            weaponHolder.UnequipWeapon();
        }

        equippedWeapon = newWeapon;
        weaponHolder.EquipWeapon(equippedWeapon);

        //Call an event once eventManager is configured to notify other systems that a weapon has been picked up
        weaponEvents.Publish(new NewWeaponEvent(equippedWeapon));
    }

}
