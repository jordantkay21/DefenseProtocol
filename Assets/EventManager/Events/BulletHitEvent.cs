using UnityEngine;

public class BulletHitEvent
{
    public Vector3 HitPoint { get; private set; }

    public BulletHitEvent() { }
    public BulletHitEvent(BulletBase bullet, Vector3 hitPoint) { HitPoint = hitPoint; }
}
