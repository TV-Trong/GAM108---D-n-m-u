using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : EnemyMain
{
    [SerializeField] private GameObject boxPOS;
    private GameObject playerPOS;

    private Vector3 targetPosition;
    private bool isMovingUp = true;
    private float moveRadius = 10f;
    private float changeTargetTime = 2f;

    void Start()
    {
        health = 1;
        speed = 5;
        damage = 3;
        isDetectedPlayer = false;

        playerPOS = GameObject.Find("Player");

        if (playerPOS == null)
        {
            Debug.LogError("Player object not found");
            return;
        }

        SetRandomTargetPosition();
        StartCoroutine(MoveCoroutine());
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (isDetectedPlayer)
            {
                targetPosition = new Vector2(transform.position.x, playerPOS.transform.position.y);
            }
            else
            {

                SetRandomTargetPosition();

            }

            yield return new WaitForSeconds(changeTargetTime);
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-moveRadius, moveRadius);
        float randomY = Random.Range(-moveRadius, moveRadius);
        targetPosition = new Vector3(boxPOS.transform.position.x + randomX, boxPOS.transform.position.y + randomY, transform.position.z);
        Debug.Log("Target position:" + targetPosition);
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
            SetRandomTargetPosition();
        }
    }
}
