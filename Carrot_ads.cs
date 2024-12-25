using UnityEngine;

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

        [Header("Main Object")]
        public Carrot_Admob admob;

        public void On_Load()
        {
            admob.Initialize(bannerAdUnitId, interstitialAdUnitId, rewardedAdUnitId);
        }

        public void On_show_interstitial()
        {
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
}