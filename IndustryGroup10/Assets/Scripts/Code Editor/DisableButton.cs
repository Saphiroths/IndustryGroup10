using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    [SerializeField] FinishLevel finishLevel;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        finishLevel.WinConditionsMet.AddListener(ChangeInteractability);
    }

    private void ChangeInteractability()
    {
        button.interactable = !button.interactable;
    }
}
