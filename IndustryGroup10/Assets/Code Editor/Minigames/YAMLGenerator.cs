using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YAMLGenerator : MonoBehaviour
{
    [SerializeField] int numberOfUneditableTextboxes = 1;
    [SerializeField] int numberOfEditableTextboxes = 1;
    [SerializeField] bool startWithUneditableTextbox = true;
    [SerializeField] GameObject UneditableTextboxPrefab;
    [SerializeField] GameObject EditableTextboxPrefab;
    [SerializeField] int Level = 1;

    private int editableTextboxesGenerated = 0;
    private int uneditableTextboxesGenerated = 0;
    private float sizeOfPreviousFields = 0;
    private string[] splitCodeText;
    private int totalBoxes;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset text = Resources.Load($"Level Code/Level {Level}/YAMLLevel{Level}", typeof(TextAsset)) as TextAsset;
        splitCodeText = text.text.Split("//switch");
        totalBoxes = numberOfEditableTextboxes + numberOfUneditableTextboxes;

        StartCoroutine(nameof(GenerateTextboxesUneditableFirst));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GenerateTextboxesUneditableFirst()
    {
        //try
        //{
        while (!(editableTextboxesGenerated == numberOfEditableTextboxes && uneditableTextboxesGenerated == numberOfUneditableTextboxes))
        {
            if (startWithUneditableTextbox)
            {
                if (totalBoxes >= 0)
                {
                    if (uneditableTextboxesGenerated != numberOfUneditableTextboxes)
                    {
                        GameObject UneditableBox = Instantiate(UneditableTextboxPrefab, transform);
                        UneditableBox.GetComponent<TextMeshProUGUI>().text = splitCodeText[editableTextboxesGenerated + uneditableTextboxesGenerated];

                        int lines = UneditableBox.GetComponent<TextMeshProUGUI>().text.Split("\n").Length;
                        UneditableBox.GetComponent<CodeGenerator>().sizeOfPreviousFields = sizeOfPreviousFields;
                        sizeOfPreviousFields += 19 * lines;
                        uneditableTextboxesGenerated++;
                    }

                    if (editableTextboxesGenerated != numberOfEditableTextboxes)
                    {
                        GameObject EditableBox = Instantiate(EditableTextboxPrefab, transform);
                        PlayerYAMLInput input = EditableBox.GetComponent<PlayerYAMLInput>();
                        int lines = splitCodeText[editableTextboxesGenerated + uneditableTextboxesGenerated].Split("\n").Length;
                        input.lines = lines;
                        input.sizeOfPreviousFields = sizeOfPreviousFields;

                        sizeOfPreviousFields += 19 * lines;

                        editableTextboxesGenerated++;
                    }
                    totalBoxes--;
                }
            }
            else
            {
                if (totalBoxes >= 0)
                {
                    if (editableTextboxesGenerated != numberOfEditableTextboxes)
                    {
                        Instantiate(EditableTextboxPrefab, transform);
                        editableTextboxesGenerated++;
                    }
                    if (uneditableTextboxesGenerated != numberOfUneditableTextboxes)
                    {
                        Instantiate(UneditableTextboxPrefab, transform);
                        uneditableTextboxesGenerated++;
                    }
                }
            }
        }
        //}
        //catch
        //{

        //}

        yield return null;
    }
}
