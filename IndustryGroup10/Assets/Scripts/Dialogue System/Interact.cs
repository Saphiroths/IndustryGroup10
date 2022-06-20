using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI dialogue;
    public GameObject interactTarget;
    [SerializeField] public GameObject dialogueGameobject;


    public void OnMouseDown() { 

        dialogueGameobject.SetActive(false);

        //de waardes in je dialog/canvas worden gereset.
        dialogueGameobject.GetComponentInChildren<Continuebtn>().Resetter();

        //Je haalt de waardes in het script npcdialogue op. dit script staat op hetzelfde object als je interactscript.
        var npcdialogue = GetComponent<NpcDialogue>();

        //je dialog in continuebtn krijgt hier de waarde van npcdialogue
        dialogueGameobject.GetComponentInChildren<Continuebtn>().dialog = npcdialogue;

        //je zet je dialog op actief
        dialogueGameobject.SetActive(true);
    }
}
