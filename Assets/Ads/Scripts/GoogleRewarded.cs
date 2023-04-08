
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
public class GoogleRewarded : MonoBehaviour
{
    private RewardedAd rewardedAd;

    public void Start()
    {
        //CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
        string adUnitId = AdsManager.instance.SetAdsId(AdsManager.AdsType.Rewarded);

        this.rewardedAd = new RewardedAd(adUnitId);
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //UserChoseToWatchAd();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //CreateAndLoadRewardedAd();
        //AdsController.Instance?._AdsPanelThrow.AdsPanel.SetActive(false);
        //AdsControllerFace.Instance?._AdsPanelThrow.AdsPanel.SetActive(false);
        CloseRewardedPanel();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //CreateAndLoadRewardedAd();
        //AdsController.Instance?._AdsPanelThrow.AdsPanel.SetActive(false);
        //AdsControllerFace.Instance?._AdsPanelThrow.AdsPanel.SetActive(false);
        CloseRewardedPanel();

    }
    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            CloseRewardedPanel();
            //UserChoseToWatchAd();
        }
    }
    public void CloseRewardedPanel()
    {

    }
}
