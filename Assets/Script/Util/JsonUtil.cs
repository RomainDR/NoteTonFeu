using Newtonsoft.Json;
using UnityEngine;

namespace Script.Util
{
    public abstract class JsonUtil
    {
        public static void SaveAccountJson(Account.Account account)
        {
            string accountData = JsonConvert.SerializeObject(account, Formatting.Indented);
            string filePath = Application.persistentDataPath + "/account.json";
            Debug.Log("Saved in: " + filePath);
            System.IO.File.WriteAllText(filePath, accountData);
            NotificationUtil.SendNotification("Account saved", "Saved in: " + filePath);
        }
    }
}