using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Physics and Player Attributes
        private Rigidbody2D rb;
        private Transform tranformPlayer;
        private SpriteRenderer spriteRenderer;
        //
        [SerializeField] private Transform groundCheck; // Object con của player
        [SerializeField] private LayerMask groundLayer; // layer của nền đứng
        [SerializeField] private LayerMask ladderLayer; // layer của cầu thang

        private float horizontal;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpPower = 16f;
        [SerializeField] public float dashPower = 50f;
        [SerializeField] private float climbSpeed = 5f;


        // States
        public bool isFacingRight = true;
        public bool isRolling = false;
        public bool isFiring = false;
        public bool isHurting = false;
        public bool isActing = false;
        public bool isMoving = false;
        // Animation
        private Animator animator;

        //
        // Audio
        private Speaker speaker;

        //

        // Health Manager
        //[SerializeField] private float currentHealth;

        //Spawn location
        Vector2 lastPosition;


        //Play time
        float timePlayed;

        //UI
        UpdateUI updateUI;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            tranformPlayer = GetComponent<Transform>();
            speaker = FindObjectOfType<Speaker>();
            updateUI = FindObjectOfType<UpdateUI>();
            spriteRenderer = GetComponent<SpriteRenderer>();


            //Spawn position
            lastPosition = DataManager.Instance.currentPlayer.lastPosition;
            gameObject.transform.position = lastPosition;
        }

        // Update is called once per frame
        void Update()
        {

            HandleMovement();
            CheckFlip();
            UpdateAnimations();

            if (isMove() && IsGrounded())
            {
                if (!isMoving)
                {
                    isMoving = true;
                    speaker.PlayAudioRemune("Walk"); 
                }
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    speaker.StopAudioRemune();
                }
            }
        }

        private void FixedUpdate()
        {
            animator.SetBool("isClimbing", IsNearLadder());
            Debug.Log("IdGrounded: " + IsGrounded());
            Debug.Log("isNearLadder: " + IsNearLadder());
            HandleClimbing();

            //Tinh thoi gian choi
            DataManager.Instance.currentPlayer.SetTimePlayed(Time.deltaTime);
        }

        #region Checking Methods
        void HandleMovement()
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);            
            animator.SetBool("isGrounding", IsGrounded());
        }

        void CheckFlip()
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                Flip();
            }
        }

        public bool IsGrounded() // kiểm tra nhân vật có đứng trên nền không
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }

        public bool IsNearLadder() // kiểm tra nhân vật có đứng ngay cầu thang không
        {
            return Physics2D.OverlapCircle(transform.position, 0.3f, ladderLayer);
        }

        public bool isMove()
        {
            return horizontal != 0;
        }

        #endregion

        #region Activites Methods

        void UpdateAnimations()
        {
            animator.SetBool("isMoving", horizontal != 0);
            float verticalSpeed = rb.velocity.magnitude;
            animator.SetFloat("Magnitude", verticalSpeed);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        void HandleClimbing()
        {
            bool nearLadder = IsNearLadder();
            float climbInput = Input.GetAxisRaw("Vertical");

            if (nearLadder)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, climbInput * climbSpeed);
            }
            else
            {
                rb.gravityScale = 5f;
            }


        }
        public void TakeDamage(int damage)
        {
            if (isHurting) return;
            speaker.PlayAudioOneShot("Hit");
            isHurting = true;

            DataManager.Instance.currentPlayer.TakeDamage(damage);
            updateUI.UpdateValue();
            //Debug.Log("Current Health: " + currentHealth);

            if (DataManager.Instance.currentPlayer.health <= 0)
            {
                Debug.Log("Die");
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(TakeDamageRoutine());
            }
        }
        IEnumerator TakeDamageRoutine()
        {
            rb.velocity = new Vector2(rb.velocity.x * (isFacingRight ? -50f : 50f), 10f);

            float duration = 2f;
            float elapsed = 0f;
            Color originalColor = spriteRenderer.color;

            while (elapsed < duration)
            {
                float alpha = Mathf.PingPong(elapsed * 5f, 1f);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                elapsed += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = originalColor;

            isHurting = false;
        }

        public void Heal(int healingAmount)
        {
            DataManager.Instance.currentPlayer.health = Mathf.Clamp(DataManager.Instance.currentPlayer.health + healingAmount, 0, 100);
            updateUI.UpdateValue();
            Debug.Log("Heal");
        }

        #endregion

        public void OnMove(InputAction.CallbackContext context)
        {             
                horizontal = context.ReadValue<Vector2>().x;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded())
            {
                speaker.PlayAudioOneShot("PlayerJump");
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }

            else if (context.canceled && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        public void SavePosition()
        {
            DataManager.Instance.currentPlayer.lastPosition = transform.position;
        }
    }
}


