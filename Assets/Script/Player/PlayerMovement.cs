using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        public bool isMoving = true;
        public bool isClimbing = false;
        public bool isHurting = false;
        public bool isActing = false;
        // Animation
        private Animator animator;

        //
        // Audio
        private AudioSource speaker;

        [SerializeField] private AudioClip walkSound;
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip rollSound;
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private AudioClip fireSound;

        //

        // Health Manager
        [SerializeField] private int currentHealth;
        //
        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            tranformPlayer = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
            {
                HandleMovement();
                CheckFlip();
                UpdateAnimations();
            }
        }

        private void FixedUpdate()
        {
            if (isClimbing)
            {
                HandleClimbing();
            }
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
            if (climbInput > 0)
            {
                tranformPlayer.transform.position += Vector3.up * climbSpeed * Time.fixedDeltaTime;
            }
            else if (climbInput < 0)
            {
                tranformPlayer.transform.position += Vector3.down * climbSpeed * Time.fixedDeltaTime;
            }

        }

        void takeAction()
        {
            // khi thực hiện hành động nào đó, sẽ di chuyển bằng cách cho isMoving = false
        }

        public void TakeDamage(int damage)
        {
            if (isHurting) return;
            if (hitSound != null) speaker.PlayOneShot(hitSound);
            isHurting = true;
            currentHealth -= damage;
            Debug.Log("Current Health: " + currentHealth);

            if (currentHealth <= 0)
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

        #endregion

        // Hàm Movement được gọi mỗi khi người chơi thực hiện hành động di chuyển.
        public void OnMove(InputAction.CallbackContext context)
        {
            // Đọc giá trị của trục x từ context của input và gán cho biến horizontal.
            // Trục x được sử dụng để xác định hướng di chuyển ngang của nhân vật.
            horizontal = context.ReadValue<Vector2>().x;
        }

        // Hàm Jump được gọi mỗi khi người chơi thực hiện hành động nhảy.
        public void OnJump(InputAction.CallbackContext context)
        {
            // Nếu hành động nhảy được thực hiện và nhân vật đang đứng trên mặt đất,
            // thì thiết lập vận tốc theo hướng y cho nhân vật để nhảy lên.
            if (context.performed && IsGrounded())
            {
                if (jumpSound != null) speaker.PlayOneShot(jumpSound);
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }

            // Nếu hành động nhảy được hủy và vận tốc theo hướng y của nhân vật đang lớn hơn 0,
            // thì giảm vận tốc theo hướng y của nhân vật để giảm độ cao của nhảy.
            else if (context.canceled && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded() && !isRolling)
            {
                if (rollSound != null) speaker.PlayOneShot(rollSound);
                isRolling = true;
                animator.Play("Roll");
                transform.position += transform.right * (isFacingRight ? 1 : -1) * dashPower * Time.deltaTime;
                isRolling = false;
            }
        }


        [SerializeField] private Transform bowPosition;
        [SerializeField] private GameObject arrowObj;
        public void OnFire(InputAction.CallbackContext context)
        {
            if (!isFiring && !IsPlayingFireAnimation())
            {
                if (fireSound != null) speaker.PlayOneShot(fireSound);
                isFiring = true;
                Quaternion arrowRotation = isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180f, 0);
                Instantiate(arrowObj, bowPosition.position, arrowRotation);
                animator.Play("Fire");
                StartCoroutine(WaitForAnimation());
            }
        }

        private IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(0.1f);
            while (IsPlayingFireAnimation())
            {
                yield return null;
            }
            isFiring = false;
        }

        private bool IsPlayingFireAnimation()
        {

            return animator.GetCurrentAnimatorStateInfo(0).IsName("Fire");
        }

        
    }
}


