using UnityEngine;
using Unity.Services.LevelPlay;
public class IronSourceAdsBanner : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    void CreateBannerAd() {
        // Create ad configuration - optional
        var adConfig = new LevelPlayBannerAd.Config.Builder()
            //.SetSize(LevelPlayAdSize.BANNER)
            .SetPlacementName("placementName")
           // .SetPosition(LevelPlayBannerPosition.BottomCenter)
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
        //Load the banner ad 
        bannerAd.LoadAd();
    }
    public void ShowBannerAd() {
        //Show the banner ad, call this method only if you turned off the auto show when you created this banner instance.
        bannerAd.ShowAd();
    }
    public void HideBannerAd() {
        //Hide banner
        bannerAd.HideAd();
    }
    public void DestroyBannerAd() {
        //Destroy banner
        bannerAd.DestroyAd();
    }
    //Implement BannAd Events
    public void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) {}
    public void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error){}
    public void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) {}
    public void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) {}
}
