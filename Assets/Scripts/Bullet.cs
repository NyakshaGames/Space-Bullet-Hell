using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;   // Speed of the bullet.
    public float lifeTime = 2f;       // Lifetime before the bullet is destroyed.

    private float timer;

    void Start()
    {
        timer = lifeTime;
    }

    void Update()
    {
        // Move the bullet forward in its local space.
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        // Countdown and destroy bullet after its lifetime expires.
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision with enemies (or other objects)
        if (other.CompareTag("Enemy") || other.CompareTag("Alien"))
        {
            // Optionally send a damage message or call a method on the enemy.
            Destroy(gameObject);
        }
    }
}
