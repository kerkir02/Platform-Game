using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] float jumpForce;

    bool isJump;
    Vector2 jump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        killPoints = 7;
        hitPoints = -6;
        InvokeRepeating(nameof(Jump), 2f, 2f);
    }


    void Update()
    {
        enemyAnimator.SetBool("isJump", isJump);
    }
    private void Jump()
    {
        v.x = isMovingRight ? speed : -speed;
        enemySR.flipX = isMovingRight;
        isJump = true;
        v.y = jumpForce;
        //enemyRB.linearVelocity = v;
        enemyRB.AddForce(v, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJump = false;
            enemyRB.linearVelocity = Vector2.zero;

        }
    }
}
