using UnityEngine;

public class SpreadShotWeapon : BaseWeapon
{
    [Header("Spread Shot Settings")]
    public GameObject bulletPrefab;      // Kinematic bullet prefab.
    public Transform bulletSpawnPoint;   // The spawn point for bullets.
    public int bulletCount = 5;          // Number of bullets per shot.
    public float spreadAngle = 30f;      // Total spread angle in degrees.

    public override void Shoot()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null)
            return;

        float angleStep = bulletCount > 1 ? spreadAngle / (bulletCount - 1) : 0f;
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            // Rotate the spawn point's rotation by the current angle around the Y-axis.
            Quaternion bulletRotation = bulletSpawnPoint.rotation * Quaternion.Euler(0, currentAngle, 0);
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
        }
    }
}
