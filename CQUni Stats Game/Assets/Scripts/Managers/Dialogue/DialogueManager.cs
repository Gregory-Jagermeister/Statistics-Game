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
    public GameObject[] dialogueChoices;
    public Image profPose;

    public Sprite [] sprites;

    private string npcName;
    public  string pcName;
    private List<Dialogue> dialogue;
    
    private string nextStatement;
    
    
    private int dialogueIndex;

    private List<Questions> quiz;
    private int quizLevel;



    //exit method

    // Start is called before the first frame update
    void Start()
    {
        
        dialoguePanel.SetActive(false);
        nextButton.SetActive(false); 
    }

     // Start is called before the first frame update
    public void OpenDialogue(string aName, List<Dialogue> convo, List<Questions> aQuiz, int level)
    {
        npcName = aName;
        aNPCName.text = npcName;
        dialogue = convo;
        quiz = aQuiz;
        dialoguePanel.SetActive(true);
        
        quizLevel = level;
        dialogueIndex = 0;
        Dialogue aSegment = dialogue[dialogueIndex];
        SetupChoices(aSegment);
        SetupPose(aSegment.npcPose);
        dialogueText.text = aSegment.statement; 
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
    public void ChooseOption1Dialogue()
    {
        
        aNPCName.text = pcName;

        Dialogue aSegment;
        aSegment = dialogue[dialogueIndex];

        dialogueText.text = aSegment.pcResponse[0]; 
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement
        if(aSegment.npcResponse != null && aSegment.npcResponse.Length >= 1)
        {
            npcResponseFound = true;
            nextStatement = aSegment.npcResponse[0];

        }
        else
        {
            npcResponseFound = false;
            
        }

        //show the next dialogue button 
        nextButton.SetActive(true);
    }

    public void ChooseOption2Dialogue()
    {
        aNPCName.text = pcName;
        
        Dialogue aSegment;
        aSegment = dialogue[dialogueIndex];

        dialogueText.text = aSegment.pcResponse[1]; 
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement
        if( aSegment.npcResponse != null && aSegment.npcResponse.Length >=2)
        {
            npcResponseFound = true;
            nextStatement = aSegment.npcResponse[1];

        }
        else
        {
            npcResponseFound = false;
            
        }

        //show the next dialogue button 
        nextButton.SetActive(true);
        
    }

    public void ChooseOption3Dialogue()
    {
        aNPCName.text = pcName;
       
        Dialogue aSegment;
        aSegment = dialogue[dialogueIndex];

        dialogueText.text = aSegment.pcResponse[2]; 
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement
        if(aSegment.npcResponse != null &&  aSegment.npcResponse.Length >=3)
        {
            npcResponseFound = true;
            nextStatement = aSegment.npcResponse[2];

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
        
        if (dialogueOpen == true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                StopDialogue();
            }

        }
         

     }

    public Vector3 offsetDialogue1Pos = new Vector3(300,0,0) ;
    public Vector3 normalDialogue1Pos = new Vector3(100,0,0);

    public void SetupChoices(Dialogue aSegment)
    {
        
        if( aSegment.pcResponse != null && aSegment.pcResponse.Length == 0 )
        {
            dialogueChoicesPanel.SetActive(false);
            dialogueChoices[0].SetActive(false);
            dialogueChoices[1].SetActive(false);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(true);

            npcResponseFound = false;

        }
        else if(aSegment.pcResponse != null && aSegment.pcResponse.Length == 1)
        {
            dialogueChoicesPanel.SetActive(true);
            dialogueChoices[0].SetActive(true);
            //dialogueChoices[0].transform.position = offsetDialogue1Pos;
            dialogueChoices[1].SetActive(false);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(false);
            
        }
        else if(aSegment.pcResponse != null && aSegment.pcResponse.Length ==2)
        {
            dialogueChoicesPanel.SetActive(true);
            dialogueChoices[0].SetActive(true);
           // dialogueChoices[0].transform.position = normalDialogue1Pos;
            dialogueChoices[1].SetActive(true);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(false);

        }
        else if(aSegment.pcResponse != null && aSegment.pcResponse.Length >= 3)
        {
            dialogueChoicesPanel.SetActive(true);
            dialogueChoices[0].SetActive(true);
            //dialogueChoices[0].transform.position = normalDialogue1Pos;
            dialogueChoices[1].SetActive(true);
            dialogueChoices[2].SetActive(true);

            
            nextButton.SetActive(false);

        }
        else
        {
            dialogueChoicesPanel.SetActive(false);
            dialogueChoices[0].SetActive(false);
            dialogueChoices[1].SetActive(false);
            dialogueChoices[2].SetActive(false);
            nextButton.SetActive(true);

            npcResponseFound = false;

        }
        

    }
     
    public void  SetupPose(int poseIndex)
    {
     
        switch (poseIndex)
        {
            case 0:
                profPose.sprite = sprites[0];
                break;
            case 1:
                profPose.sprite = sprites[1];
                break;
            case 2:
                profPose.sprite = sprites[2];
                break;
            case 3:
                profPose.sprite = sprites[3];
                break;           
            default:
                profPose.sprite = sprites[0];
                break;
        }

    }
    public void Next()
    {
        if(dialogueIndex < dialogue.Count )
        {
            
            aNPCName.text = npcName;
            Dialogue aSegment;
            aSegment = dialogue[dialogueIndex];
            SetupChoices(aSegment);
            SetupPose(aSegment.npcPose);
            dialogueText.text = aSegment.statement;  
        }
        else
        {
            DialogueOver();
        }
        
    }
    public void DialogueOver()
    {
        //GameManager.Instance.SetInteractingFalse();

        
        GameManager.Instance.OpenQuizMenu(quiz,quizLevel);
        dialogueIndex = 0;
        Debug.Log("convo over");
        //set dialogue pannel to false
        dialoguePanel.SetActive(false);
        dialogueOpen= false;
    }

   
}
