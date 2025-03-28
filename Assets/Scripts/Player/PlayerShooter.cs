using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static Action<float, float> onShootSpeedChanged;
    public static Action<float, float> onShootProjectileChanged;
    public static Action<float, float> onShootProjectileDamageChanged;

    [SerializeField] private Projectile projectilePrefab;

    [Header("Defaults")]
    [SerializeField] private float defaultShotsPerSecond = 2f; // Corrected fire rate
    [SerializeField] private float defaultProjectileDamage = 10f; // Default player damage
    [SerializeField] private float defaultProjectileSpeed = 25f;

    [Space]
    [Header("Preview only")]
    [SerializeField] private float currentShotsPerSecond;
    [SerializeField] private float currentProjectileDamage;
    [SerializeField] private float currentProjectileSpeed;

    private float nextShotTime = 0f;

    private void Start()
    {
        currentShotsPerSecond = defaultShotsPerSecond;
        currentProjectileDamage = defaultProjectileDamage;
        currentProjectileSpeed = defaultProjectileSpeed;

        // Subscribe to events
        onShootSpeedChanged += OnShootSpeedChanged;
        onShootProjectileChanged += OnShootProjectile;
        onShootProjectileDamageChanged += OnShootProjectileDamageChanged;
    }

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            if (Input.GetMouseButton(0)) // Hold for auto-fire
            {
                Shoot();
                nextShotTime = Time.time + (1f / currentShotsPerSecond); // Correct fire rate
            }
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null)
        {

            // Instantiate projectile at firePoint
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            newProjectile.IsPlayer = true;
            newProjectile.SetDamage(currentProjectileDamage); // Assign damage
            newProjectile.SetSpeed(currentProjectileSpeed);
        }
    }

    private void OnShootSpeedChanged(float speedMultiplier, float duration)
    {
        currentProjectileSpeed *= speedMultiplier;
        Invoke(nameof(ResetProjectileSpeed), duration);
    }

    private void OnShootProjectile(float shotsPerSecondMultiplier, float duration)
    {
        currentShotsPerSecond *= shotsPerSecondMultiplier;
        Invoke(nameof(ResetShotProjectile), duration);
    }

    private void OnShootProjectileDamageChanged(float damageMultiplier, float duration)
    {
        currentProjectileDamage *= damageMultiplier;
        Invoke(nameof(ResetProjectileDamage), duration);
    }

    private void ResetProjectileSpeed()
    {
        currentProjectileSpeed = defaultProjectileSpeed;
    }

    private void ResetShotProjectile()
    {
        currentShotsPerSecond = defaultShotsPerSecond;
    }

    private void ResetProjectileDamage()
    {
        currentProjectileDamage = defaultProjectileDamage;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        onShootSpeedChanged -= OnShootSpeedChanged;
        onShootProjectileChanged -= OnShootProjectile;
        onShootProjectileDamageChanged -= OnShootProjectileDamageChanged;
    }
}
