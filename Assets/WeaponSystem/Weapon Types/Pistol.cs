using System;
using System.Collections;
using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    [SerializeField] float _range;
    [SerializeField] int _bulletsPerShot = 1;

    public override void Fire()
    {
        if (CanFire())
        {
            HandleAmmoConsumption();

            Vector3 origin = muzzleTransform.position;
            Vector3 direction = (GameManager.Instance.GetCrosshairTarget().position - origin).normalized + CalculateSpread();
            RaycastHit hit;
            Vector3 hitPoint = origin + direction * _range; //default point if no hit occurs

            if (Physics.Raycast(origin, direction, out hit, _range))
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
