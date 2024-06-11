using Player;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss : EnemyMain
{
    private float baseHealth;
    private bool isAttackingPlayer = false;
    private bool isDead = false;


    private float hurtDuration = 0.1f;
    public Transform restPOS;

    private Vector3 initialPosition;

    // audio
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip meleeAttack;
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip skill1;
    [SerializeField] private AudioClip dealth;
    [SerializeField] private AudioClip skill2;

    [SerializeField] private AudioSource speaker;
    [SerializeField] private AudioSource speakerWalk;


    // skill
    [SerializeField] private GameObject skill1Obj;
    [SerializeField] private Transform skill1POS;

    [SerializeField]
    SpriteRenderer sr;

    // layerMask
    private LayerMask playerLayer;
    void Start()
    {
        //health = 10;
        baseHealth = health;
        speed = 3;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        playerLayer = LayerMask.GetMask("Player");

        isSleep = true;
        player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        initialPosition = transform.position;

        speaker = GetComponent<AudioSource>();

        StartCoroutine(SkillCoroutine());

    }

    void Update()
    {
        if (isDead || isAttackingPlayer || isTakingDamage) return;
   
        UpdateState();
        if (isDetectedPlayer)
        {
            isSleep = false;

            speakerWalk.mute = false;
            isMovingRight = player.transform.position.x > transform.position.x;
            FlipBoss();
            if (isMovingRight && !isNearXPlayerPOS())
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            } else if (!isMovingRight && !isNearXPlayerPOS())
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }

        } 
        else
        {
            if (IsWithinDistance(transform.position, restPOS.position))
            {
                isSleep = true;
                rb.velocity = Vector2.zero;
                speakerWalk.mute = true;
            }
            else
            {
                isSleep = false;
                speaker.mute = false;
                isMovingRight = restPOS.position.x > transform.position.x;
                FlipBoss();
                transform.position = Vector2.MoveTowards(transform.position, restPOS.position, speed * Time.deltaTime);
                //Hoi mau khi khong phat hien nguoi choi
                health += Time.deltaTime * 3f;
                health = Mathf.Clamp(health, 0, 35f);
            }
        }
        EnrageMode();

        if (RayCastLayer() == "Player")
        {
            AttackPlayer();
        }
    }

    string RayCastLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 2f, playerLayer);
        if (hit.collider != null)
        {
            Debug.Log("Layer Mask" + LayerMask.LayerToName(hit.collider.gameObject.layer));
            return LayerMask.LayerToName(hit.collider.gameObject.layer);
        }
        return null;
    }

    public bool EnrageMode() //Phase 2
    {
        if (health < baseHealth / 2)
        {
            speed = 6f;
            sr.color = new Color32(255, 98, 98, 255);
            return true;
        }
        else
        {
            speed = 3;
            sr.color = Color.white;
            return false;
        }
    }

    void FlipBoss()
    {
        if (!isNearXPlayerPOS() && !IsWithinDistance(transform.position, restPOS.position))
        {
            if (isMovingRight)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else return;
    }

    bool IsWithinDistance(Vector3 position, Vector3 targetPosition, float threshold = 0.3f)
    {
        return Vector3.Distance(position, targetPosition) <= threshold;
    }

    bool isNearXPlayerPOS()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= 1f;
    }

    void UpdateState()
    {
        anim.SetBool("isChasePlayer", isDetectedPlayer);
        anim.SetBool("isSleep", isSleep);
    }
    void AttackPlayer()
    {
        if (!isAttackingPlayer)
        {
            isAttackingPlayer = true;
            anim.SetTrigger("Attack");
            speaker.PlayOneShot(meleeAttack);

            int damage;

            if (EnrageMode())
            {
                damage = Random.Range(30, 45);
            }
            else
            {
                damage = Random.Range(20, 35);
            }


            StartCoroutine(MeleeAtackCoroutine(damage));
        }
    }

    IEnumerator MeleeAtackCoroutine(int damage)
    {
        player.SendMessage("TakeDamage", damage);

        yield return new WaitForSeconds(1.5f);

        isAttackingPlayer = false;

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
        speaker.PlayOneShot(dealth);
        Destroy(gameObject, 3f);
        StartCoroutine(Victory());
        Debug.Log("BossDie");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.Instance.WinGame();
    }


    IEnumerator SkillCoroutine()
    {
        while (true)
        {
            if (isDetectedPlayer && IsWithinDistance(transform.position, player.transform.position, 10f))
            {
                Debug.Log("Skill 1");
                Skill1Attack();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void Skill1Attack()
    {
        if (!isAttackingPlayer)
        {
            isAttackingPlayer = true;
            anim.SetTrigger("Attack");
            speaker.PlayOneShot(skill1);
            Quaternion skillRotation =  isMovingRight ? Quaternion.identity : Quaternion.Euler(0, 180f, 0);
            Instantiate(skill1Obj, skill1POS.position, skillRotation /*Quaternion.identity*/);
            Invoke("FinishAttack", 1f);
        }
    }

}
