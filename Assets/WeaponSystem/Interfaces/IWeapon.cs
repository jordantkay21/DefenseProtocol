using UnityEngine;

public interface IWeapon 
{
    /// <summary>
    /// Handles the logic for firing the weapon. Called whenever the player activates the weapon to shoot.
    /// </summary>
    void Fire();
    /// <summary>
    /// Manages the reloading process. Called whenver the player reloads teh weapon.
    /// </summary>
    void Reload();
    /// <summary>
    /// Checks whether the weapon is ready to fire. Used to prevent firing when the weapon is not ready.
    /// </summary>
    void CanFire();
}
