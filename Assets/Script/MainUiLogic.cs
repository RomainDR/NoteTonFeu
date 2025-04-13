using System;
using System.Collections;
using Script.Login;
using Script.Popup;
using Script.SignUp;
using Script.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;

namespace Script
{
    enum EPane
    {
        Login,
        SignUp
    }

    [RequireComponent(typeof(Switcher), typeof(LoginLogic), typeof(SignUpLogic))]
    public class MainUiLogic : MonoBehaviour
    {
        [Header("Audio")] [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private float fadeDuration = 2.0f;
        [SerializeField] private float timeBeforeStartMusic = 1.0f;

        [Header("Home")] [SerializeField] private Button startButton;

        [Header("Login/SignUp")] [SerializeField]
        private Button signPaneBtn;

        [SerializeField] private Button loginPaneBtn;
        [ReadOnlyEditor] [SerializeField] private LoginLogic loginLogic;
        [ReadOnlyEditor] [SerializeField] private SignUpLogic signUpLogic;
        


        private void Awake()
        {
            startButton.onClick.AddListener(StartApp);
            signPaneBtn.onClick.AddListener(() => SwitchPane(EPane.SignUp));
            loginPaneBtn.onClick.AddListener(() => SwitchPane(EPane.Login));
            loginLogic = GetComponent<LoginLogic>();
            signUpLogic = GetComponent<SignUpLogic>();
        }

        private void SwitchPane(EPane signUp)
        {
            loginLogic.GetLoginPane().gameObject.SetActive(signUp == EPane.Login);
            signUpLogic.GetSignUpPane().gameObject.SetActive(signUp == EPane.SignUp);
        }
        
        private void StartApp()
        {
            GetComponent<Switcher>().SetUIState(UIState.SignLogin);
            signUpLogic.GetSignUpPane().gameObject.SetActive(false);
            loginLogic.GetLoginPane().gameObject.SetActive(true);
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
            var startVolume = 0.0f;
            var targetVolume = 1.0f;
            var currentTime = 0.0f;

            audioSource.volume = startVolume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Lerp(startVolume, targetVolume, currentTime / duration);
                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }
}