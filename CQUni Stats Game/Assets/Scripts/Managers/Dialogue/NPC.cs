using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    public string  npcName;
    public List<Dialogue> convo; 
    public List<Questions> quiz; 
    public int level;

    
    void Start()
    {
        switch(level)
        {
            case 1:
                quiz = GameManager.Instance.GetJson().getQuizArray(1);
                convo = GameManager.Instance.GetJson().getDialogueArray(1);

                Debug.Log(quiz[0].question);
                Debug.Log(convo[0].statement);
                /*
                foreach (var item in collection)
                {
                    
                }
                */
                break;

            case 2:
                
                break;

            case 3:
                
                break;
            default:

                break;

        }
            

    }


    public void triggerDialogue()
    {
        GameManager.Instance.OpenDialogue(npcName,convo,quiz,level);
    } 
}
