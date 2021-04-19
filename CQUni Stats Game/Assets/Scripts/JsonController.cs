using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class JsonController
{

    private AllJsonData loadedData;



    // Start is called before the first frame update
    public JsonController()
    {
        //Using Application.dataPath (location of data folder that unity will look for) read the JSON file called
        //Information.json
        string json = File.ReadAllText(Application.dataPath + "/Information.json");

        //load the read file into an object
        loadedData = JsonUtility.FromJson<AllJsonData>(json);


        //Debug.Log(loadedData.art[0].artifactId);
        //Debug.Log(loadedData.art[1].artifactId);

        // use that object to display some output to console. if all is going to plan this should display the values in the json file.
        // Debug.Log("Image: " + loadedData.imagePath);
        // Debug.Log("Video: " + loadedData.videoUrl);
        // Debug.Log("heading: " + loadedData.heading);
        // Debug.Log("content:" + loadedData.content);

        //Set the JSON information to the correct elements.
        // heading.text = loadedData.heading;
        // content.text = loadedData.content;

        // if (!(loadedData.videoUrl.ToLower() == "none"))
        // {
        //     player.VIDEO_LINK = loadedData.videoUrl;
        // }

        // if (!(loadedData.imagePath.ToLower() == "none"))
        // {
        //     StartCoroutine(DownloadImage(loadedData.imagePath));
        //     //CanvasExtentions.SizeToParent(image, 100);
        // }

    }

    public string[] getExhibit(string ID)
    {
        bool matchFound = false;
        string[] exhibits = new string[5];
        foreach (JsonData item in loadedData.art)
        {
            if (ID == item.artifactId)
            {
                matchFound = true;

                exhibits[0] = item.artifactId;
                exhibits[1] = item.imagePath;
                exhibits[2] = item.videoUrl;
                exhibits[3] = item.heading;
                exhibits[4] = item.content;
            }
        }

        if (!matchFound)
        {
            Debug.Log("artifact ID not found");
        }
        return exhibits;
        /*moved in a refactor to a UI manager
        bool matchFound =false;

        foreach (JsonData item in loadedData.art)
        {
           if (ID == item.artifactId)
            {
                matchFound = true; 

                //Set the JSON information to the correct elements.
                heading.text = item.heading;
                content.text = item.content;

                if (!(item.videoUrl.ToLower() == "none"))
                {
                    player.VIDEO_LINK = item.videoUrl;
                }

                if (!(item.imagePath.ToLower() == "none"))
                {
                    StartCoroutine(DownloadImage(item.imagePath));
                    CanvasExtentions.SizeToParent(image, 100);
                }
            }
        }

        if(!matchFound)
        {
            Debug.Log("artifact ID not found");
        }
        */

    }

    [System.Serializable]
    private class AllJsonData
    {
        public JsonData[] art;
    }


    [System.Serializable]
    private class JsonData
    {
        public string artifactId;
        public string imagePath;
        public string videoUrl;
        public string heading;
        public string content;
    }

}

