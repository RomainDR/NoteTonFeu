using Unity.Notifications.Android;
using UnityEngine;

namespace Script.Util
{
    public class NotificationUtil : MonoBehaviour
    {
        private const string ChannelID = "notification_channel";

        public static void SendNotification(string title, string body)
        {
       
            if (Application.platform != RuntimePlatform.Android)
            {
                Debug.LogWarning("Notifications are only supported on Android.");
                return;
            }
            // Create a notification channel (required for Android 8.0 and above)
            var channel = new AndroidNotificationChannel()
            {
                Id = ChannelID,
                Name = "Notif Channel",
                Importance = Importance.Default,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            var notification = new AndroidNotification
            {
                Title = title,
                Text = body,
                FireTime = System.DateTime.Now.AddSeconds(10) // Schedule for 10 seconds later
            };

            AndroidNotificationCenter.SendNotification(notification, ChannelID);
        }
    }
}