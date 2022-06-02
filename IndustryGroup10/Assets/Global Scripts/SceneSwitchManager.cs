using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{
    [SerializeField] string GoToSceneName;
    
    public void ChangeScene()
    {
        SceneManager.LoadScene(GoToSceneName);
    }
}
