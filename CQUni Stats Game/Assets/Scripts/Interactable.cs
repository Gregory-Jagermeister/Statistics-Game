using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    public float radius = 0.7f;
    public RectTransform menu;
    public bool isProf = false;
    public bool isDoor = false;

    public bool isFinalDoor = false;


    public bool isLVL1To2Door = false;
    
    public bool isLVL2To1Door = false;
 
    public bool isLVL2To3Door = false;

    public bool isLVL3To2Door = false;



    private bool isPlayerClose = false;
    


    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdKmNRxpf0460uiCSEKMoheZodlUqtHkM8MCAEy4fGS3y_d-A/formResponse";

    void Start()
    {
        //Find the Player gameobject and if there isn't one log it to console.
        player = GameManager.Instance.GetPlayer();
        if(player == null)
        {
            player = GameObject.FindObjectOfType<Movement>();
        }
    }

    public bool IsPlayerClose()
    {
        return isPlayerClose;
    }

    // Update is called once per frame
    void Update()
    {
        //if player is close enough, can click the trigger key to trigger a message say ("you clicked on an artifact")
        if (Vector3.Distance(player.transform.position, transform.position) < radius)
        {
            isPlayerClose = true;
            if (Input.GetButtonDown("Interact"))
            {
                if (!GameManager.Instance.isInteracting)
                {
                    if (isProf)
                    {   
                        GameManager.Instance.SetInteractingTrue();
                        this.gameObject.GetComponent<NPC>().triggerDialogue();

                    }
                    else if (isDoor)
                    {
                        
                        //door leads to level 1 from level 2
                        if(isLVL2To1Door == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(1);
                            
                        }

                        //door leads to level 2 from level 1
                        else if(isLVL1To2Door == true && GameManager.Instance.GetPlayersQuizResultsLVL1() == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(2);
                            

                        }
                        
                        //door leads to level 2 from level 3
                        else if(isLVL3To2Door  == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(3);
                            
                        }

                        //door leads to level 3 from level 2
                        else if(isLVL2To3Door  == true && GameManager.Instance.GetPlayersQuizResultsLVL2()  == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(4);
                           
                            
                        }
                        //final door
                        else if(isFinalDoor  == true && GameManager.Instance.GetPlayersQuizResultsLVL3()  == true   )
                        {
                            GameManager.Instance.GameOver();
                            
                        }
                        else
                        {
                            GameManager.Instance.OpenClosedDoor();
                        }
                    }
                    else
                    {
                        Statics.artCount += 1;
                        GameManager.Instance.OpenContentMenu(this.gameObject.name);
                        GameManager.Instance.SetInteractingTrue();

                        //starts timer and counter for appropriate exhibit
                        
                    }

                }
                else
                {
                    Debug.Log("already interacting");
                }



            }

        }
        else
        {
            isPlayerClose = false;
        }
    }
}
