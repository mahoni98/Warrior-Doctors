using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * 8f;
    }
}
