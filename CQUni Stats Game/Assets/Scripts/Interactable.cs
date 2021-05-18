﻿using System.Collections;
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
    private bool isPlayerClose = false;
    


    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdKmNRxpf0460uiCSEKMoheZodlUqtHkM8MCAEy4fGS3y_d-A/formResponse";

    void Start()
    {
        //Find the Player gameobject and if there isn't one log it to console.
        player = GameManager.Instance.GetPlayer();
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
                        if (GameManager.Instance.GetPlayersQuizResults())
                        {
                            player.transform.position = new Vector3(GameManager.Instance.GetNextRoom().transform.position.x, GameManager.Instance.GetNextRoom().transform.position.y, 0);
                        }
                    }
                    else
                    {
                        Statics.artCount += 1;
                        GameManager.Instance.OpenContentMenu(this.gameObject.name);
                        GameManager.Instance.SetInteractingTrue();

                        //starts timer and counter for appropriate exhibit
                        switch (this.gameObject.name)
                        {
                            case "Exhibit1":
                                Statics.ex1TimeStart = true;
                                Statics.ex1Count += 1;
                                break;
                            case "Exhibit2":
                                Statics.ex2TimeStart = true;
                                Statics.ex2Count += 1;
                                break;
                            case "Exhibit3":
                                Statics.ex3TimeStart = true;
                                Statics.ex3Count += 1;
                                break;
                            case "Exhibit4":
                                //Statics.ex4TimeStart = true;
                                //Statics.ex4Count += 1;
                                break;
                            case "Exhibit5":
                                
                                break;
                            default:
                                
                                break;
                        }
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
