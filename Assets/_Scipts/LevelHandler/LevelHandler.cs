using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public Level[] Levels;

    [FormerlySerializedAs("Player")] 
    public GameObject PlayerPrefab;

    public PlayerController playerController;
    
    // Start Postition
    [Header("Start Postition")]
    public GameObject StartPos;
    
    // Door Locations
    public GameObject Portal;

    // Platform game Objects
    [Header("Platforms")]
    public GameObject[] Platforms;

    // enemy Game Objects
    [Header("Enemies")]
    public GameObject[] Enemies;

    // trap Locations
    public GameObject[] Traps;

    public TextMeshProUGUI LevelText;

    private AudioSource _audioSource;
    public AudioClip pop;
    public AudioClip portal;

    public int _level;
    public bool overrideLevel;
    private float timeElapsed;
    private float lerpDuration = 3;
    private float startValue = 0;
    private float endValue = 10;
    private float valueToLerp;

    private bool levelJustStarted;
    private bool levelReady;
    private GameObject _player;
    
    

    private void Start()
    {
        DOTween.SetTweensCapacity(20000, 50);
        if (!overrideLevel)
            _level = -1;
        Enemy.OnTakeDamage += ResetPlayer;
        Spikes.OnTakeDamage += ResetPlayer;
        OutOfBounds.OnTakeDamage += ResetPlayer;
        EndPortal.OnEnterPortal += EnterEndPortal;
        _audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (!levelJustStarted)
        {
            SetupLevel();
            timeElapsed = 0;
        }
        
        if (timeElapsed < lerpDuration)
        {
            for (int i = 0; i < Platforms.Length; i++)
            {
                if (Levels[_level].PlatformsLocation[i] != Vector3.zero)
                {
                    Platforms[i].SetActive(true);
                    Platforms[i].transform.DOMove(Levels[_level].PlatformsLocation[i], lerpDuration);
                    Platforms[i].transform.DORotate(Levels[_level].PlatformsRotation[i], lerpDuration);
                }
                else
                {
                    Platforms[i].SetActive(false);
                }
                
                
            }

            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Levels[_level].EnemyLocation[i] != Vector3.zero)
                {
                    Enemies[i].SetActive(true);
                    Enemies[i].transform.DOMove(Levels[_level].EnemyLocation[i], lerpDuration);
                    Enemies[i].transform.DORotate(Levels[_level].EnemyRotation[i], lerpDuration);
                }
                else
                {
                    Enemies[i].SetActive(false);
                }
            }
            
            for (int i = 0; i < Traps.Length; i++)
            {
                if (Levels[_level].TrapLocations[i] != Vector3.zero)
                {
                    Traps[i].SetActive(true);
                    Traps[i].transform.DOMove(Levels[_level].TrapLocations[i], lerpDuration);
                    Traps[i].transform.DORotate(Levels[_level].TrapRotations[i], lerpDuration);
                }
                else
                {
                    Traps[i].SetActive(false);
                }
            }
            
            timeElapsed += Time.deltaTime;
            
        }

        if (timeElapsed >= lerpDuration && !levelReady)
        {
            GetLevelReady();
        }
    }
    
    private void EnterEndPortal()
    {
        _audioSource.PlayOneShot(portal);
        Destroy(_player);
        FinishedLevel();
    }
    
    
    private void ResetPlayer()
    {
        _audioSource.PlayOneShot(pop);
        _player.transform.position = Levels[_level].StartPosition;
        _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(PauseInput());
        
    }

    IEnumerator PauseInput()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(.5f);
        playerController.canMove = true;
    }

    private void GetLevelReady()
    {
        _player = Instantiate(PlayerPrefab, Levels[_level].StartPosition, Quaternion.identity);
        playerController = _player.GetComponent<PlayerController>();
        Portal.SetActive(true);
        levelReady = true;

    }

    private void SetupLevel()
    {
        
        _level++;

        if (_level == Levels.Length)
        {
            SceneManager.LoadScene(3);
        }
        LevelText.SetText((_level + 1).ToString());
        Portal.transform.position = Levels[_level].EndPortal;
        levelJustStarted = true;
        levelReady = false;
        //StartPos.transform.position = Levels[_level].StartPosition;
    }

    private void FinishedLevel()
    {
        levelJustStarted = false;
        //_level++;
        Portal.SetActive(false);

    }

    // To check if a location for level object is empty
    private bool IsEmpty(Vector3 v)
    {
        return v == new Vector3(0, 0, 0);
    }
}
