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
    public float temp = 0;
    public static int ex1Count = 0;
    public static float ex1Time = 0;
    public static bool ex1TimeStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ex1TimeStart == true)
        {
            ex1Time += Time.deltaTime;
        }
        if (!GameManager.Instance.isInteracting)
        {
            ex1TimeStart = false;
        }
        score = Statics.quizScore;
        Statics.timer += Time.deltaTime;
        temp = ex1Time;
        //Statics.timer += Time.deltaTime;
    }
}
