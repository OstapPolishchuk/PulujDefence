using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private GameObject optionsMenu;

    void Start()
    {
        OpenMain();
    }

    public void OpenMain() 
    {
        HideAll();
        mainMenu.SetActive(true);
    }

    public void OpenOptions() 
    {
        HideAll();
        optionsMenu.SetActive(true);
    }

    public void OpenTutorial() 
    {
        HideAll();
        tutorialMenu.SetActive(true);
    }

    public void Quit() 
    {
        Application.Quit();
    }

    public void LoadGame() 
    {
        SceneManager.LoadScene(1);
    }

    private void HideAll()
    {
        mainMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
}
