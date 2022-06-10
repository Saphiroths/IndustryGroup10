using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManualTextHandler : MonoBehaviour
{

    public TextMeshProUGUI textSubject;
    public TextMeshProUGUI textInfo;

    [SerializeField]
    public List<string> subjectStrings = new List<string>();

    [SerializeField]
    [TextArea]
    List<string> infoStrings = new List<string>();

    [SerializeField]
    public int manualIndex = 0;

    public void TextUpdate()
    {
        textSubject.text = subjectStrings[manualIndex];             //Set Subject text according to which index needs to be loaded
        textInfo.text = infoStrings[manualIndex];                   //Set Info text according to which index needs to be loaded
    }

    public void NextPage()
    {
        manualIndex++;
        if(manualIndex > infoStrings.Count - 1)
        {
            manualIndex = 0;
        }
        TextUpdate();
    }

    public void PreviousPage()
    {
        manualIndex--;
        if(manualIndex < 0)
        {
            manualIndex = infoStrings.Count - 1;
        }
        TextUpdate();
    }
}
