using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void IsSuccesfullyCompletes(bool value)
    {
        if (value) resultText.text = "You won!";
        else resultText.text = "You lost!";
    }

    public void Pause()
    {
        resultText.text = "Pause!";
    }
}