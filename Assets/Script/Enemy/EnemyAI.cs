using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.SendMessage("SetFollowPlayer");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.SendMessage("SetUnFollowPlayer");
            }
        }
    }
}
