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
        AndroidNotificationCenter.CancelAllNotifications();
        var notification = new AndroidNotification();
        notification.Title = PetController.instance._name;
        notification.Text = PetController.instance._name + " precisa da sua atenção.";
        notification.FireTime = System.DateTime.Now.AddMinutes(1);
        notification.ShouldAutoCancel = true;

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
