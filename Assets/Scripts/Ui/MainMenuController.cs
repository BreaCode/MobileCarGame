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


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GoToSettingsMenu, PlayRewardedAd);

            ServiceManager.Instance.AnaliticsService.SendMainMenuOpened();
            SubscribeAds();
        }

        protected override void OnDispose()
        {
            UnsubscribeAds();
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

        private void PlayRewardedAd() =>
            ServiceManager.Instance.AdsService.RewardedPlayer.Play();

        private void SubscribeAds()
        {
            ServiceManager.Instance.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServiceManager.Instance.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
            ServiceManager.Instance.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
        }

        private void UnsubscribeAds()
        {
            ServiceManager.Instance.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServiceManager.Instance.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
            ServiceManager.Instance.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
        }

        private void OnAdsFinished() => Debug.Log("Ad finished");
        private void OnAdsCancelled() => Debug.Log("Ad Cancelled");
    }
}
