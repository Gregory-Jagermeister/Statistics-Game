using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;
using UnityEngine.Networking;

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
            StartCoroutine(FindMedia(Application.dataPath + "/api/video/" + videoUrl));
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

    public IEnumerator FindMedia(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            player.url = Application.dataPath + "/proxy/" + request.downloadHandler.text;
            player.Play();
        }

        Debug.Log(player.isPrepared);
    }

}
