using System;
using System.Collections;
using Script.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    enum EPane
    {
        Login,
        SignUp
    }

    [RequireComponent(typeof(Switcher))]
    public class MainUiLogic : MonoBehaviour
    {
        [Header("Audio")] [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private float fadeDuration = 2.0f;
        [SerializeField] private float timeBeforeStartMusic = 1.0f;

        [Header("Home")] [SerializeField] private Button startButton;

        [Header("Login/SignUp")] [SerializeField]
        private Button signPaneBtn;

        [SerializeField] private Button loginPaneBtn;

        [Header("Login")] [SerializeField] private TMP_InputField loginEmailInputField;
        [SerializeField] private TMP_InputField loginPasswordInputField;
        [SerializeField] private Button loginButton;
        [SerializeField] private UIFrame loginPane;
        [SerializeField] private Toggle toggleAutoLogin;

        [Header("Signup")] [SerializeField] private UIFrame signUpPane;
        [SerializeField] private TMP_InputField signEmailInputField;
        [SerializeField] private TMP_InputField signPasswordInputField;
        [SerializeField] private TMP_InputField signNameInputField;
        [SerializeField] private Toggle signToggleAutoLogin;
        [SerializeField] private Button signUpButton;

        private void Awake()
        {
            startButton.onClick.AddListener(StartApp);
            signUpButton.onClick.AddListener(SignUpAccount);
            signPaneBtn.onClick.AddListener(() => SwitchPane(EPane.SignUp));
            loginPaneBtn.onClick.AddListener(() => SwitchPane(EPane.Login));
        }

        private void SwitchPane(EPane signUp)
        {
            loginPane.gameObject.SetActive(signUp == EPane.Login);
            signUpPane.gameObject.SetActive(signUp == EPane.SignUp);
        }

        private void SignUpAccount()
        {
            //Account.Account account = new Account.Account(signEmailInputField.text, signPasswordInputField.text,
                //signNameInputField.text, signToggleAutoLogin.isOn);
            
            Account.Account account = new Account.Account("test@example.com", "password123", "JohnDoe", true);
            JsonUtil.SaveAccountJson(account);
        }

        private void StartApp()
        {
            GetComponent<Switcher>().SetUIState(UIState.SignLogin);
            signUpPane.gameObject.SetActive(false);
            loginPane.gameObject.SetActive(true);
        }

        private void Start()
        {
            StartCoroutine(StartMusicWithDelay());
        }

        IEnumerator StartMusicWithDelay()
        {
            yield return new WaitForSeconds(timeBeforeStartMusic);

            backgroundMusic.Play();

            yield return StartCoroutine(FadeIn(backgroundMusic, fadeDuration));
        }

        IEnumerator FadeIn(AudioSource audioSource, float duration)
        {
            float startVolume = 0.0f;
            float targetVolume = 1.0f;
            float currentTime = 0.0f;

            audioSource.volume = startVolume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
                yield return null;
            }

            audioSource.volume = targetVolume;
        }

    }
}