using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [Header("Camera setings")]
    public Transform target;
    public float smooth;

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(target.position.x, 0, 218), transform.position.y, transform.position.z), Time.deltaTime * smooth);
        }
    }
}
