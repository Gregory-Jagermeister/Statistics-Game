using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{
    public RectTransform contentBackgrond;
    public Text heading;
    public Text content;
    public GameObject media;

    public VideoController player;
    public RawImage image;

    private JsonController json;

    public RectTransform howToPanel;
    private GameObject[] exhibits;

    private RectTransform[] indicators;

    public RectTransform interactableIconPrefab;

    public RectTransform quizUI;

    private bool isAMenuOpen = false;

    void Start()
    {
        //Shortform for the Gamemanger instance making it easier to access, Store the address to this on start of the scene
        json = GameManager.Instance.GetJson();
        //Get all the Exhibit Gameobjects
        exhibits = GameObject.FindGameObjectsWithTag("Exhibits");
        indicators = new RectTransform[exhibits.Length];
        int count = 0;
        foreach (var item in exhibits)
        {
            Interactable interact = item.GetComponent<Interactable>();
            indicators[count] = Instantiate(interactableIconPrefab, interact.menu.gameObject.transform);
            indicators[count].position = Camera.main.WorldToScreenPoint(item.transform.position + new Vector3(0, 0.4f, 0));
            indicators[count].gameObject.SetActive(false);
            count++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (var item in exhibits)
        {
            Interactable interact = item.GetComponent<Interactable>();
            if (interact.IsPlayerClose() && !isAMenuOpen)
            {
                if (item.gameObject.layer == LayerMask.NameToLayer("Doors"))
                {
                    if (GameManager.Instance.GetPlayersQuizResults())
                    {
                        indicators[count].gameObject.SetActive(true);
                    }
                }
                else if (!(item.gameObject.layer == LayerMask.NameToLayer("Doors")))
                {
                    indicators[count].gameObject.SetActive(true);
                }

            }
            else
            {
                indicators[count].gameObject.SetActive(false);
            }
            indicators[count].position = Camera.main.WorldToScreenPoint(item.transform.position + new Vector3(0, 0.4f, 0));
            count++;
        }

        if (isAMenuOpen == true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                CloseMenu();
            }

        }
    }

    public void OpenQuizMenu()
    {
        isAMenuOpen = true;
        CanvasExtentions.RectTransformPosition(quizUI, 0, 0, 0, 0);
        Time.timeScale = 0;
    }

    public void CloseQuizMenu()
    {
        isAMenuOpen = false;
        CanvasExtentions.RectTransformPosition(quizUI, 2000, -2000, 2000, -2000);
        Time.timeScale = 1;
    }

    public void OpenContentMenu(string ID)
    {
        isAMenuOpen = true;
        howToPanel.gameObject.SetActive(false);
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

        CanvasExtentions.RectTransformPosition(contentBackgrond, 0, 0, 0, 0);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        CanvasExtentions.RectTransformPosition(contentBackgrond, 2000, -2000, 2000, -2000);
        player.ClearMedia();
        isAMenuOpen = false;
        Time.timeScale = 1;
        GameManager.Instance.SetInteractingFalse();


    }

    /// <summary>
    /// The Coroutine that Downloads the image from a URL for use and sets it to a texture
    /// </summary>
    /// <param name="URL">The URL of the image location</param>
    /// <param name="image">The RawImage Unity Gameobject to set the image to</param>
    /// <returns></returns>
    private IEnumerator DownloadImage(string URL, RawImage image)
    {
        UnityWebRequest request;
        if (Debug.isDebugBuild)
        {
            request = UnityWebRequestTexture.GetTexture(URL);
        }
        else
        {
            request = UnityWebRequestTexture.GetTexture("https://boiling-cliffs-78685.herokuapp.com/" + URL);
        }

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
    /// <param name="i">The Image to Resize as a rawImage</param>
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

