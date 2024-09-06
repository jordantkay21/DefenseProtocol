using UnityEngine;

public class BulletImpactAutoDestroy : MonoBehaviour
{
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
