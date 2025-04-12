using System;
using Script.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.SignUp
{
    public class SignUpLogic : MonoBehaviour
    {
        [Header("Signup")] [SerializeField] private UIFrame signUpPane;
        [SerializeField] private TMP_InputField signEmailInputField;
        [SerializeField] private TMP_InputField signPasswordInputField;
        [SerializeField] private TMP_InputField signNameInputField;
        [SerializeField] private Toggle signToggleAutoLogin;
        [SerializeField] private Button signUpButton;

        public UIFrame SignUpPane => signUpPane;

        private void Awake()
        {
            signUpButton.onClick.AddListener(SignUpAccount);
        }

        private void SignUpAccount()
        {
            var account = new Account.Account(signEmailInputField.text,
                BCrypt.Net.BCrypt.HashPassword(signPasswordInputField.text),
                signNameInputField.text, signToggleAutoLogin.isOn);
            JsonUtil.SaveAccountJson(account);
        }
    }
}