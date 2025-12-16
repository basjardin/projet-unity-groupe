// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class infoLikePV : MonoBehaviour
// {
//     public float currentHealth = 100f;
//     void Start()
//     {}

//     public void TakeDamage(float amount)
//     {
//         currentHealth -= amount;
//         Debug.Log("Joueur touché ! HP restant : " + currentHealth);

//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void Die()
//     {
//         Debug.Log("Le joueur est mort !");
//         // Ici tu peux ajouter un GameOver, respawn, etc.
//     }
// }