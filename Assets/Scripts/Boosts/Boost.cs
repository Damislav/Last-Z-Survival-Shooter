using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float boostSpeed = 5f;

    [SerializeField] private float boostHealth;
    [SerializeField] private float maxBoostHealth = 20f;

    [SerializeField] float boostDamage = 20f;

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
        boostHealth -= damage;

        //update ui
        healthUI.UpdateText(boostHealth);

        if (boostHealth <= 0)
        {
            healthUI.UpdateText(boostHealth);
            Destroy(gameObject); // Destroy Boost if health reaches zero

        }
    }


}
