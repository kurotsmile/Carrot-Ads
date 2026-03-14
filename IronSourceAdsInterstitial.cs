using Unity.Services.LevelPlay;
using UnityEngine;

public class IronSourceAdsInterstitial : MonoBehaviour
{
    private LevelPlayInterstitialAd interstitialAd;
    public void CreateInterstitialAd(string id_ads_Inters) {
        interstitialAd = new LevelPlayInterstitialAd(id_ads_Inters);

        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;
    }
    public void LoadInterstitialAd() {
        interstitialAd?.LoadAd();
    }

    public bool ShowInterstitialAd() {
        if (interstitialAd == null) return false;
        if (!interstitialAd.IsAdReady()) return false;

        interstitialAd.ShowAd();
        return true;
    }
  
    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error)
    {
        this.LoadInterstitialAd();
    }

    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        this.LoadInterstitialAd();
    }

    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
}
