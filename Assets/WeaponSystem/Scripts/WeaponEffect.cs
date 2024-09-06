using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponEffect : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("Audio Effects")]
    [SerializeField] AudioClip gunfireSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip emptyClipSound;

    [Header("Impact Effects")]
    [SerializeField] ParticleSystem impactVis;
    [SerializeField] GameObject sphereImpact;
    [SerializeField] int sphereLifetime;

    //Event Managers
    EventManager weaponEvents;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        weaponEvents = GameManager.Instance.WeaponEvents;
        weaponEvents.Subscribe<BulletHitEvent>(ImpactEffect);
    }

    private void OnDisable()
    {
        weaponEvents.Unsubscribe<BulletHitEvent>(ImpactEffect);
    }

    private void ImpactEffect(BulletHitEvent obj)
    {
        DebugUtility.Log(DebugTag.Bullet, $"Impact Effect method executed. Location: {obj.HitPoint}");
        Instantiate(sphereImpact, obj.HitPoint, Quaternion.identity);
        //Destroy(sphere, sphereLifetime);

    }

    public void PlayMuzzleFlash(Transform muzzleTransform)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }

    public void PlayGunfireSound()
    {
        if(gunfireSound != null)
        {
            audioSource.PlayOneShot(gunfireSound);
        }
    }

    public void PlayReloadSound()
    {
        if(reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
    }

    public void PlayEmptyClickSound()
    {
        if (emptyClipSound != null)
        {
            audioSource.PlayOneShot(emptyClipSound);
        }
    }


}
