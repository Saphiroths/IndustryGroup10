using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobMovement : MonoBehaviour
{
    public GameObject playerObject;
    private Transform playerTransform;
    Vector2 playerPosition;

    private Transform robTransform;
    Vector2 originalPosition;
    Vector2 midPosition;
    Vector2 upPosition;
    Vector2 robLocationToPlayer;
    bool inAnimation = false;

    float animationTime = 2f;
    float currentTime = 0;
    float normalizedValue;

    List<Vector2> positionHistory = new List<Vector2>();

    public bool robInChase = false;

    void Start()
    {
        playerTransform = playerObject.GetComponent<Transform>();
        robTransform = GetComponent<Transform>();
        originalPosition = robTransform.position;
        upPosition = originalPosition + new Vector2(0, 0.2f);
        playerPosition = playerTransform.position;
        positionHistory.Insert(0, playerPosition);
        robLocationToPlayer = new Vector2(playerPosition.x - 1.5f, playerPosition.y + 2);
    }

    void FixedUpdate()
    {
        playerPosition = playerTransform.position;

        if(positionHistory[0] != playerPosition)
        {
            positionHistory.Insert(0, playerPosition);
        }

        if(positionHistory.Count >= 30)
        {
            robInChase = true;

        }
        if(robInChase == true)
        {       
            robLocationToPlayer = positionHistory[positionHistory.Count - 1];
            positionHistory.RemoveAt(positionHistory.Count - 1);
            if(positionHistory.Count < 10)
            {
                robInChase = false;
            }
        }

        midPosition = new Vector2(robLocationToPlayer.x, robLocationToPlayer.y);
        upPosition = new Vector2(robLocationToPlayer.x, robLocationToPlayer.y + 0.6f);

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
            normalizedValue = (Mathf.Sin(normalizedValue *(4 * Mathf.PI) * 0.5f))/2 + 0.5f; //Use a sin wave in order to create a fade out effect
            robTransform.position = Vector2.Lerp(midPosition, upPosition, normalizedValue); //Use lerp to move object
            yield return null;                                      //Repeat coroutine
        }
        inAnimation = false;                                        //If animation is finished update status
    }
}
