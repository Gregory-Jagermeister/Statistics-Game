using System.Collections;
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

    public void OpenQuizMenu()
    {
        _uIManager.OpenQuizMenu();
    }

    public void CloseQuizMenu()
    {
        _uIManager.CloseQuizMenu();
    }

    public void OpenDialogue(string aName, List<Dialogue> convo)
    {
        _dialogueManager.OpenDialogue(aName,convo);
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
        //allows for manipulating timers
        string ex1Timer;
        string ex2Timer;
        string ex3Timer;
        string ex4Timer;

        //altering timers to more readable format on google sheets
        timer = timer.Substring(0, timer.IndexOf("."));
        if (float.Parse(timer) < 10)
        {
            timer = "0" + timer;
        }

        //exhibit 1
        if(Statics.ex1Time < 10)
        {
            ex1Timer = "0" + Statics.ex1Time.ToString();
            ex1Timer = ex1Timer.Substring(0, ex1Timer.IndexOf("."));
        }
        else
        {
            ex1Timer = Statics.ex1Time.ToString();
            ex1Timer = ex1Timer.Substring(0, ex1Timer.IndexOf("."));
        }

        //exhibit 2
        if (Statics.ex2Time < 10)
        {
            ex2Timer = "0" + Statics.ex2Time.ToString();
            ex2Timer = ex2Timer.Substring(0, ex2Timer.IndexOf("."));
        }
        else
        {
            ex2Timer = Statics.ex2Time.ToString();
            ex2Timer = ex2Timer.Substring(0, ex2Timer.IndexOf("."));
        }
        
        //exhibit 3
        if (Statics.ex3Time < 10)
        {
            ex3Timer = "0" + Statics.ex3Time.ToString();
            ex3Timer = ex3Timer.Substring(0, ex3Timer.IndexOf("."));
        }
        else
        {
            ex3Timer = Statics.ex3Time.ToString();
            ex3Timer = ex3Timer.Substring(0, ex3Timer.IndexOf("."));
        }
        /*
        //exhibit 4
        if (Statics.ex4Time < 10)
        {
            ex4Timer = "0" + Statics.ex4Time.ToString();
            ex4Timer = ex4Timer.Substring(0, ex4Timer.IndexOf("."));
        }
        else
        {
            ex4Timer = Statics.ex4Time.ToString();
            ex4Timer = ex4Timer.Substring(0, ex4Timer.IndexOf("."));
        }
        */
        WWWForm form = new WWWForm();
        form.AddField("entry.172307503", Statics.minutes + "." + timer + " mins"); //timer
        form.AddField("entry.1592556701", interactions); //interactions
        form.AddField("entry.1050833444", scorePercent); //quiz1 score
        form.AddField("entry.438884323", "Interactions: " + Statics.ex1Count + " Time: " + Statics.ex1Min + "." + ex1Timer + "mins"); //exhibit1
        form.AddField("entry.77173192", "Interactions: " + Statics.ex2Count + " Time: " + Statics.ex2Min + "." + ex2Timer + "mins"); //exhibit2
        form.AddField("entry.1662654721", "Interactions: " + Statics.ex3Count + " Time: " + Statics.ex3Min + "." + ex3Timer + "mins"); //exhibit3
        //form.AddField("entry.338377618", "Interactions: " + Statics.ex4Count + " Time: " + Statics.ex4Min + "." + ex4Timer + "mins"); //exhibit4
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
