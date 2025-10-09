using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.Events;

public class IronSourceRewarded : MonoBehaviour
{
    private LevelPlayRewardedAd RewardedAd;
    private bool isRewarded = false;
    public UnityAction onRewardedSuccess;
    public void CreateRewardedAd(string id_rewarded_ads) {
        RewardedAd= new LevelPlayRewardedAd(id_rewarded_ads);
        RewardedAd.OnAdLoaded += RewardedOnAdLoadedEvent;
        RewardedAd.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        RewardedAd.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        RewardedAd.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
        RewardedAd.OnAdClicked += RewardedOnAdClickedEvent;
        RewardedAd.OnAdClosed += RewardedOnAdClosedEvent;
        RewardedAd.OnAdRewarded += RewardedOnAdRewarded; 
        RewardedAd.OnAdInfoChanged += RewardedOnAdInfoChangedEvent;
    }
    public void LoadRewardedAd() {  
        RewardedAd.LoadAd();
    }
    public void ShowRewardedAd() {
        if (RewardedAd.IsAdReady()) {
            RewardedAd.ShowAd();
            isRewarded = false;
        }
    }
    
    void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void RewardedOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error){}
    void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        if (isRewarded)
        {
            onRewardedSuccess?.Invoke();
        }
    }
    void RewardedOnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward adReward)
    {
        isRewarded=true;
    } 
    void RewardedOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }  
}

