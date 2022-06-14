using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ManualDetailPageBehavior : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelBar;
    public ManualDetailPageInfoHandler infoHandler;

    public void ViewPage()                                          //When detail page is opened
    {
        panel.SetActive(true);                                      //Set page to active
        panelBar.SetActive(true);                                   //Set top page bar to active
        infoHandler.DetailUpdate();                                 //Update text according to which subject is open
    }

    public void ClosePage()                                         //When detail page is closed
    {
        panel.SetActive(false);                                     //Set page to inactive
        panelBar.SetActive(false);                                  //Set top page bar to inactive
    }

    public void ReloadPage(int pageIndex)                           //Reload a specific page without requiring a re-open
    {
        infoHandler.DetailReload(pageIndex);                        //Update text according to specific page given
    }

    public void OpenWebPage(string url)                             //Open webpage on button press
    {
        Application.OpenURL(url);                                   //Opens website
    }
}
