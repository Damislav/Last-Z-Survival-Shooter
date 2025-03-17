using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private float enemyHealth;
    [SerializeField] private float maxEnemyHealth = 20f;

    private float standingPlayerDamage = 10f;

    [SerializeField] private GameObject enemyDeathVfx;

    private void Start()
    {
        enemyHealth = maxEnemyHealth;
    }

    private void Update()
    {
        EnemyMoveForward();
    }

    private void EnemyMoveForward()
    {
        transform.position += Vector3.back * movementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.DamagePlayer(standingPlayerDamage);
        }
    }

    public void TakeDamage(float projectileDamage)
    {
        enemyHealth -= projectileDamage;
        if (enemyHealth <= 0)
        {
            Instantiate(enemyDeathVfx, transform.position, transform.rotation);
            // Instantiate()
            Destroy(this.gameObject);
        }
    }

}