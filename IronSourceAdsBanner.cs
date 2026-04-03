using UnityEngine;
using Unity.Services.LevelPlay;
public class IronSourceAdsBanner : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    private bool isBannerCreated = false;

    public void CreateBannerAd(string id_banner_ads)
    {
        var adConfig = new LevelPlayBannerAd.Config.Builder()
            .SetSize(LevelPlayAdSize.BANNER)
            .SetPosition(LevelPlayBannerPosition.TopCenter)
            .SetDisplayOnLoad(true)
            .SetRespectSafeArea(true)
            .Build();

        bannerAd = new LevelPlayBannerAd(id_banner_ads, adConfig);
        isBannerCreated = true;
        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
    }

    public void LoadBannerAd()
    {
        if (!isBannerCreated || bannerAd == null) return;
        bannerAd.LoadAd();
    }

    public void ShowBannerAd()
    {
        if (!isBannerCreated || bannerAd == null) return;
        bannerAd.ShowAd();
    }

    public void HideBannerAd()
    {
        if (!isBannerCreated || bannerAd == null) return;
        bannerAd.HideAd();
    }

    public void DestroyBannerAd()
    {
        bannerAd?.DestroyAd();
        bannerAd = null;
        isBannerCreated = false;
    }

    public void PauseAutoRefresh()
    {
        if (!isBannerCreated || bannerAd == null) return;
        bannerAd.PauseAutoRefresh();
    }

    public void ResumeAutoRefresh()
    {
        if (!isBannerCreated || bannerAd == null) return;
        bannerAd.ResumeAutoRefresh();
    }

    public void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        this.ShowBannerAd();
    }
    public void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    public void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    public void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    public void BannerOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error) { }
    public void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) { }
    public void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) { }
    public void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) { }
}
