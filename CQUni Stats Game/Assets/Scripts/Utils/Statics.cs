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
    public static int[] exCount = new int[30];
    public static float[] exTime = new float[30];
    public static bool[] exTimeStart = new bool[30];
    public static int[] exMin = new int[30];
    public static bool analyticsTrue = false;
    public static int quizLevel = 1;

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
       


        for (int x = 1; x < exTimeStart.Length; x++)
        {
            exTimeStart[x] = false;
        }







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
       


    }
}