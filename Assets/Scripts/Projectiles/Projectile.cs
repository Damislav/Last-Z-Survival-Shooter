using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public GameObject projectileVfx;

    [SerializeField] private float timeToDie = 5f;
    [SerializeField] private float projectileSpeed = 10f;

    private float damage; // Damage is now set dynamically
    [SerializeField] private float timeToLive = 0f;

    [SerializeField] public bool IsPlayer;

    void Update()
    {
        // Accumulate elapsed time
        timeToLive += Time.deltaTime;

        if (IsPlayer)
        {
            // Move forward at projectileSpeed
            transform.position += Vector3.forward * projectileSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.back * projectileSpeed * Time.deltaTime;
        }


        // Destroy when time exceeds timeToDie
        if (timeToLive >= timeToDie)
        {
            Destroy(gameObject);
        }

    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.DamagePlayer(damage);
            HandleHit();
        }
        if (other.CompareTag("Boost"))
        {
            other.GetComponent<Boost>()?.DamageBoost(damage);
            HandleHit();

        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damage);
            HandleHit();

        }
    }

    public void SpawnProjectileVfx()
    {
        Instantiate(projectileVfx, transform.position, Quaternion.identity);
    }

    private void HandleHit()
    {
        if (projectileVfx)
        {
            GameObject vfxInstance = Instantiate(projectileVfx, transform.position, Quaternion.identity);
            Destroy(vfxInstance, 2f);
        }
        Destroy(gameObject);
    }

    //change projectile speed from enemy attack script
    public void SetSpeed(float projectileSpeed)
    {
        this.projectileSpeed = projectileSpeed;
    }
}
