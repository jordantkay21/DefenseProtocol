using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem bulletTracer;

    [Header("Audio Effects")]
    [SerializeField] AudioClip gunfireSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip emptyClipSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayMuzzleFlash(Transform muzzleTransform)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }

    public void PlayBulletTracer(Vector3 targetPosition)
    {
        if(bulletTracer != null)
        {
            bulletTracer.transform.LookAt(targetPosition);
            bulletTracer.Play();
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
