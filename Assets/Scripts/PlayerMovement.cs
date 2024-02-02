using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float customGravityIntensifier;

    [SerializeField] private Transform gfxContainer;
    [SerializeField] private float previousDirection;

    private float horizontal;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        HandleHorizontalMovement();
        AddExtraGravity();
    }

    private void Update()
    {
        HandleJump();
        HandleGroundCheck();
    }

    private void HandleGroundCheck() {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, (transform.localScale.y / 2) + .1f, groundLayer);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("jump");
            rb.AddForce(new Vector2(0f, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
    }

    private void AddExtraGravity() {
        if (!isGrounded) {
            Vector2 gravity = new(0, -(rb.velocity.y * (rb.velocity.y / 2) * customGravityIntensifier));
            rb.AddForce(gravity * Time.fixedDeltaTime);
        }
    }

    private void HandleHorizontalMovement()
    {
        horizontal = Mathf.Round(Input.GetAxisRaw("Horizontal") * 2f) / 2f;
        Vector2 moveVector = new(horizontal * speed * rb.gravityScale, 0f);

        if (horizontal != 0) {
            if (horizontal != previousDirection) {
                previousDirection = horizontal;

                // if we're facing the left side, rotate
                Quaternion flippedRotation = Quaternion.Euler(new Vector3(0, 180, 0));

                if (previousDirection == -1) {
                    gfxContainer.rotation = flippedRotation;
                } else {
                    gfxContainer.rotation = Quaternion.identity;
                }
            }
        }

        if (!isGrounded) {
            moveVector.x /= 1.8f;
        }

        rb.AddForce(moveVector * Time.fixedDeltaTime);
    }
}