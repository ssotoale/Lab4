using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;  
    public float fastFallMultiplier = 10f; 
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private int coinCount = 0;
    public TMP_Text coinText;
    private bool canFastFall = true; // Prevents instant fast fall after jumping

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump"); 
            isGrounded = false;
            canFastFall = false; // Disable fast fall right after jumping
            Invoke("EnableFastFall", 0.2f); // Re-enable fast fall after 0.2 seconds
        }

        // Fast fall when pressing S or Down Arrow
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isGrounded && canFastFall)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -fastFallMultiplier); 
        }

        // Restart Scene when pressing R
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        // Set Falling Animation
        if (rb.linearVelocity.y < -0.1f && !isGrounded)
        {
            animator.SetBool("IsFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("IsFalling", false);
        }
    }

    void EnableFastFall()
    {
        canFastFall = true; // Allows fast fall after delay
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.ResetTrigger("Jump"); 
            animator.SetTrigger("Land"); 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            coinCount += 1;
            coinText.text = "Coins: " + coinCount;
            Debug.Log("Touching Coin");
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
        }
    }
}
