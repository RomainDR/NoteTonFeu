using System.Threading.Tasks;
using Script.Database;
using Script.Popup;
using Script.Util;
using Script.Util.Json;
using Script.Util.Password;
using Script.Util.RegexChecker;
using Script.Util.Response;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Login
{
    public class LoginLogic : MonoBehaviour
    {
        [Header("Login")] [SerializeField] private TMP_InputField loginEmailInputField;
        [SerializeField] private TMP_InputField loginPasswordInputField;
        [SerializeField] private Toggle autoConnect;
        [SerializeField] private Button loginButton;
        [SerializeField] private UIFrame loginPane;
        [SerializeField] private PopupInformation popupInformation;

        
        public UIFrame GetLoginPane() => loginPane;

        private void Awake()
        {
            loginButton.onClick.AddListener(OnLoginClick);
        }

        private async void OnLoginClick()
        {
            string email = loginEmailInputField.text.Trim();
            string password = loginPasswordInputField.text;
            bool bAutoConnect = autoConnect.isOn;

            CustomResponse result = await ValidateUser(email, password, bAutoConnect);

            popupInformation.ShowPopup(
                result.Response == EResponse.Valid ? "Connexion valide" : "Connexion échoué.\n" + result.ResponseMessage, "Ok",
                () => popupInformation.Hide());
        }

        private async Task<CustomResponse> ValidateUser(string email, string password, bool bAutoConnect)
        {

            if (!RegexVerifier.IsValidEmail(email))
            {
                Debug.Log("Invalid email");
                return new CustomResponse { Response = EResponse.InvalidMail, ResponseMessage = "L'email n'est pas valide." };
            }

            var account = await SupabaseManager.instance.GetAccount(email);

            if (account == null)
            {
                Debug.Log("Request to account return null");
                return new CustomResponse { Response = EResponse.AccountNull, ResponseMessage = "Le compte n'a pas été trouvé." };
            }

            if (!PasswordHasher.VerifyPassword(password, account.password))
            {
                Debug.Log("Password not matching.");
                return new CustomResponse { Response = EResponse.PasswordError, ResponseMessage = "Le mot de passe ne correspond pas." };
            }

            if (!bAutoConnect) return new CustomResponse { Response = EResponse.Valid, ResponseMessage = "" };
            var savedAccount = new Script.Account.Account(email, account.password, account.name, true);
            JsonUtil.SaveAccountJson(savedAccount);

            return new CustomResponse { Response = EResponse.Valid, ResponseMessage = "" };
        }
    }

   
}