using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{
    public int sceneNumber;

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    
        
    

}
