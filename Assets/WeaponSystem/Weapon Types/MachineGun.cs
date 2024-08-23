using UnityEngine;

public class MachineGun : WeaponBase
{
    [Header("Machine Gun Settings")]
    [SerializeField] float _bulletSpread = 0.05f;
    [SerializeField] int _bulletsPerShot = 1;
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform muzzleTransform;

    private float nextFireTime;

    public override void Fire()
    {
        if(CanFire() && Time.time >= nextFireTime)
        {
            for (int i = 0; i < _bulletsPerShot; i++)
            {
                Vector3 spread = new Vector3(
                    Random.Range(-_bulletSpread, _bulletSpread),
                    Random.Range(-_bulletSpread, _bulletSpread),
                    0);

                GameObject bullet = Instantiate(_bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                bullet.transform.forward = muzzleTransform.forward + spread;

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
