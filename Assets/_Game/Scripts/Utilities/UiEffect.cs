using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UiEffect : MonoBehaviour
{
    public string effectName;

    [SerializeField] TextMeshProUGUI myText;

    public float effectTiming = 0.6f;

    private IEnumerator Start()
    {
        myText.text = effectName;
        transform.DOScale(1.5f, effectTiming / 2f).SetUpdate(UpdateType.Late).OnComplete(() =>
        {
            transform.DOScale(1f, effectTiming / 2f);
        });
        yield return new WaitForSeconds(effectTiming);
        Destroy(this.gameObject);
    }
}
