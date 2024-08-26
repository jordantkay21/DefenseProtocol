using UnityEngine;

public class MachineGun : WeaponBase
{
    [Header("Machine Gun Settings")]
    [SerializeField] float _bulletSpread = 0.05f;
    [SerializeField] int _bulletsPerShot = 1;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform muzzleTransform;

    private bool isFiring = false;

    private void Update()
    {
        //Handle continous fire if the fire button is held down
        if (isFiring && CanFire())
            Fire();
    }
    public override void Fire()
    {
        if(CanFire())
        {
            for (int i = 0; i < _bulletsPerShot; i++)
            {
                //Calculate bullet spread
                Vector3 spread = new Vector3(
                    Random.Range(-_bulletSpread, _bulletSpread),
                    Random.Range(-_bulletSpread, _bulletSpread),
                    0);

                effectManager?.PlayMuzzleFlash(muzzleTransform);
                effectManager?.PlayGunfireSound();

                GameObject bullet = Instantiate(_bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                bullet.transform.forward = muzzleTransform.forward + spread;

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
            }

            HandleAmmoConsumption();
        }
        else
        {
            effectManager?.PlayEmptyClickSound();
        }
    }

    public override void Reload()
    {
        HandleReloading();
        effectManager?.PlayReloadSound();
    }

    /// <summary>
    /// Start firing when the fire button is pressed
    /// </summary>
    public void StartFiring()
    {
        isFiring = true;
    }

    /// <summary>
    /// Stop firing when the fire button is released
    /// </summary>
    public void StopFiring()
    {
        isFiring = false;
    }
}
