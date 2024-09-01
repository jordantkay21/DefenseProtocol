using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon, IInteractable
{
    [Header("Weapon Configuration")]
    public WeaponType weaponType;
    public Vector3 weaponMountOffset;
    public Transform muzzleTransform;
    public bool showRaycast = true;

    //Shared Ammo Management
    [Header("Ammo Settings")]
    [SerializeField] protected BulletBase bulletPrefab;
    [SerializeField] protected float bulletSpread;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int reserveAmmo;

    //Shared Cooldown Timers
    [Header("Cooldown Settings")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected float reloadTime;

    protected bool isReloading = false;
    protected float nextTimeToFire = 0f;
    protected bool isFiring;

    protected EventManager weaponEvents;
    protected Ray weaponRaycast;
    protected RaycastHit hitInfo;

    protected void Awake()
    {
        weaponEvents = GameManager.Instance.WeaponEvents;
    }
    public void StartFiring()
    {
        isFiring = true;
    }
    public void StopFiring()
    {
        isFiring = false;
    }

    protected Vector3 CalculateSpread()
    {
        Vector3 spread = new(
                     UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                     UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                     0);

        return spread;
    }

    /// <summary>
    /// Defines how the weapon fires its projectiles or performs its attack
    /// </summary>
    public abstract void Fire();
    /// <summary>
    /// Defines how the weapon reloads its ammunition
    /// </summary>
    public abstract void Reload();
    /// <summary>
    /// Checks whether the weapon is able to fire. 
    /// </summary>
    /// <returns></returns>
    public bool CanFire()
    {
        //Returns 'true' if the weapon is not reloading, has ammunition, and the cooldown period since the last shot has elapsed
        return !isReloading && currentAmmo > 0 && Time.time >= nextTimeToFire;
    }
   


    /// <summary>
    /// Handles the consumption of ammo when the weapon is fired. 
    /// It decrements the 'currentAmmo' by one and updates the 'nextTimeToFire' based on the weapon's fire rate.
    /// </summary>
    protected void HandleAmmoConsumption()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }
    /// <summary>
    /// Manages the reloading process.
    /// If the weapon has reserve ammo and is not fully loaded, 
    /// it triggers the reloading process by setting 'isReloading' to 'true' 
    /// and invoking the 'CompleteReloading' method after the reload time.
    /// </summary>
    protected void HandleReloading()
    {
        if (reserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            isReloading = true;
            Invoke(nameof(CompleteReloading), reloadTime); //Simulate reloading time
        }
    }
    /// <summary>
    /// Finalizes the reloading process. It calculates the amount of ammo needed
    /// and loads it from the reserves, updating 'currentAmmo' and 'reserveAmmo'.
    /// It also resets the 'isReloading' flag.
    /// </summary>
    private void CompleteReloading()
    {
        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToLoad = Mathf.Min(ammoNeeded, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;
    }
    


    /// <summary>
    /// Implements the interaction logic, allowing the player to pick up the weapon
    /// </summary> 
    public void Interact()
    {
        WeaponManager.Instance.PickupWeapon(this);
    }
    /// <summary>
    /// Updates the weapon's raycast, which is used for aiming and detecting hits.
    /// </summary>
    public void UpdateWeaponRaycast()
    {
        weaponRaycast = Raycasthandler.Instance.CreateRay(muzzleTransform.position, GameManager.Instance.GetCrosshairTarget().transform.position);
    }
}
