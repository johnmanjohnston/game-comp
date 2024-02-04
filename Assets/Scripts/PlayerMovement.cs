using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // TO DO: organize the order of these variable so it isn't too messy
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float distanceToGround;
    [SerializeField] private int numJumps;
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
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, groundLayer);
        isGrounded = ray.collider != null && ray.distance <= (transform.localScale.y / 2) + .1f;
        distanceToGround = ray.distance;

        if (isGrounded) numJumps = 2;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 1)
        {
            rb.AddForce(new Vector2(0f, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            rb.AddForce(new Vector2(0f, horizontal * 10f * Time.fixedDeltaTime), ForceMode2D.Impulse);

            if (!isGrounded) {
                // this is the double jump
                rb.AddForce(new Vector2(0f, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }

            numJumps--;
        }
    }

    private void AddExtraGravity() {
        if (!isGrounded) {

            Vector2 gravity = new(0, -(rb.velocity.y * (rb.velocity.y / 2) * customGravityIntensifier * Math.Max(distanceToGround * distanceToGround, customGravityIntensifier)));
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
        if (isGrounded) {
            Vector2 forceVec = new(col.relativeVelocity.x, 0);
            rb.AddForce(horizontal * reGroundingMakeUp * Time.fixedDeltaTime * -forceVec, ForceMode2D.Impulse);
        }
    }
}