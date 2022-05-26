using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        float horizontalMovment = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovment);

        Flip(horizontalMovment);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }
    
    void MovePlayer(float _horizontalMovment)
    {
        Vector3 targetVeclocity = new Vector2(_horizontalMovment, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVeclocity, ref velocity, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }


    void Flip(float _velocity)
    {
        if (_velocity  > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if (_velocity < -0.1f )
        {
            spriteRenderer.flipX = true;
        }
    }
    
}
