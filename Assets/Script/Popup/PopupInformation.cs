using Script.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.Popup
{
    public class PopupInformation : MonoBehaviour
    {
        [SerializeField] private TMP_Text textInformation;
        [SerializeField] private TMP_Text textButton;
        [SerializeField] private Button buttonPopup;

        protected void Awake()
        {
            gameObject.SetActive(false);
        }

        public void ShowPopup(string text, string buttonText, UnityAction callback)
        {
            gameObject.SetActive(true);
            buttonPopup.onClick.AddListener(callback);
            textInformation.text = text;
            textButton.text = buttonText;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            buttonPopup.onClick.RemoveAllListeners();
        }
    }
}