using UnityEngine;
using UnityEngine.Video;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoScreen;

    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;

    private bool videoPlaying = false;
    private float previousAudioVolume;

    void Start()
    {
        StartIntroVideo();
    }

    void Update()
    {
        if (videoPlaying && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            EndIntroVideo();
        }
    }

    void StartIntroVideo()
    {
        videoPlaying = true;

        previousAudioVolume = AudioListener.volume;
        AudioListener.volume = 0f;

        if (videoScreen != null)
            videoScreen.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerShooting != null)
            playerShooting.enabled = false;

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        EndIntroVideo();
    }

    void EndIntroVideo()
    {
        if (!videoPlaying) return;

        videoPlaying = false;

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.loopPointReached -= OnVideoFinished;
        }

        if (videoScreen != null)
            videoScreen.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (playerShooting != null)
            playerShooting.enabled = true;

        AudioListener.volume = previousAudioVolume;
    }
}