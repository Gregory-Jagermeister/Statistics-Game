using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactMenu : MonoBehaviour
{
    public GameObject Background;
    public Text Heading;
    public Text Content;
    public GameObject Media;
    

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
        Debug.Log("YO");
        Background.gameObject.SetActive(true);
        Heading.gameObject.SetActive(true);
        Content.gameObject.SetActive(true);
        Media.gameObject.SetActive(true);

        artifactMenuOpen = true;
        Debug.Log(artifactMenuOpen);

        Time.timeScale = 0;

    }
    
    public void CloseMenu()
    {
        Background.gameObject.SetActive(false);
        Heading.gameObject.SetActive(false);
        Content.gameObject.SetActive(false);
        Media.gameObject.SetActive(false);
        artifactMenuOpen =false;
        Time.timeScale = 1;


    }

}

