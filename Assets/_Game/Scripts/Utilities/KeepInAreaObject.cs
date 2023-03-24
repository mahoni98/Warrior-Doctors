using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInAreaObject : MonoBehaviour
{
    [SerializeField] private float spawnDistance = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrontCollider"))
        {
            transform.position -= transform.forward * spawnDistance;
        }
        if (other.CompareTag("BackCollider"))
        {
            transform.position += transform.forward * spawnDistance;
        }
        if (other.CompareTag("LeftCollider"))
        {
            transform.position += transform.right * spawnDistance;
        }
        if (other.CompareTag("RightCollider"))
        {
            transform.position -= transform.right * spawnDistance;
        }
    }
}
