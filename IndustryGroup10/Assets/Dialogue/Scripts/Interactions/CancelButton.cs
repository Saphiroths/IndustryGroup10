using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CancelButton : MonoBehaviour
{
    [SerializeField] public GameObject dialogueGameobject;

    public void Cancelbutton()
    {
        dialogueGameobject.GetComponentInChildren<Continuebtn>().Resetter();
        dialogueGameobject.SetActive(false);
    }
    
}
