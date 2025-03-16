using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float shotsPerSecond = 2f; // Corrected fire rate
    [SerializeField] public float projectileDamage = 10f; // Default player damage

    private float nextShotTime = 0f;

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            if (Input.GetMouseButton(0)) // Hold for auto-fire
            {
                Shoot();
                nextShotTime = Time.time + (1f / shotsPerSecond); // Correct fire rate
            }
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null)
        {
            // Instantiate projectile at firePoint
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            newProjectile.SetDamage(projectileDamage); // Assign damage
        }
    }
}
