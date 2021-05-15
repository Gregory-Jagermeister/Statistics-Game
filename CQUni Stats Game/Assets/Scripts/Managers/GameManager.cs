﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public SpriteRenderer[] door;
    public GameObject[] rooms;

    private int roomIndex = 0;
    public Sprite doorOpen;
    private JsonController _json;

    private UIManager _uIManager;

    private QuizManager _quizManager;
    private DialogueManager _dialogueManager;

    public bool isInteracting = false;
    private Movement player;
    private bool quizPassed = false;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdKmNRxpf0460uiCSEKMoheZodlUqtHkM8MCAEy4fGS3y_d-A/formResponse";


    void Awake()
    {

        if (GameManager.Instance != null && GameManager.Instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }


        //Generate the Instance of the Json Document.
        _json = this.gameObject.AddComponent<JsonController>();
        _json.GetJson();

        _uIManager = this.gameObject.GetComponent<UIManager>();
        _quizManager = this.gameObject.GetComponent<QuizManager>();
        _dialogueManager = this.gameObject.GetComponent<DialogueManager>();

    }

    public void OpenContentMenu(string id)
    {
        _uIManager.OpenContentMenu(id);
    }

    public void OpenQuizMenu(List<Questions> quiz)
    {

        //_quizManager.StartQuiz(quiz);
        
        _uIManager.OpenQuizMenu(_quizManager,quiz);
    }


    public void CloseQuizMenu()
    {
        _uIManager.CloseQuizMenu();
    }

    public void OpenDialogue(string aName, List<Dialogue> convo,List<Questions> quiz )
    {
        _dialogueManager.OpenDialogue(aName,convo,quiz);
    }

    public void CloseDialogue()
    {
        _dialogueManager.StopDialogue();
    }

    public GameObject GetNextRoom()
    {
        return rooms[roomIndex];
    }

    public QuizManager GetQuizManager()
    {
        return _quizManager;
    }
    public Movement GetPlayer()
    {
        return player;
    }

    public void DidPlayerPassQuiz(bool passed)
    {
        quizPassed = passed;
    }

    public bool GetPlayersQuizResults()
    {
        return quizPassed;
    }

    public void SetInteractingFalse()
    {
        isInteracting = false;
    }
    public void SetInteractingTrue()
    {
        isInteracting = true;
    }

    public bool GetInteraction()
    {
        return isInteracting;
    }

    /// <summary>
    /// Creates and sends the Analytics data to a google spreadsheet.
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="interactions"></param>
    /// <param name="scorePercent"></param>
    /// <returns></returns>
    public IEnumerator CreateAnalyticsData(string timer, string interactions, string scorePercent)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.172307503", timer);
        form.AddField("entry.1592556701", interactions);
        form.AddField("entry.1050833444", scorePercent);
        byte[] rawData = form.data;
        //Updated this to UnityWebRequest as WWW is obsolete.
        using (var w = UnityWebRequest.Post(BASE_URL, form))
        {
            yield return w.SendWebRequest();
            if (w.isHttpError || w.isNetworkError)
            {
                Debug.Log(w.error);
            }
            else
            {
                Debug.Log("Finished Sending Analytics Data");
            }
        }

    }

    /// <summary>
    /// Get the Json Instance generated by the gamemanager and return it. 
    /// </summary>
    /// <returns>JsonController jsonObject</returns>
    public JsonController GetJson()
    {
        return _json;
    }

    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
        player = GameObject.FindObjectOfType<Movement>();
        if (player == null)
        {
            Debug.Log("no player to interact with");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetPlayersQuizResults())
        {
            door[0].sprite = doorOpen;
            if (roomIndex > rooms.Length - 1)
            {
                roomIndex = rooms.Length - 1;
            }
            else
            {
                roomIndex++;
            }

        }
    }
}
