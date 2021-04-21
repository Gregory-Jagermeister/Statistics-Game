using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;

public class VideoController : MonoBehaviour
{


    private VideoPlayer player;
    private string videoUrl;

    public string VIDEO_LINK
    {
        get
        {
            return videoUrl;
        }
        set
        {
            videoUrl = value;
        }
    }

    //Start is called before the first frame update
    void Start()
    {
        //get the video player component.
        player = this.GetComponent<VideoPlayer>();
        //This will set the processing server that the youtube player uses, change this if you would like to use your own implementation.
        YoutubeDl.ServerUrl = "https://statsgamevideoserver.herokuapp.com";
    }

    public void PlayMedia()
    {
        Debug.Log(player.isPrepared);
        if (!player.isPrepared)
        {
            FindMedia(videoUrl);
        }

        player.Play();
    }

    public void PauseMedia()
    {
        player.Pause();
    }

    public void ClearMedia()
    {
        player.Stop();
    }

    public async void FindMedia(string url)
    {
        await VideoPlayerExtensions.PlayYoutubeVideoAsync(player, url);
        Debug.Log(player.isPrepared);
    }

}
