using UnityEngine;

public class ChargedBeam : MonoBehaviour
{
    [Header("Beam Settings")]
    public float beamDuration = 0.5f;       // How long the beam lasts (in seconds).
    public float damagePerSecond = 50f;     // Damage dealt per second to enemies inside the beam.

    private float timer;

    void Start()
    {
        timer = beamDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    // Called every frame for every collider that is touching the beam's trigger.
    private void OnTriggerStay(Collider other)
    {
        // Check if the object is tagged as "Enemy".
        if (other.CompareTag("Enemy"))
        {
            // For now, simply log that an enemy is being hit.
            Debug.Log("ChargedBeam is hitting an enemy: " + other.name);

            // When you implement enemy health, you can replace the above with:
            // Health enemyHealth = other.GetComponent<Health>();
            // if (enemyHealth != null)
            // {
            //     enemyHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            // }
        }
    }
}
