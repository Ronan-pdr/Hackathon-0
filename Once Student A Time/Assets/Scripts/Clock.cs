using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const float
        Hours = 360f / 12f,
        Minutes = 360f / 60f,
        Seconds = 360f / 60f;

    [SerializeField] Transform hoursTransform, minutesTransform, secondsTransform;
    [SerializeField] private float timeScale;
    [SerializeField] private bool analog;
    [SerializeField] private TextMeshProUGUI ComputerText;

    private void Update()
    {
        if (analog)
        {
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            hoursTransform.localRotation =
                Quaternion.Euler(0, 0, (float) timespan.TotalHours * Hours * timeScale);
            minutesTransform.localRotation =
                Quaternion.Euler(0, 0, (float) timespan.TotalMinutes * Minutes * timeScale);
            secondsTransform.localRotation =
                Quaternion.Euler(0, 0, (float) timespan.TotalSeconds * Seconds * timeScale);
            ComputerText.text = $"{timespan.Hours}:{timespan.Minutes}:{timespan.Seconds}";
        }
        else
        {
            DateTime time = DateTime.Now;
            hoursTransform.localRotation =
                Quaternion.Euler(0, 0, time.Hour * Hours * timeScale);
            minutesTransform.localRotation =
                Quaternion.Euler(0, 0, time.Minute * Minutes * timeScale);
            secondsTransform.localRotation =
                Quaternion.Euler(0, 0, time.Second * Seconds * timeScale);
            ComputerText.text = $"{time.Hour}:{time.Minute}:{time.Second}";
        }
    }
}