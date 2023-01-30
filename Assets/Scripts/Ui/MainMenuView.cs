using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;


        public void InitStart(UnityAction startGame) =>
            _buttonStart.onClick.AddListener(startGame);

        public void InitSettings(UnityAction settingsMenu) =>
           _buttonSettings.onClick.AddListener(settingsMenu);

        public void OnDestroy() =>
            _buttonStart.onClick.RemoveAllListeners();
    }
}
