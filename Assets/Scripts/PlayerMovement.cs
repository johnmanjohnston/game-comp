using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // TO DO: organize the order of these variable so it isn't too messy
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float customGravityIntensifier;
    [SerializeField] private float ungroundedSpeedDivisor;
    [SerializeField] private float enteredPortalSpeed;

    [SerializeField] private Transform gfxContainer;

    [SerializeField] private float reGroundingMakeUp;
    public float previousDirection;

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
            rb.AddForce(new Vector2(0f, horizontal * 10f * Time.fixedDeltaTime), ForceMode2D.Impulse);
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
        horizontal = Input.GetAxisRaw("Horizontal");
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
            moveVector.x /= ungroundedSpeedDivisor;
        }

        rb.AddForce(moveVector * Time.fixedDeltaTime);
    }

    public void AddForceOnEnteringPortal() {
        rb.AddForce(enteredPortalSpeed * Time.fixedDeltaTime * rb.velocity);
        rb.AddForce(100f * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        // add a force in the direction that we're headed to make the movement feel smoother
        Vector2 forceVec = new(col.relativeVelocity.x, 0);
        rb.AddForce(reGroundingMakeUp * Time.fixedDeltaTime * -forceVec, ForceMode2D.Impulse);
    }
}