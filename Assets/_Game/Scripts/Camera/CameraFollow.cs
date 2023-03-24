using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * 4f);
    }
}
