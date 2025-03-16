using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    private float maxPlayerHealth = 100f;

    private void Start()
    {
        playerHealth = maxPlayerHealth;
    }

    public void DamagePlayer(float amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            // Trigger death (e.g., animations, sounds, etc.)
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        // You can add death-related actions here (animations, sounds, etc.)
        Debug.Log("Player has died.");

        // Destroy the player object after the death process is handled
        Destroy(gameObject);
    }

}