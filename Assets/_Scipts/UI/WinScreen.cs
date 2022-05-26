using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void BackToMainMenu()
    {
        audioSource.PlayOneShot(audioClip);
        SceneManager.LoadScene(0);
    }
}
