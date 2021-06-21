using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questions 
{
  
  public string question;
  public string[] answers;
  public int correctAnswer;
  public bool isMultiChoice;
  public int questionIndex;
  public string imgLink;
  public bool isLocalImg;
}
