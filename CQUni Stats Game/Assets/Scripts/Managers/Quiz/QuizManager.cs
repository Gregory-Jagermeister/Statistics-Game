﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

//code was adapted or obtained from youtube videos made by Awesome Tuts, Brackeys and The Game Guy
public class QuizManager : MonoBehaviour
{
    private List<Questions> questions = new List<Questions>();
    public GameObject[] choices;
    public int currentQuestion;
    public Text questionText;

    public float[] TestquizScore = new float[99];

    public GameObject scorePanel;
    public GameObject quizPanel;
    public Text scoreText;
    private int totalQuestions;
    private int score;
    public int numQuestions = 2;
    private int totalNumQuestions;





    public int[] tester = new int[10];


    public int[] testing = new int[10];

    public GameObject multiChoicePanel;
    public GameObject InputPanel;

    private int quizLevel;

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        totalNumQuestions = numQuestions;
        //totalQuestions = questions.Count;
        //scorePanel.gameObject.SetActive(false);
        //quizPanel.gameObject.SetActive(true);
        //NextQuestion();
    }

    public void StartQuiz(List<Questions> quiz, int level)
    {
        
        foreach (Questions item in quiz)
        {
            questions.Add(item);
        }
        totalQuestions = questions.Count;
        quizLevel = level;
        scorePanel.gameObject.SetActive(false);
        quizPanel.gameObject.SetActive(true);
        NextQuestion();

    }



    public void Correct()
    {
        Statics.questCorrect[Statics.correctCounter] += 1;
        Statics.correctCounter += 1;
        score = score + 1;
        numQuestions = numQuestions-1;
        questions.RemoveAt(currentQuestion);


        

        NextQuestion();
    }
    public void Wrong()
    {

        questions.RemoveAt(currentQuestion);
        numQuestions = numQuestions-1;
        NextQuestion();
    }

    public void QuizOver()
    {
        GameManager.Instance.SetInteractingFalse();
        quizPanel.gameObject.SetActive(false);
        scorePanel.gameObject.SetActive(true);
        scoreText.text = "You achieved a score of " + score + "/" + totalNumQuestions;
        if (quizLevel == 1)
        {
            Statics.quizScore[Statics.quizCount1] = (100 / totalNumQuestions) * score;
            TestquizScore[Statics.quizCount1] = (100 / totalNumQuestions) * score;
            Statics.quizCount1 += 1;
        } 
        if ((100 / totalNumQuestions) * score == 100)
        {
            GameManager.Instance.DidPlayerPassQuiz(true,quizLevel);
            StartCoroutine(GameManager.Instance.CreateAnalyticsData(Statics.timer.ToString(), Statics.artCount.ToString(), Statics.quizScore.ToString()));
        }
        //Statics.timer = Statics.timer / 60;
        //kickstarts the analytics routine
        Debug.Log("made it to before post");
        
        score =0;
        numQuestions = totalNumQuestions;
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
                choices[i].transform.GetChild(0).GetComponent<Text>().text = questions[currentQuestion].answers[i];
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
        if (questions.Count > 0 && numQuestions != 0 )
        {
            currentQuestion = Random.Range(0, questions.Count);
            Statics.questChosen[Statics.questCounter] = questions[currentQuestion].questionIndex;
            tester[Statics.questCounter] = questions[currentQuestion].questionIndex;
            Statics.questCounter += 1;
            questionText.text = questions[currentQuestion].question;
            SetAnswers();

        }
        else
        {
            Debug.Log("out of questions");

            QuizOver();
        }



    }

    public InputField input;
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
}
