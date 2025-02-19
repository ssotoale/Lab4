using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private bool canFastFall = true; 
    private Vector3 originalScale; 
    private bool isSquishing = false; 
    private float distanceTraveled = 0f;
    public TMP_Text distanceText;
    private float backgroundSpeed = 0.4f;
    private int distanceGoal = 0;
    private int coinGoal = 0;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject restartText;
    private int currentLevel = 0;
    public GameObject explosion;
    public AudioSource audioSource;  
    public AudioClip jumpSound; 
    public AudioClip squishSound;
    public AudioClip coinSound;
    public AudioClip boomSound;

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale; // Save the original scale of the player
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default to level 1
        AdjustSpeedMultiplier(currentLevel);
    }

    void AdjustSpeedMultiplier(int level)
    {
        if (level == 1)
        {
            backgroundSpeed = 0.4f;
            distanceGoal = 200;
            coinGoal = 15;

        }
        else if (level == 2)
        {
            backgroundSpeed = 0.7f;
            distanceGoal = 300;
            coinGoal = 25;
        }
        else if (level == 3)
        {
            backgroundSpeed = 1f;
            distanceGoal = 500;
            coinGoal = 35;
        }
        else
        {
            backgroundSpeed = 1.4f;
            distanceGoal = 1000;
            coinGoal = 100;
        }
    }

    void Update()
    {

        distanceTraveled += backgroundSpeed * Time.deltaTime * 10f; // Multiply by 10 for better scaling
        distanceText.text = "Distance: " + Mathf.FloorToInt(distanceTraveled) + "m"; 

        if (distanceTraveled > distanceGoal)
        {
            if (coinCount > coinGoal)
            {
                winScreen.SetActive(true); 
                int highest = PlayerPrefs.GetInt("UnlockedLevel", 1);
                if (highest == currentLevel)
                {
                    PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                loseScreen.SetActive(true);
            }
            Time.timeScale = 0;
        }

        // Squish the player when S is held down and grounded
        if (Input.GetKey(KeyCode.S) && isGrounded && !isSquishing)
        {
            audioSource.PlayOneShot(squishSound);
            transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z); 
            isSquishing = true; // Prevent repeated scaling
        }
        else if (!Input.GetKey(KeyCode.S) && isSquishing)
        {
            transform.localScale = originalScale; // Restore normal size
            isSquishing = false;
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audioSource.PlayOneShot(jumpSound);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump"); 
            isGrounded = false;
            canFastFall = false; // Disable fast fall right after jumping
            Invoke("EnableFastFall", 0.2f); // Re-enable fast fall after 0.2 seconds
        }

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

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
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
            string coinName = collision.gameObject.name;
            audioSource.PlayOneShot(coinSound);
            Debug.Log(coinName);
            if (coinName == "Yerb(Clone)")
            {
                coinCount += 1; 
            }
            else if (coinName == "Taco(Clone)")
            {
                coinCount += 2;
            }
            else if (coinName == "Donut(Clone)")
            {
                coinCount += 3;
            }
            else 
            {
                coinCount += 5;
            }
            coinText.text = "Coins: " + coinCount;
            Debug.Log("Touching Coin");
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(boomSound);
            GameObject obstacle = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            restartText.SetActive(true);
            StartCoroutine(DelayedGameStop());
        }
    }

    // Coroutine to delay stopping the game
    IEnumerator DelayedGameStop()
    {
        yield return new WaitForSeconds(0.5f); // Adjust based on explosion animation length
        Time.timeScale = 0; // Stop the game AFTER animation plays
    }
}
