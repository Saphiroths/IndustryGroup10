using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerYAMLInput : MonoBehaviour
{
    [SerializeField] string correctAnswer;
    [SerializeField] int maxLines;
    private float lineSize = 23;
    public float sizeOfPreviousFields = 0;
    public int lines;
    public int size;
    public FinishLevel finishLevel;
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] TMP_InputField inputField;
    private RectTransform rtransform;

    // Start is called before the first frame update
    void Start()
    {
        rtransform = gameObject.GetComponent<RectTransform>();

        //correctly sets the size and position of the player text input field
        rtransform.sizeDelta = new Vector2(rtransform.sizeDelta.x, lineSize * lines);
        rtransform.anchoredPosition = new Vector2(0, (-(rtransform.sizeDelta.y) / 2) - sizeOfPreviousFields);
        finishLevel.WinConditionsMet.AddListener(LevelFinished);
    }

    private void LevelFinished()
    {
        inputField.interactable = false;
    }
}
