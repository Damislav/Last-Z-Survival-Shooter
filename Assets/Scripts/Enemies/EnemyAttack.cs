using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float shotsPerSecond = 2f; // Corrected fire rate
    [SerializeField] public float projectileDamage = 10f; // Default player damage
    [SerializeField] private float projectileSpeed;

    private float nextShotTime = 0f;

    [SerializeField] bool isShooting;

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            if (isShooting)
            {
                Shoot();
                nextShotTime = Time.time + (1f / shotsPerSecond);
            }

        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null)
        {
            Projectile newProjectile = Instantiate(projectilePrefab, spawnPoint.position, projectilePrefab.transform.rotation);
            newProjectile.IsPlayer = false;
            newProjectile.SetDamage(projectileDamage);
            newProjectile.SetSpeed(projectileSpeed);

        }
    }
}