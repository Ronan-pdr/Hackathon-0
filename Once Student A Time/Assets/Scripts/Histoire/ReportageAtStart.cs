using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ReportageAtStart : MonoBehaviour
{
    public static ReportageAtStart Instance;
    
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        Instance = this;
        Invoke(nameof(PlayReportage), 20);
    }

    private void PlayReportage()
    {
        _videoPlayer.Play();
    }

    public void PauseReportage() => _videoPlayer.Pause();
}
