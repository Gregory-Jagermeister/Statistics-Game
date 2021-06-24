using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public string npcName;
    private List<Dialogue> convo;
    private List<Questions> quiz;
    public int level;


    void Start()
    {
        switch (level)
        {
            case 1:
                quiz = GameManager.Instance.GetJson().getQuizArray(1);
                convo = GameManager.Instance.GetJson().getDialogueArray(1);
                break;

            case 2:
                quiz = GameManager.Instance.GetJson().getQuizArray(2);
                convo = GameManager.Instance.GetJson().getDialogueArray(2);
                break;

            case 3:
                quiz = GameManager.Instance.GetJson().getQuizArray(3);
                convo = GameManager.Instance.GetJson().getDialogueArray(3);
                break;
            default:

                break;

        }


    }


    public void triggerDialogue()
    {
        GameManager.Instance.OpenDialogue(npcName, convo, quiz, level);
    }
}
