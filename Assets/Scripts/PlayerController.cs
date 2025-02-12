using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;  // Jump strength
    public float fastFallMultiplier = 2f; // Speed multiplier when falling
    private Rigidbody2D rb;
    private bool isGrounded;
    private int coinCount = 0;
    public TMP_Text coinText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Jump if on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Fast fall when pressing S or Down Arrow
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.linearVelocity += Vector2.down * fastFallMultiplier * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If player touches ground, allow jumping again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Coins"))
        {
            coinCount += 1;
            coinText.text = "Coins: " + coinCount;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
