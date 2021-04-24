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

    // public string username;
    // public GameObject usernameInput;
    // public GameObject usernameOutput;

    // public void StoreUsername()
    // {
    //     // usernameInput = GameObject.Find("UsernameInput");
    //     InputField input = usernameInput.GetComponent<InputField>();
    //     username = input.text;
    //     print(username);
    //     // usernameOutput.GetComponent<Text>().text = "Username: " + username;

    // }

    
    public void RulesClick (string LevelName) 
    {
        SceneManager.LoadScene("RulesScene", LoadSceneMode.Single);
    }


    public void PlayClick (string LevelName) 
    {
        // if(usernameInput.GetComponent<Text>().text == ""){
        //     username = "Player";
        // }
        // PlayerPrefs.SetString("username", username);
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
