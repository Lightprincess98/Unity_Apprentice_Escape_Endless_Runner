using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteraction : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ControlMenu;
    public GameObject CreditsMenu;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMain()
    {
        ControlMenu.SetActive(false);
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void GoToControls()
    {
        ControlMenu.SetActive(true);
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void GoToCredits()
    {
        ControlMenu.SetActive(false);
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GameMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
