using UnityEngine;

public class MachineGun : WeaponBase
{
    [Header("Machine Gun Settings")]
    [SerializeField] float _range;

    private void Update()
    {
        if(isFiring && CanFire())
        {
            Fire();
        }
    }
    public override void Fire()
    {
        if (CanFire())
        {
            HandleAmmoConsumption();

            Vector3 origin = muzzleTransform.position;
            Vector3 direction = (GameManager.Instance.GetCrosshairTarget().position - origin).normalized + CalculateSpread();
            Vector3 hitPoint = origin + direction * _range; //default point if no hit occurs

            if (Physics.Raycast(origin, direction, out RaycastHit hit, _range))
            {
                hitPoint = hit.point;

                //Instantiate and initialize the bullet
                BulletBase newBullet = Instantiate(bulletPrefab, origin, Quaternion.LookRotation(direction));
                newBullet.Initialize(bulletSpeed, bulletDamage);
                //Move the bullet to the hit point
                newBullet.MoveBulletToHitPoint(hitPoint);
                //Handle the hit logic within the bullet class
                newBullet.OnHit(hit);
            }
            else
            {
                // no hit occured, but bullet should still move to a distant point
                BulletBase newBullet = Instantiate(bulletPrefab, origin, Quaternion.LookRotation(direction));
                newBullet.Initialize(bulletSpeed, bulletDamage);
                newBullet.MoveBulletToHitPoint(hitPoint);
            }
        }
    }

    public override void Reload()
    {
        HandleReloading();
    }
}
