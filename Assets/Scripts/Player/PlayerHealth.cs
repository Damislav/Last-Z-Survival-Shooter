using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float CurrentHealth { get; private set; }

    private float maxPlayerHealth = 100f;

    private void Start()
    {
        CurrentHealth = maxPlayerHealth;
    }

    public void DamagePlayer(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
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