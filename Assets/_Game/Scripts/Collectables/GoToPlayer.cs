using DG.Tweening;
using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    private Transform target;
    private float moveSpeed = 20;
    private bool startMoving = false;
    public void GoPlayer()
    {
        Debug.Log("GOTO PLAYER");

        target = FindObjectOfType<PlayerController>().transform;
        Invoke("lateStartMove", 0.5f);
        //transform.DOJump(target.position, 1, 1, 1f);
        //transform.DOScale(Vector3.zero, 1f);
    }
    private void lateStartMove()
    {
        startMoving = true;
    }
    private void FixedUpdate()
    {
        if (!startMoving) return;
        transform.LookAt(target);
        transform.position += transform.forward * Time.deltaTime * moveSpeed;

    }
}
