using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float shotsPerSecond = 2f; // Corrected fire rate
    [SerializeField] public float projectileDamage = 10f; // Default player damage
    [SerializeField] private float projectileSpeed;

    [SerializeField] bool isShooting;

    [SerializeField] private float nextShotTime = 0f;

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            if (isShooting)
            {
                HandleEnemyShooting();
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

    private void HandleEnemyShooting()
    {
        RaycastHit hit; // Declare a variable to store the hit information
        Vector3 rayDirection = -transform.forward; // Change this based on enemy facing direction

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 50f))
        {
            Debug.DrawRay(transform.position, rayDirection * 50f, Color.red, 0.2f); // Visualize no-hit ray

            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.green, 0.2f); // Visualize hit ray
                Debug.Log($"Enemy saw {hit.transform.name}");
                Shoot();
            }
        }

    }


}