using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMain: MonoBehaviour
{
    public float health;
    public float speed;
    public int damage;


    public bool isDetectedPlayer;
    public virtual void Move()
    {
    }

    public virtual void Attack()
    {
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Death logic
        // Update score
        Destroy(gameObject);
    }

    public virtual void SetFollowPlayer()
    {
        isDetectedPlayer = true;
        speed += 2;
    }

    public virtual void SetUnFollowPlayer()
    {
        isDetectedPlayer = false;
        speed -= 2;
    }
}
