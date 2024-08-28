using System;
using System.Collections;
using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    [SerializeField] float _bulletSpread = 0.02f;
    [SerializeField] int _bulletsPerShot = 1;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float bulletTravelTime = 0.1f; //Time it takes for the bullet to reach destination

    public override void Fire()
    {
        if (CanFire())
        {
            for (int i =0; i<_bulletsPerShot; i++)
            {
               Vector3 spread = new Vector3(
                    UnityEngine.Random.Range(-_bulletSpread, _bulletSpread),
                    UnityEngine.Random.Range(-_bulletSpread, _bulletSpread),
                    0);

                effectManager?.PlayMuzzleFlash(muzzleTransform);
                effectManager?.PlayGunfireSound();

                GameObject bullet = Instantiate(_bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);

                if (Physics.Raycast(weaponRaycast.origin, weaponRaycast.direction, out hitInfo))
                {
                    StartCoroutine(SimulateBullet(bullet, hitInfo.point + spread));
                }
                else
                {
                    StartCoroutine(SimulateBullet(bullet, WeaponManager.Instance.GetCrosshairTarget().position + spread));
                }

                //Rigidbody rb = bullet.GetComponent<Rigidbody>();
                //if (rb != null)
                //    rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            }

            HandleAmmoConsumption();
        }
        else
        {
            effectManager?.PlayEmptyClickSound();
        }
    }

    private IEnumerator SimulateBullet(GameObject bullet, Vector3 targetPoint)
    {
        float elapsedTime = 0f;
        Vector3 startPos = bullet.transform.position;

        while (elapsedTime < bulletTravelTime)
        {
            elapsedTime += Time.deltaTime;
            bullet.transform.position = Vector3.Lerp(startPos, targetPoint, elapsedTime/bulletTravelTime);
            yield return null;
        }

        //Ensure the bullet reaches the exact target point
        bullet.transform.position = targetPoint;

        //Handle bullet impact (destroy the bullet, play impact effects, etc.)
        HandleBulletImpact(bullet, targetPoint);

    }

    private void HandleBulletImpact(GameObject bullet, Vector3 impactPoint)
    {
        //Example: Destroy the bullet after it reaches the target
        Destroy(bullet);

        //Play impact effects here if needed
        Debug.Log($"Bullet hit at: {impactPoint}");
    }

    public override void Reload()
    {
        HandleReloading();
        effectManager?.PlayReloadSound();
    }
}
