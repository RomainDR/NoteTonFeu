using Script.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Login
{
    public class LoginLogic : MonoBehaviour
    {
        [Header("Login")] [SerializeField] private TMP_InputField loginEmailInputField;
        [SerializeField] private TMP_InputField loginPasswordInputField;
        [SerializeField] private Button loginButton;
        [SerializeField] private UIFrame loginPane;
        [SerializeField] private Toggle toggleAutoLogin;
        
        public UIFrame LoginPane => loginPane;
    }
}