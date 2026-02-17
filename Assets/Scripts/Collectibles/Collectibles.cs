using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public enum CollectType
    {
        Heart,
        Score,
        Power
    }

    public int value { get; protected set; }
    public CollectType type { get; protected set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
