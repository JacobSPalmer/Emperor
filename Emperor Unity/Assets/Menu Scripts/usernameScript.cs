using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class usernameScript : MonoBehaviour
{
    [SerializeField]
    public InputField inputText;
    // Start is called before the first frame update
    void Start()
    {
        inputText.onValueChanged.AddListener(delegate {DebugInput();});
    }

    private void DebugInput()
    {
        Debug.Log("Input: " + inputText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        if(string.Equals(inputText.text, "")){
            int rNum = Random.Range(1, 1000000);
            string rName = "Player" + rNum.ToString();
            PlayerPrefs.SetString("username", rName);
        }
        else{
             PlayerPrefs.SetString("username", inputText.text);
        }
    }
}
