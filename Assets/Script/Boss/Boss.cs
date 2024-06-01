
using UnityEngine;

public class Boss : EnemyMain
{
    private bool movingRight = true;
    public Transform leftLimit;
    public Transform rightLimit;

    void Start()
    {
        health = 10;
        speed  = 1;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit.transform.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit.transform.position.x)
            {
                movingRight = true;
            }
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
