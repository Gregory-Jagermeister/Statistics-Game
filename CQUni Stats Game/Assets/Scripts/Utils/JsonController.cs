using System.Collections;
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
        //added 
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

    public List<Questions> getQuizArray(int level)
    {
        List<Questions> returning = new List<Questions>();
        switch(level)
        {
            case 1:
                foreach (Questions item in loadedData.lvl1Quiz)
                {
                    returning.Add(item);
                
                }
                break;

            case 2:
                foreach (Questions item in loadedData.lvl2Quiz)
                {
                    returning.Add(item);
                
                }
                break;

            case 3:
                foreach (Questions item in loadedData.lvl3Quiz)
                {
                    returning.Add(item);
                
                }
                break;
            default:
            
                break;

        }
        return returning;
    }

    public List<Dialogue> getDialogueArray(int level)
    {
        List<Dialogue> returning = new List<Dialogue>();
        switch(level)
        {
            case 1:
                foreach (Dialogue item in loadedData.lvl1Dialogue)
                {
                    returning.Add(item);
                
                }
                break;

            case 2:
                foreach (Dialogue item in loadedData.lvl2Dialogue)
                {
                    returning.Add(item);
                
                }
                break;

            case 3:
                foreach (Dialogue item in loadedData.lvl3Dialogue)
                {
                    returning.Add(item);
                
                }
                break;
            default:
            
                break;

        }
        return returning;
    }

    [System.Serializable]
    private class AllJsonData
    {
        public JsonData[] art;
        
        public Questions[] lvl1Quiz;
        public Questions[] lvl2Quiz;
        public Questions[] lvl3Quiz;

        public Dialogue[] lvl1Dialogue;
        public Dialogue[] lvl2Dialogue;
        public Dialogue[] lvl3Dialogue;

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

