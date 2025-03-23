using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [Space]
    [SerializeField] private BoostSpawner boostSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerShooter playerShooter;

    [Header("Game State")]
    [Space]
    [SerializeField] private bool isGameOver = false;



    [SerializeField] private float currentShotsPerSecond;
    [SerializeField] private float currentProjectileDamage;
    [SerializeField] private float currentProjectileSpeed;

    private float boostTimer;
    private float currentBoostTimer;


    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!isGameOver && playerHealth.CurrentHealth <= 0)
        {
            GameOver();
        }
    }

    private void StartGame()
    {
        isGameOver = false;
        // Debug.Log("Game Started!");

        // Example: Start spawning enemies and boosts
        boostSpawner?.SpawnBoost();
        enemySpawner?.SpawnEnemy();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");

        // Stop enemy and boost spawning
        if (boostSpawner != null) boostSpawner.StopSpawnBoost();
        if (enemySpawner != null) enemySpawner.StopSpawnEnemy();

        // Disable player shooting
        if (playerShooter != null) playerShooter.enabled = false;
    }

    private void StrengthBoost()
    {

    }

    public void SpeedBoost()
    {

    }
}
