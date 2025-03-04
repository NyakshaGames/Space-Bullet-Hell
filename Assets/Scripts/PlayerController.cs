using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Define available weapon types.
    public enum WeaponType
    {
        Laser,
        SpreadShot,
        HomingMissile,
        ChargedBeam,
        RicochetBullet,
        MultiDirectionalBurst,
        ClusterBomb,
        AuxiliaryTurret
    }

    #region Public Variables

    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Movement speed in world units per second.
    public WeaponType currentWeapon = WeaponType.Laser;

    [Header("Shooting Settings")]
    public GameObject laserBulletPrefab;
    public GameObject spreadBulletPrefab;
    public GameObject homingMissilePrefab;
    public GameObject chargedBeamPrefab;
    public GameObject ricochetBulletPrefab;
    public GameObject multiDirectionalBulletPrefab;
    public GameObject clusterBombPrefab;
    public GameObject auxiliaryTurretPrefab;

    // For ChargedBeam: how long the beam lasts.
    public float chargedBeamDuration = 0.5f;

    // Common bullet spawn point.
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f; // (Optional: if needed by bullet scripts.)

    // Separate cooldown timers for each weapon type.
    [Header("Weapon Cooldowns")]
    public float laserCooldown = 0.25f;
    public float spreadShotCooldown = 0.5f;
    public float homingMissileCooldown = 5f;
    public float chargedBeamCooldown = 1f;
    public float ricochetBulletCooldown = 0.3f;
    public float multiDirectionalBurstCooldown = 1.5f;
    public float clusterBombCooldown = 2f;
    public float auxiliaryTurretCooldown = 4f;

    #endregion

    #region Private Variables

    private float shootTimer;
    private PlayerRotation playerRotation; // Reference for movement relative to mouse aiming.

    #endregion

    #region Unity Methods

    void Start()
    {
        InitializePlayer();
    }

    void Update()
    {
        HandleMovement();
        HandleWeaponSwitch();
        HandleShooting();
    }

    #endregion

    #region Initialization

    void InitializePlayer()
    {
        // Initialize shoot timer using the current weapon's cooldown.
        shootTimer = GetCurrentCooldown();
        playerRotation = GetComponent<PlayerRotation>();
    }

    #endregion

    #region Movement

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;

        // When using mouse aiming, move relative to the player's current facing direction.
        if (playerRotation != null && playerRotation.useMouseAiming)
        {
            // Combine forward/backward and right/left relative to the player's rotation.
            movement = (transform.forward * v + transform.right * h).normalized;
        }
        else
        {
            // Otherwise, use world-space axes for movement.
            movement = new Vector3(h, 0, v).normalized;
        }

        // Update position using transform.Translate.
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    #endregion

    #region Weapon Switching

    void HandleWeaponSwitch()
    {
        // Switch weapon based on key press (for testing).
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentWeapon = WeaponType.Laser;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentWeapon = WeaponType.SpreadShot;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentWeapon = WeaponType.HomingMissile;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            currentWeapon = WeaponType.ChargedBeam;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            currentWeapon = WeaponType.RicochetBullet;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            currentWeapon = WeaponType.MultiDirectionalBurst;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            currentWeapon = WeaponType.ClusterBomb;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            currentWeapon = WeaponType.AuxiliaryTurret;
    }

    #endregion

    #region Shooting

    void HandleShooting()
    {
        // Decrement shoot timer.
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            Shoot();
            // Reset timer based on the current weapon's specific cooldown.
            shootTimer = GetCurrentCooldown();
        }
    }

    void Shoot()
    {
        // Delegate shooting behavior based on current weapon.
        switch (currentWeapon)
        {
            case WeaponType.Laser:
                ShootLaser();
                break;
            case WeaponType.SpreadShot:
                ShootSpreadShot();
                break;
            case WeaponType.HomingMissile:
                ShootHomingMissile();
                break;
            case WeaponType.ChargedBeam:
                ShootChargedBeam();
                break;
            case WeaponType.RicochetBullet:
                ShootRicochetBullet();
                break;
            case WeaponType.MultiDirectionalBurst:
                ShootMultiDirectionalBurst();
                break;
            case WeaponType.ClusterBomb:
                ShootClusterBomb();
                break;
            case WeaponType.AuxiliaryTurret:
                DeployAuxiliaryTurret();
                break;
        }
    }

    // Returns the cooldown value for the current weapon.
    float GetCurrentCooldown()
    {
        switch (currentWeapon)
        {
            case WeaponType.Laser:
                return laserCooldown;
            case WeaponType.SpreadShot:
                return spreadShotCooldown;
            case WeaponType.HomingMissile:
                return homingMissileCooldown;
            case WeaponType.ChargedBeam:
                return chargedBeamCooldown;
            case WeaponType.RicochetBullet:
                return ricochetBulletCooldown;
            case WeaponType.MultiDirectionalBurst:
                return multiDirectionalBurstCooldown;
            case WeaponType.ClusterBomb:
                return clusterBombCooldown;
            case WeaponType.AuxiliaryTurret:
                return auxiliaryTurretCooldown;
            default:
                return laserCooldown;
        }
    }

    // Standard Laser: fire a single bullet.
    void ShootLaser()
    {
        if (laserBulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(laserBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    // Spread Shot: fire multiple bullets in a cone.
    void ShootSpreadShot()
    {
        int bulletCount = 5;
        float spreadAngle = 30f; // Total spread angle in degrees.
        float angleStep = (bulletCount > 1) ? spreadAngle / (bulletCount - 1) : 0;
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            Quaternion bulletRotation = bulletSpawnPoint.rotation * Quaternion.Euler(0, currentAngle, 0);
            if (spreadBulletPrefab != null)
                Instantiate(spreadBulletPrefab, bulletSpawnPoint.position, bulletRotation);
        }
    }

    // Homing Missile: instantiate a missile that tracks enemies.
    void ShootHomingMissile()
    {
        if (homingMissilePrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(homingMissilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    // Charged Beam: instantiate a beam that lasts for a short duration.
    void ShootChargedBeam()
    {
        if (chargedBeamPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiate the beam as a child of the bulletSpawnPoint so it follows the player.
            GameObject beam = Instantiate(chargedBeamPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletSpawnPoint);
            // Optionally reset its local position if needed.
            beam.transform.localPosition = Vector3.zero;
            Destroy(beam, chargedBeamDuration);
        }
    }

    // Ricochet Bullet: instantiate a bullet that bounces off surfaces.
    void ShootRicochetBullet()
    {
        if (ricochetBulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(ricochetBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    // Multi-Directional Burst: fire bullets in every direction.
    void ShootMultiDirectionalBurst()
    {
        int burstBulletCount = 12;
        float angleStep = 360f / burstBulletCount;
        for (int i = 0; i < burstBulletCount; i++)
        {
            Quaternion bulletRotation = bulletSpawnPoint.rotation * Quaternion.Euler(0, angleStep * i, 0);
            if (multiDirectionalBulletPrefab != null)
                Instantiate(multiDirectionalBulletPrefab, bulletSpawnPoint.position, bulletRotation);
        }
    }

    // Cluster Bomb: instantiate a bomb that later splits into smaller projectiles.
    void ShootClusterBomb()
    {
        if (clusterBombPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(clusterBombPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    // Auxiliary Turret: deploy a turret that attaches to the player.
    void DeployAuxiliaryTurret()
    {
        if (auxiliaryTurretPrefab != null)
        {
            Instantiate(auxiliaryTurretPrefab, transform.position, transform.rotation, transform);
        }
    }

    #endregion
}
