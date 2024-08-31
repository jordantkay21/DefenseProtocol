using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [SerializeField] WeaponBase initialWeapon;
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

        InputManager.Instance.InputActions.Player.Fire.performed += ctx => StartFiring();
        InputManager.Instance.InputActions.Player.Fire.canceled += ctx => StopFiring();
    }

    private void Start()
    {
        equippedWeapon = initialWeapon;   
    }

    private void Update()
    {
        if (equippedWeapon != null && equippedWeapon.showRaycast)
        {
            equippedWeapon.UpdateWeaponRaycast();
        }
    
    }

    public Transform GetCrosshairTarget()
    {
        return crosshairTarget;
    }

    private void StartFiring()
    {
        if (equippedWeapon is MachineGun machineGun)
            machineGun.StartFiring();
        else
            HandleFire();
    }

    private void HandleFire()
    {
        equippedWeapon?.Fire();
    }

    private void StopFiring()
    {
        if (equippedWeapon is MachineGun machineGun)
            machineGun.StopFiring();
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
        weaponHolder.EquipWeapon(equippedWeapon);

        //Call an event once eventManager is configured to notify other systems that a weapon has been picked up
        weaponEvents.Publish(new NewWeaponEvent(equippedWeapon));
    }

    private void DropWeapon()
    {
        //Logic to drop the equipped weapon
        weaponHolder.UnequipWeapon();
        equippedWeapon = null;
    }
}
