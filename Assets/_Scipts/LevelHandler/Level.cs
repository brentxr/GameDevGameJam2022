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
    public Quaternion[] PlatformsRotation;
    /*public Vector3 PlatformTwo;
    public Vector3 PlatformThree;
    public Vector3 PlatformFour;
    public Vector3 PlatformFive;*/
    
    // Enemies
    [Header("Enemy start Locations")]
    public Vector3 EnemyOne;
    public Vector3 EnemyTwo;
    public Vector3 EnemyThree;
    public Vector3 EnemyFour;
    
    // Traps
    [Header("Trap Locations")]
    public Vector3 TrapOne;
    public Vector3 TrapTwo;
    
    

}