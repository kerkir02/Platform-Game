using UnityEngine;

public class Saw : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        speed = 200f;
        killPoints = 0;
        hitPoints = -5;
    }

    void FixedUpdate()
    {
        EnemyMove();
    }

    protected override void EnemyMove()
    {
        enemyRB.angularVelocity = speed;
    }
}
