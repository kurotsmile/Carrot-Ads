using UnityEngine;
using UnityEngine.Events;

namespace Carrot
{
    public class Carrot_ads_manage : MonoBehaviour
    {
        [Header("Config General")]
        public int count_step_show_interstitial = 5;
        private int count_step=0;

        [Header("Config Admob")]
        public string bannerAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";
        public string interstitialAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";
        public string rewardedAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";

        [Header("Emplement Ui Ads")]
        public GameObject[] emplement_Ads;

        [Header("Main Object")]
        public Carrot_Admob admob;
        private bool is_ads=false;
        public UnityAction onRewardedSuccess;

        public void On_Load()
        {
            if (PlayerPrefs.GetInt("is_ads", 0) == 0)
                this.is_ads = true;
            else
                this.is_ads = false;

            if(this.is_ads){
                admob.Initialize(bannerAdUnitId, interstitialAdUnitId, rewardedAdUnitId);
            }
            this.Check_Emplement_Ads();
        }

        private void Check_Emplement_Ads(){
            if(this.emplement_Ads.Length!=0){
                for(int i=0;i<this.emplement_Ads.Length;i++){
                    if(this.is_ads)
                        this.emplement_Ads[i].SetActive(true);
                    else
                        this.emplement_Ads[i].SetActive(false);
                }
            }
        }

        public void On_show_interstitial()
        {
            if(this.is_ads){
                if(this.count_step>=this.count_step_show_interstitial)
                {
                    this.count_step=0;
                    admob.ShowInterstitialAd();
                }
                else
                {
                    this.count_step++;
                }
            }
        }

        public void On_show_rewarded()
        {
            if(this.is_ads){
                admob.ShowRewardedAd();
                admob.onRewardedSuccess=this.onRewardedSuccess;
            }
        }

        public void RemoveAds(){
            this.admob.HideBannerAd();
            PlayerPrefs.SetInt("is_ads",1);
            this.is_ads=false;
            this.Check_Emplement_Ads();
        }

        public bool get_status_ads(){
            return this.is_ads;
        }
    }
}