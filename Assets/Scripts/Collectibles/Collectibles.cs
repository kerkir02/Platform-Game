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
}
