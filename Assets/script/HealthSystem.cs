using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Maximum health points for this entity")]
    [SerializeField] private float maxHealth = 100f;
    
    // Serialized for debugging in Inspector, but logic should use public property/methods
    [SerializeField] private float currentHealth;

    [Header("Events")]
    [Tooltip("Event triggered when this entity takes damage")]
    public UnityEvent OnTakeDamage;
    
    [Tooltip("Event triggered when health reaches 0")]
    public UnityEvent OnDeath;

    // Property to access current health safely
    public float CurrentHealth => currentHealth;

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Applies damage to the entity.
    /// </summary>
    /// <param name="amount">Amount of damage to deal</param>
    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return; // Already dead

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Current HP: {currentHealth}");
        
        // Trigger generic damage event (useful for simple effects)
        OnTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Restores health to the entity.
    /// </summary>
    /// <param name="amount">Amount of health to restore</param>
    public void Heal(float amount)
    {
        if (currentHealth <= 0) return; // Can't heal dead things usually

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"{gameObject.name} healed {amount}. Current HP: {currentHealth}");
    }

    private void Die()
    {
        currentHealth = 0;
        Debug.Log($"{gameObject.name} has died!");
        
        // Trigger death event
        OnDeath?.Invoke();

        // Optional default behavior: Disable object or Destroy it
        // Destroy(gameObject); // Uncomment if you want immediate destruction
    }
}
