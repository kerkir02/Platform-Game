using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer playerSR;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;

    private float verticalInput;
    private float horizontalInput;
    private bool isInMove;
    private bool spaceDown;
    private bool isOnGround;
    private int jumpNumber;
    private Vector2 v;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
        spaceDown = false;
        jumpNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        PlayerAnimationMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
        }
    }

    void FixedUpdate()
    {
        PlayerMove();
        if (spaceDown && jumpNumber < 2)
        {
            PlayerJump();
        }
    }

    private void PlayerJump()
    {
        v = playerRB.linearVelocity;
        v.y = jumpForce;
        playerRB.linearVelocity = v;

        spaceDown = false;
        isOnGround = false;
        jumpNumber++;
    }

    private void PlayerMove()
    {
        v = playerRB.linearVelocity;
        v.x = horizontalInput * speed;
        playerRB.linearVelocity = v;
    }

    private void PlayerAnimationMove()
    {
        isInMove = horizontalInput != 0f;
        playerAnimator.SetBool("isInMove", isInMove);
        playerAnimator.SetBool("isOnGround", isOnGround);

        if (horizontalInput > 0f)
        {
            playerSR.flipX = false;
        }
        else if (horizontalInput < 0f)
        {
            playerSR.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isOnGround = true;
                    jumpNumber = 0;
                    break;
                }
            }
        }
    }
}
