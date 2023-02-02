using Profile;
using Tool;
using UnityEngine;
using Services;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private ServiceManager _services;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GoToSettingsMenu, PlayRewardedAd, BuyProduct);
            _services = ServiceManager.Instance;
            _services.AnaliticsService.SendMainMenuOpened();
            SubscribeAds();
            SubscribeIAP();
        }

        protected override void OnDispose()
        {
            UnsubscribeAds();
            UnsubscribeIAP();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void GoToSettingsMenu() =>
           _profilePlayer.CurrentState.Value = GameState.Settings;

        private void BuyProduct(string productId)
        {
            _services.IAPService.Buy(productId);
        }

        private void PlayRewardedAd() =>
            _services.AdsService.RewardedPlayer.Play();

        #region Subscribes
        private void SubscribeAds()
        {
            _services.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            _services.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
            _services.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
        }

        private void UnsubscribeAds()
        {
            _services.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            _services.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
            _services.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
        }

        private void SubscribeIAP()
        {
            _services.IAPService.PurchaseSucceed.AddListener(OnPurchaseFinished);
            _services.IAPService.PurchaseFailed.AddListener(OnPurchaseFailed);
        }

        private void UnsubscribeIAP()
        {
            _services.IAPService.PurchaseSucceed.RemoveAllListeners();
            _services.IAPService.PurchaseFailed.RemoveAllListeners();
        }
        #endregion

        #region Events
        private void OnAdsFinished() => Debug.Log("Ad finished");
        private void OnAdsCancelled() => Debug.Log("Ad Cancelled");
        private void OnPurchaseFinished() => Debug.Log("Purchase succeed");
        private void OnPurchaseFailed() => Debug.Log("Purchase failed");
        #endregion

    }
}
