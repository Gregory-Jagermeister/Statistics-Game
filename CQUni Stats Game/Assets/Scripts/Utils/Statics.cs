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
    public static int ex1Count = 0;
    public static float ex1Time = 0;
    public static bool ex1TimeStart = false;
    public static int intCount = 0;
    public int tempCount = 0;
    public static int minutes = 0;
    public static int ex1Min = 0;
    //public string prev;
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
        timer += (float)diff.TotalSeconds;
        if (timer >= 60)
        {
            timer -= 60;
            minutes += 1;
        }
        prev = System.DateTime.Now;
        if (ex1TimeStart == true)
        {
            Statics.ex1Time += (float)diff.TotalSeconds;
        }
        if (!GameManager.Instance.isInteracting)
        {
            Statics.ex1TimeStart = false;
        }

        //Statics.timer += Time.deltaTime;

        

      

        score = Statics.quizScore;
        
        tempQuiz = Statics.ex1Time;
        tempPlay = timer;
        tempCount = Statics.intCount;
        //Statics.timer += Time.deltaTime;
    }
}
