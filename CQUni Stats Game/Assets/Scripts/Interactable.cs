using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    public float radius = 0.7f;
    public ArtifactMenu menu;
    public bool isProf = false;
    public string sceneNameTransition = "Quiz";


    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdKmNRxpf0460uiCSEKMoheZodlUqtHkM8MCAEy4fGS3y_d-A/formResponse";

    void Start()
    {
        //Find the Player gameobject and if there isn't one log it to console.
        player = GameObject.FindObjectOfType<Movement>();
        if (player == null)
        {
            Debug.Log("no player to interact with");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        
        
        //if player is close enough, can click the trigger key to trigger a message say ("you clicked on an artifact")
        if (Vector3.Distance(player.transform.position, transform.position) < radius)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (isProf)
                {
                    


                    SceneManager.LoadScene(sceneNameTransition);
                    
                }
                else
                {
                    Statics.artCount += 1;
                    menu.OpenMenu(this.gameObject.name);
                }


            }

        }

    }


}
