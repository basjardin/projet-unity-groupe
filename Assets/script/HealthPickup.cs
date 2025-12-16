using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Amount of health to restore")]
    public float healAmount = 25f;

    [Tooltip("Effect to spawn when picked up (optional)")]
    public GameObject pickupEffect;

    [Tooltip("Tag of the player")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is the player
        if (other.CompareTag(playerTag))
        {
            // Try to get the HealthSystem
            HealthSystem health = other.GetComponent<HealthSystem>();

            // Heal ONLY if the script is found AND health is not full
            if (health != null && health.CurrentHealth < 100f) // Note: ideally we check maxHealth from property but 100f is safe default check or we just call Heal and let it clamp
            {
                health.Heal(healAmount);

                // Optional: Spawn particle effect
                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }

                // Destroy the potion/medkit object
                Destroy(gameObject);
            }
        }
    }
}
