using UnityEngine;

public class Shotgun : WeaponBase
{
    [Header("Shotgun Settings")]
    [SerializeField] int _bulletsPerShot = 8;
    [SerializeField] float _spreadAngle = 15f;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _muzzleTransform;

    public override void Fire()
    {
        if (CanFire())
        {
            for (int i = 0; i < _bulletsPerShot; i++)
            {
                Vector3 spread = Quaternion.Euler(
                    Random.Range(-_spreadAngle, _spreadAngle),
                    Random.Range(-_spreadAngle, _spreadAngle),
                    0) * _muzzleTransform.forward;

                GameObject bullet = Instantiate(_bulletPrefab, _muzzleTransform.position, _muzzleTransform.rotation);
                bullet.transform.forward = spread;

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
            }

            HandleAmmoConsumption();
        }
    }

    public override void Reload()
    {
        HandleReloading();
    }
}
