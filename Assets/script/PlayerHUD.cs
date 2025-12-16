using UnityEngine;
using UnityEngine.UI; // Nécessaire pour toucher à l'interface (Slider, Text, Images)

public class PlayerHUD : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Glisse ici le slider de ta barre de vie")]
    public Slider healthBar;
    
    [Tooltip("Glisse ici le joueur (ou laisse vide pour qu'il le trouve tout seul)")]
    public HealthSystem playerHealth;

    void Start()
    {
        // Si on a oublié de mettre le joueur, on le cherche par son Tag
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<HealthSystem>();
            }
            else
            {
                Debug.LogWarning("PlayerHUD : Impossible de trouver le joueur !");
            }
        }
    }

    void Update()
    {
        // Met à jour la barre de vie à chaque image
        if (playerHealth != null && healthBar != null)
        {
            healthBar.value = playerHealth.CurrentHealth;
        }
    }
}
