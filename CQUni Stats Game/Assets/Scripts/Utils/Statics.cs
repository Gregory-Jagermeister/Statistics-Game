using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Statics : MonoBehaviour
{
    public static float timer = 0.0f;
    public static int artCount = 0;
    public static float quizScore = 0.0f;
    public float score;
    public float tempPlay = 0;
    public float tempQuiz = 0;
    public static int intCount = 0; //interactions count
    public int tempCount = 0;
    public static int minutes = 0;


    //exhibits
    public static int ex1Count = 0;
    public static float ex1Time = 0;
    public static bool ex1TimeStart = false;
    public static int ex1Min = 0;

    public static int ex2Count = 0;
    public static float ex2Time = 0;
    public static bool ex2TimeStart = false;
    public static int ex2Min = 0;


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
        if (!GameManager.Instance.isInteracting)
        {
            Statics.ex1TimeStart = false;
            Statics.ex2TimeStart = false;
        }

       

        

      
        //testing variables for statics
        score = Statics.quizScore;  
        tempQuiz = Statics.ex1Time;
        tempPlay = timer;
        tempCount = Statics.intCount;
        //Statics.timer += Time.deltaTime;
    }
}
