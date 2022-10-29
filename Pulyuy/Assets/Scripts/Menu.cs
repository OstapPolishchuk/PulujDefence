using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private GameObject optionsMenu;
    [Space]
    [SerializeField] private Animator tutorialAnimator;
    [SerializeField] private TMP_Text nextButtonText;

    private int animationPlayed = 0;

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

    public void NextAnimation()
    {
        if (animationPlayed >= 4) LoadGame();
        else
        {
            if (animationPlayed == 3) nextButtonText.text = "Play";
            tutorialAnimator.SetTrigger("Trigger");
            animationPlayed++;
        }
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
