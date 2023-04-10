using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GoogleInterstitial : MonoBehaviour
{
    public InterstitialAd inter;

    private void Start()
    {
        Requestinter();
    }
    public void Requestinter()
    {
        string id = AdsManager.instance.SetAdsId(AdsManager.AdsType.Instertitial);
        this.inter = new InterstitialAd(id);
        this.inter.OnAdClosed += HandleOnAdClosed;
        this.inter.OnAdLoaded += HandleOnAdLoaded;
        this.inter.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.inter.OnAdOpening += HandleOnAdOpened;
        AdRequest request = new AdRequest.Builder().Build();
        this.inter.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {

    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        AfterSeeInter();
    }
    public void InterShow()
    {
        if (inter != null && inter.CanShowAd())
        {
            inter.Show();
        }
        else
        {
            AfterSeeInter();
        }
    }

    private void AfterSeeInter()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            Requestinter();
        }
    }
}
