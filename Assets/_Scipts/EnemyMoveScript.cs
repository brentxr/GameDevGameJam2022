using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMoveScript : MonoBehaviour
{
    public Transform startingPoint;
    public Transform pointA;
    public Transform pointB;
    
    private void Start()
    {
        transform.DOMove(pointA.position, 1);
    }
    
    private void Update()
    {

        if (transform.position == pointA.position)
            transform.DOMove(pointB.position, 1);
        if (transform.position == pointB.position) 
            transform.DOMove(pointA.position, 1);
    }
}
