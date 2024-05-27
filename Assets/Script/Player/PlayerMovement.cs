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
        //
        [SerializeField] private Transform groundCheck; // Object con của player
        [SerializeField] private LayerMask groundLayer; // layer của nền đứng

        private float horizontal;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpPower = 16f;
        [SerializeField] public float dashPower = 50f;
        [SerializeField] private float climbSpeed = 5f;


        // States
        public bool isFacingRight = true;
        public bool isRolling = false;
        public bool isFiring = false;
        public bool isClimbing = false;
        public bool isHurting = false;
        public bool isActing = false;
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
        }

        private void FixedUpdate()
        {
            if (isClimbing)
            {
                HandleClimbing();
            }

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

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
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
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        void HandleClimbing() //climb
        {
            float climbInput = Input.GetAxisRaw("Vertical");
            if (climbInput != 0)
            {
                speaker.PlayAudioOneShot("Climb");
                if (climbInput > 0)
                {
                    tranformPlayer.transform.position += Vector3.up * climbSpeed * Time.fixedDeltaTime;
                }
                else if (climbInput < 0)
                {
                    tranformPlayer.transform.position += Vector3.down * climbSpeed * Time.fixedDeltaTime;
                }
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
            rb.velocity = new Vector2(rb.velocity.x   * (isFacingRight ? -50f : 50f)  , 10f);
            yield return new WaitForSeconds(2f);
            isHurting = false;
        }

        public void Heal(int healingAmount)
        {
            //
            Debug.Log("Heal");
        }

        #endregion

        public void OnMove(InputAction.CallbackContext context)
        {

                speaker.PlayAudioRemune("Move");
                horizontal = context.ReadValue<Vector2>().x;
            
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded())
            {
                speaker.PlayAudioOneShot("Jump");
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


