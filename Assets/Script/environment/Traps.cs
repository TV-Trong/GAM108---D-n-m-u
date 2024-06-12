
using Player;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.TakeDamage(1000);
        }
    }
}
