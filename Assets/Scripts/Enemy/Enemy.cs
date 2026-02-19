using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    public int killPoints { get; protected set; }
    public int hitPoints { get; protected set; }

    protected bool isMovingRight;
    protected Vector2 v;

    protected Rigidbody2D enemyRB;
    protected SpriteRenderer enemySR;
    protected Animator enemyAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySR = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        isMovingRight = true;
    }

    // Handles horizontal movement and sprite flipping based on direction
    protected virtual void EnemyMove()
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

    // Reverses movement direction when exiting its own border trigger
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            if (other.transform.parent != transform)
                return;

            isMovingRight = !isMovingRight;

            v = transform.position;
            v.x += isMovingRight ? 0.1f : -0.1f;
            transform.position = v;
        }
    }

    // Plays death animation, disables physics and schedules destruction
    public virtual void DestroyEnemy()
    {
        enemyAnimator.SetBool("isHit", true);
        enemyRB.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        Invoke(nameof(DestroyThisEnemy), 1f);
    }

    // Permanently removes the enemy from the scene
    protected virtual void DestroyThisEnemy()
    {
        Destroy(gameObject);
    }
}
