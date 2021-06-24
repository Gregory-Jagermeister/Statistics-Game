using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Statics : MonoBehaviour
{
    public static float timer = 0.0f;
    public static int artCount = 0;
    public static float[] quizScore = new float[99];
    public static float[] quizScore2 = new float[99];
    public static float[] quizScore3 = new float[99];
    public static int quizCount1 = 0;
    public static int quizCount2 = 0;
    public static int quizCount3 = 0;
    public float score = 0;
    public float tempPlay = 0;
    public float tempQuiz = 0;
    public static int intCount = 0; //interactions count
    public int tempCount = 0;
    public static int minutes = 0;

    //quiz scores

    public static int[] questChosen = new int[99];
    public static int[] questCorrect = new int[99];

    public static int questCounter = 0;
    public static int correctCounter = 0;

    //exhibits
    public static int ex1Count = 0;
    public static float ex1Time = 0;
    public static bool ex1TimeStart = false;
    public static int ex1Min = 0;

    public static int ex2Count = 0;
    public static float ex2Time = 0;
    public static bool ex2TimeStart = false;
    public static int ex2Min = 0;

    public static int ex3Count = 0;
    public static float ex3Time = 0;
    public static bool ex3TimeStart = false;
    public static int ex3Min = 0;

    public static int ex4Count = 0;
    public static float ex4Time = 0;
    public static bool ex4TimeStart = false;
    public static int ex4Min = 0;

    public static int ex5Count = 0;
    public static float ex5Time = 0;
    public static bool ex5TimeStart = false;
    public static int ex5Min = 0;

    public static int ex6Count = 0;
    public static float ex6Time = 0;
    public static bool ex6TimeStart = false;
    public static int ex6Min = 0;

    public static int[] exCount = new int[30];
    public static float[] exTime = new float[30];
    public static bool[] exTimeStart = new bool[30];
    public static int[] exMin = new int[30];

    public static float score1;
    public static float score2;
    public static float score3;
    public static float score4;
    public static float score5;
    public static float score6;
    public static float score7;

    public static float[] scores = new float[45];

    //used for timer
    public System.DateTime prev = System.DateTime.Now;
    public System.DateTime test;
    public System.TimeSpan diff;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        diff = System.DateTime.Now - prev;
        //test = System.DateTime.Compare(prev, System.DateTime.Now);
        //timer += System.DateTime.Now - prev;

        //main timer
        timer += (float)diff.TotalSeconds;
        if (timer >= 60)
        {
            timer -= 60;
            minutes += 1;
        }
        prev = System.DateTime.Now;

        for (int x = 1; x < exTimeStart.Length; x++)
        {
            if (exTimeStart[x] == true)
            {
                Statics.exTime[x] += (float)diff.TotalSeconds;
                if (exTime[x] >= 60)
                {
                    exMin[x] += 1;
                    exTime[x] -= 60;
                }
            }
        }

        //exhibit timers
        if (ex1TimeStart == true)
        {
            Statics.ex1Time += (float)diff.TotalSeconds;
            if (ex1Time >= 60)
            {
                ex1Min += 1;
                ex1Time -= 60;
            }

        }
        if (ex2TimeStart == true)
        {
            Statics.ex2Time += (float)diff.TotalSeconds;
            if (ex2Time >= 60)
            {
                ex2Min += 1;
                ex2Time -= 60;
            }

        }

        if (ex3TimeStart == true)
        {
            Statics.ex3Time += (float)diff.TotalSeconds;
            if (ex3Time >= 60)
            {
                ex3Min += 1;
                ex3Time -= 60;
            }

        }
        if (ex4TimeStart == true)
        {
            Statics.ex4Time += (float)diff.TotalSeconds;
            if (ex4Time >= 60)
            {
                ex4Min += 1;
                ex4Time -= 60;
            }

        }
        if (ex5TimeStart == true)
        {
            Statics.ex5Time += (float)diff.TotalSeconds;
            if (ex5Time >= 60)
            {
                ex5Min += 1;
                ex5Time -= 60;
            }

        }
        if (ex6TimeStart == true)
        {
            Statics.ex6Time += (float)diff.TotalSeconds;
            if (ex6Time >= 60)
            {
                ex6Min += 1;
                ex6Time -= 60;
            }

        }
        if (!GameManager.Instance.isInteracting)
        {
            Statics.ex1TimeStart = false;
            Statics.ex2TimeStart = false;
            Statics.ex3TimeStart = false;
            Statics.ex4TimeStart = false;
            Statics.ex5TimeStart = false;
            Statics.ex6TimeStart = false;
        }


        for (int x = 1; x < exTimeStart.Length; x++)
        {
            exTimeStart[x] = false;
        }



        //testing variables for statics
        //score = Statics.quizScore;  
        tempQuiz = Statics.ex1Time;
        tempPlay = timer;
        tempCount = Statics.intCount;
        //Statics.timer += Time.deltaTime;



    }

    public void Reset()
    {
        timer = 0.0f;
        artCount = 0;
        quizScore = new float[99];
        quizScore2 = new float[99];
        quizScore3 = new float[99];
        quizCount1 = 0;
        quizCount2 = 0;
        quizCount3 = 0;
        score = 0;
        tempPlay = 0;
        tempQuiz = 0;
        intCount = 0; //interactions count
        tempCount = 0;
        minutes = 0;

    //quiz scores

        questChosen = new int[99];
        questCorrect = new int[99];

        questCounter = 0;
        correctCounter = 0;

    //exhibits
        ex1Count = 0;
        ex1Time = 0;
        ex1TimeStart = false;
        ex1Min = 0;

        ex2Count = 0;
        ex2Time = 0;
        ex2TimeStart = false;
        ex2Min = 0;

        ex3Count = 0;
        ex3Time = 0;
        ex3TimeStart = false;
        ex3Min = 0;

        ex4Count = 0;
        ex4Time = 0;
        ex4TimeStart = false;
        ex4Min = 0;

        ex5Count = 0;
        ex5Time = 0;
        ex5TimeStart = false;
        ex5Min = 0;

        ex6Count = 0;
        ex6Time = 0;
        ex6TimeStart = false;
        ex6Min = 0;

        score1 = 0;
        score2 = 0;
        score3 = 0;
        score4 = 0;
        score5 = 0;
        score6 = 0;
        score7 = 0;


    }
}