using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControler : MonoBehaviour
{
    int hitCount = 0;
    public void rockDestroy()
    {
        hitCount++;
        
        if(hitCount>4)
        {
            Destroy(this.gameObject);
        }
    }
}
