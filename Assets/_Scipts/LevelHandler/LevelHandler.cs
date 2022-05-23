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
    public GameObject StartDoor;
    public GameObject EndDoor;

    // Platform game Objects
    [Header("Platforms")]
    public GameObject PlatformOne;
    public GameObject PlatformTwo;
    public GameObject PlatformThree;
    public GameObject PlatformFour;
    public GameObject PlatformFive;

    // enemy Game Objects
    [Header("Enemies")]
    public GameObject EnemyOne;
    public GameObject Enemytwo;
    public GameObject EnemyThree;
    public GameObject EnemyFour;
    
    // trap Locations
    public GameObject TrapOne;
    public GameObject TrapTwo;

    private void Start()
    {
        Debug.Log(Levels[0].PlatformOne);
        Debug.Log(Levels[1].PlatformOne);
    }

    // To check if a location for level object is empty
    private bool IsEmpty(Vector3 v)
    {
        return v == new Vector3(0, 0, 0);
    }
}
