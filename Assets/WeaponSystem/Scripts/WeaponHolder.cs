using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Transform equippedWeaponTransform;   
    
    [SerializeField] WeaponBase equippedWeapon;

    //private void Update()
    //{
    //    if (equippedWeapon != null)
    //        equippedWeaponTransform.rotation = Camera.main.transform.rotation;
    //}

    public void EquipWeapon(WeaponBase newWeapon)
    {
        if (equippedWeapon != null)
            UnequipWeapon();

        equippedWeapon = newWeapon;
        equippedWeapon.showRaycast = true;
        AttachWeapon(newWeapon);
    }

    private void AttachWeapon(WeaponBase weapon)
    {
        //equippedWeaponTransform.transform.position += weapon.weaponMountOffset;

        weapon.transform.SetParent(equippedWeaponTransform);
        weapon.transform.localPosition = weapon.weaponMountOffset;
        weapon.transform.localRotation = Quaternion.identity; //Reset rotation
    }

    public void UnequipWeapon()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.showRaycast = false;
            equippedWeapon.transform.SetParent(null); //Detach from player
            equippedWeapon = null;
        }
    }
}
