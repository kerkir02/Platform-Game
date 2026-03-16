using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer playerSR;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    //[SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] private float hitPower = 5f;
    [SerializeField] private float hitTime = 0.1f;
    [SerializeField] private GameObject GameOverEffect;
    [SerializeField] List<GameObject> heartsList;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private GameObject nextLevelText;
    [SerializeField] private GameObject gameOverText;

    private float verticalInput;
    private float horizontalInput;
    private bool isInMove;
    private bool canMove;
    private bool spaceDown;
    private bool isOnGround;
    private bool isHit;
    private int jumpNumber;
    private Vector2 v;
    private Enemy enemy;
    private Collectibles collectible;

    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
        spaceDown = false;
        jumpNumber = 0;
        playerSR.enabled = true;
        lives = heartsList.Count;
        gameManager = GameManager.Instance;
        nextLevelText.SetActive(false);
        gameOverText.SetActive(false);
        canMove = true;
        scoreText.text = gameManager.ScoreUpdate(0);
    }

    // Handles input, animations and game over check each frame
    void Update()
    {
        GameOver();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        PlayerAnimationMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
        }
    }

    // Handles physics-based movement and jumping
    void FixedUpdate()
    {
        if (canMove)
        {
            PlayerMove();
            if (spaceDown && jumpNumber < 2)
            {
                PlayerJump();
            }
        }
    }

    // Applies vertical velocity to perform a jump
    private void PlayerJump()
    {
        v = playerRB.linearVelocity;
        v.y = jumpForce;
        playerRB.linearVelocity = v;

        spaceDown = false;
        isOnGround = false;
        jumpNumber++;
        gameManager.PlayJumpSound();
    }

    // Applies horizontal movement based on input
    private void PlayerMove()
    {
        v = playerRB.linearVelocity;
        v.x = horizontalInput * speed;
        playerRB.linearVelocity = v;
    }

    // Updates animator parameters and sprite direction
    private void PlayerAnimationMove()
    {
        isInMove = horizontalInput != 0f;
        playerAnimator.SetBool("isInMove", isInMove);
        playerAnimator.SetBool("isOnGround", isOnGround);
        playerAnimator.SetBool("isHit", isHit);

        if (horizontalInput > 0f)
        {
            playerSR.flipX = false;
        }
        else if (horizontalInput < 0f)
        {
            playerSR.flipX = true;
        }
    }

    // Handles collision with enemies and ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            enemy = collision.collider.GetComponent<Enemy>();
            if (isHit || enemy == null)
            {
                return;
            }

            if (enemy.killPoints == 0)
            {
                scoreText.text = gameManager.ScoreUpdate(enemy.hitPoints);
                GetHit(collision.transform);
                return;
            }

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    scoreText.text = gameManager.ScoreUpdate(enemy.killPoints);
                    enemy.DestroyEnemy();
                    gameManager.PlayHitSound();
                    return;
                }
            }

            scoreText.text = gameManager.ScoreUpdate(enemy.hitPoints);
            GetHit(collision.transform);
            return;
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

    // Handles trigger interaction with collectibles
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectibles") && !isHit)
        {
            collectible = other.GetComponent<Collectibles>();
            if (collectible == null)
            {
                return;
            }

            if (collectible.type == Collectibles.CollectType.Score)
            {
                scoreText.text = gameManager.ScoreUpdate(collectible.value);
                if (collectible.value == 5) gameManager.PlayCoinSound();
                else gameManager.PlayGemSound();
                Destroy(other.gameObject);
            }
            else if (collectible.type == Collectibles.CollectType.Heart && lives < heartsList.Count)
            {
                heartsList[lives].SetActive(true);
                lives++;
                gameManager.PlayHeartSound();
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("Door"))
        {
            gameManager.PlayWinSound();
            nextLevelText.SetActive(true);
            scoreText.gameObject.SetActive(false);
            canMove = false;
            finalScoreText.text = gameManager.ScoreUpdate(0);
            playerRB.linearVelocity = Vector2.zero;
        }
    }

    // Applies knockback, reduces life and starts hit effect
    private void GetHit(Transform enemy)
    {
        Vector2 knockback = new Vector2(Mathf.Sign(transform.position.x - enemy.position.x), 1f);

        playerRB.linearVelocity = Vector2.zero;
        playerRB.linearVelocity = knockback * hitPower;

        lives--;
        if(jumpNumber > 0)
        {
            jumpNumber--;
        }
        heartsList[lives].SetActive(false);
        isHit = true;
        isOnGround = false;
        gameManager.PlayHurtSound();
        Invoke(nameof(GetUnHit), 2f);
        UnFlashing();
    }

    // Resets hit state and stops flashing effect
    private void GetUnHit()
    {
        CancelInvoke(nameof(GetUnHit));
        CancelInvoke(nameof(UnFlashing));
        CancelInvoke(nameof(Flashing));
        isHit = false;
        playerSR.enabled = true;
    }

    // Enables sprite visibility and schedules hiding (flashing effect)
    private void Flashing()
    {
        CancelInvoke(nameof(Flashing));
        playerSR.enabled = true;
        if (isHit)
        {
            Invoke(nameof(UnFlashing), hitTime);
        }
    }

    // Disables sprite visibility and schedules showing (flashing effect)
    private void UnFlashing()
    {
        CancelInvoke(nameof(UnFlashing));
        playerSR.enabled = false;
        if (isHit)
        {
            Invoke(nameof(Flashing), hitTime);
        }
    }

    // Checks if player has no lives left and triggers game over
    private void GameOver()
    {
        if (lives <= 0)
        {
            scoreText.gameObject.SetActive(false);
            gameOverText.SetActive(true);
            gameOverScoreText.text = gameManager.ScoreUpdate(0);
            gameManager.PlayGameOverSound();
            Instantiate(GameOverEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
    /*
    // Updates score value and refreshes UI text
    private void ScoreUpdate(int value)
    {
        gameManager.score += value;
        if (gameManager.score < 0)
        {
            gameManager.score = 0;
        }
        scoreText.text = "Score:" + ScoreZeros() + gameManager.score;
    }

    // Generates leading zeros for score formatting
    private string ScoreZeros()
    {
        string zeros = "";
        for (int i = 6; i > Mathf.Abs(gameManager.score).ToString().Length; i--)
        {
            zeros += "0";
        }
        return zeros;
    }*/
}
