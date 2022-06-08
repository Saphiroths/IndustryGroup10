using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbing : MonoBehaviour
{
    float animationTime = 2f;
    float currentTime = 0;
    float normalizedValue;

    private Transform robTransform;
    Vector2 midPosition;
    Vector2 upPosition;

    bool inAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        robTransform = GetComponent<Transform>();
        midPosition = robTransform.position;
        upPosition = midPosition + new Vector2(0, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (inAnimation == false)
        {
            currentTime = 0;                                    //Reset animation timer
            IEnumerator bobbingCoroutine = RobBobbing();           //Index coroutine containing the opening animation
            StartCoroutine(bobbingCoroutine);                      //Starts the coroutine
        }
    }

    IEnumerator RobBobbing()                                         //Animation for opening the sidebar
    {
        while (currentTime <= animationTime)                         //While the time spent in animation is smaller than the duration
        {
            inAnimation = true;                                     //Update animation status
            currentTime += Time.deltaTime;                          //Update time
            normalizedValue = currentTime / animationTime;           //Value that is used to calculate speed and progression in animation
            normalizedValue = (Mathf.Sin(normalizedValue * (4 * Mathf.PI) * 0.5f)) / 2 + 0.5f; //Use a sin wave in order to create a fade out effect
            robTransform.position = Vector2.Lerp(midPosition, upPosition, normalizedValue); //Use lerp to move object
            yield return null;                                      //Repeat coroutine
        }
        inAnimation = false;                                        //If animation is finished update status
    }

}
