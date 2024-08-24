using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    [SerializeField] float _bulletSpread = 0.02f;
    [SerializeField] int _bulletsPerShot = 1;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _muzzleTransform;

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

                effectManager?.PlayMuzzleFlash(_muzzleTransform);
                effectManager?.PlayGunfireSound();

                //GameObject bullet = Instantiate(_bulletPrefab, _muzzleTransform.position, _muzzleTransform.rotation);
                //bullet.transform.forward = _muzzleTransform.forward + spread;

                //Rigidbody rb = bullet.GetComponent<Rigidbody>();
                //if (rb != null)
                //    rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

                Vector3 targetPosition = _muzzleTransform.position + _muzzleTransform.forward * 100f;
                effectManager?.PlayBulletTracer(targetPosition);
            }

            HandleAmmoConsumption();
        }
        else
        {
            PlayEmptyClick();
        }
    }

    public override void Reload()
    {
        HandleReloading();
        effectManager?.PlayReloadSound();
    }
}
