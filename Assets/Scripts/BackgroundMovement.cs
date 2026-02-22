using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float maxDistance = 20;


    // Update is called once per frame
    void LateUpdate()
    {
        BackgroundFollow();
    }
    private void BackgroundFollow()
    {
        //      15                         -5                       20
        if(transform.position.x - player.transform.position.x > maxDistance)
        {
            transform.position = new Vector3(
                transform.position.x - 35,
                transform.position.y,
                transform.position.z);
        }          
        else if (transform.position.x - player.transform.position.x < -maxDistance)
        {
            transform.position = new Vector3(
                transform.position.x + 35,
                transform.position.y,
                transform.position.z);
        }
    }
}
