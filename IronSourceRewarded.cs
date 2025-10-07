using Unity.Services.LevelPlay;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private LevelPlayRewardedAd RewardedAd;
    public void CreateRewardedAd() {
        RewardedAd= new LevelPlayRewardedAd("RewardedAdUnitId");
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
        }
    }
    
    void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void RewardedOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error){}
    void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward adReward){} 
    void RewardedOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }  
}

