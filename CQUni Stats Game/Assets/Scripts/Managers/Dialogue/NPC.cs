using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    public string  npcName;
    public List<Dialogue> convo; 

    public void triggerDialogue()
    {
        GameManager.Instance.OpenDialogue(npcName,convo);
    } 
}
