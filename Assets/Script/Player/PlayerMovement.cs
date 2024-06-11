using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
        [SerializeField] private LayerMask underwater;  // water
        //Water Bubble Particle System
        [SerializeField] ParticleSystem waterBubble;
        bool toggleWaterBubble;

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

        //Spawn location
        Vector2 lastPosition;


        //Play time
        float timePlayed;

        //UI
        UpdateUI updateUI;

        //God Mode
        bool isGodModeOn;

        GameObject godeModeObj;
        private void Start()
        {
            waterBubble = GetComponentInChildren<ParticleSystem>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            tranformPlayer = GetComponent<Transform>();
            speaker = FindObjectOfType<Speaker>();
            updateUI = FindObjectOfType<UpdateUI>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            godeModeObj = GameObject.Find("GodModeObj");
            godeModeObj.SetActive(false);

            //Set spawn and Save
            transform.position = DataManager.Instance.currentPlayer.lastPosition;
            SceneLoader.Instance.SaveOnNewLevel();

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

            if (IsUnderwater())
            {
                Debug.Log("Is Underwater");
                speed = 4f;
                jumpPower = 8f;
                if (!toggleWaterBubble)
                {
                    StartCoroutine(MakingWaterBubble());
                    toggleWaterBubble = !toggleWaterBubble;
                }
            }
            else
            {
                speed = 8f;
                jumpPower = 16f;
                if (toggleWaterBubble)
                {
                    toggleWaterBubble = !toggleWaterBubble;
                }
            }

            //God Mod is on
            if (isGodModeOn)
            {
                DataManager.Instance.currentPlayer.ResetHP();
                DataManager.Instance.currentPlayer.life = 99;
                updateUI.UpdateValue();
            }
            godeModeObj.SetActive(isGodModeOn);

            //Tinh thoi gian choi
            DataManager.Instance.currentPlayer.SetTimePlayed(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            animator.SetBool("isClimbing", IsNearLadder());
            HandleClimbing();

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

        public bool IsUnderwater()
        {
            return Physics2D.OverlapCircle(transform.position, 0.1f, underwater);
        }
        public bool isMove()
        {
            return horizontal != 0;
        }

        public bool IsStandingStillOnGround()
        {
            return IsGrounded() && horizontal == 0;
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
                if (IsUnderwater())
                {
                    rb.gravityScale = 1f;
                }
                else
                {
                    rb.gravityScale = 5f;
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
                gameObject.transform.localScale = Vector3.zero;
                rb.bodyType = RigidbodyType2D.Static;

                DataManager.Instance.currentPlayer.LoseLife();
                SceneLoader.Instance.LoseALife();

                Button pause = GameObject.Find("PauseButton").GetComponent<Button>();
                pause.interactable = false;
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
            DataManager.Instance.currentPlayer.lastCurrentScene = SceneManager.GetActiveScene().name;
            DataManager.Instance.currentPlayer.lastPosition = transform.position;
        }
        
        IEnumerator MakingWaterBubble()
        {
            while (IsUnderwater())
            {
                yield return new WaitForSeconds(0.5f);
                ParticleSystem bubble = Instantiate(waterBubble, transform.position, Quaternion.identity);
                bubble.Play();
            }
        }

        public void CheckGodModeOn(bool isTrue)
        {
            isGodModeOn = isTrue;
        }
    }
}


