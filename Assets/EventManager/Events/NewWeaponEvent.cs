using UnityEngine;

public class NewWeaponEvent
{
    public WeaponBase NewWeapon { get; }

    public NewWeaponEvent(WeaponBase newWeapon)
    {
        NewWeapon = newWeapon;
    }
}
