using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon, IInteractable
{
    [Header("Weapon Configuration")]
    public Vector3 weaponMountOffset;
    public Transform muzzleTransform;
    public Transform crosshairTarget;
    public bool showRaycast = true;

    //Shared Ammo Management
    [Header("Ammo Settings")]
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int reserveAmmo;

    //Shared Cooldown Timers
    [Header("Cooldown Settings")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected float reloadTime;

    protected bool isReloading = false;
    protected float nextTimeToFire = 0f;

    protected WeaponEffect effectManager;
    protected Raycasthandler raycastHandler;

    void Awake()
    {
        effectManager = GetComponent<WeaponEffect>();
        raycastHandler = GetComponent<Raycasthandler>();
    }

    public abstract void Fire();
    public abstract void Reload();
    
    public bool CanFire()
    {
        return !isReloading && currentAmmo > 0 && Time.time >= nextTimeToFire;
    }

    protected void HandleAmmoConsumption()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    protected void HandleReloading()
    {
        if (reserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            isReloading = true;
            Invoke("CompleteReloading", reloadTime); //Simulate reloading time
        }
    }

    private void CompleteReloading()
    {
        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToLoad = Mathf.Min(ammoNeeded, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;
    }

    public void Interact()
    {
        WeaponManager.Instance.PickupWeapon(this);
    }

    public void UpdateWeaponRaycast()
    {
        raycastHandler.CreateRay(muzzleTransform.position, WeaponManager.Instance.GetCrosshairTarget().transform.position);
    }
}
