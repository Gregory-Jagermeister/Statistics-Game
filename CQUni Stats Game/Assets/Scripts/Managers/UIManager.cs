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


    public GameObject launchScreen;

    public GameObject pauseMenu;
    public GameObject exhibitPanel;


    public TextMeshProUGUI resourcesText;

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
        launchScreen.SetActive(true);
        pauseMenu.SetActive(false);
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
            if (Input.GetButtonDown("Cancel") && !inQuiz)
            {
                if (videoPlaying)
                {
                    CloseVideoPlayer();
                }
                else
                {
                    CloseMenu();
                }


            }

        }
        else if (isAMenuOpen == false || GameManager.Instance.isInteracting == false)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                OpenPauseMenu();
            }
        }
    }

    private bool inQuiz;
    public void OpenQuizMenu(QuizManager manager, List<Questions> quiz, int level)
    {
        isAMenuOpen = true;
        inQuiz = true;
        CanvasExtentions.RectTransformPosition(quizUI, 0, 0, 0, 0);
        manager.StartQuiz(quiz, level);
        Time.timeScale = 0;
    }

    public void CloseQuizMenu()
    {
        isAMenuOpen = false;
        inQuiz = false;
        CanvasExtentions.RectTransformPosition(quizUI, 2000, -2000, 2000, -2000);
        Time.timeScale = 1;
    }
    private bool videoPlaying;

    public void OpenVideoPlayer()
    {

        videoPlaying = true;

        CanvasExtentions.RectTransformPosition(VideoPlayerUI, 0, 0, 0, 0);
    }

    public void CloseVideoPlayer()
    {
        videoPlaying = false;
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
        isAMenuOpen = false;
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
        isAMenuOpen = false;
        GameManager.Instance.isInteracting = false;
        ClosedDoorPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        Time.timeScale = 1;
    }


    // how to display the content for an exhibit
    public void OpenContentMenu(string ID)
    {
        isAMenuOpen = true;
        howToPanel.gameObject.SetActive(false);
        JsonData exhibit;
        exhibit = json.getExhibit(ID);
        Debug.Log(exhibit.artifactId);
        //Set the JSON information to the correct elements.
        heading.text = exhibit.heading;
        content.text = exhibit.content;

        if (!(exhibit.videoUrl == null || exhibit.videoUrl.ToLower() == "none" || exhibit.videoUrl.ToLower() == ""))
        {
            player.VIDEO_LINK = exhibit.videoUrl;
        }
        //if imagePath exists attempt to download the image
        if (!(exhibit.imagePath == null || exhibit.imagePath.ToLower() == "none" || exhibit.imagePath.ToLower() == ""))
        {
            if (exhibit.isLocalImg == true)
            {
                DLImage(exhibit.imagePath, image, true);
            }
            else
            {
                DLImage(exhibit.imagePath, image, false);
            }

        }
        if (exhibit.sources != null)
        {
            string resources = "this artifact used the following resources: \n ";
            foreach (string item in exhibit.sources)
            {

                resources = resources + item + "\n\n";
            }
            resourcesText.text = resources;
        }
        else
        {
            resourcesText.text = "this artifact has no sources that require attributions";

        }
        exhibitPanel.SetActive(true);
        CanvasExtentions.RectTransformPosition(contentBackgrond, 0, 0, 0, 0);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        CanvasExtentions.RectTransformPosition(contentBackgrond, 2000, -2000, 2000, -2000);
        player.ClearMedia();
        exhibitPanel.SetActive(false);
        pauseMenu.SetActive(false);
        ClosedDoorPanel.SetActive(false);
        EndGamePanel.SetActive(false);

        isAMenuOpen = false;
        Time.timeScale = 1;
        GameManager.Instance.SetInteractingFalse();

    }

    public void OpenLaunchScreen()
    {
        isAMenuOpen = true;
        launchScreen.SetActive(true);

        pauseMenu.SetActive(false);
        ClosedDoorPanel.SetActive(false);
        EndGamePanel.SetActive(false);

        CanvasExtentions.RectTransformPosition(contentBackgrond, 2000, -2000, 2000, -2000);


        Time.timeScale = 0;
    }

    public void CloseLaunchScreen()
    {
        isAMenuOpen = false;
        GameManager.Instance.isInteracting = false;
        launchScreen.SetActive(false);
        Time.timeScale = 1;

    }

    public void OpenPauseMenu()
    {
        isAMenuOpen = true;
        GameManager.Instance.isInteracting = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        isAMenuOpen = false;
        GameManager.Instance.isInteracting = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// The Coroutine that Downloads the image from a URL for use and sets it to a texture
    /// </summary>
    /// <param name="URL">The URL of the image location</param>
    /// <param name="image">The RawImage Unity Gameobject to set the image to</param>
    /// <returns></returns>
    private IEnumerator DownloadImage(string URL, RawImage image, bool isLocalImg)
    {
        UnityWebRequest request;
        if (Debug.isDebugBuild)
        {
            request = UnityWebRequestTexture.GetTexture(URL);
        }
        else
        {
            if (isLocalImg == true)
            {
                request = UnityWebRequestTexture.GetTexture(URL);

            }
            else
            {
                request = UnityWebRequestTexture.GetTexture("/proxy/" + URL);

            }
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

    public void OpenDialogueFilePopUp(string imagePath, RawImage imgTexture, bool isLocal)
    {

    }

    /// <summary>
    /// Downloads the image by using the "DownloadImage" Coroutine and sets the size of image to fit the parent
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="imgTexture"></param>
    public void DLImage(string imagePath, RawImage imgTexture, bool isLocal)
    {
        StartCoroutine(DownloadImage(imagePath, imgTexture, isLocal));
        CanvasExtentions.SizeToParent(imgTexture, 100);
        //ResizeImage(imgTexture);


    }

    /// <summary>
    /// Resizes the image to fit the requires ratio.
    /// </summary>
    /// <param name="i">The Image to Resize as a rawImage</param>
    public int imageMaxWidth = 375;
    public int imageMaxHeight = 375;
    public void ResizeImage(RawImage i)
    {

        if (i.rectTransform.rect.width > imageMaxWidth && i.rectTransform.rect.height > imageMaxHeight)
        {
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, i.rectTransform.rect.width / 2);
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i.rectTransform.rect.height / 2);
            ResizeImage(i);
        }
        else if (i.rectTransform.rect.width < 0 || i.rectTransform.rect.height < 0)
        {
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, i.rectTransform.rect.width * -1);
            i.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i.rectTransform.rect.height * -1);
            ResizeImage(i);

        }
        else
        {
            return;
        }

    }

    public GameObject dialogueChoicesPanel;
    public GameObject[] dialogueChoices;
    public GameObject nextButton;



    //setup dialogue chocies
    private bool npcResponseFound = true;

    public bool GetnpcResponseFound()
    {
        return npcResponseFound;
    }

    public void SetupChoices(Dialogue aSegment)
    {

        if (aSegment.pcResponse != null && aSegment.pcResponse.Length == 0)
        {
            dialogueChoicesPanel.SetActive(false);
            dialogueChoices[0].SetActive(false);
            dialogueChoices[1].SetActive(false);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(true);

            npcResponseFound = false;

        }
        else if (aSegment.pcResponse != null && aSegment.pcResponse.Length == 1)
        {
            dialogueChoicesPanel.SetActive(true);
            dialogueChoices[0].SetActive(true);
            //dialogueChoices[0].transform.position = offsetDialogue1Pos;
            dialogueChoices[1].SetActive(false);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(false);

        }
        else if (aSegment.pcResponse != null && aSegment.pcResponse.Length == 2)
        {
            dialogueChoicesPanel.SetActive(true);
            dialogueChoices[0].SetActive(true);
            // dialogueChoices[0].transform.position = normalDialogue1Pos;
            dialogueChoices[1].SetActive(true);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(false);

        }
        else if (aSegment.pcResponse != null && aSegment.pcResponse.Length >= 3)
        {
            dialogueChoicesPanel.SetActive(true);
            dialogueChoices[0].SetActive(true);
            //dialogueChoices[0].transform.position = normalDialogue1Pos;
            dialogueChoices[1].SetActive(true);
            dialogueChoices[2].SetActive(true);


            nextButton.SetActive(false);

        }
        else
        {
            dialogueChoicesPanel.SetActive(false);
            dialogueChoices[0].SetActive(false);
            dialogueChoices[1].SetActive(false);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(true);

            npcResponseFound = false;

        }

    }

}


