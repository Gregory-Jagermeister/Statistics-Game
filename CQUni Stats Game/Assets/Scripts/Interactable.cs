using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    public float radius = 2f;
    public ArtifactMenu menu;
    public bool isProf =false;
    public string sceneNameTransition = "Quiz";
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
                    menu.OpenMenu(this.gameObject.name);
                }
                
                
            }

        }

    }


}
