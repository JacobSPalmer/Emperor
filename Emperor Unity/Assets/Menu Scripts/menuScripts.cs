using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RulesClick (string LevelName) 
    {
        SceneManager.LoadScene("RulesScene", LoadSceneMode.Single);
    }


    public void PlayClick (string LevelName) 
    {
        SceneManager.LoadScene("LoginScene", LoadSceneMode.Single);
    }

    public void Exit() 
    {
    	Application.Quit();
    }

    public void BackClick (string LevelName)
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void Gameplay (string LevelName)
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void WinStateGame (string LevelName)
    {
        SceneManager.LoadScene("WinGameScene", LoadSceneMode.Single);
    }

}
