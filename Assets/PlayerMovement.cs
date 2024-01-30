using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float horizontal;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        HandleHorizontalMovement();
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        // TODO: Ground check
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rb.AddForce(new Vector2(0f, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
    }

    private void HandleHorizontalMovement()
    {
        horizontal = Mathf.Round(Input.GetAxisRaw("Horizontal") * 2f) / 2f;

        Vector2 moveVector = new(horizontal * speed * rb.gravityScale, 0f);
        rb.AddForce(moveVector * Time.fixedDeltaTime);
    }
}