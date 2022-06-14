using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeGenerator : MonoBehaviour
{
    private float lineSize = 23;
    public float sizeOfPreviousFields = 0;
    public int lines;
    [SerializeField] TextMeshProUGUI text;
    private RectTransform rtransform;

    // Start is called before the first frame update
    void Start()
    {
        rtransform = gameObject.GetComponent<RectTransform>();

        //correctly sets the size and position of the player text input field
        rtransform.sizeDelta = new Vector2(rtransform.sizeDelta.x, lineSize * lines);
        rtransform.anchoredPosition = new Vector2(0, (-(rtransform.sizeDelta.y) / 2) - sizeOfPreviousFields);
    }
}
