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


    public bool artifactMenuOpen = false;

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
        }

        if (!(exhibits[1].ToLower() == "none"))
        {
            DLImage(exhibits[1], image);
        }


        background.gameObject.SetActive(true);
        heading.gameObject.SetActive(true);
        content.gameObject.SetActive(true);
        media.gameObject.SetActive(true);

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

    public void DLImage(string imagePath, RawImage imgTexture)
    {
        StartCoroutine(DownloadImage(imagePath, imgTexture));
        CanvasExtentions.SizeToParent(imgTexture, 100);

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

}

