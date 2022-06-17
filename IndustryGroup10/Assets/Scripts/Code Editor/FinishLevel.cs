using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevel : MonoBehaviour
{
    public UnityEvent WinConditionsMet = new UnityEvent();

    public void EmitWinEvent()
    {
        WinConditionsMet.Invoke();
    }
}
