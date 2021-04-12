using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactMenu : MonoBehaviour
{
    public GameObject background;
    public Text heading;
    public Text content;
    public GameObject media;

    public VideoController player;
    public RawImage image;

    public JsonController json;
    

    public bool artifactMenuOpen = false;

    // Update is called once per frame
    void Update()
    {
        if(artifactMenuOpen == true)
        {
            if(Input.GetButtonDown("Cancel"))
            {
                CloseMenu();
            }
            
        }
        else
        {
            CloseMenu();

        }
       
        
    }

    public void OpenMenu(string ID)
    {
        string[] exhibits = new string[5];
        exhibits= json.getExhibit(ID);
        Debug.Log(exhibits[0]);
//Set the JSON information to the correct elements.
        heading.text = exhibits[3];
        content.text = exhibits[4];

            if (!(exhibits[2].ToLower() == "none"))
            {
                player.VIDEO_LINK = exhibits[2];
            }

            if (!(exhibits[1].ToLower() == "none"))
            {
                json.DLImage(exhibits[1],image);
            }
        

        background.gameObject.SetActive(true);
        heading.gameObject.SetActive(true);
        content.gameObject.SetActive(true);
        media.gameObject.SetActive(true);

        artifactMenuOpen = true;      
        Time.timeScale = 0;

    }
    
    public void CloseMenu()
    {
        background.gameObject.SetActive(false);
        heading.gameObject.SetActive(false);
        content.gameObject.SetActive(false);
        media.gameObject.SetActive(false);
        artifactMenuOpen =false;
        Time.timeScale = 1;


    }

    public void UpdateMenu()
    {

    }

}

