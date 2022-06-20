using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropStory : MonoBehaviour
{

    private Transform cameraTransform;
    public GameObject robObject;
    public GameObject DialogueObject;
    public GameObject ManualObject;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        robObject.GetComponent<Interact>().OnMouseDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueObject.GetComponent<Continuebtn>().count == 3)
        {
            ManualObject.SetActive(true);
            cameraTransform.position = new Vector3(-1.36f, 1.18f, -10);
        }
    }
}
