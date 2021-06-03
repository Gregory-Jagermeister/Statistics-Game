using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Networking;
using TMPro;

public class UIManager : MonoBehaviour
{
    public RectTransform contentBackgrond;
    public TextMeshProUGUI heading;
    public TextMeshProUGUI content;
    public GameObject media;

    public VideoController player;
    public RawImage image;

    private JsonController json;

    public RectTransform howToPanel;
    private GameObject[] exhibits;

    public Image playButton;
    public Sprite[] playButtonIcons;
    private bool isPlayButton = true;

    private RectTransform[] indicators;

    public RectTransform interactableIconPrefab;

    public RectTransform quizUI;

    public RectTransform VideoPlayerUI;

    public GameObject ClosedDoorPanel;
    public GameObject EndGamePanel;

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
        ClosedDoorPanel.SetActive(false);
        EndGamePanel.SetActive(false);

    }

    public void PlayOrPauseMedia()
    {
        Debug.Log("isPlayButton is: " + isPlayButton);
        if (isPlayButton)
        {
            playButton.sprite = playButtonIcons[1];
        }
        else
        {
            playButton.sprite = playButtonIcons[0];
        }
        isPlayButton = !isPlayButton;
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

    public void OpenQuizMenu(QuizManager manager, List<Questions> quiz, int level)
    {
        isAMenuOpen = true;
        CanvasExtentions.RectTransformPosition(quizUI, 0, 0, 0, 0);
        manager.StartQuiz(quiz, level);
        Time.timeScale = 0;
    }

    public void CloseQuizMenu()
    {
        isAMenuOpen = false;
        CanvasExtentions.RectTransformPosition(quizUI, 2000, -2000, 2000, -2000);
        Time.timeScale = 1;
    }

    public void OpenVideoPlayer()
    {
        CanvasExtentions.RectTransformPosition(VideoPlayerUI, 0, 0, 0, 0);
    }

    public void CloseVideoPlayer()
    {
        CanvasExtentions.RectTransformPosition(VideoPlayerUI, 2000, -2000, 2000, -2000);
        player.ClearMedia();
    }

    public void OpenClosedDoorPanel()
    {
        isAMenuOpen = true;
        ClosedDoorPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseClosedDoorPanel()
    {
        isAMenuOpen = true;
        ClosedDoorPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenEndGamePanel()
    {
        isAMenuOpen = true;
        EndGamePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseEndGamePanel()
    {
        isAMenuOpen = true;
        ClosedDoorPanel.SetActive(false);
        EndGamePanel.SetActive(false);
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

        if (!(exhibits[2].ToLower() == "none"|| exhibits[1] == null))
        {
            player.VIDEO_LINK = exhibits[2];
        }
        
        if (!(exhibits[1].ToLower() == "none" || exhibits[1] == null))
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
        ClosedDoorPanel.SetActive(false);
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
            request = UnityWebRequestTexture.GetTexture("/proxy/" + URL);

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
        //ResizeImage(imgTexture);
        

    }

    /// <summary>
    /// Resizes the image to fit the requires ratio.
    /// </summary>
    /// <param name="i">The Image to Resize as a rawImage</param>
    public void ResizeImage(RawImage i)
    {

        Debug .Log(i.rectTransform.rect.height);
        if (i.rectTransform.rect.width> 375 && i.rectTransform.rect.height > 375)
        {
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  i.rectTransform.rect.width/2);
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  i.rectTransform.rect.height/2);
            ResizeImage(i);
        }
        else if(i.rectTransform.rect.width< 0 || i.rectTransform.rect.height < 0)
        {
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  i.rectTransform.rect.width *-1);
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  i.rectTransform.rect.height *-1);
            ResizeImage(i);

        }
        else
        {
            return;
        }
        
    }

}


