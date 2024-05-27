using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;

public class Slime : EnemyMain
{
    [SerializeField] private GameObject boxPOS;
    [SerializeField] private GameObject playerPOS;

    void Start()
    {
        health = 4;
        speed = 5;
        damage = 2;
        isDetectedPlayer = false;
        isMovingRight = true;
       
       

        playerPOS = GameObject.Find("Player");

        if (playerPOS == null)
        {
            Debug.LogError("Player object not found");
            return;
        }



        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(MoveCoroutine());
    }
    void Flip()
    {
        transform.localScale = new Vector2(isMovingRight ? 1f : -1f * 1f, 1);
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (isDetectedPlayer)
            {
                isMovingRight = playerPOS.transform.position.x > transform.position.x;
                
            }
            float direction = isMovingRight ? 1f : -1f;
            Flip();
            anim.Play("Jump");
            rb.AddForce(new Vector2(direction * speed, 10f), ForceMode2D.Impulse);

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(10);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BoxEnemy"))
        {
            if (transform.position.x < boxPOS.transform.position.x)
            {
                isMovingRight = true;
            }
            else isMovingRight = false;
        }
    }
}
