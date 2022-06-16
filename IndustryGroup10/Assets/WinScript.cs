using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private int pointsToWin;
    private int currentpoints;
    public GameObject fill;
    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        pointsToWin = fill.transform.childCount;  
    }

    // Update is called once per frame
    void Update()
    {
        if (currentpoints >= pointsToWin)
        {
            //WIN
            SceneManager.LoadScene(sceneNumber);
        }
    }

    public void AddPoints()
    {
        currentpoints++;
    }
}
