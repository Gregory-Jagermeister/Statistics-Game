using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public SpriteRenderer[] door;
    public GameObject[] rooms;

    private int roomIndex = 0;
    public Sprite doorOpen;
    public Sprite doorClose;
    private JsonController _json;

    private UIManager _uIManager;

    private QuizManager _quizManager;
    private DialogueManager _dialogueManager;
    private SoundManager _soundManager;
    private Statics _statsManager;

    public bool isInteracting = false;
    private Movement player;
    private bool quizPassed = false;
    private bool lvl1QuizPassed = false;
    private bool lvl2QuizPassed = false;
    private bool lvl3QuizPassed = false;

    public float questCount1 = 0;
    public float questCount2 = 0;
    public float questCount3 = 0;
    public float questCount4 = 0;
    public float questCount5 = 0;
    public float questCount6 = 0;
    public float questCount7 = 0;
    public float questCorrect1 = 0;
    public float questCorrect2 = 0;
    public float questCorrect3 = 0;
    public float questCorrect4 = 0;
    public float questCorrect5 = 0;
    public float questCorrect6 = 0;
    public float questCorrect7 = 0;

    public float[] questCount = new float[45];
    public float[] questCorrect = new float[45];
    public string[] exTimer = new string[30];

    public float quiz1Average = 0f;
    public float quiz2Average = 0f;
    public float quiz3Average = 0f;
    public int quiz1Attempts = 0;
    public int quiz2Attempts = 0;
    public int quiz3Attempts = 0;

    public int[] qTest = new int[99];
    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdKmNRxpf0460uiCSEKMoheZodlUqtHkM8MCAEy4fGS3y_d-A/formResponse";

    void Awake()
    {
        //Generate the Instance of the Json Document.
        _json = this.gameObject.AddComponent<JsonController>();
        _json.GetJson();

        _uIManager = this.gameObject.GetComponent<UIManager>();
        _quizManager = this.gameObject.GetComponent<QuizManager>();
        _dialogueManager = this.gameObject.GetComponent<DialogueManager>();
        _soundManager = this.gameObject.GetComponent<SoundManager>();
        _statsManager = this.gameObject.GetComponent<Statics>();


    }


    public void OpenContentMenu(string id)
    {
        _uIManager.OpenContentMenu(id);
    }

    public void OpenQuizMenu(List<Questions> quiz, int level)
    {

        //_quizManager.StartQuiz(quiz);

        _uIManager.OpenQuizMenu(_quizManager, quiz, level);
    }

    public void CloseQuizMenu()
    {
        _uIManager.CloseQuizMenu();
    }

    public void OpenDialogue(string aName, List<Dialogue> convo, List<Questions> quiz, int level)
    {
        _dialogueManager.OpenDialogue(aName, convo, quiz, level);
    }

    public void CloseDialogue()
    {
        _dialogueManager.StopDialogue();
    }

    public void OpenClosedDoor()
    {

        //_quizManager.StartQuiz(quiz);

        _uIManager.OpenClosedDoorPanel();
    }

    public void DownloadImage(string imagePath, RawImage imgTexture, bool isLocal)
    {
        _uIManager.DLImage(imagePath, imgTexture, isLocal);
    }

    public void CloseClosedDoor()
    {

        _uIManager.CloseClosedDoorPanel();
    }

    public UIManager GetUIManager()
    {
        return _uIManager;
    }

    public QuizManager GetQuizManager()
    {
        return _quizManager;
    }


    public DialogueManager GetDialogueManager()
    {
        return _dialogueManager;
    }
    public Movement GetPlayer()
    {
        return player;
    }

    public void DidPlayerPassQuiz(bool passed, int level)
    {
        Debug.Log(passed);
        if (passed == true)
        {
            if (level == 1)
            {

                lvl1QuizPassed = true;
                Debug.Log(lvl1QuizPassed);

            }
            else if (level == 2)
            {
                lvl2QuizPassed = true;

            }
            else if (level == 3)
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
        return null;
    }

    /// <summary>
    /// Get the Json Instance generated by the gamemanager and return it. 
    /// </summary>
    /// <returns>JsonController jsonObject</returns>
    public JsonController GetJson()
    {
        return _json;
    }

    public void TestReactJS()
    {
        string timer;
        string interactions;
        string scorePercent1;
        //allows for manipulating timers
        string ex1Timer;
        string ex2Timer;
        string ex3Timer;
        string ex4Timer;
        string ex5Timer;
        string ex6Timer;

        timer = Statics.timer.ToString();
        interactions = Statics.artCount.ToString();
        scorePercent1 = Statics.quizScore.ToString();

        scorePercent1 = "";

        //altering timers to more readable format on google sheets
        timer = timer.Substring(0, timer.IndexOf("."));
        if (float.Parse(timer) < 10)
        {
            timer = "0" + timer;
        }

        //new array based exhibit timer format
        for (int x = 1; x < Statics.exCount.Length; x++)
        {
            if (Statics.exTime[x] < 10 && Statics.exTime[x] > 0)
            {
                exTimer[x] = "0" + Statics.exTime[x].ToString();
                exTimer[x] = exTimer[x].Substring(0, exTimer[x].IndexOf("."));
            }
            else
            {
                exTimer[x] = Statics.exTime[x].ToString();
                if (float.Parse(exTimer[x]) != 0)
                {
                    exTimer[x] = exTimer[x].Substring(0, exTimer[x].IndexOf("."));
                }
            }
        }


        
        quiz1Attempts = Statics.quizCount1;
        for (int count = 0; count <= Statics.quizCount1; count++)
        {
            quiz1Average += Statics.quizScore[count];
        }
        quiz1Average = quiz1Average / (float)Statics.quizCount1;

        quiz2Attempts = Statics.quizCount2;
        for (int count = 0; count <= Statics.quizCount2; count++)
        {
            quiz2Average += Statics.quizScore2[count];
        }
        quiz2Average = quiz2Average / (float)Statics.quizCount2;

        quiz3Attempts = Statics.quizCount3;
        for (int count = 0; count <= Statics.quizCount3; count++)
        {
            quiz3Average += Statics.quizScore3[count];
        }
        quiz3Average = quiz3Average / (float)Statics.quizCount3;

        for (int x = 0; x < Statics.questChosen.Length; x++)
        {

            qTest = Statics.questCorrect;

            questCount[Statics.questChosen[x]] += 1;
            if (Statics.questCorrect[x] == 1)
            {
                Debug.Log("QCorrect: " + Statics.questChosen[x]);
                questCorrect[Statics.questChosen[x]] += 1;
            }

            
        }
        WWWForm form = new WWWForm();
        Debug.Log("it worked");
        

        form.AddField("entry.172307503", Statics.minutes + "." + timer + " mins"); //timer
        form.AddField("entry.1592556701", interactions); //interactions
        form.AddField("entry.1050833444", quiz1Average.ToString()); //quiz1 score
        form.AddField("entry.319759789", Statics.quizCount1.ToString());
        form.AddField("entry.812539046", quiz2Average.ToString()); //quiz2 score
        form.AddField("entry.845701505", Statics.quizCount2.ToString());
        form.AddField("entry.567110892", quiz3Average.ToString()); //quiz3 score
        form.AddField("entry.2093471443", Statics.quizCount3.ToString());
        form.AddField("entry.438884323", "Interactions: " + Statics.exCount[1] + " Time: " + Statics.exMin[1] + "." + exTimer[1] + "mins"); //exhibit1
        form.AddField("entry.77173192", "Interactions: " + Statics.exCount[2] + " Time: " + Statics.exMin[2] + "." + exTimer[2] + "mins"); //exhibit2
        form.AddField("entry.1662654721", "Interactions: " + Statics.exCount[3] + " Time: " + Statics.exMin[3] + "." + exTimer[3] + "mins"); //exhibit3
        form.AddField("entry.1998886816", "Interactions: " + Statics.exCount[4] + " Time: " + Statics.exMin[4] + "." + exTimer[4] + "mins"); //exhibit4
        form.AddField("entry.1161738900", "Interactions: " + Statics.exCount[5] + " Time: " + Statics.exMin[5] + "." + exTimer[5] + "mins"); //exhibit5
        form.AddField("entry.109809298", "Interactions: " + Statics.exCount[6] + " Time: " + Statics.exMin[6] + "." + exTimer[6] + "mins"); //exhibit6
        form.AddField("entry.552244305", "Interactions: " + Statics.exCount[7] + " Time: " + Statics.exMin[7] + "." + exTimer[7] + "mins"); //exhibit7
        form.AddField("entry.1569537396", "Interactions: " + Statics.exCount[8] + " Time: " + Statics.exMin[8] + "." + exTimer[8] + "mins"); //exhibit7
        form.AddField("entry.581882808", "Interactions: " + Statics.exCount[9] + " Time: " + Statics.exMin[9] + "." + exTimer[9] + "mins"); //exhibit7
        form.AddField("entry.118421317", "Interactions: " + Statics.exCount[10] + " Time: " + Statics.exMin[10] + "." + exTimer[10] + "mins"); //exhibit7
        form.AddField("entry.1573977286", "Interactions: " + Statics.exCount[11] + " Time: " + Statics.exMin[11] + "." + exTimer[11] + "mins"); //exhibit7
        form.AddField("entry.429450491", "Interactions: " + Statics.exCount[12] + " Time: " + Statics.exMin[12] + "." + exTimer[12] + "mins"); //exhibit7
        form.AddField("entry.1086717581", "Interactions: " + Statics.exCount[13] + " Time: " + Statics.exMin[13] + "." + exTimer[13] + "mins"); //exhibit7
        form.AddField("entry.105990075", "Interactions: " + Statics.exCount[14] + " Time: " + Statics.exMin[14] + "." + exTimer[14] + "mins"); //exhibit7
        form.AddField("entry.1178212982", "Interactions: " + Statics.exCount[15] + " Time: " + Statics.exMin[15] + "." + exTimer[15] + "mins"); //exhibit7
        form.AddField("entry.1699216393", "Interactions: " + Statics.exCount[16] + " Time: " + Statics.exMin[16] + "." + exTimer[16] + "mins"); //exhibit7
        form.AddField("entry.1173178830", "Interactions: " + Statics.exCount[17] + " Time: " + Statics.exMin[17] + "." + exTimer[17] + "mins"); //exhibit7
        form.AddField("entry.1348928950", "Interactions: " + Statics.exCount[18] + " Time: " + Statics.exMin[18] + "." + exTimer[18] + "mins"); //exhibit7
        form.AddField("entry.1973187384", "Interactions: " + Statics.exCount[19] + " Time: " + Statics.exMin[19] + "." + exTimer[19] + "mins"); //exhibit7
        form.AddField("entry.294493121", "Interactions: " + Statics.exCount[20] + " Time: " + Statics.exMin[20] + "." + exTimer[20] + "mins"); //exhibit7
        form.AddField("entry.1134536029", "Interactions: " + Statics.exCount[21] + " Time: " + Statics.exMin[21] + "." + exTimer[21] + "mins"); //exhibit7
        form.AddField("entry.2092347547", "Interactions: " + Statics.exCount[22] + " Time: " + Statics.exMin[22] + "." + exTimer[22] + "mins"); //exhibit7
        form.AddField("entry.152809792", "Interactions: " + Statics.exCount[23] + " Time: " + Statics.exMin[23] + "." + exTimer[23] + "mins"); //exhibit7
        form.AddField("entry.1808146852", "Interactions: " + Statics.exCount[24] + " Time: " + Statics.exMin[24] + "." + exTimer[24] + "mins"); //exhibit7
        form.AddField("entry.1562034790", "Interactions: " + Statics.exCount[25] + " Time: " + Statics.exMin[25] + "." + exTimer[25] + "mins"); //exhibit7
        form.AddField("entry.1751400838", "Interactions: " + Statics.exCount[26] + " Time: " + Statics.exMin[26] + "." + exTimer[26] + "mins"); //exhibit7
        form.AddField("entry.1018383147", "Interactions: " + Statics.exCount[27] + " Time: " + Statics.exMin[27] + "." + exTimer[27] + "mins"); //exhibit7
        form.AddField("entry.534917422", "Interactions: " + Statics.exCount[28] + " Time: " + Statics.exMin[28] + "." + exTimer[28] + "mins"); //exhibit7
        form.AddField("entry.638504544", "Interactions: " + Statics.exCount[29] + " Time: " + Statics.exMin[29] + "." + exTimer[29] + "mins"); //exhibit7
        /*form.AddField("entry.1688014076", "Interactions: " + Statics.exCount[30] + " Time: " + Statics.exMin[30] + "." + exTimer[30] + "mins"); //exhibit7
        form.AddField("entry.882659897", "Interactions: " + Statics.exCount[31] + " Time: " + Statics.exMin[31] + "." + exTimer[31] + "mins"); //exhibit7
        form.AddField("entry.1531127296", "Interactions: " + Statics.exCount[32] + " Time: " + Statics.exMin[32] + "." + exTimer[32] + "mins"); //exhibit7
        form.AddField("entry.128699622", "Interactions: " + Statics.exCount[33] + " Time: " + Statics.exMin[33] + "." + exTimer[33] + "mins"); //exhibit7
        form.AddField("entry.1703272451", "Interactions: " + Statics.exCount[34] + " Time: " + Statics.exMin[34] + "." + exTimer[34] + "mins"); //exhibit7
        form.AddField("entry.1913717145", "Interactions: " + Statics.exCount[35] + " Time: " + Statics.exMin[35] + "." + exTimer[35] + "mins"); //exhibit7
        form.AddField("entry.1964409348", "Interactions: " + Statics.exCount[36] + " Time: " + Statics.exMin[36] + "." + exTimer[36] + "mins"); //exhibit7
        form.AddField("entry.2062039290", "Interactions: " + Statics.exCount[37] + " Time: " + Statics.exMin[37] + "." + exTimer[37] + "mins"); //exhibit7
        form.AddField("entry.478828006", "Interactions: " + Statics.exCount[38] + " Time: " + Statics.exMin[38] + "." + exTimer[38] + "mins"); //exhibit7
        form.AddField("entry.1068964887", "Interactions: " + Statics.exCount[39] + " Time: " + Statics.exMin[39] + "." + exTimer[39] + "mins"); //exhibit7
        form.AddField("entry.1982339590", "Interactions: " + Statics.exCount[40] + " Time: " + Statics.exMin[40] + "." + exTimer[40] + "mins"); //exhibit7
        form.AddField("entry.336680536", "Interactions: " + Statics.exCount[41] + " Time: " + Statics.exMin[41] + "." + exTimer[41] + "mins"); //exhibit7
        form.AddField("entry.152876685", "Interactions: " + Statics.exCount[42] + " Time: " + Statics.exMin[42] + "." + exTimer[42] + "mins"); //exhibit7
        form.AddField("entry.2029894070", "Interactions: " + Statics.exCount[43] + " Time: " + Statics.exMin[43] + "." + exTimer[43] + "mins"); //exhibit7
        form.AddField("entry.95540132", "Interactions: " + Statics.exCount[44] + " Time: " + Statics.exMin[44] + "." + exTimer[44] + "mins"); //exhibit7
        //form.AddField("entry.31867279", "Interactions: " + Statics.exCount[45] + " Time: " + Statics.exMin[45] + "." + exTimer[45] + "mins"); //exhibit7
        */
        form.AddField("entry.141957908", Statics.scores[1].ToString());
        form.AddField("entry.862675307", Statics.scores[2].ToString());
        form.AddField("entry.2080658045", Statics.scores[3].ToString());
        form.AddField("entry.1205516660", Statics.scores[4].ToString());
        form.AddField("entry.1703204616", Statics.scores[5].ToString());
        form.AddField("entry.835495359", Statics.scores[6].ToString());
        form.AddField("entry.760970667", Statics.scores[7].ToString());
        form.AddField("entry.194625936", Statics.scores[8].ToString());
        form.AddField("entry.1402326931", Statics.scores[9].ToString());
        form.AddField("entry.687072787", Statics.scores[10].ToString());
        form.AddField("entry.1513667136", Statics.scores[11].ToString());
        form.AddField("entry.1958193764", Statics.scores[12].ToString());
        form.AddField("entry.1781220641", Statics.scores[13].ToString());
        form.AddField("entry.1837846753", Statics.scores[14].ToString());
        form.AddField("entry.1896298954", Statics.scores[15].ToString());
        form.AddField("entry.365069418", Statics.scores[16].ToString());
        form.AddField("entry.368909920", Statics.scores[17].ToString());
        form.AddField("entry.1231966821", Statics.scores[18].ToString());
        form.AddField("entry.1138919450", Statics.scores[19].ToString());
        form.AddField("entry.1450878625", Statics.scores[20].ToString());
        form.AddField("entry.1889914736", Statics.scores[21].ToString());
        form.AddField("entry.880150055", Statics.scores[22].ToString());
        form.AddField("entry.1119551145", Statics.scores[23].ToString());
        form.AddField("entry.490004042", Statics.scores[24].ToString());
        form.AddField("entry.1813787391", Statics.scores[25].ToString());
        form.AddField("entry.1579110839", Statics.scores[26].ToString());
        form.AddField("entry.596621397", Statics.scores[27].ToString());
        form.AddField("entry.488203482", Statics.scores[28].ToString());
        form.AddField("entry.990576209", Statics.scores[29].ToString());
        form.AddField("entry.683662848", Statics.scores[30].ToString());
        form.AddField("entry.1381665140", Statics.scores[31].ToString());
        form.AddField("entry.1109958314", Statics.scores[32].ToString());
        form.AddField("entry.1127080202", Statics.scores[33].ToString());
        form.AddField("entry.301965043", Statics.scores[34].ToString());
        form.AddField("entry.1821239925", Statics.scores[35].ToString());
        form.AddField("entry.932171742", Statics.scores[36].ToString());
        form.AddField("entry.151677643", Statics.scores[37].ToString());
        form.AddField("entry.1566952998", Statics.scores[38].ToString());
        form.AddField("entry.987484748", Statics.scores[39].ToString());
        form.AddField("entry.2104464970", Statics.scores[40].ToString());
        form.AddField("entry.1445144531", Statics.scores[41].ToString());
        form.AddField("entry.125963211", Statics.scores[42].ToString());
        form.AddField("entry.137897000", Statics.scores[43].ToString());
        form.AddField("entry.321041919", Statics.scores[44].ToString());
        //form.AddField("entry.848689805", Statics.scores[45].ToString()); //question 45 array not big enough or something idk
        byte[] rawData = form.data;
        Debug.Log("Hello World");
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
        _uIManager.OpenLaunchScreen();
    }


    public void MovePlayerToRoom(int index)
    {
        player.transform.position = new Vector3(rooms[index].transform.position.x, rooms[index].transform.position.y, 0);

    }



    public void OpenDoors()
    {
        if (GetPlayersQuizResultsLVL1())
        {
            door[0].sprite = doorOpen;
            door[1].sprite = doorOpen;

        }
        else
        {
            door[0].sprite = doorClose;
            door[1].sprite = doorClose;
        }
        if (GetPlayersQuizResultsLVL2())
        {
            door[2].sprite = doorOpen;
            door[3].sprite = doorOpen;

        }
        else
        {
            door[2].sprite = doorClose;
            door[3].sprite = doorClose;
        }

        if (GetPlayersQuizResultsLVL3())
        {
            door[4].sprite = doorOpen;

        }
        else
        {
            door[4].sprite = doorClose;
        }
    }




    public void GameOver()
    {
        _uIManager.OpenEndGamePanel();
    }

    public void ReloadGame()
    {

        MovePlayerToRoom(0);
        lvl1QuizPassed = false;
        lvl2QuizPassed = false;
        lvl3QuizPassed = false;

        questCount1 = 0;
        questCount2 = 0;
        questCount3 = 0;
        questCount4 = 0;
        questCount5 = 0;
        questCount6 = 0;
        questCount7 = 0;

        questCorrect1 = 0;
        questCorrect2 = 0;
        questCorrect3 = 0;
        questCorrect4 = 0;
        questCorrect5 = 0;
        questCorrect6 = 0;
        questCorrect7 = 0;

        quiz1Average = 0;
        quiz1Attempts = 0;

        _statsManager.Reset();
        _uIManager.CloseEndGamePanel();
        _uIManager.OpenLaunchScreen();

    }
    public void CloseLaunch()
    {

        _uIManager.CloseLaunchScreen();

    }

    public void SetupDialogueChoices(Dialogue aSegment)
    {

        _uIManager.SetupChoices(aSegment);

    }


    public bool GetnpcResponseFound()
    {
        return _uIManager.GetnpcResponseFound();
    }
    // Update is called once per frame
    void Update()
    {


        OpenDoors();
    }


    public void PlaySound(string clip)
    {
        _soundManager.PlaySound(clip);
    }
}
