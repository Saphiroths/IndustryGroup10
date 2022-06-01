using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeGenerator : MonoBehaviour
{
    private float firstLineSize = 30;
    private float lineSize = 19;
    public float sizeOfPreviousFields = 0;
    [SerializeField] TextMeshProUGUI text;
    private RectTransform rtransform;

    // Start is called before the first frame update
    void Start()
    {
        rtransform = gameObject.GetComponent<RectTransform>();

        //correctly sets the size and position of the player text input field
        rtransform.sizeDelta = new Vector2(rtransform.sizeDelta.x, firstLineSize + lineSize);
        rtransform.anchoredPosition = new Vector2(0, (-(rtransform.sizeDelta.y) / 2) - sizeOfPreviousFields);
    }
}
