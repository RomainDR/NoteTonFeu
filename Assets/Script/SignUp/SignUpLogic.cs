using System;
using System.Threading.Tasks;
using Script.Database;
using Script.Login;
using Script.Popup;
using Script.Util;
using Script.Util.Json;
using Script.Util.Password;
using Script.Util.RegexChecker;
using Script.Util.Response;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TableAccount = Script.Database.Table.Account;

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
        [SerializeField] private PopupInformation popupInformation;


        public UIFrame GetSignUpPane() => signUpPane;

        private void Awake()
        {
            signUpButton.onClick.AddListener(OnSignUpClicked);
        }

        private async void OnSignUpClicked()
        {
            var response = await SignUpAccount();
            popupInformation.ShowPopup(
                response.Response == EResponse.Valid ? "Inscription valide."  : "Inscription échoué.\n" + response.ResponseMessage, "Ok",
                () => popupInformation.Hide());
        }

        private async Task<CustomResponse> SignUpAccount()
        {
            try
            {
                string email = RegexVerifier.IsValidEmail(signEmailInputField.text.Trim()) ? signEmailInputField.text.Trim() : "";
                string password = PasswordHasher.HashPassword(signPasswordInputField.text);
                string aName = signNameInputField.text;
                bool autoLogin = signToggleAutoLogin.isOn;

                if (string.IsNullOrEmpty(email))
                {
                    return new CustomResponse
                        { Response = EResponse.InvalidMail, ResponseMessage = "L'email ne peux pas être vide." };
                }

                await RegisterAndSaveAccount(email, password, autoLogin, aName);
                Debug.Log("Utilisateur ajouté avec succès à Supabase.");
                return new CustomResponse { Response = EResponse.Valid, ResponseMessage = "" };
            }
            catch (Exception ex)
            {
                Debug.LogError($"Une erreur s'est produite lors de l'inscription : {ex.Message}");
            }

            return new CustomResponse { Response = EResponse.Valid, ResponseMessage = "" };
        }


        private async Task<CustomResponse> RegisterAndSaveAccount(string email, string password, bool autoLogin,
            string aName)
        {
            if (autoLogin)
            {
                var account = new Account.Account(email, password, aName, true);
                JsonUtil.SaveAccountJson(account);
                return new CustomResponse { Response = EResponse.Valid, ResponseMessage = "" };
            }


            var tAccount = new TableAccount(email, password, aName);

            var response = await SupabaseManager.instance
                .GetClient()
                .From<TableAccount>()
                .Insert(tAccount);

            if (response == null)
            {
                throw new Exception("L'insertion a échoué. Réponse de Supabase vide.");
            }

            return new CustomResponse { Response = EResponse.Valid, ResponseMessage = "" };
        }
    }
}