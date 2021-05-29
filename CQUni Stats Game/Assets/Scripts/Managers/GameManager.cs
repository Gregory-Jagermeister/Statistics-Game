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
    private bool lvl1QuizPassed = false;
    private bool lvl2QuizPassed = false;
    private bool lvl3QuizPassed = false;

    public float quiz1Average = 0f;
    public int quiz1Attempts = 0;

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

    public void OpenQuizMenu(List<Questions> quiz,int level)
    {

        //_quizManager.StartQuiz(quiz);
        
        _uIManager.OpenQuizMenu(_quizManager,quiz,level);
    }


    public void CloseQuizMenu()
    {
        _uIManager.CloseQuizMenu();
    }

    public void OpenDialogue(string aName, List<Dialogue> convo,List<Questions> quiz, int level)
    {
        _dialogueManager.OpenDialogue(aName,convo,quiz,level);
    }

    public void CloseDialogue()
    {
        _dialogueManager.StopDialogue();
    }

    /*public GameObject GetNextRoom()
    {
        Debug.Log(roomIndex);
        return rooms[roomIndex];
    }
    */

    public QuizManager GetQuizManager()
    {
        return _quizManager;
    }
    public Movement GetPlayer()
    {
        return player;
    }

    public void DidPlayerPassQuiz(bool passed,int level)
    {
        if(passed == true)
        {
            if(level == 1)
            {
                lvl1QuizPassed = true;

            }
            else if( level == 2)
            {
                lvl2QuizPassed = true;

            }
            else if(level ==3)
            {
                lvl3QuizPassed = true;

            }

        }
        quizPassed = passed;
    }

    public bool GetPlayersQuizResults()
    {
        return quizPassed;
    }

    public bool GetPlayersQuizResultsLVL1()
    {
        return lvl1QuizPassed;
    }
    public bool GetPlayersQuizResultsLVL2()
    {
        return lvl2QuizPassed;
    }
    public bool GetPlayersQuizResultsLVL3()
    {
        return lvl3QuizPassed;
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
    public IEnumerator CreateAnalyticsData(string timer, string interactions, string scorePercent1)
    {
        //allows for manipulating timers
        string ex1Timer;
        string ex2Timer;
        string ex3Timer;
        string ex4Timer;
        string ex5Timer;
        string ex6Timer;
        scorePercent1 = "";
        
        //altering timers to more readable format on google sheets
        timer = timer.Substring(0, timer.IndexOf("."));
        if (float.Parse(timer) < 10)
        {
            timer = "0" + timer;
        }

        //exhibit 1
        if(Statics.ex1Time < 10 && Statics.ex1Time > 0)
        {
            ex1Timer = "0" + Statics.ex1Time.ToString();
            ex1Timer = ex1Timer.Substring(0, ex1Timer.IndexOf("."));
        }
        else
        {
            
                ex1Timer = Statics.ex1Time.ToString();
            if (float.Parse(ex1Timer) != 0)
            {
                ex1Timer = ex1Timer.Substring(0, ex1Timer.IndexOf("."));
            }
        }

        //exhibit 2
        if (Statics.ex2Time < 10 && Statics.ex2Time > 0)
        {
            ex2Timer = "0" + Statics.ex2Time.ToString();
            ex2Timer = ex2Timer.Substring(0, ex2Timer.IndexOf("."));
        }
        else
        {
            ex2Timer = Statics.ex2Time.ToString();
            if (float.Parse(ex2Timer) != 0)
            {
                ex2Timer = ex2Timer.Substring(0, ex2Timer.IndexOf("."));
            }
        }
        
        //exhibit 3
        if (Statics.ex3Time < 10 && Statics.ex3Time > 0)
        {
            ex3Timer = "0" + Statics.ex3Time.ToString();
            ex3Timer = ex3Timer.Substring(0, ex3Timer.IndexOf("."));
        }
        else
        {
            ex3Timer = Statics.ex3Time.ToString();
            if (float.Parse(ex3Timer) != 0)
            {
                ex3Timer = ex3Timer.Substring(0, ex3Timer.IndexOf("."));
            }
        }

        //exhibit 4
        if (Statics.ex4Time < 10 && Statics.ex4Time > 0)
        {
            ex4Timer = "0" + Statics.ex4Time.ToString();
            ex4Timer = ex4Timer.Substring(0, ex4Timer.IndexOf("."));
        }
        else
        {

            ex4Timer = Statics.ex4Time.ToString();
            if (float.Parse(ex4Timer) != 0)
            {
                ex4Timer = ex4Timer.Substring(0, ex4Timer.IndexOf("."));
            }
        }

        //exhibit 5
        if (Statics.ex5Time < 10 && Statics.ex5Time > 0)
        {
            ex5Timer = "0" + Statics.ex5Time.ToString();
            ex5Timer = ex5Timer.Substring(0, ex5Timer.IndexOf("."));
        }
        else
        {
            ex5Timer = Statics.ex5Time.ToString();
            if (float.Parse(ex5Timer) != 0)
            {
                ex5Timer = ex5Timer.Substring(0, ex5Timer.IndexOf("."));
            }
        }

        //exhibit 6
        if (Statics.ex6Time < 10 && Statics.ex6Time > 0)
        {
            ex6Timer = "0" + Statics.ex6Time.ToString();
            ex6Timer = ex6Timer.Substring(0, ex6Timer.IndexOf("."));
        }
        else
        {
            ex6Timer = Statics.ex6Time.ToString();
            if (float.Parse(ex6Timer) != 0)
            {
                ex6Timer = ex6Timer.Substring(0, ex6Timer.IndexOf("."));
            }
        }
        quiz1Attempts = Statics.quizCount1;
        for(int count = 0; count <= Statics.quizCount1; count ++)
        {
            quiz1Average += Statics.quizScore[count];
        }
        quiz1Average = quiz1Average / Statics.quizCount1;

        WWWForm form = new WWWForm();
        Debug.Log("it worked");
        form.AddField("entry.172307503", Statics.minutes + "." + timer + " mins"); //timer
        form.AddField("entry.1592556701", interactions); //interactions
        form.AddField("entry.1050833444", quiz1Average.ToString()); //quiz1 score
        form.AddField("entry.319759789", Statics.quizCount1.ToString());
        form.AddField("entry.438884323", "Interactions: " + Statics.ex1Count + " Time: " + Statics.ex1Min + "." + ex1Timer + "mins"); //exhibit1
        form.AddField("entry.77173192", "Interactions: " + Statics.ex2Count + " Time: " + Statics.ex2Min + "." + ex2Timer + "mins"); //exhibit2
        form.AddField("entry.1662654721", "Interactions: " + Statics.ex3Count + " Time: " + Statics.ex3Min + "." + ex3Timer + "mins"); //exhibit3
        form.AddField("entry.1998886816", "Interactions: " + Statics.ex4Count + " Time: " + Statics.ex4Min + "." + ex4Timer + "mins"); //exhibit4
        form.AddField("entry.1161738900", "Interactions: " + Statics.ex5Count + " Time: " + Statics.ex5Min + "." + ex5Timer + "mins"); //exhibit5
        form.AddField("entry.109809298", "Interactions: " + Statics.ex5Count + " Time: " + Statics.ex5Min + "." + ex5Timer + "mins"); //exhibit6
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
    
    /*public void SetRoomIndex(int index)
    {
        Debug.Log(index);
        roomIndex = index;
        Debug.Log(roomIndex);

    }
    */

    public void MovePlayerToRoom(int index)
    {
       player.transform.position = new Vector3(rooms[index].transform.position.x, rooms[index].transform.position.y, 0);

    }



    public void OpenDoors()
    {
        if(GetPlayersQuizResultsLVL1())
        {
            door[0].sprite = doorOpen;
            door[1].sprite = doorOpen;
        
        }
        if(GetPlayersQuizResultsLVL2())
        {
            door[2].sprite = doorOpen;
            door[3].sprite = doorOpen;
            
        }
        /*
        if(GetPlayersQuizResultsLVL3())
        {
            door[4].sprite = doorOpen;
        
        }
        */

    }

    // Update is called once per frame
    void Update()
    {

       
        OpenDoors();
    }
}
