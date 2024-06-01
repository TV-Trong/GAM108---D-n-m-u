using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMain: MonoBehaviour
{
    public float health;
    public float speed;
    public int damage;

    public Transform pos1;
    public Transform pos2;
    public Transform currentPosFocus;


    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Animator anim;

    private EnemyAI enemyAI;

    public bool isMovingRight;
    public bool isDetectedPlayer;
    public bool isTakingDamage = false;

    void Start()
    {
    }

    public virtual void Flip()
    {
        transform.localScale = new Vector2(isMovingRight ? 1f : -1f * 1f, 1);
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
        rb.AddForce(new Vector2((isMovingRight ? -1: 1) * 3, 10f), ForceMode2D.Impulse);


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


    private void OnDestroy()
    {
        // Update score
        //enemyAI.RemoveMissingEnemies();
    }
}
