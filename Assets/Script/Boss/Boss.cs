using Player;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss : EnemyMain
{
    public GameObject player;

    private bool isAttackingPlayer = false;
    private bool isDead = false;
    private float hurtDuration = 0.5f;
    public Transform restPOS;

    private Vector3 initialPosition;

    // audio
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip walk;

    private AudioSource speaker;
    [SerializeField] private AudioSource speakerWalk;

    void Start()
    {
        //health = 10;
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

        speaker = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead || isAttackingPlayer || isTakingDamage) return;
   
        UpdateState();
        if (isDetectedPlayer)
        {
            speakerWalk.mute = false;
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
        } else
        {
            rb.velocity = Vector2.zero;
            speakerWalk.mute = true;
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
        speaker.PlayOneShot(hurt);
        yield return new WaitForSeconds(hurtDuration);
        isTakingDamage = false;
    }

    public override void Die()
    {
        col.enabled = false;
        isDead = true;
        anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
        Debug.Log("BossDie");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
            collision.gameObject.SendMessage("TakeDamage", 10);
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.Instance.WinGame();
    }
}
