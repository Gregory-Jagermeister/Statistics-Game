using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JsonController : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        //Using Application.dataPath (location of data folder that unity will look for) read the JSON file called
        //Information.json
        string json = File.ReadAllText(Application.dataPath + "/Information.json");

        //load the read file into an object
        JsonData loadedData = JsonUtility.FromJson<JsonData>(json);

        // use that object to display some output to console. if all is going to plan this should display the values in the json file.
        Debug.Log("name: " + loadedData.name);
        Debug.Log("Image: " + loadedData.imagePath);
        Debug.Log("Video: " + loadedData.videoUrl);
        Debug.Log("text: " + loadedData.text);
    }

    private class JsonData 
    {
        public string name;
        public string imagePath;
        public string videoUrl;
        public string text;
    }

}

