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
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] private float hitPower = 5f;
    [SerializeField] private float hitTime = 0.1f;
    [SerializeField] private GameObject GameOverEffect;
    [SerializeField] List<GameObject> heartsList;
    [SerializeField] private TMP_Text scoreText;

    private float verticalInput;
    private float horizontalInput;
    private bool isInMove;
    private bool spaceDown;
    private bool isOnGround;
    private bool isHit;
    private int jumpNumber;
    private Vector2 v;
    private Enemy enemy;
    private Collectibles collectible;
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
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
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
            enemy = collision.collider.GetComponent<Enemy>();
            if (isHit || enemy == null)
            {
                return;
            }
            if (enemy.killPoints == 0)
            {
                ScoreUpdate(enemy.hitPoints);
                GetHit(collision.transform);
                return;
            }
            //check is jumped on his head
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    ScoreUpdate(enemy.killPoints);
                    enemy.DestroyEnemy();
                    return;
                }
            }
            ScoreUpdate(enemy.hitPoints);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectibles") && !isHit)
        {
            collectible = other.GetComponent<Collectibles>();
            if (collectible == null)
            {
                return;
            }
            if (collectible.value != 0)
            {
                ScoreUpdate(collectible.value);
                Destroy(other.gameObject);
            }
            else if (collectible.value == 0 && lives < heartsList.Count)
            {
                heartsList[lives].SetActive(true);
                lives++;
                Destroy(other.gameObject);
            }
        }
    }

    private void GetHit(Transform enemy)
    {
        Vector2 knockback = new Vector2(Mathf.Sign(transform.position.x - enemy.position.x), 1f);

        playerRB.linearVelocity = Vector2.zero;
        playerRB.linearVelocity = knockback * hitPower;

        lives--;
        heartsList[lives].SetActive(false);
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

    private void GameOver()
    {
        if(lives <= 0)
        {
            Instantiate(GameOverEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void ScoreUpdate(int value)
    {
        score += value;
        if(score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score:" + ScoreZeros() + score;
    }
    private string ScoreZeros()
    {
        string zeros = "";
        for(int i = 6; i > Mathf.Abs(score).ToString().Length; i--)
        {
            zeros += "0";
        }
        return zeros;
    }
}
