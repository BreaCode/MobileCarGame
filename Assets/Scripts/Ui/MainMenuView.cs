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

        public void Init(UnityAction startGame, UnityAction settingsMenu, UnityAction playRewardedAd)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsMenu);
            _buttonRewardedAd.onClick.AddListener(playRewardedAd);
        }


        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAd.onClick.RemoveAllListeners();
        }
    }
}
