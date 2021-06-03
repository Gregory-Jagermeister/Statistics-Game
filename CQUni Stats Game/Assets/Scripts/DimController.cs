using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DimController : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Image imgProf;
    public Image imgBob;
    private Color32 c;
    private Color32 full;

    // Start is called before the first frame update
    void Start()
    {
        c = new Color32(77, 77, 77, 255);
        full = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (txt.GetParsedText() == "Bob")
        {
            imgBob.color = full;
            imgProf.color = c;
        }

        if (txt.GetParsedText() == "The Professor")
        {
            imgBob.color = c;
            imgProf.color = full;
        }
    }
}
