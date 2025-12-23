using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : Singleton<AdManager>
{

    private BannerView banner;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private AppOpenAd appOpenAd;

    [Header("AdMob Test IDs")]
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";
    private string interID = "ca-app-pub-3940256099942544/1033173712";
    private string rewardID = "ca-app-pub-3940256099942544/5224354917";
    private string appOpenID = "ca-app-pub-3940256099942544/9257395921";

   

    void Start()
    {
        MobileAds.Initialize(init =>
        {
            LoadBanner();
            LoadInterstitial();
            LoadRewarded();
            LoadAppOpen();
        });
    }

    #region BANNER
    public void LoadBanner()
    {
        if (banner != null)
            banner.Destroy();

        banner = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
        banner.LoadAd(new AdRequest());
    }
    #endregion

    #region INTERSTITIAL
    public void LoadInterstitial()
    {
        InterstitialAd.Load(interID, new AdRequest(), (ad, error) =>
        {
            if (error != null) return;

            interstitial = ad;
            interstitial.OnAdFullScreenContentClosed += () =>
            {
                interstitial.Destroy();
                interstitial = null;
                LoadInterstitial();
            };
        });
    }

    public void ShowInterstitial()
    {
        if (interstitial != null && interstitial.CanShowAd())
            interstitial.Show();
    }
    #endregion

    #region REWARDED
    public void LoadRewarded()
    {
        RewardedAd.Load(rewardID, new AdRequest(), (ad, error) =>
        {
            if (error != null) return;

            rewardedAd = ad;
            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                rewardedAd.Destroy();
                rewardedAd = null;
                LoadRewarded();
            };
        });
    }

    public void ShowRewarded(Action onRewardAndClosed)
    {
        if (rewardedAd == null || !rewardedAd.CanShowAd())
            return;

        bool rewardEarned = false;

        rewardedAd.OnAdFullScreenContentClosed += () =>
        {
            if (rewardEarned)
            {
                onRewardAndClosed?.Invoke();
            }
        };

        rewardedAd.Show(reward =>
        {
            rewardEarned = true; 
        });
    }


    #endregion

    #region APP OPEN
    public void LoadAppOpen()
    {
        AppOpenAd.Load(appOpenID, new AdRequest(), (ad, error) =>
        {
            if (error != null)
            {
                Debug.Log("AppOpen Load Fail");
                return;
            }

            appOpenAd = ad;
            appOpenAd.OnAdFullScreenContentClosed += () =>
            {
                appOpenAd = null;
                LoadAppOpen();
            };
        });
    }

    public bool AppOpenAdReady()
    {
        return appOpenAd != null && appOpenAd.CanShowAd();
    }

    public void ShowAppOpen(Action onClosed)
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.OnAdFullScreenContentClosed += () =>
            {
                appOpenAd = null;
                LoadAppOpen();
                onClosed?.Invoke();
            };
            appOpenAd.Show();
        }
        else
        {
            onClosed?.Invoke();
        }
    }
    #endregion
}
