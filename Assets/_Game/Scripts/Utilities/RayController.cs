using UnityEngine;

public class RayController : MonoBehaviour
{

    public LineRenderer target;
    [SerializeField] Transform targetTrans;
    private Material material;
    private void Awake()
    {
        material = target.material;
    }
    public void ChangeColor(Color color)
    {
        material.color = color;
    }
    private void Update()
    {
        target.SetPosition(0, transform.position);
        target.SetPosition(1, targetTrans.position);
    }
}
