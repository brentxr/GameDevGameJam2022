using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;

    private void Awake()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnControlsButton()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void OnBackButton()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }
}
