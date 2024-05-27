using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bird : EnemyMain
{
    [SerializeField] private Transform targetA;
    [SerializeField] private Transform targetB;

    private Transform currentTarget;
    //private float changeTargetTime = 2f;


    private GameObject playerPOS;
    void Start()
    {
        health = 1f;
        damage = 3;
        speed = 5f;
        isDetectedPlayer = false;
        currentTarget = targetA;

        playerPOS = GameObject.Find("Player");

        if (playerPOS == null)
        {
            Debug.Log("Player cant found");
            return;
        }

        StartCoroutine(MainCoroutine());
    }
    
    IEnumerator MainCoroutine()
    {
        while (true)
        {
            if (isDetectedPlayer)
            {
                StartCoroutine(DetectedPlayerCoroutine());
                yield break;
            }
            else
            {
                yield return StartCoroutine(MoveCoroutine());
            }
        }
    }

    IEnumerator DetectedPlayerCoroutine()
    {
        while (Vector3.Distance(transform.position, playerPOS.transform.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPOS.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                SwitchTarget();
            }


            yield return null;
        }
        
    }

    void SwitchTarget()
    {
        currentTarget = (currentTarget == targetA) ? targetB : targetA;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(10);
        }
    }
}
