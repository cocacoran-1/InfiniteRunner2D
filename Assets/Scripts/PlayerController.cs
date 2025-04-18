using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isJumping;

    Rigidbody2D playerRb;
    BoxCollider2D playerCollider;
    SpriteRenderer playerSprite;
    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Flip();
        Move();
        Jump();
        Fall();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            playerAnimator.SetBool("isFall", false);
            playerAnimator.SetBool("isJump", false);
        }
    }

    /*void Flip()
    {
        if (horizontalInput < 0)
        {
            playerSprite.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            playerSprite.flipX = false;
        }
    }*/
    void Move()
    {

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        playerAnimator.SetBool("isRun", true);
        
    }
    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            isJumping = true;
            playerAnimator.SetBool("isJump", true);
        }
    }
    void Fall()
    {
        if (playerRb.velocity.y < 0)
        {
            playerAnimator.SetBool("isFall", true);
            playerAnimator.SetBool("isJump", false);
        }
    }
}
