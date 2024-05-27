using System.Collections;
using UnityEngine;

public class SuperEnemy : EnemyMain
{
    void Start()
    {
        StartCoroutine(MainCoroutine());
    }

    void Update()
    {
        // Các logic khác của Update nếu cần
    }

    IEnumerator MainCoroutine()
    {
        while (true)
        {
            if (!isDetectedPlayer)
            {
                yield break;
            }
            else
            {
                yield return StartCoroutine(AttackPlayerCoroutine());
            }

            yield return null;
        }
    }

    IEnumerator AttackPlayerCoroutine()
    {
         Debug.Log("Up");
        yield return null;

    }
}
