using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;

public class VideoPlayerScript : MonoBehaviour
{

    VideoPlayer videoPlayer;
    public GameObject canvas;

    // Update is called once per frame
    public void playVideo()
    {        
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        videoPlayer.aspectRatio = VideoAspectRatio.FitInside;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "video.mp4");

        videoPlayer.loopPointReached += EndReached;

        videoPlayer.Play();
        canvas.SetActive(false);
        Time.timeScale = 0;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        SoundManager.Instance.MuteAll();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        canvas.SetActive(true);
        Time.timeScale = 1;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(videoPlayer);
        SceneManager.LoadScene("MainGameScene");
        SoundManager.Instance.UnmuteAll();
    }
}
