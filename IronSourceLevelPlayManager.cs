using System;
using UnityEngine;
using Unity.Services.LevelPlay;

public class LevelPlayAdsManager : MonoBehaviour
{
    [Header("IronSource App Key (Dashboard)")]
    public string appKey = "your_app_key_here";

    // Rewarded / Interstitial / Banner
    private LevelPlayRewardedAd rewardedAd;
    private LevelPlayInterstitialAd interstitialAd;
    private LevelPlayBannerAd bannerAd;

    // Banner config
    public string bannerAdUnitId = "your_banner_unit_id";
    public string rewardedAdUnitId = "your_rewarded_unit_id";
    // Interstitial AdUnit ID
    public string interstitialAdUnitId = "your_interstitial_unit_id";

    public LevelPlayAdSize bannerSize = LevelPlayAdSize.BANNER;
    public LevelPlayBannerPosition bannerPosition = LevelPlayBannerPosition.TopCenter;
    public LevelPlayBannerAd.Config bannerAdsConfig;
    // Rewarded AdUnit ID (copy từ Dashboard)


    void Awake()
    {
        Debug.Log("Initializing LevelPlay...");

        // Đăng ký callback Init
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed  += OnInitFailed;

        // Init (KHÔNG truyền adUnits nữa)
        LevelPlay.Init(appKey);
    }

    private void OnDestroy()
    {
        LevelPlay.OnInitSuccess -= OnInitSuccess;
        LevelPlay.OnInitFailed  -= OnInitFailed;
    }

    #region --- Init Callbacks ---
    private void OnInitSuccess(LevelPlayConfiguration config)
    {
        Debug.Log($"[LevelPlay] Init Success");
        InitRewarded();
        InitInterstitial();
        InitBanner();
    }

    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError("[LevelPlay] Init Failed: " + error.ErrorMessage);
    }
    #endregion

    #region --- Rewarded ---
    private void InitRewarded()
    {
        rewardedAd = new LevelPlayRewardedAd(rewardedAdUnitId);

        rewardedAd.OnAdLoaded += (adInfo) => Debug.Log("[Rewarded] Loaded");
        rewardedAd.OnAdLoadFailed += (error) => Debug.LogError("[Rewarded] Load Failed: " + error.ErrorMessage);
        rewardedAd.OnAdDisplayed += (adInfo) => Debug.Log("[Rewarded] Displayed");
        rewardedAd.OnAdClosed += (adInfo) => Debug.Log("[Rewarded] Closed");
        rewardedAd.OnAdRewarded += (adInfo, reward) =>{
            Debug.Log($"[Rewarded] User Rewarded! Reward: {reward.Name} x {reward.Amount}");
        };

        rewardedAd.LoadAd();
    }

    public void ShowRewarded(string placement = null)
    {
        if (rewardedAd != null && rewardedAd.IsAdReady())
        {
            if (!string.IsNullOrEmpty(placement))
            {
                if (!LevelPlayRewardedAd.IsPlacementCapped(placement))
                {
                    rewardedAd.ShowAd(placement);
                }
                else
                {
                    Debug.LogWarning("[Rewarded] Placement capped: " + placement);
                }
            }
            else
            {
                rewardedAd.ShowAd();
            }
        }
        else
        {
            Debug.LogWarning("[Rewarded] Not ready yet or rewardedAd is null");
        }
    }

    #endregion

    #region --- Interstitial ---
    private void InitInterstitial()
    {
        interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);

        interstitialAd.OnAdLoaded += (adInfo) => Debug.Log("[Interstitial] Loaded");
        interstitialAd.OnAdLoadFailed += (error) => Debug.LogError("[Interstitial] Load Failed: " + error.ErrorMessage);
        interstitialAd.OnAdDisplayed += (adInfo) => Debug.Log("[Interstitial] Displayed");
        interstitialAd.OnAdClosed += (adInfo) => Debug.Log("[Interstitial] Closed");

        interstitialAd.LoadAd();
    }

    public void ShowInterstitial(string placement)
    {
        if (rewardedAd != null && rewardedAd.IsAdReady())
        {
            if (!string.IsNullOrEmpty(placement))
            {
                if (!LevelPlayInterstitialAd.IsPlacementCapped(placement))
                {
                   interstitialAd.ShowAd();
                }
                else
                {
                    Debug.LogWarning("[Interstitial] Placement capped: " + placement);
                }
            }
            else
            {
                rewardedAd.ShowAd();
            }
        }
        else
        {
            Debug.LogWarning("[Interstitial] Not ready yet");
        }
    }
    #endregion

    #region --- Banner ---
    private void InitBanner()
    {
        //bannerAd = new LevelPlayBannerAd(bannerAdUnitId, bannerSize, bannerPosition, "sds", true, false);
        bannerAd.OnAdLoaded += (adInfo) => Debug.Log("[Banner] Loaded");
        bannerAd.OnAdLoadFailed += (error) => Debug.LogError("[Banner] Load Failed: " + error.ErrorMessage);
        bannerAd.OnAdClicked += (adInfo) => Debug.Log("[Banner] Clicked");

        bannerAd.LoadAd();
    }

    public void ShowBanner()
    {
        if (bannerAd != null)
        {
            bannerAd.ShowAd();
        }
    }

    public void HideBanner()
    {
        if (bannerAd != null)
        {
            bannerAd.HideAd();
        }
    }
    #endregion

    #region --- Offerwall ---
    public void ShowOfferwall()
    {
        /*
        if (IronSource.Agent.isOfferwallAvailable())
        {
            IronSource.Agent.showOfferwall();
        }
        else
        {
            Debug.Log("[Offerwall] Not available yet");
        }
        */
    }
    #endregion
}
