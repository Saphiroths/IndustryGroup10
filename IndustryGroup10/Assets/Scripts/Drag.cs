using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public GameObject correctForm;

    private bool moving;
    private bool finish;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;

    [SerializeField]
    private float SnapMargin = 1.5f;


    // Start is called before the first frame update
    // Sets the starting position as resetposition.
    void Start()
    {   
        resetPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update() //Update sets the position/transform of the gameobject according to the mouse and start positions.
    {
        if(finish == false)
        {
            if (moving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z); //sets the position of the gameobject.
            }
        }
     
    }

    private void OnMouseDown() //OnMouseDown fills the starting x and y positions with its values when the left mouse button is pressed and makes sure the object can be moved in update.
    {
        if(Input.GetMouseButtonDown(0)) //Mousebutton 0 is left mouse button
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x; //Sets startPosX
            startPosY = mousePos.y - this.transform.localPosition.y; //Sets startPosY

            moving = true; //object is moving
        }
    }

    private void OnMouseUp() //OnMouseUp checks if the object is in margin to snap to the correct position. If so it snaps, if not it resets the position to the initial position.
    {
        moving = false;

        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= SnapMargin && 
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= SnapMargin) //Checks if the x and y position is in the margin to snap to its correct position
        {
            this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z); //Snap the object to the correct position
            finish = true; //Finish is true so object can not be moved again
        }
        else //Else reset localposition to starting position so the object snaps back to original starting position
        {
            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }
    }
}
