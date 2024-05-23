using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerTriggerAndCollision : MonoBehaviour
    {
        private Rigidbody2D rb;
        private PlayerMovement playerMovement;
        private Animator animator;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Thang")) // Ladder
            {
                rb.gravityScale = 0f;
                playerMovement.isClimbing = true;
                animator.SetBool("isClimbing", true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Thang")) // Ladder
            {
                rb.gravityScale = 5f;
                playerMovement.isClimbing = false;
                animator.SetBool("isClimbing", false);
            }
        }
    }

}