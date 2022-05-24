using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public Level[] Levels;
    
    // Start Postition
    [Header("Start Postition")]
    public GameObject StartPos;
    
    // Door Locations
    public GameObject EndPortal;

    // Platform game Objects
    [Header("Platforms")]
    public GameObject[] Platforms;

    // enemy Game Objects
    [Header("Enemies")]
    public GameObject EnemyOne;
    public GameObject Enemytwo;
    public GameObject EnemyThree;
    public GameObject EnemyFour;
    
    // trap Locations
    public GameObject TrapOne;
    public GameObject TrapTwo;

    private int _level;
    private float timeElapsed;
    private float lerpDuration = 100;
    private float startValue = 0;
    private float endValue = 10;
    private float valueToLerp;

    private bool levelJustStarted;

    private void Start()
    {
        _level = 0;

    }

    private void Update()
    {
        if (!levelJustStarted)
        {
            SetupLevel();
        }
        
        if (timeElapsed < lerpDuration)
        {
            for (int i = 0; i < Platforms.Length; i++)
            {
                if (Levels[_level].PlatformsLocation[i] != Vector3.zero)
                {
                    Platforms[i].SetActive(true);
                    Platforms[i].transform.position = Vector3.Lerp(Platforms[i].transform.position, Levels[_level].PlatformsLocation[i], timeElapsed * .01f / lerpDuration);
                    Platforms[i].transform.rotation = Quaternion.Lerp(Platforms[i].transform.rotation, Levels[_level].PlatformsRotation[i], timeElapsed * .01f / lerpDuration);
                    
                }
                else
                {
                    Platforms[i].SetActive(false);
                }
                
                timeElapsed += Time.deltaTime;
            }
            
        }
    }

    private void SetupLevel()
    {
        EndPortal.transform.position = Levels[_level].EndPortal;
        StartPos.transform.position = Levels[_level].StartPosition;
    }

    private void FinishedLevel()
    {
        levelJustStarted = false;
        _level++;
    }

    // To check if a location for level object is empty
    private bool IsEmpty(Vector3 v)
    {
        return v == new Vector3(0, 0, 0);
    }
}
