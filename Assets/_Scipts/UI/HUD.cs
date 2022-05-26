using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject _pauseMenu;
    public GameObject _pauseButton;
    public GameObject _HUDMenu;

    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BackToMainMenu()
    {
        audioSource.PlayOneShot(audioClip);
        SceneManager.LoadScene(1);
    }

    public void PauseButton()
    {
        audioSource.PlayOneShot(audioClip);
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _HUDMenu.SetActive(false);
    }

    public void BackToGameButton()
    {
        audioSource.PlayOneShot(audioClip);
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _HUDMenu.SetActive(true);
    }
}
