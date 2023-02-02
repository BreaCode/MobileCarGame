using Profile;
using UnityEngine;
using Services;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private ServiceManager _serviceManager;


    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        if (_serviceManager.AdsService.IsInitialized) OnAdsInitialized();
        else _serviceManager.AdsService.Initialized.AddListener(OnAdsInitialized);

        if (_serviceManager.IAPService.IsInitialized) OnIapInitialized();
        else _serviceManager.IAPService.Initialized.AddListener(OnIapInitialized);
    }

    private void OnDestroy()
    {
        _serviceManager.AdsService.Initialized.RemoveListener(OnAdsInitialized);
        _serviceManager.IAPService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }


    private void OnAdsInitialized() => _serviceManager.AdsService.InterstitialPlayer.Play();
    private void OnIapInitialized() => _serviceManager.IAPService.Buy("product_1");
}
