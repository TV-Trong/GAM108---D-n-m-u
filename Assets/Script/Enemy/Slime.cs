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

            if (!isDetectedPlayer && Mathf.Abs(transform.position.x - currentPosFocus.position.x) < 0.1f)
            {
                currentPosFocus = currentPosFocus == pos1 ? pos2 : pos1;
            }
            
            Debug.Log("Current POS Focus: " + currentPosFocus);

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Gai"))
        {
            Destroy(gameObject);
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
