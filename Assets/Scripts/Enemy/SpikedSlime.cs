using UnityEngine;

public class SpikedSlime : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        killPoints = 0;
        hitPoints = -5;
    }


    void FixedUpdate()
    {
        EnemyMove();
    }
}
