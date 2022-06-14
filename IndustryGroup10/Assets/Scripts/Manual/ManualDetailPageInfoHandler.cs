using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManualDetailPageInfoHandler : MonoBehaviour
{
    [SerializeField]
    List<GameObject> detailPages = new List<GameObject>();

    public TextMeshProUGUI textSubject;

    public ManualTextHandler textHandler;

    public ScrollRect scrollRect;

    public void DetailUpdate()                                      //Updates detail page
    {
        foreach(var item in detailPages)                            //Loop through all detail pages
        {
            item.SetActive(false);                                  //Disable them to avoid duplicate rendering
        }
        detailPages[textHandler.manualIndex].SetActive(true);       //Open the detail page that corresponds to the subject
        textSubject.text = textHandler.subjectStrings[textHandler.manualIndex]; //Sets title of the page to the subject

        Transform pageContent = detailPages[textHandler.manualIndex].transform.Find("PageContent"); //Find content of detail page
        RectTransform pageRect = pageContent.GetComponent<RectTransform>(); //Get RectTransform of the page content
        scrollRect.content = pageRect;                             //Set the scroll size to the size of the page
    }

    public void DetailReload(int index)                             //Reloads detail page with desired page in case of redirection
    {
        foreach (var item in detailPages)                           //Loop through all detail pages
        {
            item.SetActive(false);                                  //Disable them to avoid duplicate rendering
        }
        detailPages[index].SetActive(true);                         //Open the detail page that corresponds to the index given
        textSubject.text = textHandler.subjectStrings[index];       //Sets title of the page to the subject

        Transform pageContent = detailPages[index].transform.Find("PageContent");   //Find content of detail page
        RectTransform pageRect = pageContent.GetComponent<RectTransform>();         //Get RectTransform of the page content
        scrollRect.content = pageRect;                                              //Set the scroll size to the size of the page
    }
}
