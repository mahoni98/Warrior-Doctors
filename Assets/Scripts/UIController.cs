using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIController : MonoBehaviour
{

    [SerializeField] private Transform PopUp;
    [SerializeField] private GameObject PopUpBack;

    [SerializeField] private Ease TypeOpen;
    [SerializeField] private Ease TypeClose;


    public void PopUpScale(bool Open)
    {
        if (!Open)
        {
            PopUp.DOScale(Vector3.zero, 0.5f).SetEase(TypeClose).OnComplete(() =>
            {
                PopUpBack.SetActive(false);
            });
        }
        else
        {
            PopUpBack.SetActive(true);
            PopUp.localScale = Vector3.zero;
            PopUp.DOScale(Vector3.one, 0.5f).SetEase(TypeOpen);
        }
    }

}
