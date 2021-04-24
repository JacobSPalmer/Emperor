using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rulesTabScript : MonoBehaviour
{
    public GameObject SetupPanel;
    public GameObject DealPanel;
    public GameObject HowToPanel;
    public GameObject OtherPanel;
    // Start is called before the first frame update
    void Start()
    {
        SetupPanel.SetActive(true);
        DealPanel.SetActive(false);
        HowToPanel.SetActive(false);
        OtherPanel.SetActive(false);
    }

    public void activateSetupPanel()
    {
        SetupPanel.SetActive(true);
        DealPanel.SetActive(false);
        HowToPanel.SetActive(false);
        OtherPanel.SetActive(false);
    }

    public void activateDealPanel()
    {
        SetupPanel.SetActive(false);
        DealPanel.SetActive(true);
        HowToPanel.SetActive(false);
        OtherPanel.SetActive(false);
    }

    public void activateHowToPanel()
    {
        SetupPanel.SetActive(false);
        DealPanel.SetActive(false);
        HowToPanel.SetActive(true);
        OtherPanel.SetActive(false);
    }

    public void activateOtherPanel()
    {
        SetupPanel.SetActive(false);
        DealPanel.SetActive(false);
        HowToPanel.SetActive(false);
        OtherPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
