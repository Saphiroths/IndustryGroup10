using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class YAMLGenerator : MonoBehaviour
{
    [SerializeField] int numberOfUneditableTextboxes = 1;
    [SerializeField] int numberOfEditableTextboxes = 1;
    [SerializeField] bool startWithUneditableTextbox = true;
    [SerializeField] GameObject UneditableTextboxPrefab;
    [SerializeField] GameObject EditableTextboxPrefab;
    [SerializeField] int Level = 1;
    [SerializeField] PreviewManager preview;

    private List<TMP_InputField> inputFields = new List<TMP_InputField>();
    private string finalYAML;
    private int editableTextboxesGenerated = 0;
    private int uneditableTextboxesGenerated = 0;
    private float sizeOfPreviousFields = 0;
    private string[] splitCodeText;
    private int totalBoxes;
    private bool winCondition = false;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset text = Resources.Load($"Level Code/YAMLLevel{Level}", typeof(TextAsset)) as TextAsset;
        splitCodeText = text.text.Split("//switch");
        totalBoxes = numberOfEditableTextboxes + numberOfUneditableTextboxes;

        StartCoroutine(nameof(GenerateTextboxesUneditableFirst));
    }

    IEnumerator GenerateTextboxesUneditableFirst()
    {
        while (!(editableTextboxesGenerated == numberOfEditableTextboxes && uneditableTextboxesGenerated == numberOfUneditableTextboxes))
        {
            if (startWithUneditableTextbox)
            {
                if (totalBoxes >= 0)
                {
                    if (uneditableTextboxesGenerated != numberOfUneditableTextboxes)
                    {
                        GameObject UneditableBox = Instantiate(UneditableTextboxPrefab, transform);
                        UneditableBox.GetComponent<TMP_InputField>().text = splitCodeText[editableTextboxesGenerated + uneditableTextboxesGenerated];

                        int lines = UneditableBox.GetComponent<TMP_InputField>().text.Split("\n").Length;
                        UneditableBox.GetComponent<CodeGenerator>().lines = lines;
                        UneditableBox.GetComponent<CodeGenerator>().sizeOfPreviousFields = sizeOfPreviousFields;
                        sizeOfPreviousFields += 23 * lines;
                        uneditableTextboxesGenerated++;
                        inputFields.Add(UneditableBox.GetComponent<TMP_InputField>());
                    }

                    if (editableTextboxesGenerated != numberOfEditableTextboxes)
                    {
                        GameObject EditableBox = Instantiate(EditableTextboxPrefab, transform);
                        PlayerYAMLInput input = EditableBox.GetComponent<PlayerYAMLInput>();
                        int lines = splitCodeText[editableTextboxesGenerated + uneditableTextboxesGenerated].Split("\n").Length;
                        input.lines = lines;
                        input.sizeOfPreviousFields = sizeOfPreviousFields;
                        sizeOfPreviousFields += 23 * lines;

                        editableTextboxesGenerated++;
                        inputFields.Add(EditableBox.GetComponent<TMP_InputField>());
                    }
                    totalBoxes--;
                }
                yield return null;
            }
            else
            {
                if (totalBoxes >= 0)
                {
                    if (editableTextboxesGenerated != numberOfEditableTextboxes)
                    {
                        GameObject EditableBox = Instantiate(EditableTextboxPrefab, transform);
                        PlayerYAMLInput input = EditableBox.GetComponent<PlayerYAMLInput>();
                        int lines = splitCodeText[editableTextboxesGenerated + uneditableTextboxesGenerated].Split("\n").Length;
                        input.lines = lines;
                        input.sizeOfPreviousFields = sizeOfPreviousFields;
                        sizeOfPreviousFields += 23 * lines;

                        editableTextboxesGenerated++;
                        inputFields.Add(EditableBox.GetComponent<TMP_InputField>());
                    }
                    if (uneditableTextboxesGenerated != numberOfUneditableTextboxes)
                    {
                        GameObject UneditableBox = Instantiate(UneditableTextboxPrefab, transform);
                        UneditableBox.GetComponent<TMP_InputField>().text = splitCodeText[editableTextboxesGenerated + uneditableTextboxesGenerated];

                        int lines = UneditableBox.GetComponent<TMP_InputField>().text.Split("\n").Length;
                        UneditableBox.GetComponent<CodeGenerator>().lines = lines;
                        UneditableBox.GetComponent<CodeGenerator>().sizeOfPreviousFields = sizeOfPreviousFields;
                        sizeOfPreviousFields += 23 * lines;
                        uneditableTextboxesGenerated++;
                        inputFields.Add(UneditableBox.GetComponent<TMP_InputField>());
                    }
                    totalBoxes--;
                }
                yield return null;
            }
        }
    }

    public void SubmitAnswer()
    {
        foreach(TMP_InputField input in inputFields)
        {
            finalYAML += input.text;
        }
        CheckFinalAnswer();
        //StartCoroutine(preview.PatchCode(finalYAML, " ", " "));
    }

    private void CheckFinalAnswer()
    {
        for(int i = 0; i < editableTextboxesGenerated; i++)
        {
            int j = i;

            if (startWithUneditableTextbox)
            {
                j++;
            }

            string[] inputSeparated = inputFields[j].text.Split('\n');
            string[] splitCodeSeparated = splitCodeText[j].Split('\n');

            for (int k = 0; k < inputSeparated.Length; k++)
            {
                Debug.Log(inputSeparated[k]);
                Debug.Log(splitCodeSeparated[k]);

                if (string.Compare(splitCodeSeparated[k], inputSeparated[k]) == 0)
                {
                }
            }
        }


    }
}
