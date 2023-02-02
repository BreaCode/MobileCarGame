using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Products")]
        [SerializeField] private string _productID;

        [Header("Buttons")]
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAd;
        [SerializeField] private Button _buttonIAP;

        public void Init(UnityAction startGame, UnityAction settingsMenu, UnityAction playRewardedAd, UnityAction<string> buyProduct)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsMenu);
            _buttonRewardedAd.onClick.AddListener(playRewardedAd);
            _buttonIAP.onClick.AddListener(() => buyProduct(_productID));
        }


        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAd.onClick.RemoveAllListeners();
            _buttonIAP.onClick.RemoveAllListeners();
        }
    }
}
