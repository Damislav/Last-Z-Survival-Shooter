using System.Collections;
using UnityEngine;


public class Boost : MonoBehaviour
{
    [SerializeField]
    public enum BoostType { None, Speed, Strength, }

    [SerializeField] private BoostType boostType; // ✅ Will appear in 


    [Header("Boost Player Stats")]
    [Space]
    [SerializeField] private float boostPlayerSpeed = 5f;
    [SerializeField] private float boostPlayerShootSpeed;
    [SerializeField] private float boostPlayerShotsPerSecond = 5f;
    [SerializeField] private float boostPlayerShootDamage;


    [Header("Boost own stats")]
    [Space]
    [SerializeField] private float boostSpeed = 5f;
    [SerializeField] private float boostHealth;
    [SerializeField] private float maxBoostHealth = 20f;
    [SerializeField] private float boostDamage = 20f;
    [SerializeField] private float boostDuration;

    [Space]
    [SerializeField] HealthUI healthUI;




    void Awake()
    {
        healthUI = GetComponent<HealthUI>();
    }

    private void Start()
    {
        boostHealth = maxBoostHealth;
        healthUI.UpdateText(boostHealth);
    }

    private void Update()
    {
        BoostLifeHealth();
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += Vector3.back * boostSpeed * Time.deltaTime;
    }

    private void BoostLifeHealth()
    {
        if (boostHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Checking if the other object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has hit me");

            // Get the PlayerHealth component
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Ensure the PlayerHealth component is found before applying damage
            if (playerHealth != null)
            {
                Debug.Log($"Im taking damage");
                playerHealth.DamagePlayer(boostDamage); // Apply the damage
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the player.");
            }
        }
    }

    public void DamageBoost(float damage)
    {
        //damage myself
        boostHealth -= damage;

        //update ui
        healthUI.UpdateText(boostHealth);

        //if boost health is less or equal to 0
        if (boostHealth <= 0)
        {
            //check what type of boost it is
            switch (boostType)
            {
                case BoostType.Speed:
                    SpeedBoost();
                    break;

                case BoostType.Strength:
                    StrengthBoost();
                    break;

                case BoostType.None:
                    Debug.Log($"Generic boost destroyed");
                    healthUI.UpdateText(boostHealth);
                    Destroy(gameObject); // Destroy Boost if health reaches zero
                    break;
            }
        }
    }

    public void SpeedBoost()
    {
        Debug.Log($"Give me Speed boost");

        PlayerController.onChangePlayerSpeed?.Invoke(boostPlayerSpeed, boostDuration);
        PlayerShooter.onShootSpeedChanged?.Invoke(boostPlayerShootSpeed, boostDuration);
        PlayerShooter.onShootProjectileChanged?.Invoke(boostPlayerShotsPerSecond, boostDuration);

        healthUI.UpdateText(boostHealth);
        Destroy(gameObject); // Destroy Boost if health reaches zero
    }


    public void StrengthBoost()
    {
        Debug.Log($"Give me strength boost");
        PlayerShooter.onShootProjectileDamageChanged?.Invoke(boostPlayerShootDamage, boostDuration);
        healthUI.UpdateText(boostHealth);
        Destroy(gameObject); // Destroy Boost if health reaches zero
    }



}
