using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    void Start()
    {
        player = GameObject.FindObjectOfType<Movement>();
        if(player == null)
        {
            Debug.Log("no player to interact with");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if player is close enough, can click space to trigger a message say ("you clicked on an artifact")
        if(Vector3.Distance(player.transform.position, transform.position) < 2) 
        {
            
            if(Input.GetButtonDown("Interact"))
            {
                Debug.Log("you clicked on an artifact");

            }

        }
        
    }
    

}
