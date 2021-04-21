using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Networking;

public class ArtifactMenu : MonoBehaviour
{
    public GameObject background;
    public Text heading;
    public Text content;
    public GameObject media;

    public VideoController player;
    public RawImage image;

    public JsonController json;


    private bool artifactMenuOpen = false;

    void Start()
    {
        //Shortform for the Gamemanger instance making it easier to access, Store the address to this on start of the scene
        json = GameManager.Instance.GetJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (artifactMenuOpen == true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                CloseMenu();
            }

        }
        else
        {
            CloseMenu();

        }


    }

    public void OpenMenu(string ID)
    {
        string[] exhibits = new string[5];
        exhibits = json.getExhibit(ID);
        Debug.Log(exhibits[0]);
        //Set the JSON information to the correct elements.
        heading.text = exhibits[3];
        content.text = exhibits[4];

        if (!(exhibits[2].ToLower() == "none"))
        {
            player.VIDEO_LINK = exhibits[2];
            media.gameObject.SetActive(true);
            player.PlayMedia();
        }

        if (!(exhibits[1].ToLower() == "none"))
        {
            DLImage(exhibits[1], image);
        }


        background.gameObject.SetActive(true);
        heading.gameObject.SetActive(true);
        content.gameObject.SetActive(true);


        artifactMenuOpen = true;
        Time.timeScale = 0;

    }

    public void CloseMenu()
    {
        background.gameObject.SetActive(false);
        heading.gameObject.SetActive(false);
        content.gameObject.SetActive(false);
        media.gameObject.SetActive(false);
        artifactMenuOpen = false;
        Time.timeScale = 1;


    }

    public void UpdateMenu()
    {

    }

    /// <summary>
    /// The Coroutine that Downloads the image from a URL for use and sets it to a texture
    /// </summary>
    /// <param name="URL">The URL of the image location</param>
    /// <param name="image">The RawImage Unity Gameobject to set the image to</param>
    /// <returns></returns>
    private IEnumerator DownloadImage(string URL, RawImage image)
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

    /// <summary>
    /// Downloads the image by using the "DownloadImage" Coroutine and sets the size of image to fit the parent
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="imgTexture"></param>
    public void DLImage(string imagePath, RawImage imgTexture)
    {
        StartCoroutine(DownloadImage(imagePath, imgTexture));
        CanvasExtentions.SizeToParent(imgTexture, 100);

    }

    /// <summary>
    /// Resizes the image to fit the requires ratio.
    /// </summary>
    /// <param name="i"></param>
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

}

