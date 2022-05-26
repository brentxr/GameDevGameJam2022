using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create New Level")]
public class Level : ScriptableObject
{
    // Level Start 
    [Header("Start Position")]
    public Vector3 StartPosition;
    
    // Door Locations
    [Header("Door Locations")]
    public Vector3 EndPortal;
    
    // Platforms
    [Header("Platforms")]
    public Vector3[] PlatformsLocation;
    public Vector3[] PlatformsRotation;
    /*public Vector3 PlatformTwo;
    public Vector3 PlatformThree;
    public Vector3 PlatformFour;
    public Vector3 PlatformFive;*/
    
    // Enemies
    [Header("Enemy start Locations")]
    public Vector3[] EnemyLocation;
    public Vector3[] EnemyRotation;

    // Traps
    [Header("Trap Locations")]
    public Vector3[] TrapLocations;
    public Vector3[] TrapRotations;



}