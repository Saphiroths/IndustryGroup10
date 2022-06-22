using System.Collections;
using UnityEngine;

public class ManualBehavior : MonoBehaviour
{
    private RectTransform rectTransform;
    Vector2 originalPosition;
    Vector2 openPosition = new Vector2(770, 0);

    public ManualTextHandler textHandler;

    bool isOpen = false;
    bool inAnimation = false;

    float openDuration = 0.5f; 
    float currentTime = 0; 
    float normalizedValue;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();              //Get rectTransform of Sidebar
        originalPosition = rectTransform.anchoredPosition;          //Store the starting position of the sidebar for return sequence
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && inAnimation == false)  //Detects tab placement + failsafe no animations are running
        {
            if(isOpen == false)                                     //Looks if the sidebar is opened or closed
            {
                currentTime = 0;                                    //Reset animation timer
                textHandler.TextUpdate();                           //Updates text with proper information
                IEnumerator openCoroutine = PanelOpen();            //Index coroutine containing the opening animation
                StartCoroutine(openCoroutine);                      //Starts the coroutine
                isOpen = true;                                      //Updates status of sidebar
            }
            else if (isOpen == true)                                //Same as above but for closing sequence
            {
                currentTime = 0;                                    //Reset animation timer
                IEnumerator closeCoroutine = PanelClose();          //Index coroutine containing the closing animation
                StartCoroutine(closeCoroutine);                     //Starts the coroutine
                isOpen = false;                                     //Updates status of sidebar
            }
        }
    }

    IEnumerator PanelOpen()                                         //Animation for opening the sidebar
    {
        while (currentTime <= openDuration)                         //While the time spent in animation is smaller than the duration
        {
            inAnimation = true;                                     //Update animation status
            currentTime += Time.deltaTime;                          //Update time
            normalizedValue = currentTime / openDuration;           //Value that is used to calculate speed and progression in animation
            normalizedValue = Mathf.Sin(normalizedValue * Mathf.PI * 0.5f); //Use a sin wave in order to create a fade out effect
            rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, openPosition, normalizedValue); //Use lerp to move object
            yield return null;                                      //Repeat coroutine
        }
        inAnimation = false;                                        //If animation is finished update status
    }

    IEnumerator PanelClose()                                        //Animation for closing the sidebar
    {
        while (currentTime <= openDuration)                         //While the time spent in animation is smaller than the duration
        {
            inAnimation = true;                                     //Update animation status
            currentTime += Time.deltaTime;                          //Update time
            normalizedValue = currentTime / openDuration;           //Value that is used to calculate speed and progression in animation
            normalizedValue = Mathf.Sin(normalizedValue * Mathf.PI * 0.5f); //Use a sin wave in order to create a fade out effect
            rectTransform.anchoredPosition = Vector2.Lerp(openPosition, originalPosition, normalizedValue); //Use lerp to move object
            yield return null;                                      //Repeat coroutine
        }
        inAnimation = false;                                        //If animation is finished update status
    }
}
