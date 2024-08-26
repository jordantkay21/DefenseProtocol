using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    [SerializeField] float _bulletSpread = 0.02f;
    [SerializeField] int _bulletsPerShot = 1;
    [SerializeField] GameObject _bulletPrefab;

    public override void Fire()
    {
        if (CanFire())
        {
            for (int i =0; i<_bulletsPerShot; i++)
            {
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
}
