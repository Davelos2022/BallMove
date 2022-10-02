using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    //Attaching the camera to an object

    [Header("Camera setings")]
    public Transform target; 
    public float smooth; //smoothness of movement

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(target.position.x, 0, 218), transform.position.y, transform.position.z), Time.deltaTime * smooth);
        }
    }
}
