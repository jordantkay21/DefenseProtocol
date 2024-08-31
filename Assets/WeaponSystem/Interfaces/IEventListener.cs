using UnityEngine;

public interface IEventListener <TEvent>
{
    void OnNewWeaponEquipped(TEvent eventArgs);
}
