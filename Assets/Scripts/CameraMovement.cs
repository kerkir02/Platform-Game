using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform hearts;

    private Vector3 heartsStartPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heartsStartPosition = new Vector3(7.5f, 4f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
        HeartsFollow();
    }

    private void CameraFollow()
    {
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z);
    }
    private void HeartsFollow()
    {
        hearts.position = new Vector3(
            player.position.x + heartsStartPosition.x,
            player.position.y + heartsStartPosition.y,
            hearts.position.z);
    }
}
