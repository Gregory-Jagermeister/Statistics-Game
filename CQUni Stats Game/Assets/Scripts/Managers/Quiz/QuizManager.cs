﻿using System.Collections;
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
    public GameManager _gameManager;
    public float[] TestquizScore = new float[99];

    public GameObject backgroundQuizPanel;
    public GameObject scorePanel;
    public GameObject quizPanel;
    public TextMeshProUGUI scoreText;
    private int totalQuestions;
    public int score;
    public int numQuestions = 2;
    private int totalNumQuestions;
    private int initalNumQuestions;

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
        initalNumQuestions = numQuestions;

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

        GameManager.Instance.isInteracting = true;
        NextQuestion();

    }


    public void Correct()
    {
        Statics.questCorrect[Statics.questCounter] += 1;

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
        GameManager.Instance.PlaySound("clapping");
        scoreText.SetText(congratsMessage + " " + score + "/" + totalNumQuestions);

        if (quizLevel == 1)
        {
            Statics.quizScore[Statics.quizCount1] = (100 / totalNumQuestions) * score;
            TestquizScore[Statics.quizCount1] = (100 / totalNumQuestions) * score;
            Statics.quizCount1 += 1;
        }
        if (quizLevel == 2)
        {
            Statics.quizScore2[Statics.quizCount2] = (100 / totalNumQuestions) * score;
            TestquizScore[Statics.quizCount2] = (100 / totalNumQuestions) * score;
            Statics.quizCount2 += 1;
        }
        if (quizLevel == 3)
        {
            Statics.quizScore3[Statics.quizCount3] = (100 / totalNumQuestions) * score;
            TestquizScore[Statics.quizCount3] = (100 / totalNumQuestions) * score;
            Statics.quizCount3 += 1;
        }
        

        if (score <= totalNumQuestions)
        {
            Debug.Log("thingo");
            Statics.analyticsTrue = true;
            //_gameManager.
            StartCoroutine(playSoundAfterSeconds(1));
            GameManager.Instance.DidPlayerPassQuiz(true, quizLevel);
            StartCoroutine(GameManager.Instance.CreateAnalyticsData(Statics.timer.ToString(), Statics.artCount.ToString(), Statics.quizScore.ToString()));
        }
        //Statics.timer = Statics.timer / 60;
        //kickstarts the analytics routine
        Debug.Log("made it to before post");


        score = 0;
        questions.Clear();
        numQuestions = initalNumQuestions;

    }
    IEnumerator playSoundAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.PlaySound("doorOpening");
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
            if (currentQuestion > questions.Count)
            {
                Debug.Log("something has gone wrong");
            }
            Statics.questChosen[Statics.questCounter] = questions[currentQuestion].questionIndex;

            Statics.questCounter += 1;
            questionText.SetText(questions[currentQuestion].question);
            SetAnswers();
            image.SetActive(false);
            if (!(questions[currentQuestion].imgLink == null || questions[currentQuestion].imgLink.ToLower() == "none" || questions[currentQuestion].imgLink.ToLower() == ""))
            {
                image.SetActive(true);
                rawImage = image.gameObject.GetComponent<RawImage>();
                if (rawImage != null)
                {
                    GameManager.Instance.DownloadImage(questions[currentQuestion].imgLink, rawImage, questions[currentQuestion].isLocalImg);

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

}


