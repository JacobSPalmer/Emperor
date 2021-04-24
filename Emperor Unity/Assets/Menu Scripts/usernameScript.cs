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
        PlayerPrefs.SetString("username", inputText.text);
    }
}
