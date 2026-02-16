using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2;

    private bool isMovingRight;
    private Vector2 v;

    private Rigidbody2D enemyRB;
    private SpriteRenderer enemySR;
    private Animator enemyAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySR = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        isMovingRight = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        EnemyMove();
    }
    //enemy move and flip
    private void EnemyMove()
    {
        v = enemyRB.linearVelocity;
        if (isMovingRight)
        {
            v.x = speed;
            enemySR.flipX = true;
        }
        else
        {
            v.x = -speed;
            enemySR.flipX = false;
        }
        enemyRB.linearVelocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Border"))
        {
            isMovingRight = !isMovingRight;

            Vector2 pos = transform.position;
            pos.x += isMovingRight ? 0.1f : -0.1f;
            transform.position = pos;
            return;
        }
    }

    public void DestroyEnemy()
    {
        enemyAnimator.SetBool("isHit", true);
        enemyRB.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        Invoke(nameof(DestroyThisEnemy), 1f);
    }

    private void DestroyThisEnemy()
    {
        Destroy(gameObject);
    }
}
