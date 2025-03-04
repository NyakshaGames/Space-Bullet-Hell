using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [Header("Weapon Base Settings")]
    public float shootCooldown = 0.25f; // Time between shots.
    protected float shootTimer;

    protected virtual void Start()
    {
        shootTimer = shootCooldown; // Fire immediately at start.
    }

    protected virtual void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    // Derived classes must implement this to define their firing behavior.
    public abstract void Shoot();
}
