using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("Missile Settings")]
    public float speed = 20f;               // Forward speed of the missile.
    public float rotationSpeed = 5f;        // How fast the missile rotates toward its target.
    public float detectionRadius = 30f;     // Radius in which the missile searches for an enemy.
    public float lifetime = 10f;            // Maximum lifetime before self-destruction.
    public float hitDistanceThreshold = 1f; // Distance at which missile considers it has "hit" the enemy.

    private Transform target;               // The current enemy target.

    void Start()
    {
        // Destroy the missile after its lifetime expires.
        Destroy(gameObject, lifetime);
        // Attempt to find an initial target.
        FindTarget();
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            // Compute the direction toward the target.
            Vector3 direction = (target.position - transform.position).normalized;
            // Smoothly rotate toward the target.
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Optionally, if you want the missile to "hit" when very close:
            if (Vector3.Distance(transform.position, target.position) < hitDistanceThreshold)
            {
                Explode();
            }
        }

        // Move forward continuously.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Find the nearest enemy tagged "Enemy".
    void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        float minDistance = Mathf.Infinity;
        Transform nearest = null;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = hit.transform;
                }
            }
        }
        target = nearest;
    }

    // Called when the missile "hits" its target.
    void Explode()
    {
        // You can add explosion effects here.
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the missile collides with an enemy, explode.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    // Optional: Visualize the detection radius in the editor.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
