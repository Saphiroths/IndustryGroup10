using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{

    private Transform cameraTransform;
    public GameObject robObject;
    public GameObject DialogueObject;
    public Camera cameraReference;

    Vector2 originalPosition;

    bool inAnimation;
    bool completeCityView = false;
    bool completePersonView = false;

    float openDuration = 10f;
    float currentTime = 0;
    float normalizedValue;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        robObject.GetComponent<Interact>().OnMouseDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueObject.GetComponent<Continuebtn>().count == 4 && completeCityView == false)
        {
            currentTime = 0;                                    
            IEnumerator openCoroutine = CameraMove();            
            StartCoroutine(openCoroutine);                      
        }
        if (DialogueObject.GetComponent<Continuebtn>().count == 5 && completePersonView == false)
        {
            StopAllCoroutines();
            currentTime = 0;
            IEnumerator openCoroutine = PeopleShow();
            
            StartCoroutine(openCoroutine);
        }
        if (DialogueObject.GetComponent<Continuebtn>().count == 7)
        {
            StopAllCoroutines();
            GetComponent<Camera>().orthographicSize = 10;
            cameraTransform.position = new Vector3(0, -50, -10);
        }
    }
    IEnumerator CameraMove()                                         
    {
        completeCityView = true;
        while (currentTime <= openDuration)                         
        {
            GetComponent<Camera>().orthographicSize = 20;                                 
            currentTime += Time.deltaTime;                         
            normalizedValue = currentTime / openDuration;         
            normalizedValue = Mathf.Sin(normalizedValue * Mathf.PI * 0.5f); 
            cameraTransform.position = Vector3.Lerp(new Vector3(-20,-12,-10), new Vector3(-80,40,-10), normalizedValue); 
            yield return null;                                    
        }
        
    }
    IEnumerator PeopleShow()                                  
    {
        completePersonView = true;
        while (currentTime <= openDuration)                      
        {
            GetComponent<Camera>().orthographicSize = 8;
            inAnimation = true;                                   
            currentTime += Time.deltaTime;                         
            normalizedValue = currentTime / openDuration;          
            normalizedValue = Mathf.Sin(normalizedValue * Mathf.PI * 0.5f);
            cameraTransform.position = Vector3.Lerp(new Vector3(-27, -18, -10), new Vector3(-27, -5, -10), normalizedValue);

            yield return null;                                    
        }
        inAnimation = false;
        completeCityView = true;
        GetComponent<Camera>().orthographicSize = 10;
    }
}
