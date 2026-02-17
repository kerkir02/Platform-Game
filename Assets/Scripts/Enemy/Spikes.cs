using UnityEngine;

public class Spikes : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        speed = 0f;
        killPoints = 0;
        hitPoints = -7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
