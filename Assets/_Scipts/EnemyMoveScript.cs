using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMoveScript : MonoBehaviour
{
    public Transform startingPoint;
    public Transform pointA;
    public Transform pointB;

    public GameObject sprite;

    public float duration = 2f;

    private bool isReady;
    private bool isMovingUp = true;
    float elapsedTime;



    private void Update()
    {


        elapsedTime += Time.deltaTime;
        if (isMovingUp)
        {
            sprite.transform.DOMove(pointB.position, duration);
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;
                isMovingUp = false;
            }
            
        }
        else
        {
            sprite.transform.DOMove(pointA.position, duration);
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;
                isMovingUp = true;
            }
        }

    }
}
