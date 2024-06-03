using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.TakeDamage(100);
        }
    }
}
