using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;

namespace Services
{
    internal class ServiceManager : MonoBehaviour
    {
        [SerializeField] private AnalyticsService _analiticsService;
        [SerializeField] private UnityAdsService _adsService;
        [SerializeField] private IAPService _iAPService;

        private static ServiceManager _instace;
        public static ServiceManager Instance
        {
            get { return _instace; }
        }
        public AnalyticsService AnaliticsService
        {
            get { return _analiticsService; }
        }
        public UnityAdsService AdsService
        {
            get { return _adsService; }
        }
        public IAPService IAPService
        {
            get { return _iAPService; }
        }

        private void Awake()
        {
            _instace = this;
        }
    }
}

