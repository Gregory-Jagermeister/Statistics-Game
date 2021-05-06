using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Answers : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    private void Start()
    {
        quizManager = GameManager.Instance.GetQuizManager();

    }

    public void Answer()
    {

        if (isCorrect == true)
        {
            quizManager.Correct();

        }
        else
        {
            quizManager.Wrong();
        }

    }
}
