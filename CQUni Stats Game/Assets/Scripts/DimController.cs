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

    public Sprite open;
    public Sprite closed;

    public Image diaBox;
    public Sprite bobTalk;
    public Sprite profTalk;

    // All of Bob's Values.
    private float xVar;
    private float yVar;
    private float xVar2;
    private float yVar3;

    // All of Professor's Values.
    private float xVarProf;
    private float yVarProf;
    private float xVar2Prof;
    private float yVar3Prof;

    // Start is called before the first frame update
    void Start()
    {
        c = new Color32(77, 77, 77, 255);
        full = new Color32(255, 255, 255, 255);

        // All of Bob's Values.
        xVar = imgBob.rectTransform.rect.width + 20;
        yVar = imgBob.rectTransform.rect.height + 20;
        xVar2 = imgBob.rectTransform.rect.width;
        yVar3 = imgBob.rectTransform.rect.height;

        // All of the Professor's Values.
        xVarProf = imgProf.rectTransform.rect.width + 20;
        yVarProf = imgProf.rectTransform.rect.height + 20;
        xVar2Prof = imgProf.rectTransform.rect.width;
        yVar3Prof = imgProf.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (txt.GetParsedText() == GameManager.Instance.GetDialogueManager().pcName)
        {
            imgBob.sprite = open;
            diaBox.sprite = bobTalk;
            imgBob.color = full;
            imgProf.color = c;
            // Bob Size Control (Make Big)
            if (imgBob.rectTransform.rect.width <= xVar) {
                if (imgBob.rectTransform.rect.height <= yVar) {
                    imgBob.rectTransform.sizeDelta += new Vector2(0.7f, 0.7f);
                }
            }

            // Prof Size Control (Make Small)
            if (imgProf.rectTransform.rect.width >= xVar2Prof) {
                if (imgProf.rectTransform.rect.height >= yVar3Prof) {
                    imgProf.rectTransform.sizeDelta -= new Vector2(0.7f, 0.7f);
                }
            }
        }

        if (txt.GetParsedText() == GameManager.Instance.GetDialogueManager().npcName)
        {
            imgBob.sprite = closed;
            diaBox.sprite = profTalk;
            imgBob.color = c;
            imgProf.color = full;
            // Bob Size Control (Make Small)
            if (imgBob.rectTransform.rect.width >= xVar2) {
                if (imgBob.rectTransform.rect.height >= yVar3) {
                    imgBob.rectTransform.sizeDelta -= new Vector2(0.7f, 0.7f);
                }
            }

            // Prof Size Control (Make Big)
            if (imgProf.rectTransform.rect.width <= xVarProf) {
                if (imgProf.rectTransform.rect.height <= yVarProf) {
                    imgProf.rectTransform.sizeDelta += new Vector2(0.7f, 0.7f);
                }
            }
        }
    }
}
