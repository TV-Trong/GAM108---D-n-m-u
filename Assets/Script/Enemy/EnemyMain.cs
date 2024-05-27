using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMain: MonoBehaviour
{
    public float health;
    public float speed;
    public int damage;

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Animator anim;

    public bool isMovingRight;
    public bool isDetectedPlayer;
    public bool isTakingDamage = false;

    public virtual void Move()
    {
    }

    public virtual void Attack()
    {
    }

    public virtual void TakeDamage(float amount)
    {
        isTakingDamage = true;
        health -= amount;
        if (health <= 0)
        {
            isTakingDamage = false;
            Die();
        }

        StartCoroutine(TakeDamageCoroutine());

    }

    protected IEnumerator TakeDamageCoroutine()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.PingPong(elapsed * 5f, 1f);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
        isTakingDamage = false;
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
