using UnityEngine;

public class Coin : Collectibles
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        value = 5;
        type = CollectType.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
