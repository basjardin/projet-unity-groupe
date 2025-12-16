using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Tooltip("Amount of damage to deal")]
    public float damageAmount = 10f;

    [Tooltip("Tags of objects that can be damaged (e.g., 'Player' or 'Enemy')")]
    public string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we hit has the correct tag
        if (other.CompareTag(targetTag))
        {
            // Try to get the HealthSystem component from the object we hit
            HealthSystem health = other.GetComponent<HealthSystem>();

            // If the component exists, deal damage
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    // Optional: Also handle physical collisions if not using Triggers
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
