using UnityEngine;
using Unity.Services.LevelPlay;
public class IronSourceAdsBanner : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    void CreateBannerAd() {
        // Create ad configuration - optional
        var adConfig = new LevelPlayBannerAd.Config.Builder()
            .SetSize(LevelPlayAdSize.BANNER)
            .SetPlacementName("placementName")
            .SetPosition(LevelPlayBannerPosition.BottomCenter)
            .SetDisplayOnLoad(true)
            .SetRespectSafeArea(true)
            .Build();
        
        // Create banner instance
        bannerAd = new LevelPlayBannerAd("bannerAdUnitId", adConfig);
        // Subscribe BannerAd events
        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
    }
    public void LoadBannerAd() {
        bannerAd.LoadAd();
    }
    public void ShowBannerAd() {
        bannerAd.ShowAd();
    }
    public void HideBannerAd() {
        bannerAd.HideAd();
    }
    public void DestroyBannerAd() {
        bannerAd.DestroyAd();
    }
    
    public void PauseAutoRefresh() {
        bannerAd.PauseAutoRefresh();
    }
    public void ResumeAutoRefresh() {
        bannerAd.ResumeAutoRefresh();
    }

    public void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    public void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) {}
    public void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error){}
    public void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) {}
}
