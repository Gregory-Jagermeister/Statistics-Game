using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

//code was adapted or obtained from youtube videos made by Awesome Tuts, Brackeys and The Game Guy
public class QuizManager : MonoBehaviour
{
    private List<Questions> questions = new List<Questions>();
    public GameObject[] choices;
    private int currentQuestion;
    public TextMeshProUGUI questionText;

    public float[] TestquizScore = new float[99];

    public GameObject backgroundQuizPanel;
    public GameObject scorePanel;
    public GameObject quizPanel;
    public TextMeshProUGUI scoreText;
    private int totalQuestions;
    private int score;
    public int numQuestions = 2;
    private int totalNumQuestions;

    public GameObject multiChoicePanel;
    public GameObject InputPanel;
    public string congratsMessage;

    private int quizLevel;
    private int arraySize;
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {

        if (numQuestions > questions.Count)
        {

            totalNumQuestions = questions.Count;

        }
        else
        {
            totalNumQuestions = numQuestions;
        }

    }

    public void StartQuiz(List<Questions> quiz, int level)
    {

        foreach (Questions item in quiz)
        {
            questions.Add(item);
        }

        arraySize = quiz.Count;
        if (numQuestions > arraySize)
        {

            totalNumQuestions = arraySize;

        }
        else
        {
            totalNumQuestions = numQuestions;
        }

        quizLevel = level;
        scorePanel.gameObject.SetActive(false);
        quizPanel.gameObject.SetActive(true);
        backgroundQuizPanel.gameObject.SetActive(true);
        image.SetActive(false);
        NextQuestion();

    }


    public void Correct()
    {
        Statics.questCorrect[Statics.correctCounter] += 1;
        Statics.correctCounter += 1;
        score = score + 1;
        numQuestions = numQuestions - 1;
        questions.RemoveAt(currentQuestion);
        NextQuestion();
    }
    public void Wrong()
    {

        questions.RemoveAt(currentQuestion);
        numQuestions = numQuestions - 1;
        NextQuestion();
    }

    public void QuizOver()
    {
        GameManager.Instance.SetInteractingFalse();
        scorePanel.gameObject.SetActive(true);
        quizPanel.gameObject.SetActive(false);
        image.SetActive(false);
        scoreText.SetText(congratsMessage + " " + score + "/" + totalNumQuestions);

        if (quizLevel == 1)
        {
            Statics.quizScore[Statics.quizCount1] = (100 / totalNumQuestions) * score;
            TestquizScore[Statics.quizCount1] = (100 / totalNumQuestions) * score;
            Statics.quizCount1 += 1;
        }
        if ((100 / totalNumQuestions) * score == 100)
        {
            GameManager.Instance.DidPlayerPassQuiz(true, quizLevel);
            StartCoroutine(GameManager.Instance.CreateAnalyticsData(Statics.timer.ToString(), Statics.artCount.ToString(), Statics.quizScore.ToString()));
        }
        //Statics.timer = Statics.timer / 60;
        //kickstarts the analytics routine
        Debug.Log("made it to before post");

        score = 0;
        questions.Clear();
        numQuestions = totalNumQuestions;
        //GameManager.Instance.CloseQuizMenu();
    }

    void SetAnswers()
    {
        if (questions[currentQuestion].isMultiChoice)
        {
            multiChoicePanel.gameObject.SetActive(true);
            InputPanel.gameObject.SetActive(false);

            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].GetComponent<Answers>().isCorrect = false;
                choices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(questions[currentQuestion].answers[i]);
                int answerIndex = i + 1;

                if (questions[currentQuestion].correctAnswer == answerIndex)
                {
                    choices[i].GetComponent<Answers>().isCorrect = true;
                }

            }

        }

        else
        {
            multiChoicePanel.gameObject.SetActive(false);
            InputPanel.gameObject.SetActive(true);
            correctInputAnswer = questions[currentQuestion].answers[0];
        }

    }

    // Update is called once per frame
    void NextQuestion()
    {
        if (questions.Count > 0 && numQuestions != 0)
        {
            currentQuestion = Random.Range(0, questions.Count);
            if(currentQuestion > questions.Count)
            {
                Debug.Log("something has gone wrong");
            }
            Statics.questChosen[Statics.questCounter] = questions[currentQuestion].questionIndex;

            Statics.questCounter += 1;
            questionText.SetText(questions[currentQuestion].question);
            SetAnswers();
            if(!(questions[currentQuestion].imgLink == null || questions[currentQuestion].imgLink.ToLower() == "none" ||  questions[currentQuestion].imgLink.ToLower() == ""))
            {
               image.SetActive(true);
               rawImage = image.gameObject.GetComponent<RawImage>();
               if(rawImage != null)
               {
                    DLImage(questions[currentQuestion].imgLink, rawImage);

               }
               else
               {
                   image.SetActive(false);
                   Debug.Log("could not find image component");
               }
              
                
            }

        }
        else
        {
            Debug.Log("out of questions");

            QuizOver();
        }


    }

    public TMP_InputField input;
    private string correctInputAnswer;

    public void GetInput(string guess)
    {

        checkGuess(guess, correctInputAnswer);

    }

    public void checkGuess(string guess, string correctAnswer)
    {
        if (guess == correctAnswer)
        {
            //Debug.Log("you entered " + guess + " and it was right");
            Correct();
        }
        else
        {
            //Debug.Log("you entered " + guess+ " and it was wrong");
            Wrong();

        }
        input.text = "";

    }

    public GameObject image; // need raw image
    private RawImage rawImage;


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
            if( questions[currentQuestion].isLocalImg == true)
            {
           
                string url = "file://" + URL;
                // Create an empty texture
                Texture2D newTexture = new Texture2D (512,512,TextureFormat.ARGB32,true);
                 // loads the image

                

                request = UnityWebRequestTexture.GetTexture(url);
                yield return request.SendWebRequest();
                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log(request.error);
                } 
                

            
            }
    
            
            image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;       
            image.SetNativeSize();
            ResizeImage(image);

        }
    }

    /// <summary>
    /// Resizes the image to fit the requires ratio.
    /// </summary>
    /// <param name="i">The Image to Resize as a rawImage</param>
    public int imageMaxWidth =300;
    public int imageMaxHeight=300;
    public void ResizeImage(RawImage i)
    {

        Debug .Log(i.rectTransform.rect.height);
        if (i.rectTransform.rect.width> imageMaxWidth && i.rectTransform.rect.height > imageMaxHeight)
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


