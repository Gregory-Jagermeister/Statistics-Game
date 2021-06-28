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

                        //door leads to level 2 from level 1
                        if (isLVL2To1Door == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(1);

                        }

                        //door leads to level 1 from level 2
                        else if (isLVL1To2Door == true && GameManager.Instance.GetPlayersQuizResultsLVL1() == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(2);


                        }

                        //door leads to level 2 from level 3
                        else if (isLVL3To2Door == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(3);

                        }

                        //door leads to level 3 from level 2
                        else if (isLVL2To3Door == true && GameManager.Instance.GetPlayersQuizResultsLVL2() == true)
                        {
                            GameManager.Instance.MovePlayerToRoom(4);


                        }
                        //final door
                        else if (isFinalDoor == true && GameManager.Instance.GetPlayersQuizResultsLVL3() == true)
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
                        //for(int x = 0; x < Statics.exTimeStart.Length; x++)
                        //{
                        //
                        //}
                        //int temp = int.Parse(this.gameObject.name.Substring(this.gameObject.name.IndexOf("t")));
                        //Statics.exTimeStart[temp] = true;
                        //if (int.Parse(this.gameObject.name.Substring(this.gameObject.name.IndexOf("t" + 1))) != 0)
                        //{
                        //    Statics.exTimeStart[int.Parse(this.gameObject.name.Substring(this.gameObject.name.IndexOf("t" + 1)))] = true;
                        //}
                        switch (this.gameObject.name)
                        {
                            case "Exhibit1":
                                Statics.exTimeStart[1] = true;
                                Statics.exCount[1] += 1;
                                break;
                            case "Exhibit2":
                                Statics.exTimeStart[2] = true;
                                Statics.exCount[2] += 1;
                                break;
                            case "Exhibit3":
                                Statics.exTimeStart[3] = true;
                                Statics.exCount[3] += 1;
                                break;
                            case "Exhibit4":
                                Statics.exTimeStart[4] = true;
                                Statics.exCount[4] += 1;
                                break;
                            case "Exhibit5":
                                Statics.exTimeStart[5] = true;
                                Statics.exCount[5] += 1;
                                break;
                            case "Exhibit6":
                                Statics.exTimeStart[6] = true;
                                Statics.exCount[6] += 1;
                                break;
                            case "Exhibit7":
                                Statics.exTimeStart[7] = true;
                                Statics.exCount[7] += 1;
                                break;
                            case "Exhibit8":
                                Statics.exTimeStart[8] = true;
                                Statics.exCount[8] += 1;
                                break;
                            case "Exhibit9":
                                Statics.exTimeStart[9] = true;
                                Statics.exCount[9] += 1;
                                break;
                            case "Exhibit10":
                                Statics.exTimeStart[10] = true;
                                Statics.exCount[10] += 1;
                                break;
                            case "Exhibit11":
                                Statics.exTimeStart[11] = true;
                                Statics.exCount[11] += 1;
                                break;
                            case "Exhibit12":
                                Statics.exTimeStart[12] = true;
                                Statics.exCount[12] += 1;
                                break;
                            case "Exhibit13":
                                Statics.exTimeStart[13] = true;
                                Statics.exCount[13] += 1;
                                break;
                            case "Exhibit14":
                                Statics.exTimeStart[14] = true;
                                Statics.exCount[14] += 1;
                                break;
                            case "Exhibit15":
                                Statics.exTimeStart[15] = true;
                                Statics.exCount[15] += 1;
                                break;
                            case "Exhibit16":
                                Statics.exTimeStart[16] = true;
                                Statics.exCount[16] += 1;
                                break;
                            case "Exhibit17":
                                Statics.exTimeStart[17] = true;
                                Statics.exCount[17] += 1;
                                break;
                            case "Exhibit18":
                                Statics.exTimeStart[18] = true;
                                Statics.exCount[18] += 1;
                                break;
                            case "Exhibit19":
                                Statics.exTimeStart[19] = true;
                                Statics.exCount[19] += 1;
                                break;
                            case "Exhibit20":
                                Statics.exTimeStart[20] = true;
                                Statics.exCount[20] += 1;
                                break;
                            case "Exhibit21":
                                Statics.exTimeStart[21] = true;
                                Statics.exCount[21] += 1;
                                break;
                            case "Exhibit22":
                                Statics.exTimeStart[22] = true;
                                Statics.exCount[22] += 1;
                                break;
                            case "Exhibit23":
                                Statics.exTimeStart[23] = true;
                                Statics.exCount[23] += 1;
                                break;
                            case "Exhibit24":
                                Statics.exTimeStart[24] = true;
                                Statics.exCount[24] += 1;
                                break;
                            case "Exhibit25":
                                Statics.exTimeStart[25] = true;
                                Statics.exCount[25] += 1;
                                break;
                            case "Exhibit26":
                                Statics.exTimeStart[26] = true;
                                Statics.exCount[26] += 1;
                                break;
                            case "Exhibit27":
                                Statics.exTimeStart[27] = true;
                                Statics.exCount[27] += 1;
                                break;
                            case "Exhibit28":
                                Statics.exTimeStart[28] = true;
                                Statics.exCount[28] += 1;
                                break;
                            case "Exhibit29":
                                Statics.exTimeStart[29] = true;
                                Statics.exCount[29] += 1;
                                break;
                            case "Exhibit30":
                                Statics.exTimeStart[30] = true;
                                Statics.exCount[30] += 1;
                                break;
                            
                            default:

                                break;
                        }
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
