using UnityEngine;
using UnityEngine.Events;
using Unity.Services.LevelPlay;

public class IronSourceAds : MonoBehaviour
{
    [Header("Config General")]
    public int count_step_show_interstitial = 5;
    private int count_step = 0;

    [Header("Emplement Ui Ads")]
    public GameObject[] emplement_Ads;

    [Header("Config IronSource")]
    public string app_key;
    public string id_banner;
    public string id_video;
    public string id_rewarded;
    public UnityAction onRewardedSuccess;
    public IronSourceAdsBanner bannerAd;
    public IronSourceAdsInterstitial interstitialAd;
    public IronSourceRewarded RewardedAd;
    private bool is_ads = false;

    public void On_Load()
    {
        if (PlayerPrefs.GetInt("is_ads", 0) == 0)
            this.is_ads = true;
        else
            this.is_ads = false;

        if (this.is_ads)
        {
            LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
            LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
            LevelPlay.Init(app_key);
        }
        this.Check_Emplement_Ads();
    }
    void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        LevelPlay.LaunchTestSuite();
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: "+ config);
        bannerAd.CreateBannerAd(id_banner);
        bannerAd.LoadBannerAd();
        interstitialAd.CreateInterstitialAd(id_video);
        RewardedAd.CreateRewardedAd(id_rewarded);
        RewardedAd.onRewardedSuccess = onRewardedSuccess;
    }
    
    void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.Log("unity-script: I got SdkInitializationFailedEvent with error: "+ error);
    }

    void OnApplicationPause(bool isPaused)
    {
        //IronSource.Agent.onApplicationPause(isPaused);
    }

    private void Check_Emplement_Ads()
    {
        if (this.emplement_Ads.Length != 0)
        {
            for (int i = 0; i < this.emplement_Ads.Length; i++)
            {
                if (this.is_ads)
                    this.emplement_Ads[i].SetActive(true);
                else
                    this.emplement_Ads[i].SetActive(false);
            }
        }
    }

    public void ShowBannerAd()
    {
        if (bannerAd != null&&is_ads) bannerAd.ShowBannerAd();
    }
    public void HideBannerAd()
    {
        if (bannerAd != null&&is_ads) bannerAd.HideBannerAd();
    }
    
    public void DestroyBannerAd()
    {
        if (bannerAd != null&&is_ads) bannerAd.DestroyBannerAd();
    }

    public void ShowRewardedVideo()
    {
        if(is_ads) RewardedAd.ShowRewardedAd();
    }

    public void show_ads_Interstitial()
    {
        if(this.is_ads){
            this.count_step++;
            if (this.count_step > this.count_step_show_interstitial)
            {
                this.count_step=0;
                this.ShowInterstitialAd();
            }
        }
    }

    public void ShowInterstitialAd()
    {
        if(is_ads) interstitialAd.ShowInterstitialAd();
    }

    public void RemoveAds()
    {
        this.HideBannerAd();
        PlayerPrefs.SetInt("is_ads", 1);
        this.is_ads = false;
        this.Check_Emplement_Ads();
    }

    public bool get_status_ads()
    {
        return this.is_ads;
    }

    private void OnDisable()
    {
        bannerAd?.DestroyBannerAd();
    }
}


