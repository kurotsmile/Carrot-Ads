using GoogleMobileAds.Api;
using UnityEngine;

namespace Carrot
{
    public class Carrot_ads_manage : MonoBehaviour
    {
        private BannerView bannerView;
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;

        public string bannerAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";
        public string interstitialAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";
        public string rewardedAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";

        void Start()
        {
            MobileAds.Initialize(initStatus => { 
                RequestBanner();
                RequestInterstitial();
                RequestRewardedAd();
            });
        }

        private void RequestBanner()
        {
            bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
            bannerView.LoadAd(new AdRequest());
            bannerView.Show();
        }

        private void RequestInterstitial()
        {
            InterstitialAd.Load(interstitialAdUnitId, new AdRequest(), (InterstitialAd ad, LoadAdError error) =>
            {

                if (error != null)
                {
                    Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                    return;
                }

                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
                interstitial = ad;
            });
        }

        private void RequestRewardedAd()
        {
            RewardedAd.Load(rewardedAdUnitId, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                    return;
                }
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                    return;
                }
                Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
                rewardedAd = ad;
            });
        }

        public void ShowInterstitialAd()
        {
            if (interstitial != null && interstitial.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                interstitial.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                Debug.Log("Showing rewarded ad.");
                rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log(System.String.Format("Rewarded ad granted a reward: {0} {1}",reward.Amount,reward.Type));
                });
            }
            else
            {
                Debug.LogError("Rewarded ad is not ready yet.");
            }
        }

        public void HideBannerAd()
        {
            if (bannerView != null)
            {
                bannerView.Hide();
            }
        }
    }
}