using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialoguePanel;
    public Text aNPCName;
    public Text dialogueText;

    
    public GameObject nextButton;
    public GameObject dialogueChoicesPanel;

    private string npcName;
    public  string pcName;
    private List<Dialogue> dialogue;
    
    private string nextStatement;
    
    
    private int dialogueIndex;


    //exit method

    // Start is called before the first frame update
    void Start()
    {
        
        dialoguePanel.SetActive(false);
        nextButton.SetActive(false); 
    }

     // Start is called before the first frame update
    public void OpenDialogue(string aName, List<Dialogue> convo)
    {
        npcName = aName;
        aNPCName.text = npcName;
        dialogue = convo;
        dialoguePanel.SetActive(true);
        dialogueChoicesPanel.SetActive(true);
        nextButton.SetActive(false);

        dialogueIndex = 0;
        dialogueText.text = dialogue[dialogueIndex].statement; 
        dialogueOpen= true;
    }

    public void StopDialogue()
    {
        
        GameManager.Instance.SetInteractingFalse();
        dialoguePanel.SetActive(false);
        dialogueIndex = 0;
        dialogueOpen= false;
    }

    private bool npcResponseFound = false;
    public void NextButtonCheck()
    {

        if(npcResponseFound)
        {
            aNPCName.text = npcName;
            dialogueText.text = nextStatement;
            npcResponseFound = false;

        }
        else
        {
            //next button check should lead to the next dialogue without the response
            dialogueIndex++;
            Next();

        }
        
        

        // hide original next button
        //display new next button that progresses the next dialogue chain
    }
    public void ChooseSeriousDialogue()
    {
        
        aNPCName.text = pcName;

        dialogueText.text = dialogue[dialogueIndex].pcResponse[0]; 
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement
        if(dialogue[dialogueIndex].npcResponse != null)
        {
            npcResponseFound = true;
            nextStatement = dialogue[dialogueIndex].npcResponse[0];

        }
        else
        {
            npcResponseFound = false;
            
        }

        //show the next dialogue button 
        nextButton.SetActive(true);
    }

    public void ChooseExpositionDialogue()
    {
        aNPCName.text = pcName;

        dialogueText.text = dialogue[dialogueIndex].pcResponse[1]; 
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement
        if(dialogue[dialogueIndex].npcResponse != null)
        {
            npcResponseFound = true;
            nextStatement = dialogue[dialogueIndex].npcResponse[1];

        }
        else
        {
            npcResponseFound = false;
            
        }

        //show the next dialogue button 
        nextButton.SetActive(true);
        
    }

    public void ChooseFunnyDialogue()
    {
        aNPCName.text = pcName;

        dialogueText.text = dialogue[dialogueIndex].pcResponse[2]; 
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement
        if(dialogue[dialogueIndex].npcResponse != null)
        {
            npcResponseFound = true;
            nextStatement = dialogue[dialogueIndex].npcResponse[2];

        }
        else
        {
            npcResponseFound = false;
            
        }

        //show the next dialogue button 
        nextButton.SetActive(true); 
        
    }

    public bool dialogueOpen = false;

     void Update()
     {
        //Debug.Log(dialogueOpen);
        if (dialogueOpen == true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                StopDialogue();
            }

        }
         

     }

    public void Next()
    {
        if(dialogueIndex < dialogue.Count )
        {
            aNPCName.text = npcName;
            dialogueText.text = dialogue[dialogueIndex].statement;            
            dialogueChoicesPanel.SetActive(true);
        }
        else
        {
            DialogueOver();
        }
        
    }
    public void DialogueOver()
    {
        //GameManager.Instance.SetInteractingFalse();

        
        GameManager.Instance.OpenQuizMenu();
        dialogueIndex = 0;
        Debug.Log("convo over");
        //set dialogue pannel to false
        dialoguePanel.SetActive(false);
        dialogueOpen= false;
    }

   
}
