using System;
using System.Collections;
using UnityEngine;

namespace GameComp.PlayerConfigs {
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // TO DO: organize the order of these variable so it isn't too messy
    // Variables -- '[SerializeField]' just lets it be visible in the Unity Inspector
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float distanceToGround;
    [SerializeField] private int numJumps;
    [SerializeField] private float customGravityIntensifier;
    [SerializeField] private float ungroundedSpeedDivisor;
    [SerializeField] private float enteredPortalSpeed;

    [SerializeField] private float dashSpeed;
    [SerializeField] private bool dashCooldownExpired;
    [SerializeField] private float dashCooldown;

    [SerializeField] private Transform gfxContainer;

    [SerializeField] private float reGroundingMakeUp;
    public float previousDirection { get; private set; }
    private Vector2 moveVector;

    public float horizontal { get; private set; }
    public Rigidbody2D rb { get; private set; }
    private BoxCollider2D col;

    private float customGravityAmountToAdd;

    // Called when the scene loads
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        col = GetComponent<BoxCollider2D>();

        dashCooldownExpired = true; // allow dashing when game starts
    }

    // Called every PHYSICS update
    private void FixedUpdate()
    {
        HandleHorizontalMovement();
        AddExtraGravity();
    }

    // Called every frame
    private void Update()
    {
        HandleDash();
        HandleJump();
        HandleGroundCheck();
    }

    private void HandleGroundCheck() {
        // raycast downwords and check if the collider exists, and the distance is sufficient
        // to mark the player as grounded
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, groundLayer);
        isGrounded = ray.collider != null && ray.distance <= (col.size.y / 2) + .1f;
        distanceToGround = ray.distance;

        if (isGrounded) numJumps = 2; // if we're grounded, restore the double-jump ability

        if (!isGrounded) {
            customGravityAmountToAdd += Time.deltaTime;
        } else { customGravityAmountToAdd = 0f; }
    }

    private void HandleJump()
    {
        // bind the Space, W, and Up Arrow Keys to jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && numJumps > 1)
        {
            rb.AddForce(new Vector2(0f, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            rb.AddForce(new Vector2(0f, horizontal * 10f * Time.fixedDeltaTime), ForceMode2D.Impulse);

            if (!isGrounded) {
                // this is the double jump
                rb.AddForce(new Vector2(0f, doubleJumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }

            numJumps--;
            customGravityAmountToAdd /= 1.5f;
        }
    }

    private void AddExtraGravity() {
        // add extra gravity using custom equation, which lets the movement
        // feel more "responsive"        
        if (!isGrounded) {
            Vector2 gravity = new(0, -(customGravityIntensifier * (customGravityAmountToAdd * customGravityAmountToAdd)));
            rb.AddForce(gravity);
        }
    }

    private void HandleHorizontalMovement()
    {
        // get axis value and create vector based off of set
        // speed and physics body's gravity scale
        horizontal = Input.GetAxisRaw("Horizontal");
        moveVector = new(horizontal * speed, 0f);

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

        // if we're not grounded, make the player a bit slower
        if (!isGrounded) {
            moveVector.x /= ungroundedSpeedDivisor;
        }

        rb.AddForce(moveVector * Time.fixedDeltaTime);
    }

    private void HandleDash() {
        // ensure the physics body's velocity is ~0
        bool isMoving = !(rb.velocity.x < (0 + float.Epsilon) && rb.velocity.x > (0 - float.Epsilon));

        // make sure player can dash if Left Shift is pressed and they're moving
        if (Input.GetKeyDown(KeyCode.LeftShift) && isMoving && dashCooldownExpired) {
            // add force to move player in the direction that they are facing
            Vector2 dashVector = new(dashSpeed * previousDirection, jumpForce / 2f);
            rb.AddForce(dashVector * Time.fixedDeltaTime, ForceMode2D.Impulse);

            StartCoroutine(ResetDashCooldown());
        }
    }

    private IEnumerator ResetDashCooldown() {
        dashCooldownExpired = false;
        yield return new WaitForSeconds(dashCooldown);
        dashCooldownExpired = true;
    }

    // When we enter a portal, add a force to make the transition
    // from one portal to another, feel "smooth."
    // This function is called from the Portal class when we
    // teleport the player.
    public void AddForceOnEnteringPortal() {
        rb.AddForce(enteredPortalSpeed * Time.fixedDeltaTime * rb.velocity);
        rb.AddForce(100f * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
       // add force in the direction that we're headed to make movement feel "smoother"
        if (distanceToGround < (col.size.y / 2) + 0.2) {    
            Vector2 vec = new(reGroundingMakeUp * horizontal * ((customGravityAmountToAdd + 1) * customGravityAmountToAdd) * 1.5f, 0f);
            rb.AddForce(vec * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        print($"Player collision with {col.gameObject.name}");
    }

    // boost player, for use from the FLIER
    public void BoostPlayer(float amount, Vector2 dir) {
        customGravityAmountToAdd = Math.Min(customGravityAmountToAdd / 2f, .5f);
        rb.AddForce(amount * Time.fixedDeltaTime * dir, ForceMode2D.Impulse);
    }
}
}