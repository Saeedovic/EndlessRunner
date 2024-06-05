using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public float boostedJumpForce;
    private float originalJumpForce;
    private bool isJumpBoosted = false;
    [SerializeField] bool isGrounded = false;
    Rigidbody2D rb;
    public int coinCount = 0;
    public bool isDead = false;
    public bool doneReady = false;

    public ObstacleGenerator obstacleGenerator;
    public UIController uiController;
    private float startTime;
    private int jumpCount = 0;
    public int maxJumps = 2;

    public int distanceTraveled = 0;
    public float jumpBoostDuration = 5.0f;

    Animator animator;

    public void Awake()
    {
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalJumpForce = jumpForce;
    }

    public void Start()
    {

    }

    void Update()
    {

        if (!uiController.ready) return;

        if (doneReady == true)
        {
            
            startTime = Time.time;
            doneReady = false;
        }

        

        if (doneReady == false)
        {

            if (isDead) return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HandleJump();
            }

            if (Input.GetKey(KeyCode.Mouse0) && isJumpBoosted)
            {
                HandleContinuousJump();
            }

            distanceTraveled = (int)((Time.time - startTime) * obstacleGenerator.currentSpeed);
        }
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
        }
    }

    private void HandleContinuousJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            StartCoroutine(JumpBoost());
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator JumpBoost()
    {
        isJumpBoosted = true;
        jumpForce = boostedJumpForce;

        UIController uiController = GameObject.FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.StartJumpBoostUI(jumpBoostDuration);
        }

        yield return new WaitForSeconds(jumpBoostDuration);

        jumpForce = originalJumpForce;
        isJumpBoosted = false;
    }
}