using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI content;
    public Camera mainCam;
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(content, Input.mousePosition, null);
        Debug.Log(linkIndex);
        if (linkIndex != -1)
        { // was a link clicked?
            TMP_LinkInfo linkInfo = content.textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}