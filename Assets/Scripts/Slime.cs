using UnityEngine;

public class Slime : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        speed = 2f;
        killPoints = 1;
        hitPoints = -3;
    }

    
    void FixedUpdate()
    {
        EnemyMove();
    }
}
