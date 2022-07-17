using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject PanelManager;
    private bool IsInInstructions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void GoToInstructions()
    {
        PanelManager.GetComponent<Animator>().SetTrigger("GoToInstructions");
        IsInInstructions = true;
    }

    public void Back()
    {

        if (IsInInstructions == true)
        {
            PanelManager.GetComponent<Animator>().SetTrigger("Back");
            IsInInstructions = false;
        }
        
    }
}
