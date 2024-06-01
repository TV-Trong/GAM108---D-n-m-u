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
            float step = speed * Time.deltaTime;
            float xCurrentPosFocus = currentPosFocus.position.x;

            if (isDetectedPlayer)
            {
                Debug.Log("Move to Player");
                // Di chuyển về phía người chơi
                isMovingRight = playerPOS.transform.position.x > transform.position.x;
            }
            else
            {
                Debug.Log("Move to POS");
                // Di chuyển qua lại giữa pos1 và pos2
                isMovingRight = transform.position.x < currentPosFocus.position.x;
            }

            float direction = isMovingRight ? 1f : -1f;
            Flip();
            anim.Play("Jump");
            Debug.Log("Jump Actions by Slime");
            rb.AddForce(new Vector2(direction * speed, 10f), ForceMode2D.Impulse);

            // Kiểm tra nếu đã tới vị trí focus hiện tại và chuyển focus nếu cần thiết
            if (!isDetectedPlayer && Mathf.Abs(transform.position.x - xCurrentPosFocus) < 0.1f)
            {
                currentPosFocus = currentPosFocus == pos1 ? pos2 : pos1;
            }

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
