using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
public class GoogleBanner : MonoBehaviour
{
    private BannerView bannerView;
    [SerializeField] private AdPosition BannerPos;
    public void reklamAc()
    {
            MobileAds.Initialize(initStatus => { });
            this.RequestBanner();
    }
    private void RequestBanner()
    {
            string id = AdsManager.instance.SetAdsId(AdsManager.AdsType.Banner);
            this.bannerView = new BannerView(id, AdSize.Banner, BannerPos);
            this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
            this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            this.bannerView.OnAdOpening += this.HandleOnAdOpened;
            this.bannerView.OnAdClosed += this.HandleOnAdClosed;
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerView.LoadAd(request);
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
    }
    public void ReklamKapat()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView.Hide();
        }
    }
}
