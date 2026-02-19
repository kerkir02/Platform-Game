using UnityEngine;

public class Bee : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        killPoints = 0;
        hitPoints = -4;
    }

    void FixedUpdate()
    {
        EnemyMove();
    }
}
