using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;

    private void Start()
    {
        if (enemies.Count == 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        enemies.RemoveAll(item => item == null);
        if (enemies.Count == 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                if (enemy!= null)
                {
                    enemy.SendMessage("SetFollowPlayer");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.SendMessage("SetUnFollowPlayer");
                }
            }
        }
    }
}
