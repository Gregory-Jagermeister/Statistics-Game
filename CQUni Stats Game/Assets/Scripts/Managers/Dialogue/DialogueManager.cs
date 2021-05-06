using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialoguePanel;
    public Text aNPCName;
    public Text dialogueText;

    

    public string[][] aNPCDialogue;
    

    public InputField playerInputText;
    private int dialogueIndex;
    private int bonusDialogueIndex;


    //exit method

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }


     // Start is called before the first frame update
    public void StartDialogue(string aName, string[][] convo)
    {
        aNPCName.text = aName;
        DeepCopy2DArray(aNPCDialogue,convo);
        dialoguePanel.SetActive(true);
        dialogueIndex = 0;
    }

    private void DeepCopy2DArray(string[][] targetArray,string[][] arrayToCopy)
    {
        for (int i = 0; i < arrayToCopy.GetLength(0); i++)
        {
            for (int j = 0; j < arrayToCopy.GetLength(1); j++)
            {
                targetArray[i][j] = targetArray[i][j];

            }
            
        }

    }

    public void StopDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue()
    {
        dialogueText.text = aNPCDialogue[dialogueIndex][dialogueIndex];
    }

    public void Next()
    {
        if(dialogueIndex <aNPCDialogue.GetLength(0) -1 )
        {
            dialogueIndex++;
            ShowDialogue();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
