using UnityEngine;

public class LaserWeapon : BaseWeapon
{
    [Header("Laser Weapon Settings")]
    public GameObject bulletPrefab;      // Kinematic bullet prefab.
    public Transform bulletSpawnPoint;   // The spawn point for bullets.

    public override void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}
