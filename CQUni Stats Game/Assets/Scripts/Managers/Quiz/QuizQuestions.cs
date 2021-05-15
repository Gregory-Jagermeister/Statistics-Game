using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestions : MonoBehaviour
{
    
    public List<Questions> Quiz; 

    public void triggerQuiz()
    {
        GameManager.Instance.OpenQuizMenu(Quiz);
    } 
}
