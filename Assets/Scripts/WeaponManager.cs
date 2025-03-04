using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Assign your weapon objects (which have BaseWeapon-derived scripts attached) in the Inspector.
    public BaseWeapon[] weapons;
    private int currentWeaponIndex = 0;

    void Start()
    {
        ActivateWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // For testing, switch weapons with number keys.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            ActivateWeapon(currentWeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            currentWeaponIndex = 1;
            ActivateWeapon(currentWeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            currentWeaponIndex = 2;
            ActivateWeapon(currentWeaponIndex);
        }
        // Additional keys can be added as needed.
    }

    void ActivateWeapon(int index)
    {
        // Loop through all weapons and enable only the selected one.
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].gameObject.SetActive(i == index);
            }
        }
    }
}
