using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public float boostedJumpForce;
    private float originalJumpForce;
    [SerializeField] bool isGrounded = false;
    Rigidbody2D rb;
    public int coinCount = 0;
    public bool isDead = false;

    public ObstacleGenerator obstacleGenerator;
    private float startTime;
    private int jumpCount = 0;
    public int maxJumps = 2;

    public int distanceTraveled = 0;
    public float jumpBoostDuration = 5.0f;
    
    Animator animator;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalJumpForce = jumpForce;
    }

    public void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (isDead) return; 

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleJump();
        }

        distanceTraveled = (int)((Time.time - startTime) * obstacleGenerator.currentSpeed);
        Debug.Log("Distance Traveled: " + distanceTraveled);
    }

    private void HandleJump()
    {
        if (isGrounded || jumpCount < maxJumps)
        {
            animator.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
            jumpCount++;
            if (isGrounded)
            {
                isGrounded = false;
            }
            Debug.Log("Jumping with force: " + jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
            jumpCount = 0;
            Debug.Log("Landed on the ground");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(collision.gameObject);
            Debug.Log("Collected Coins: " + coinCount);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            Destroy(gameObject);
            Debug.Log("Player is Dead");
        }

        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            StartCoroutine(JumpBoost());
            Destroy(collision.gameObject);
            Debug.Log("Jump Boost Collected");
        }
    }

    public IEnumerator JumpBoost()
    {
        Debug.Log("Jump boost started. Boosted jump force: " + boostedJumpForce);
        jumpForce = boostedJumpForce;
        yield return new WaitForSeconds(jumpBoostDuration);
        jumpForce = originalJumpForce;
        Debug.Log("Jump boost ended. Reverted jump force: " + originalJumpForce);
    }
}
