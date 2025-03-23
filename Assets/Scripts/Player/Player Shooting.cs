using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Action<float> onShootSpeedChanged;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private float defaultShotsPerSecond = 2f; // Corrected fire rate
    [SerializeField] private float defaultProjectileDamage = 10f; // Default player damage
    [SerializeField] private float defaultProjectileSpeed = 25f;

    private float nextShotTime = 0f;

    private void Start()
    {
        //listen to speed changes
        onShootSpeedChanged += OnShootSpeedChanged;
    }

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            if (Input.GetMouseButton(0)) // Hold for auto-fire
            {
                Shoot();
                nextShotTime = Time.time + (1f / defaultShotsPerSecond); // Correct fire rate
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
            newProjectile.SetDamage(defaultProjectileDamage); // Assign damage
            newProjectile.SetSpeed(defaultProjectileSpeed);
        }
    }

    private void OnShootSpeedChanged(float speed)
    {
        defaultProjectileSpeed += speed;
        defaultProjectileSpeed += speed;
    }
}
