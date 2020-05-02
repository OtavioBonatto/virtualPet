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
        notification.Title = "Luna";
        notification.Text = "Luna precisa da sua atenção.";
        notification.FireTime = System.DateTime.Now.AddHours(6);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
