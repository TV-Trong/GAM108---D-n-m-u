using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private Transform groundCheck; // Object con của player
    [SerializeField] private LayerMask groundLayer; // layer của nền đứng

    private float horizontal;
    private float speed = 8f;
    private float jumpPower = 16f;
    private bool isFacingRight = true;


    private bool isRolling = false;
    private bool isFiring = false;
    private bool isMoving = true;
    // Animation
    private Animator animator;

    //
    // Audio
    private AudioSource speaker;

    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private AudioClip rollSound;

    //

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       if (isMoving)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // di chuyển nhân vật theo trục ngang

            // Kiểm tra hướng nhìn nhân vật
            if (!isFacingRight && horizontal > 0f)
            {
                Flip();
            }
            else if (isFacingRight && horizontal < 0f)
            {
                Flip();
            }

            AnimationController();
        }
    }

    private bool isGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        // Nếu nhân vật đang chạm đất, thì set trigger "isGrounding" trong animator thành true
        animator.SetBool("isGrounding", grounded);
        return grounded;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    #region Input Action System
    // Hàm Movement được gọi mỗi khi người chơi thực hiện hành động di chuyển.
    public void Movement(InputAction.CallbackContext context)
    {
        // Đọc giá trị của trục x từ context của input và gán cho biến horizontal.
        // Trục x được sử dụng để xác định hướng di chuyển ngang của nhân vật.
        horizontal = context.ReadValue<Vector2>().x;
    }

    // Hàm Jump được gọi mỗi khi người chơi thực hiện hành động nhảy.
    public void Jump(InputAction.CallbackContext context)
    {
        // Nếu hành động nhảy được thực hiện và nhân vật đang đứng trên mặt đất,
        // thì thiết lập vận tốc theo hướng y cho nhân vật để nhảy lên.
        if (context.performed && isGrounded())
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        // Nếu hành động nhảy được hủy và vận tốc theo hướng y của nhân vật đang lớn hơn 0,
        // thì giảm vận tốc theo hướng y của nhân vật để giảm độ cao của nhảy.
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded() && !isRolling)
        {
            // Đánh dấu là nhân vật đang trong quá trình lăn
            isRolling = true;

            // Chạy animation roll
            animator.Play("Roll");

            // Xác định hướng lực sẽ được áp dụng (trong trường hợp này, theo hướng người chơi đang nhìn)
            Vector2 forceDirection = transform.right * (isFacingRight ? 1f : -1f); // Điều chỉnh hướng tùy thuộc vào hướng nhìn

            // Áp dụng lực 
            
            rb.AddForce(new Vector2(forceDirection.x * 100f, forceDirection.y), ForceMode2D.Impulse);
            //transform.Translate(forceDirection * 300f * Time.deltaTime);
        }
        isRolling = false;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded() && !isFiring)
        {
            isFiring = true;
            Debug.Log("Fire");
            animator.Play("Fire");
        }
        isFiring = false;
    }
    #endregion
    void AnimationController()
    {
        float speedMove = rb.velocity.magnitude;
        if (speedMove > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void takeAction()
    {
        // khi thực hiện hành động nào đó, sẽ di chuyển bằng cách cho isMoving = false
    }

}
