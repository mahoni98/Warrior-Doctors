using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Destoryer : MonoBehaviour
{
    public bool isFade = false;
    public bool isScale = false;

    public float destroyTiming = 2f;
    
    IEnumerator Start()
    {
        if (isFade)
        {
            var mat = GetComponent<MeshRenderer>().material;
            mat.DOFade(0, 1.5f);
        }
        if (isScale)
        {
            transform.DOScale(transform.localScale * 1.1f, 1.5f);
        }
        yield return new WaitForSeconds(destroyTiming);
        Destroy(this.gameObject);
    }
}
