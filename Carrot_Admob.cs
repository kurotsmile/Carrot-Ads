using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Events;

namespace Carrot
{
    public class Carrot_Admob : MonoBehaviour
    {
        private BannerView bannerView;
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;

        private string bannerAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";
        private string interstitialAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";
        private string rewardedAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx";

        public UnityAction onRewardedSuccess;
        public void Initialize(string bannerAdUnitId, string interstitialAdUnitId, string rewardedAdUnitId)
        {
            this.bannerAdUnitId = bannerAdUnitId;
            this.interstitialAdUnitId = interstitialAdUnitId;
            this.rewardedAdUnitId = rewardedAdUnitId;
            MobileAds.Initialize(initStatus =>
            {
                RequestBanner();
                RequestInterstitial();
                RequestRewardedAd();
            });
        }

        private void RequestBanner()
        {
            Debug.Log("Requesting banner ad.");
            if(bannerView != null)
            {
                bannerView.Destroy();
                bannerView = null;
            }
            bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Top);
            bannerView.LoadAd(new AdRequest());
            //bannerView.Show();
        }

        private void RequestInterstitial()
        {   if (interstitial != null)
            {
                interstitial.Destroy();
                interstitial = null;
            }

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
            if (interstitial != null)
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
                    this.onRewardedSuccess?.Invoke();
                    Debug.Log(System.String.Format("Rewarded ad granted a reward: {0} {1}", reward.Amount, reward.Type));
                });
                rewardedAd.OnAdPaid += HandleUserEarnedReward;
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

        private void HandleUserEarnedReward(AdValue adValue)
        {
            onRewardedSuccess?.Invoke();
        }
    }
}
