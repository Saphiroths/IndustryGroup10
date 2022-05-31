using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManualDrag : EventTrigger
{
    private Vector2 lastMousePosition;
    private Vector2 offset;

    public GameObject outline;

    public override void OnBeginDrag(PointerEventData eventData)    //Executed when player clicks on detail window
    {
        RectTransform rect = GetComponent<RectTransform>();         //Get rectTransform of the page
        offset = eventData.position - new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y);    //Use an offset depending on mouse position when drag was iniated
    }
    public override void OnDrag(PointerEventData eventData)         //While mouse button is being held down
    {
        Vector2 currentMousePosition = eventData.position;          //Get current mouse poisition
        Vector2 diff = currentMousePosition - offset;               //Subtract the offset to account for different iniation place
        RectTransform rect = GetComponent<RectTransform>();         //Get rectTransform of the page
        Rect screen = new Rect(0, 0, Screen.width, Screen.height);  //Get rect of the screen

        Vector2 oldpos = rect.anchoredPosition;                     //Store the old position of the page
        Vector2 newpos = new Vector2(diff.x, diff.y);               //Store the new position of the page

        if (-(screen.max.x / 2) < newpos.x &&                       //Check if the new position is inside boundaries of screen
            newpos.x < (screen.max.x / 2) &&
            -(screen.max.y / 2) < newpos.y &&
            newpos.y < (screen.max.y / 2))
        {
            rect.anchoredPosition = newpos;                         //Set the position of the page to the new position
        }
        else                                                        //If the new position would go out of bounds
        {
            rect.anchoredPosition = oldpos;                         //Keep old position
        }
    }
}
