using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxXVelocity = 100;
    public float acceleration = 10;
    public float maxAcceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxMaxHoldJumpTime = 0.4f;

    public float holdJumpTimer = 0.0f;

    public bool isDead = false;
    public Animator animator;

    public float jumpGroundThreshold = 1;

    void Start()
    {

    }


    void Update()
    {
        if (isDead)
        {
            return;
        }

        Vector2 pos = transform.position;

        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            animator.SetBool("Jump", false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0.0f;
                animator.SetBool("Jump", true);
                
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
            animator.SetBool("Jump", true);

        }
    }

    public void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (isDead)
        {
            return;
        }

        if (pos.y < -20)
        {
            isDead = true;
        }

        if (!isGrounded)
        {
            animator.SetBool("Jump", true);

            if (isHoldingJump)
            {

                holdJumpTimer += Time.fixedDeltaTime;

                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;

            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;

            }

            Vector2 rayOrigin = new Vector2(pos.x + 0.6f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;

                    }
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }

                }
            }

        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            animator.SetBool("Jump", false);


            float velocityRatio = velocity.x / maxXVelocity;

            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;

            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }
            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);




        }

        Vector2 obstOrgin = new Vector2(pos.x, pos.y);

        RaycastHit2D obstHitx = Physics2D.Raycast(obstOrgin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        if (obstHitx.collider != null)
        {
            Obstcale obstcale = obstHitx.collider.GetComponent<Obstcale>();
            if (obstcale != null)
            {

                hitObstcale(obstcale);
            }
        }

        RaycastHit2D obstHity = Physics2D.Raycast(obstOrgin, Vector2.up, velocity.y * Time.fixedDeltaTime);
        if (obstHity.collider != null)
        {
            Obstcale obstcale = obstHity.collider.GetComponent<Obstcale>();
            if (obstcale != null)
            {

                hitObstcale(obstcale);
                
            }
        }


        transform.position = pos;

    }

    void hitObstcale(Obstcale obstcale)
    {
        Destroy(obstcale.gameObject);
        isDead = true;
        Destroy(gameObject);
        velocity.x *= 0.7f;

    }
}