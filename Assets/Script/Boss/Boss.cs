using Player;
using System.Collections;
using UnityEngine;

public class Boss : EnemyMain
{
    public GameObject player;

    private bool isAttackingPlayer = false;
    private bool isDead = false;
    private float attackRange = 2f;
    private float hurtDuration = 0.5f;
    public Transform restPOS;

    private Vector3 initialPosition;

    void Start()
    {
        health = 10;
        speed = 3;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isDead || isAttackingPlayer) return;
   
        UpdateState();
        if (isDetectedPlayer)
        {
            //bool isPlayerRight = player.transform.position.x > transform.position.x;
            isMovingRight = player.transform.position.x > transform.position.x;
            FlipBoss();
            if (/*isPlayerRight*/isMovingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            } else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }

    void FlipBoss()
    {
        if (isMovingRight)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
    }

    void UpdateState()
    {
        anim.SetBool("isChasePlayer", isDetectedPlayer);
    }
    void AttackPlayer()
    {
        if (!isAttackingPlayer)
        {
            isAttackingPlayer = true;
            anim.SetTrigger("Attack");
            Invoke("FinishAttack", 1f);
        }
    }

    void FinishAttack()
    {
        isAttackingPlayer = false;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (health > 0)
        {
            StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        anim.SetTrigger("Hurt");
        yield return new WaitForSeconds(hurtDuration);
        isTakingDamage = false;
    }

    public override void Die()
    {
        col.enabled = false;
        isDead = true;
        anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
            collision.gameObject.SendMessage("TakeDamage", 10);
        }
    }
}
