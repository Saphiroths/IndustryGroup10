using UnityEngine;
using TMPro;


public class Continuebtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI text;
    //[SerializeField] public GameObject interactTarget;
    public NpcDialogue dialog;
    public GameObject npc;
    private int count;
  
    /* void Start()
     {
         dialog = interactTarget.GetComponent<NpcDialogue>();
         ContinueButton();
     }*/

    //zet alle waardes weer op nul
    public void Resetter()
    {
        npcName.text = "";
        text.text = "";
        count = 0;
    }

    //Bij deze methode klik je op een "continue" button. Iedere keer als je op deze button klikt willen we de volgende zin uit dialog.sentences aanroepen.
    public void ContinueButton()
    {
        //zolang dialog.sentences.count kleiner of gelijk is aan count, 
        //if (dialog.sentences.Count <= count)
        //{
        //    text.text = dialog.sentences[count];
        //    count++;
        //    return;
        //}

        //als count groter is dan het aantal zinnen, close de dialogue.
        if (count >= dialog.sentences.Count)
        {
            GameObject.FindGameObjectWithTag("Dialogue").gameObject.SetActive(false);
            npc.GetComponent<SceneSwitch>().SwitchScene();
            return;
        }
        text.text = dialog.sentences[count];
        count++;

        //GetComponentInChildren<Interact>().dialogueGameobject.SetActive(false);
    }
        

private void OnEnable()
    {
        npcName.text = dialog.npcName;
        text.text = dialog.sentences[0];
        count++;
    }
}
