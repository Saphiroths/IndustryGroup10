using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{
    public int sceneNumber;

    public void OnMouseDown()
    {
        Debug.Log("???");
        SceneManager.LoadScene(sceneNumber);
    }
    
        
    

}
