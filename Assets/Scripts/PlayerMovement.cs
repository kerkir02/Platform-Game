using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer playerSR;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float hitPower = 5f;
    [SerializeField] private float hitTime = 0.1f;

    private float verticalInput;
    private float horizontalInput;
    private bool isInMove;
    private bool spaceDown;
    private bool isOnGround;
    private bool isHit;
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
        playerSR.enabled = true;
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
        playerAnimator.SetBool("isHit", isHit);

        //turn player
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
        //collision with enemy
        if (collision.collider.CompareTag("Enemy"))
        {
            if (isHit)
            {
                return;
            }
            //check is jumped on his head
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    Destroy(collision.collider.gameObject);
                    return;
                }
            }
            GetHit(collision.transform);           
        }
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

    private void GetHit(Transform enemy)
    {
        Vector2 knockback = new Vector2(Mathf.Sign(transform.position.x - enemy.position.x), 1f);

        playerRB.linearVelocity = Vector2.zero;
        playerRB.linearVelocity = knockback * hitPower;

        lives--;
        isHit = true;
        isOnGround = false;
        Invoke(nameof(GetUnHit), 2f);
        UnFlashing();
    }

    private void GetUnHit()
    {
        CancelInvoke(nameof(GetUnHit));
        CancelInvoke(nameof(UnFlashing));
        CancelInvoke(nameof(Flashing));
        isHit = false;
        playerSR.enabled = true;
    }
    //flashing with sprite renderer
    private void Flashing()
    {
        CancelInvoke(nameof(Flashing));
        playerSR.enabled = true;
        if (isHit)
        {
            Invoke(nameof(UnFlashing), hitTime);
        }
    }
    private void UnFlashing()
    {
        CancelInvoke(nameof(UnFlashing));
        playerSR.enabled = false;
        if (isHit)
        {
            Invoke(nameof(Flashing), hitTime);
        }
    }
}
