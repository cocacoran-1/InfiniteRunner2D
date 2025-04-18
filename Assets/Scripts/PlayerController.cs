using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed = 10f;

    [SerializeField] float baseSpeed = 10f;
    [SerializeField] float speedGainPerTick = 0.1f;
    [SerializeField] float maxSpeed = 15f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;

    Rigidbody2D playerRb;
    BoxCollider2D playerCollider;
    SpriteRenderer playerSprite;
    Animator playerAnimator;

    public bool run = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();

        isGrounded = true;
    }
    void Update()
    {
        CheckGround();
        Move();
        Jump();
        Fall();
    }
    void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (run)
        {
            playerAnimator.SetBool("isRun", true);
        }
        else
        {
            playerAnimator.SetBool("isRun", false);
        }
    }
    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnimator.SetBool("isJump", true);
        }
    }
    void Fall()
    {
        if (!isGrounded && playerRb.velocity.y < 0)
        {
            playerAnimator.SetBool("isFall", true);
            playerAnimator.SetBool("isJump", false);
        }
    }
    void CheckGround()
    {
        Vector2 origin = new Vector2(transform.position.x, 
            transform.position.y - playerCollider.bounds.extents.y - 0.1f);
        Vector2 direction = Vector2.down;
        float distance = 0.4f;
        Debug.DrawRay(origin, direction * distance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, LayerMask.GetMask("Ground"));
        bool wasGrounded = isGrounded;
        isGrounded = hit.collider != null;

        if(!wasGrounded && isGrounded)
        {
            playerAnimator.SetBool("isFall", false);
            playerAnimator.SetBool("isRun", run);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            GameManager.Instance.AddScore(1);
            collision.gameObject.SetActive(false);
        }
    }
    public void ApplyDifficulty(float difficultyTick)
    {
        moveSpeed = Mathf.Min(baseSpeed + difficultyTick * speedGainPerTick, maxSpeed);
    }
}
