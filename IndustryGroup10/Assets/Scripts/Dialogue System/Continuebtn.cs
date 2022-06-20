using UnityEngine;
using TMPro;


public class Continuebtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI text;
    public NpcDialogue dialog;
    public GameObject npc;
    public int count;
  
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
        //als count groter is dan het aantal zinnen, close de dialogue.
        if (count >= dialog.sentences.Count)
        {
            GameObject.FindGameObjectWithTag("Dialogue").gameObject.SetActive(false);
            if(npc.GetComponent<Interact>().hasSceneSwitch == true)
            {
                npc.GetComponent<SceneSwitch>().SwitchScene();
            }
            
            return;
        }
        text.text = dialog.sentences[count];
        count++;
    }
        

    private void OnEnable()
    {
        npcName.text = dialog.npcName;
        text.text = dialog.sentences[0];
        count++;
    }
}
