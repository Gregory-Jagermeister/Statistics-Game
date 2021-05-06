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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = Statics.quizScore;
        Statics.timer += Time.deltaTime;
        temp += Time.deltaTime;
        //Statics.timer += Time.deltaTime;
    }
}
