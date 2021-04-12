using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Networking;
using System.IO;



public class JsonController : MonoBehaviour
{
    public Text heading;
    public Text content;
    public VideoController player;
    public RawImage image;


    // Start is called before the first frame update
    void Start()
    {
        //Using Application.dataPath (location of data folder that unity will look for) read the JSON file called
        //Information.json
        string json = File.ReadAllText(Application.dataPath + "/Information.json");

        //load the read file into an object
        AllJsonData loadedData = JsonUtility.FromJson<AllJsonData>(json);

        Debug.Log(loadedData.art[1].artifactId);

        // use that object to display some output to console. if all is going to plan this should display the values in the json file.
        // Debug.Log("Image: " + loadedData.imagePath);
        // Debug.Log("Video: " + loadedData.videoUrl);
        // Debug.Log("heading: " + loadedData.heading);
        // Debug.Log("content:" + loadedData.content);

        //Set the JSON information to the correct elements.
        // heading.text = loadedData.heading;
        // content.text = loadedData.content;

        // if (!(loadedData.videoUrl.ToLower() == "none"))
        // {
        //     player.VIDEO_LINK = loadedData.videoUrl;
        // }

        // if (!(loadedData.imagePath.ToLower() == "none"))
        // {
        //     StartCoroutine(DownloadImage(loadedData.imagePath));
        //     //CanvasExtentions.SizeToParent(image, 100);
        // }

    }

    public IEnumerator DownloadImage(string URL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL);

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            image.SetNativeSize();
            ResizeImage(image);

        }
    }

    public void ResizeImage(RawImage i)
    {
        if (i.rectTransform.sizeDelta.x > 256 && i.rectTransform.sizeDelta.y > 256)
        {
            i.rectTransform.sizeDelta = new Vector2(i.rectTransform.sizeDelta.x / 2, i.rectTransform.sizeDelta.y / 2);
            ResizeImage(i);
        }
        else
        {
            return;
        }

    }

    [System.Serializable]
    private class AllJsonData
    {
        public JsonData[] art;
    }


    [System.Serializable]
    private class JsonData
    {
        public string artifactId;
        public string imagePath;
        public string videoUrl;
        public string heading;
        public string content;
    }

}

