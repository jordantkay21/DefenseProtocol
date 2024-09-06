using System;
using System.Collections;
using UnityEngine;
public abstract class BulletBase : MonoBehaviour
{
    private float _speed;
    private int _damage;

    public float distance;
    public float time;
    public float elapsedTime;

    [SerializeField] TrailRenderer trailRenderer;

    protected EventManager weaponManager;

    public virtual void Initialize(float speed, int damage)
    {
        weaponManager = GameManager.Instance.WeaponEvents;
        _speed = speed;
        _damage = damage;
    }

    public virtual void OnHit(RaycastHit hit)
    {
        //Call OnHit Event
        DebugUtility.Log(DebugTag.Bullet, $"Bullet Hit: {hit.collider.name} @ {hit.point}");
        weaponManager.Publish<BulletHitEvent>(new BulletHitEvent(this, hit.point));
    }

    public void MoveBulletToHitPoint(Vector3 hitPoint)
    {
        StartCoroutine(MoveBulletToHitPointRoutine(hitPoint));
    }

    private IEnumerator MoveBulletToHitPointRoutine(Vector3 hitPoint)
    {
        Debug.Log($"transform position = {transform.position} | hitPoint position = {hitPoint}");
        distance = Vector3.Distance(transform.position, hitPoint);
        time = distance / _speed;
        elapsedTime = 0f;
        DebugUtility.Log(DebugTag.Bullet, $"Bullet Movement Configuration: Distance - {distance} | Time - {time}");

        Vector3 startPos = transform.position;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPos, hitPoint, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = hitPoint;
        Destroy(gameObject);
    }
}
