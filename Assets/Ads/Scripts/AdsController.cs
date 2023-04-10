using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AdsController : MonoBehaviour
{
    private GoogleInterstitial _GoogleInterstitial;
    private GoogleBanner _GoogleBanner;




    [Header("Banner")]
    [SerializeField] private bool Show;

    public static AdsController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) { Destroy(gameObject); }
    }

    private void Start()
    {
        _GoogleInterstitial = GetComponent<GoogleInterstitial>();
        _GoogleBanner = GetComponent<GoogleBanner>();
        if (Show)
        {
            BannerShow();
        }
    }
    public void ShowInter()
    {
        _GoogleInterstitial.InterShow();
    }

    public void BannerShow()
    {
        _GoogleBanner.reklamAc();
    }
    public void RewardedPrepare()
    {
    }
    public void HideBanner()
    {
        _GoogleBanner.ReklamKapat();
    }
}