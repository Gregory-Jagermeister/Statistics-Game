﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;



public class JsonController : MonoBehaviour
{
    private AllJsonData loadedData;
    private string json;

    public void GetJson()
    {
        //Using Application.dataPath (location of data folder that unity will look for) read the JSON file called
        //Information.json

        /*       using (var webClient = new System.Net.WebClient())
              {
                  json = webClient.DownloadString("https://drive.google.com/uc?export=download&id=1AdlqF_1IWYO0LGF-XMgXPtQpKEKRHwBW");
                  // Now parse with JSON.Net
                  Debug.Log(json);
                  loadedData = JsonUtility.FromJson<AllJsonData>(json);
              } */
        //json = File.ReadAllText(Application.dataPath + "/Information.json");
        if (Debug.isDebugBuild)
        {
            json = File.ReadAllText(Application.dataPath + "/Information.json");
            loadedData = JsonUtility.FromJson<AllJsonData>(json);
            Debug.Log(json);
        }
        else
        {
            StartCoroutine(GetData("https://boiling-cliffs-78685.herokuapp.com/https://www.drive.google.com/uc?export=download&id=1AdlqF_1IWYO0LGF-XMgXPtQpKEKRHwBW"));
        }

    }

    IEnumerator GetData(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            loadedData = JsonUtility.FromJson<AllJsonData>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
        }
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

