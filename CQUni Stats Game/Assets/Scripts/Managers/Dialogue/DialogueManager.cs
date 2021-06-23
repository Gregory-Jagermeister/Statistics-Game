using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialoguePanel;
    public TextMeshProUGUI aNPCName;
    public TextMeshProUGUI dialogueText;


    public GameObject nextButton;
    public GameObject dialogueChoicesPanel;
    public GameObject[] dialogueChoices;
    public Image profPose;

    public Sprite[] sprites;

    public string npcName;
    public string pcName;
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
        aNPCName.SetText(npcName);
        dialogue = convo;
        quiz = aQuiz;
        dialoguePanel.SetActive(true);

        quizLevel = level;
        dialogueIndex = 0;
        Dialogue aSegment = dialogue[dialogueIndex];
        GameManager.Instance.SetupDialogueChoices(aSegment);
        npcResponseFound = GameManager.Instance.GetnpcResponseFound();
        SetupPose(aSegment.npcPose);
        dialogueText.SetText(aSegment.statement);
        dialogueOpen = true;
    }

    public void StopDialogue()
    {

        GameManager.Instance.SetInteractingFalse();
        dialoguePanel.SetActive(false);
        dialogueIndex = 0;
        dialogueOpen = false;
    }

    private bool npcResponseFound = false;
    public void NextButtonCheck()
    {

        if (npcResponseFound)
        {
            aNPCName.SetText(npcName);
            dialogueText.SetText(nextStatement);
            npcResponseFound = false;

        }
        else
        {
            //next button check should lead to the next dialogue without the response
            NextSegment();
        }



        // hide original next button
        //display new next button that progresses the next dialogue chain
    }
    public void ChooseOption1Dialogue()
    {

        ChooseOptionDialogue(0);
    }

    public void ChooseOption2Dialogue()
    {
        ChooseOptionDialogue(1);

    }

    public void ChooseOption3Dialogue()
    {
        ChooseOptionDialogue(2);

    }

    public void ChooseOptionDialogue(int choiceIndex)
    {
        aNPCName.SetText(pcName);

        Dialogue aSegment;
        aSegment = dialogue[dialogueIndex];

        dialogueText.SetText(aSegment.pcResponse[choiceIndex]);
        //hide the dialogue chocies panel
        dialogueChoicesPanel.SetActive(false);

        // update text to show the normal dialogue
        //update the next statement

        if (aSegment.npcResponse != null && aSegment.npcResponse.Length >= (choiceIndex + 1))
        {
            npcResponseFound = true;
            nextStatement = aSegment.npcResponse[choiceIndex];

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

    public void SetupPose(int poseIndex)
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


    public GameObject openFileButton;

    public GameObject dialogueFilePopUp;
    public RawImage rawDialogueFileImage;


    public void Next()
    {
        if (dialogueIndex < dialogue.Count)
        {

            aNPCName.SetText(npcName);

            Dialogue aSegment;
            aSegment = dialogue[dialogueIndex];
            GameManager.Instance.SetupDialogueChoices(aSegment);
            npcResponseFound = GameManager.Instance.GetnpcResponseFound();
            SetupPose(aSegment.npcPose);
            dialogueText.SetText(aSegment.statement);
        }
        else
        {
            DialogueOver();
        }

    }

    public void NextSegment()
    {

        dialogueIndex++;
        Next();
    }





    public void DialogueOver()
    {
        //GameManager.Instance.SetInteractingFalse();


        GameManager.Instance.OpenQuizMenu(quiz, quizLevel);
        dialogueIndex = 0;
        Debug.Log("convo over");
        //set dialogue pannel to false
        dialoguePanel.SetActive(false);
        dialogueOpen = false;
    }


}
