using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float maxDistance = 20;
    [SerializeField] float yDis = 1;
    [SerializeField] float xDis = 35;
    [SerializeField] float yBorder = -8;

    // Update is called once per frame
    void LateUpdate()
    {
        BackgroundFollow();
    }
    private void BackgroundFollow()
    {
        if(transform.position.x - player.transform.position.x > maxDistance)
        {
            if (player.transform.position.y > yBorder)
            {
                transform.position = new Vector3(
                    transform.position.x - xDis,
                    player.transform.position.y + yDis,
                    transform.position.z);
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x - xDis,
                    yBorder + yDis,
                    transform.position.z);
            }
        }          
        else if (transform.position.x - player.transform.position.x < -maxDistance)
        {
            if (player.transform.position.y > yBorder)
            {
                transform.position = new Vector3(
                    transform.position.x + xDis,
                    player.transform.position.y + yDis,
                    transform.position.z);
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x + xDis,
                    yBorder + yDis,
                    transform.position.z);
            }
        }
        else
        {
            if (player.transform.position.y > yBorder)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    player.transform.position.y + yDis,
                    transform.position.z);
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x,
                    yBorder + yDis,
                    transform.position.z);
            }
        }
    }
}
