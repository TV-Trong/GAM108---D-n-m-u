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

        if (pos1 != null && pos2 != null)
        {
            currentPosFocus = pos1;
        }


        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            Debug.Log("Current Pos Focus: " + currentPosFocus);

            if (isDetectedPlayer)
            {
                isMovingRight = playerPOS.transform.position.x > transform.position.x;
            }
            else
            {
                isMovingRight = transform.position.x < currentPosFocus.position.x;
            }

            float direction = isMovingRight ? 1f : -1f;
            Flip();
            anim.Play("Jump");
            rb.AddForce(new Vector2(direction * speed, 10f), ForceMode2D.Impulse);

            if (!isDetectedPlayer && Mathf.Abs(transform.position.x) >= Mathf.Abs(currentPosFocus.position.x))
            {
                currentPosFocus = currentPosFocus == pos1 ? pos2 : pos1;
            }
            
            Debug.Log("Pos focus after mathf: " + currentPosFocus);

            //Nhay ngau nhien
            float randomJumpTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(randomJumpTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Them sat thuong ngau nhien
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(Random.Range(20, 35));
        }
        if (collision.gameObject.CompareTag("Gai"))
        {
            Destroy(gameObject);
        }
    }
}
