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

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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
