using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class YAMLGenerator : MonoBehaviour
{
    [SerializeField] bool startWithUneditableTextbox = true;
    [SerializeField] GameObject UneditableTextboxPrefab;
    [SerializeField] GameObject EditableTextboxPrefab;
    [SerializeField] int Level = 1;
    [SerializeField] PreviewManager preview;
    [SerializeField] FinishLevel finishLevel;

    private List<TMP_InputField> inputFields = new List<TMP_InputField>();
    private string finalYAML;
    private float sizeOfPreviousFields = 0;
    private string[] splitCodeText;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset text = Resources.Load($"Level Code/YAMLLevel{Level}", typeof(TextAsset)) as TextAsset;
        splitCodeText = text.text.Split("//switch");
        preview.Compiled.AddListener(CheckFinalAnswer);

        StartCoroutine(nameof(GenerateTextboxesUneditableFirst));
    }

    IEnumerator GenerateTextboxesUneditableFirst()
    {
        for(int i = 0; i < splitCodeText.Length; i++)
        {
            if((startWithUneditableTextbox && i % 2 == 0) || (!startWithUneditableTextbox && i % 2 != 0))
            {
                GameObject UneditableBox = Instantiate(UneditableTextboxPrefab, transform);
                CodeGenerator generator = UneditableBox.GetComponent<CodeGenerator>();
                TMP_InputField inputField = UneditableBox.GetComponent<TMP_InputField>();

                inputField.text = splitCodeText[i];

                int lines = inputField.text.Split("\n").Length;
                generator.lines = lines;
                generator.sizeOfPreviousFields = sizeOfPreviousFields;
                sizeOfPreviousFields += 23 * lines;
                inputFields.Add(inputField);
            }
            else
            {
                GameObject EditableBox = Instantiate(EditableTextboxPrefab, transform);
                PlayerYAMLInput input = EditableBox.GetComponent<PlayerYAMLInput>();

                int lines = splitCodeText[i].Split("\n").Length;
                input.lines = lines;
                input.sizeOfPreviousFields = sizeOfPreviousFields;
                sizeOfPreviousFields += 23 * lines;
                input.finishLevel = finishLevel;
                inputFields.Add(EditableBox.GetComponent<TMP_InputField>());
            }

            yield return null;
        }
    }

    public void SubmitAnswer()
    {
        foreach(TMP_InputField input in inputFields)
        {
            finalYAML += input.text;
        }
        //CheckFinalAnswer();
        StartCoroutine(preview.PatchCode(finalYAML, " ", " "));
    }

    private void CheckFinalAnswer()
    {
        finalYAML = Regex.Replace(finalYAML, " |\r\n", "");
        string correctAnswer = "";

        foreach(string text in splitCodeText)
        {
            correctAnswer += text;
        }

        correctAnswer = Regex.Replace(correctAnswer, " |\r\n", "");

        if(string.Compare(finalYAML, correctAnswer) == 0)
        {
            finishLevel.EmitWinEvent();
        }
    }
}
