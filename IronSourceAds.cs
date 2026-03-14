using UnityEngine;
using UnityEngine.Events;
using Unity.Services.LevelPlay;

public class IronSourceAds : MonoBehaviour
{
    [Header("Config General")]
    public int count_step_show_interstitial = 5;
    public float min_seconds_between_interstitial = 90f;
    private int count_step = 0;
    private float last_interstitial_show_time = float.NegativeInfinity;

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
    private bool is_initialized = false;
    private bool is_sdk_ready = false;

    private void Start()
    {
        this.On_Load();
    }

    public void On_Load()
    {
        this.RefreshAdsState();

        if (this.is_initialized) return;
        this.is_initialized = true;

        if (!this.is_ads) return;
        if (!this.CanInitializeAds()) return;

        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
        LevelPlay.Init(app_key);
    }

    void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: "+ config);
        this.is_sdk_ready = true;
        if (bannerAd == null || interstitialAd == null || RewardedAd == null)
        {
            Debug.LogWarning("IronSourceAds is missing ad components on Carrot_Ads.");
            return;
        }

        bannerAd.CreateBannerAd(id_banner);
        bannerAd.LoadBannerAd();
        interstitialAd.CreateInterstitialAd(id_video);
        interstitialAd.LoadInterstitialAd();
        RewardedAd.CreateRewardedAd(id_rewarded);
        RewardedAd.onRewardedSuccess = this.onRewardedSuccess;
        RewardedAd.LoadRewardedAd();
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
                if (this.emplement_Ads[i] == null) continue;
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
        if (bannerAd != null) bannerAd.HideBannerAd();
    }
    
    public void DestroyBannerAd()
    {
        if (bannerAd != null) bannerAd.DestroyBannerAd();
    }

    public void ShowRewardedVideo()
    {
        if (this.is_ads && this.is_sdk_ready && RewardedAd != null)
        {
            RewardedAd.onRewardedSuccess = this.onRewardedSuccess;
            RewardedAd.ShowRewardedAd();
        }
    }

    public void show_ads_Interstitial()
    {
        if (!this.is_ads || !this.is_sdk_ready) return;

        this.count_step++;
        if (this.count_step < Mathf.Max(1, this.count_step_show_interstitial)) return;
        if (Time.unscaledTime - this.last_interstitial_show_time < this.min_seconds_between_interstitial) return;

        if (this.ShowInterstitialAd())
        {
            this.count_step = 0;
            this.last_interstitial_show_time = Time.unscaledTime;
        }
    }

    public bool ShowInterstitialAd()
    {
        if (!this.is_ads || !this.is_sdk_ready || interstitialAd == null) return false;
        return interstitialAd.ShowInterstitialAd();
    }

    public void RemoveAds()
    {
        PlayerPrefs.SetInt("is_buy_ads", 1);
        PlayerPrefs.SetInt("is_ads", 1);
        PlayerPrefs.Save();

        this.HideBannerAd();
        this.DestroyBannerAd();
        this.is_ads = false;
        this.count_step = 0;
        this.Check_Emplement_Ads();
    }

    public bool get_status_ads()
    {
        return this.is_ads;
    }

    public void SetRewardedSuccessCallback(UnityAction act_rewarded_success)
    {
        this.onRewardedSuccess = act_rewarded_success;
        if (this.RewardedAd != null) this.RewardedAd.onRewardedSuccess = act_rewarded_success;
    }

    private void RefreshAdsState()
    {
        this.is_ads = this.ShouldShowAds();
        this.Check_Emplement_Ads();
        if (!this.is_ads) this.HideBannerAd();
    }

    private bool ShouldShowAds()
    {
        return PlayerPrefs.GetInt("is_ads", 0) == 0 && PlayerPrefs.GetInt("is_buy_ads", 0) == 0;
    }

    private bool CanInitializeAds()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (string.IsNullOrWhiteSpace(this.app_key))
        {
            Debug.LogWarning("IronSourceAds app_key is empty, skip ads initialization.");
            return false;
        }

        return true;
#else
        Debug.Log("IronSourceAds only initializes on Android/iOS builds.");
        return false;
#endif
    }

    private void OnDisable()
    {
        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
        this.is_sdk_ready = false;
        bannerAd?.DestroyBannerAd();
    }
}
