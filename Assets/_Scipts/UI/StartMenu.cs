using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    private AudioSource audioSource;
    public AudioClip audioClip;


    private void Awake()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartButton()
    {
        audioSource.PlayOneShot(audioClip);
        StartCoroutine(WaitForSound());

    }

    IEnumerator WaitForSound()
    {

        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("SampleScene");
    }

    public void OnControlsButton()
    {
        audioSource.PlayOneShot(audioClip);
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void OnBackButton()
    {
        audioSource.PlayOneShot(audioClip);
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }
}
