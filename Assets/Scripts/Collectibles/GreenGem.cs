using UnityEngine;

public class GreenGem : Collectibles
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        value = 10;
        type = CollectType.Score;
    }
}
