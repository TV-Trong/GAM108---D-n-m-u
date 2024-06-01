
using UnityEditor.Tilemaps;
using UnityEngine;

public class Boss : EnemyMain
{
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
        if (isMovingRight)
        {
            transform.localScale = new Vector2(-1f, 1f);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit.transform.position.x)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit.transform.position.x)
            {
                isMovingRight = true;
            }
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
