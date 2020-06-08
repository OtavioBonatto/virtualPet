using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class Notification : MonoBehaviour {

    private void Start() {
        var c = new AndroidNotificationChannel() {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    private void OnApplicationQuit() {
        var notification = new AndroidNotification();
        notification.Title = PetController.instance._name;
        notification.Text = PetController.instance._name + " precisa da sua atenção.";
        notification.FireTime = System.DateTime.Now.AddHours(6);
        notification.ShouldAutoCancel = true;

        //AndroidNotificationCenter.CancelAllScheduledNotifications();
        //AndroidNotificationCenter.SendNotification(notification, "channel_id");

        var identifier = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled) {
            // Replace the currently scheduled notification with a new notification.
            AndroidNotificationCenter.UpdateScheduledNotification(identifier, notification, "channel_id");
        } else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Delivered) {
            //Remove the notification from the status bar
            AndroidNotificationCenter.CancelNotification(identifier);
        } else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Unknown) {
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
