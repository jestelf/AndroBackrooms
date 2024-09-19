using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 15f;
    public float dashDuration = 0.3f;
    public float slideSpeed = 8f;
    public float slideDuration = 0.5f;
    public Animator animator;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isSliding = false;
    private bool isDashing = false;
    private bool wasInAir = false;
    private bool wantToSlide = false;
    private bool canSlideAfterLanding = false;
    private bool canJumpAgain = true;
    private float jumpCooldown = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        if (!isGrounded)
        {
            wasInAir = true;
            animator.SetBool("IsJump", true);
        }
        else if (wasInAir)
        {
            EndAirPhase();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJumpAgain)
        {
            if (isGrounded && !isSliding && !isDashing)
            {
                PerformJump();
            }
            else if (isSliding)
            {
                EndSlide();
                PerformJump();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            HandleSlideInput();
        }
    }

    void FixedUpdate()
    {
        if (!isSliding && !isDashing && isGrounded)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    void PerformJump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        animator.SetBool("IsJump", true);
        wasInAir = true;
        canJumpAgain = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    void StartDash()
    {
        isDashing = true;
        animator.SetBool("IsJumpSlide", true);
        rb.velocity = new Vector2(dashForce, rb.velocity.y);
        StartCoroutine(StopDash());
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        animator.SetBool("IsJumpSlide", false);
        if (!isGrounded) animator.SetBool("IsJump", true);
    }

    void StartSlide()
    {
        isSliding = true;
        animator.SetBool("IsSlide", true);
        rb.velocity = new Vector2(slideSpeed, rb.velocity.y);
        StartCoroutine(StopSlide());
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideDuration);
        EndSlide();
    }

    void EndSlide()
    {
        isSliding = false;
        animator.SetBool("IsSlide", false);
        wantToSlide = false;
    }

    void EndAirPhase()
    {
        wasInAir = false;
        animator.SetBool("IsJump", false);
        animator.SetTrigger("Landing");
        StartCoroutine(AllowSlideAfterLanding());
    }

    void HandleSlideInput()
    {
        if (isGrounded && !isSliding && canSlideAfterLanding && !isDashing)
        {
            StartSlide();
        }
        else if (!isGrounded && !isDashing)
        {
            StartDash();
        }
    }

    IEnumerator AllowSlideAfterLanding()
    {
        yield return new WaitForSeconds(0.2f);
        canSlideAfterLanding = true;
    }

    void ResetJump()
    {
        canJumpAgain = true;
    }
}
